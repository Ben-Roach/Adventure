
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Adventure.View;

namespace Adventure.Controller
{
    /// <summary>
    /// An ordered <see cref="IReadOnlyList{T}"/> of <see cref="Node"/> objects that represents an interpretable sentence.
    /// </summary>
    public class Sentence : IReadOnlyList<Node>
    {
        /// <summary>The <see cref="Node"/> objects in the <see cref="Sentence"/>.</summary>
        private List<Node> baseList;

        /// <summary>The number of top-level nodes this <see cref="Sentence"/> contains.</summary>
        public int Length => baseList.Count;
        /// <summary>The number of top-level nodes this <see cref="Sentence"/> contains.</summary>
        int IReadOnlyCollection<Node>.Count => baseList.Count;
        /// <summary>Gets the top-level <see cref="Node"/> in the <see cref="Sentence"/> at the specified index.</summary>
        public Node this[int i] => baseList[i];


        /// <summary>
        /// Create a new <see cref="Sentence"/>.
        /// </summary>
        private Sentence(List<Node> nodeList)
        {
            baseList = nodeList;
        }

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


        /// <summary>
        /// Construct a new <see cref="Sentence"/> from a string, using the specified <see cref="Glossary"/>.
        /// </summary>
        /// <param name="inputString">The raw string input by the player.</param>
        /// <param name="glossary">The <see cref="Glossary"/> to use when parsing the input string.</param>
        /// <param name="errorMessage">Describes any issue that occurred when parsing, else null if parsed successfully.</param>
        /// <returns>A new, interpretable <see cref="Sentence"/>, or null if parsing failed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public static Sentence Parse(string inputString, Glossary glossary, out string errorMessage)
        {
            // PREPARATION -- Argument checking, property/field instantiation.
            if (inputString == null) throw new ArgumentNullException(nameof(inputString));
            if (glossary == null) throw new ArgumentNullException(nameof(glossary));
            // TOKENIZATION -- Split input string into a list of strings, while removing invalid characters and unnecessary words.
            List<Token> tokenList = Tokenize(inputString, glossary, out errorMessage);
            if (errorMessage != null) return null;
            // PARSING -- Construct a sentence out of tokens by converting them to meaningful nodes and organizing them syntactically.
            List<Node> nodeList = glossary.ConvertToNodes(tokenList);
            CollectAdjectives(nodeList);
            CollectNouns(nodeList);
            // FINISH -- Return new sentence
            return new Sentence(nodeList);
        }

        /// <summary>
        /// Constructor helper method.
        /// Converts a player input string into a list of <see cref="Token"/> objects.
        /// </summary>
        /// <param name="inputString">The string input by the player.</param>
        /// <param name="glossary">The <see cref="Glossary"/> used to normalize and validate the input.</param>
        /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
        /// <returns>A list of <see cref="Token"/> objects, or null if an error occured.</returns>
        private static List<Token> Tokenize(string inputString, Glossary glossary, out string errorMessage)
        {
            // trim and split string by whitespace
            string[] wordList = inputString.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            // Check for empty / whitespace input
            if (wordList.Length == 0)
            {
                errorMessage = "Speak up, please.";
                return null;
            }
            // create tokens
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
        /// Constructor helper method.
        /// Collects <see cref="AdjectiveNode"/> objects in <see paramref="nodeList"/> contiguous to each <see cref="NounNode"/>,
        /// and adds them to the <see cref="NounNode"/>.
        /// </summary>
        private static void CollectAdjectives(List<Node> nodeList)
        {
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                if (nodeList[i] is NounNode n)
                {
                    // collect preceeding Adjectives
                    while (i - 1 >= 0 && nodeList[i - 1] is AdjectiveNode adj)
                    {
                        n.AddAdjective(adj);
                        nodeList.RemoveAt(--i);
                    }
                    // collect subsequent Adjectives
                    while (i + 1 < nodeList.Count && nodeList[i + 1] is AdjectiveNode adj)
                    {
                        n.AddAdjective(adj);
                        nodeList.RemoveAt(i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Constructor helper method.
        /// Collects chained <see cref="NounNode"/> objects in <see paramref="nodeList"/> (those with one or more
        /// <see cref="ConjunctionNode"/> objects between them), replacing the words with a new <see cref="NounGroupNode"/> object.
        /// </summary>
        private static void CollectNouns(List<Node> nodeList)
        {
            int chainLength = 0;
            List<NounNode> nounList = new List<NounNode>();
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                // try to start/continue chain, if current word is noun and preceeding word is conjunction
                if (i - 2 >= 0 && nodeList[i] is NounNode currNoun && nodeList[i - 1] is ConjunctionNode)
                {
                    int p = i - 2;
                    // skip all preceeding consecutive conjunctions
                    while (p >= 0 && nodeList[p] is ConjunctionNode)
                        p--;
                    // check if preceeding non-conjunction is a noun
                    if (nodeList[p] is NounNode)
                    {
                        nounList.Insert(0, currNoun);
                        chainLength += i - p;
                        i = p + 1; // p is now position of preceeding noun
                        continue;
                    }
                }
                // otherwise, try to add noun to existing chain, then terminate
                if (nounList.Count > 0 && nodeList[i] is NounNode nFinal)
                {
                    nounList.Insert(0, nFinal);
                    // turn first Noun in chain into NounCollection
                    nodeList[i] = new NounGroupNode(nounList);
                    // remove chained nouns and conjunctions from sentence
                    for (int j = 0; j < chainLength; j++)
                        nodeList.RemoveAt(i + 1);
                    // prep for next chain
                    nounList.Clear();
                    chainLength = 0;
                }
            }
        }

        /// <summary>
        /// Interprets a given <see cref="Sentence"/>. Initiates game model manipulation.
        /// </summary>
        /// <param name="sentence">The <see cref="Sentence"/> to interpret.</param>
        public static void Interpret(Sentence sentence)
        {
            if (sentence == null) throw new ArgumentNullException(nameof(sentence));
            // iterate through each top level node in sentence
            for (int i = 0; i < sentence.Length; i++)
            {
                // Conjunction
                if (sentence[i] is ConjunctionNode)
                    continue;

                // Command
                else if (sentence[i] is CommandNode command)
                    command.Delegate();

                // Verb
                else if (sentence[i] is VerbNode verb)
                {
                    foreach (VerbPhrase syntax in verb.Syntaxes)
                    {

                    }
                    // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                    // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                    // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                    // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
                }

                // Unknown
                else if (sentence[i] is UnknownNode)
                {
                    ConsoleGUI.Print("I don't understand the word \"" + sentence[i].OrigWord + ".\"");
                    return;
                }

                // anything else
                else
                {
                    ConsoleGUI.Print("You lost me at \"" + sentence[i].OrigWord + ".\"");
                    return;
                }
            }
        }
    }
}