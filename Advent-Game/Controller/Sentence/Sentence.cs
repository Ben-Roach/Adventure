
using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// An ordered <see cref="IReadOnlyList{T}"/> of <see cref="Node"/> objects that represents an interpretable sentence.
    /// </summary>
    public class Sentence : IReadOnlyList<Node>
    {
        /// <summary>The <see cref="Node"/> objects in the <see cref="Sentence"/>.</summary>
        private List<Node> baseList;

        /// <summary>The number of top-level nodes the <see cref="Sentence"/> contains.</summary>
        public int Length => baseList.Count;

        /// <summary>
        /// Create a new <see cref="Sentence"/> from <paramref name="inputString"/>, using the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="inputString">The string input by the player.</param>
        /// <param name="glossary">The <see cref="Glossary"/> used to validate and interpret the input.</param>
        /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="inputString"/> is null.</exception>
        public Sentence(string inputString, Glossary glossary, out string errorMessage)
        {
            // PREPARATION -- Argument checking, property/field instantiation.
            if (inputString == null) throw new ArgumentNullException(nameof(inputString));
            baseList = new List<Node>();
            // TOKENIZATION -- Split input string into a list of strings, while removing invalid characters and unnecessary words.
            List<string> tokenList = Tokenize(inputString, glossary, out errorMessage);
            if (errorMessage != null) return;
            // PARSING -- Construct a sentence out of tokens by validating words, assigning data to them, and organizing them syntactically.
            foreach (string token in tokenList) { baseList.Add(glossary.CreateNodeFromToken(token)); }
            CollectAdjectives();
            CollectNouns();
        }

        /// <summary>
        /// Removes invalid characters from <paramref name="inputString"/>, splits it into a list, and removes unnecessary words.
        /// </summary>
        /// <param name="inputString">The string input by the player.</param>
        /// <param name="glossary">The <see cref="Glossary"/> used to validate and interpret the input.</param>
        /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
        /// <returns>A list of tokens, which are acceptable strings that represent words.</returns>
        private List<string> Tokenize(string inputString, Glossary glossary, out string errorMessage)
        {
            // trim & normalize
            string tempStr = glossary.Normalize(inputString.Trim());
            // Check for empty / whitespace input
            if (inputString == "")
            {
                errorMessage = "Speak up, please.";
                return null;
            }
            // Remove invalid characters
            StringBuilder strBuilder = new StringBuilder(inputString);
            for (int i = strBuilder.Length - 1; i >= 0; i--)
            {
                if (glossary.IsInvalidChar(strBuilder[i]))
                    strBuilder.Remove(i, 1);
            }
            // Check if any characters remain
            tempStr = strBuilder.ToString();
            if (tempStr.Length == 0)
            {
                errorMessage = "Try using actual words.";
                return null;
            }
            // Split inputString into outputList -- a list of string tokens, each representing one word
            List<string> outputList = tempStr.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).ToList();
            // Remove unnecessary words
            for (int i = outputList.Count() - 1; i >= 0; i--)
            {
                if (glossary.IsInvalidWord(outputList[i]))
                    outputList.RemoveAt(i);
            }
            // Check if any tokens remain
            if (outputList.Count == 0)
            {
                errorMessage = "I'm pretty sure that isn't a sentence.";
                return null;
            }
            // Input passed validation
            errorMessage = null;
            return outputList;
        }

        /// <summary>
        /// Collects <see cref="Adjective"/> objects in <see cref="baseList"/> contiguous to each <see cref="Noun"/>,
        /// and adds them to the <see cref="Noun"/>.
        /// </summary>
        private void CollectAdjectives()
        {
            for (int i = baseList.Count - 1; i >= 0; i--)
            {
                if (baseList[i] is Noun n)
                {
                    // collect preceeding Adjectives
                    while (i - 1 >= 0 && baseList[i - 1] is Adjective adj)
                    {
                        n.AddAdjective(adj);
                        baseList.RemoveAt(--i);
                    }
                    // collect subsequent Adjectives
                    while (i + 1 < baseList.Count && baseList[i + 1] is Adjective adj)
                    {
                        n.AddAdjective(adj);
                        baseList.RemoveAt(i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Collects chained <see cref="Noun"/> objects in <see cref="baseList"/> (those with one or more
        /// <see cref="Conjunction"/> objects between them), replacing the words wit a new <see cref="NounGroup"/> object.
        /// </summary>
        private void CollectNouns()
        {
            int chainLength = 0;
            List<Noun> nounList = new List<Noun>();
            for (int i = baseList.Count - 1; i >= 0; i--)
            {
                // try to start/continue chain, if current word is noun and preceeding word is conjunction
                if (i - 2 >= 0 && baseList[i] is Noun nCurrent && baseList[i - 1] is Conjunction)
                {
                    int p = i - 2;
                    // skip all preceeding consecutive conjunctions
                    while (p >= 0 && baseList[p] is Conjunction)
                        p--;
                    // check if preceeding non-conjunction is a noun
                    if (baseList[p] is Noun)
                    {
                        nounList.Insert(0, nCurrent);
                        chainLength += i - p;
                        i = p + 1; // p is now position of preceeding noun
                        continue;
                    }
                }
                // otherwise, try to add noun to existing chain, then terminate
                if (nounList.Count > 0 && baseList[i] is Noun nFinal)
                {
                    nounList.Insert(0, nFinal);
                    // turn first Noun in chain into NounCollection
                    baseList[i] = new NounGroup(nounList);
                    // remove chained nouns and conjunctions from sentence
                    for (int j = 0; j < chainLength; j++)
                        baseList.RemoveAt(i + 1);
                    // prep for next chain
                    nounList.Clear();
                    chainLength = 0;
                }
            }
        }

        /// <summary>
        /// Returns the length of the <see cref="Sentence"/>, considering only one level of depth.
        /// </summary>
        int IReadOnlyCollection<Node>.Count => Length;

        /// <summary>
        /// Gets the top-level <see cref="Node"/> in the <see cref="Sentence"/> at the specified index.
        /// </summary>
        /// <param name="i">The index of the <see cref="Node"/> to get.</param>
        /// <returns>The <see cref="Node"/> at index <paramref name="i"/>.</returns>
        public Node this[int i] => baseList[i];

        /// <summary>
        /// Enumerates the top-level <see cref="Node"/> objects in the <see cref="Sentence"/>.
        /// </summary>
        /// <returns>An enumeration of <see cref="Node"/> objects.</returns>
        public IEnumerator<Node> GetEnumerator()
        {
            foreach (Node n in baseList)
            {
                yield return n;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}