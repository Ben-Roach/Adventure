
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
            // TOKENIZATION -- Split input string into a list of strings, while removing invalid characters and unnecessary words.
            List<Token> tokenList = Tokenize(inputString, glossary, out errorMessage);
            if (errorMessage != null) return;
            // PARSING -- Construct a sentence out of tokens by converting them to meaningful nodes and organizing them syntactically.
            baseList = ConvertToNodes(tokenList, glossary);
            CollectAdjectives();
            CollectNouns();
        }

        /// <summary>
        /// Converts a player input string into a list of <see cref="Token"/> objects.
        /// </summary>
        /// <param name="inputString">The string input by the player.</param>
        /// <param name="glossary">The <see cref="Glossary"/> used to normalize and validate the input.</param>
        /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
        /// <returns>A list of <see cref="Token"/> objects, or null if an error occured.</returns>
        private List<Token> Tokenize(string inputString, Glossary glossary, out string errorMessage)
        {
            List<string> wordList = inputString.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).ToList();
            // Check for empty / whitespace input
            if (wordList.Count == 0)
            {
                errorMessage = "Speak up, please.";
                return null;
            }
            List<Token> tokenList = new List<Token>();
            foreach (string origWord in wordList)
            {
                // copy and normalize word
                string n = glossary.Normalize(string.Copy(origWord));
                // remove invalid chars
                StringBuilder strBuilder = new StringBuilder(n);
                for (int j = strBuilder.Length - 1; j >= 0; j--)
                {
                    if (glossary.IsInvalidChar(strBuilder[j]))
                        strBuilder.Remove(j, 1);
                }
                string newWord = strBuilder.ToString();
                // Make tokens, but not of empty or invalid words
                if (!(newWord == String.Empty || glossary.IsInvalidWord(newWord)))
                    tokenList.Add(new Token(origWord, newWord));
            }
            // Check if entire input contained invalid chars / words / whitespace
            if (tokenList.Count == 0)
            {
                errorMessage = "I'm pretty sure that isn't a sentence.";
                return null;
            }
            // Input passed validation
            errorMessage = null;
            return tokenList;
        }

        /// <summary>
        /// Converts the given <see cref="Token"/> list into a <see cref="Node"/> list using the given <see cref="Glossary"/>.
        /// </summary>
        /// <param name="tokenList">The <see cref="Tokens"/> to convert.</param>
        /// <param name="glossary">The <see cref="Glossary"/> to reference for interpretation.</param>
        private List<Node> ConvertToNodes(List<Token> tokenList, Glossary glossary)
        {
            List<Node> nodeList = new List<Node>();
            for (int i = 0; i < tokenList.Count; i++)
            {
                Token token = tokenList[i];
                Definition def = glossary.GetDefOfWord(token.LookupWord);
                // Check if defined
                if (def == null)
                    nodeList.Add(new UnknownNode(token.OrigWord, glossary.UnknownID));
                // use definition
                else
                    nodeList.Add(def.CreateNode(token.OrigWord));
            }
            return nodeList;
        }

        /// <summary>
        /// Collects <see cref="AdjectiveNode"/> objects in <see cref="baseList"/> contiguous to each <see cref="NounNode"/>,
        /// and adds them to the <see cref="NounNode"/>.
        /// </summary>
        private void CollectAdjectives()
        {
            for (int i = baseList.Count - 1; i >= 0; i--)
            {
                if (baseList[i] is NounNode n)
                {
                    // collect preceeding Adjectives
                    while (i - 1 >= 0 && baseList[i - 1] is AdjectiveNode adj)
                    {
                        n.AddAdjective(adj);
                        baseList.RemoveAt(--i);
                    }
                    // collect subsequent Adjectives
                    while (i + 1 < baseList.Count && baseList[i + 1] is AdjectiveNode adj)
                    {
                        n.AddAdjective(adj);
                        baseList.RemoveAt(i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Collects chained <see cref="NounNode"/> objects in <see cref="baseList"/> (those with one or more
        /// <see cref="ConjunctionNode"/> objects between them), replacing the words with a new <see cref="NounGroupNode"/> object.
        /// </summary>
        private void CollectNouns()
        {
            int chainLength = 0;
            List<NounNode> nounList = new List<NounNode>();
            for (int i = baseList.Count - 1; i >= 0; i--)
            {
                // try to start/continue chain, if current word is noun and preceeding word is conjunction
                if (i - 2 >= 0 && baseList[i] is NounNode currNoun && baseList[i - 1] is ConjunctionNode)
                {
                    int p = i - 2;
                    // skip all preceeding consecutive conjunctions
                    while (p >= 0 && baseList[p] is ConjunctionNode)
                        p--;
                    // check if preceeding non-conjunction is a noun
                    if (baseList[p] is NounNode)
                    {
                        nounList.Insert(0, currNoun);
                        chainLength += i - p;
                        i = p + 1; // p is now position of preceeding noun
                        continue;
                    }
                }
                // otherwise, try to add noun to existing chain, then terminate
                if (nounList.Count > 0 && baseList[i] is NounNode nFinal)
                {
                    nounList.Insert(0, nFinal);
                    // turn first Noun in chain into NounCollection
                    baseList[i] = new NounGroupNode(nounList);
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
        public IEnumerator<Node> GetEnumerator()
        {
            foreach (Node n in baseList)
                yield return n;
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}