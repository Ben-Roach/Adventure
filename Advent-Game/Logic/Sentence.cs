using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SentenceStructure;
using Lexicon;

/// <summary>
/// 
/// </summary>
public class Sentence
{
    private List<INode> baseList;

    /// <summary></summary>
    public List<INode> NodeList { get { return baseList; } }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputString"></param>
    /// <param name="removableWords"></param>
    /// <param name="glossary"></param>
    /// <param name="errorMessage"></param>
    public Sentence(string inputString, string[] removableWords, Glossary glossary, out string errorMessage)
    {
        baseList = new List<INode>();

        // TOKENIZATION -- Split input string into a list of strings, while removing unnecessary words and invalid strings.
        List<string> tokenList = Tokenize(inputString, removableWords, out errorMessage);
        if (errorMessage != null)
            return;

        // PARSING -- Construct a sentence out of tokens by validating words, assigning data to them, and organizing them syntactically.
        foreach (string token in tokenList)
        {
            baseList.Add(CreateNodeFromToken(token, glossary));
        }
        CollectAdjectives();
        CollectNouns();
    }

    /// <summary>
    /// Removes invalid characters from inputString, splits it into a List, and removes unnecessary words. Returns the List.
    /// </summary>
    /// <param name="inputString">The player's raw input as a string.</param>
    /// <param name="removableTokens"></param>
    /// <param name="errorMessage"></param>
    /// <returns>True if inputString is valid, else false.</returns>
    private List<string> Tokenize(string inputString, string[] removableTokens, out string errorMessage)
    {
        // Check for empty input
        if (inputString == "")
        {
            errorMessage = "Speak up, please.";
            return null;
        }
        // Remove invalid characters
        StringBuilder strBuilder = new StringBuilder();
        foreach (char letter in inputString)
        {
            if ((letter >= 'A' && letter <= 'z') || (letter >= '0' && letter <= '9') || letter == ' ' || letter == '&' || letter == '?')
                strBuilder.Append(letter);
        }
        // Check if any valid characters remain
        string tempStr = strBuilder.ToString();
        if (tempStr.Length == 0)
        {
            errorMessage = "Try using actual words.";
            return null;
        }
        // Split inputString into outputList -- a list of string tokens, each representing one word
        List<string> outputList = tempStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        // Remove unnecessary words
        for (int i = outputList.Count() - 1; i >= 0; i--)
        {
            if (removableTokens.Contains(outputList[i]))
                outputList.Remove(outputList[i]);
        }
        // Check if any potentially valid tokens remain
        if (outputList.Count == 0)
        {
            errorMessage = "I'm pretty sure that isn't a sentence.";
            return null;
        }
        // Input passed initial validation
        errorMessage = null;
        return outputList;
    }

    /// <summary>
    /// Creates and returns a list of INodes representing a sentence, derived from tokens.
    /// </summary>
    /// <param name="token">A word input by the player.</param>
    /// <returns>An INode that represents the token.</returns>
    private INode CreateNodeFromToken(string token, Glossary glossary)
    {
        string tokenLower = token.ToLower();
        foreach (Tuple<string[], VerbSyntax[]> entry in glossary.Verbs)
        {
            if (entry.Item1.Contains(tokenLower))
                return new Verb(tokenLower, entry.Item2);
        }
        foreach (Tuple<string[], Action> entry in glossary.Commands)
        {
            if (entry.Item1.Contains(tokenLower))
                return new Command(tokenLower, entry.Item2);
        }
        foreach (Tuple<string[], string> entry in glossary.Particles)
        {
            if (entry.Item1.Contains(tokenLower))
                return new Particle(tokenLower, entry.Item2);
        }
        foreach (Tuple<string[], string> entry in glossary.Directions)
        {
            if (entry.Item1.Contains(tokenLower))
                return new Direction(tokenLower, entry.Item2);
        }
        // always search writable glossaries last, to avoid accidentally hiding entries in readonly glossaries.
        foreach (string entry in glossary.Nouns)
        {
            if (entry == tokenLower)
                return new Noun(token);
        }
        foreach (string entry in glossary.Adjectives)
        {
            if (entry == tokenLower)
                return new Adjective(token);
        }
        return new UnknownWord(token);
    }

    /// <summary>
    /// Collects Adjectives contiguous to each Noun, and adds them to the respective Noun.
    /// </summary>
    /// <returns>The sentence with Adjectives collected into their respective Nouns.</returns>
    private void CollectAdjectives()
    {
        List<Adjective> adjectiveList = new List<Adjective>();
        for (int i = 0; i < baseList.Count; i++)
        {
            if (baseList[i] is Noun)
            {
                // collect preceding Adjectives
                if (i > 0)
                {
                    for (int p = i - 1; p >= 0; p--)
                    {
                        if (baseList[p] is Adjective)
                        {
                            adjectiveList.Add((Adjective)baseList[p]);
                            // make adjective a null object
                            baseList[p] = null;
                        }
                        else break;
                    }
                }
                // collect subsequent Adjectives
                if (i < baseList.Count - 1)
                {
                    for (int s = i + 1; s < baseList.Count; s++)
                    {
                        if (baseList[s] is Adjective)
                        {
                            adjectiveList.Add((Adjective)baseList[s]);
                            baseList[s] = null;
                        }
                        else break;
                    }
                }
                Noun noun = (Noun)baseList[i];
                noun.AddAdjectives(adjectiveList.ToArray());
                adjectiveList.Clear();
            }
        }
        // remove null items (collected Adjectives) from sentence
        for (int i = baseList.Count - 1; i >= 0; i--)
        {
            if (baseList[i] == null)
                baseList.RemoveAt(i);
        }
    }

    /// <summary>
    /// Collects chained Nouns (those with single "and" Particles between them) into a NounCollection.
    /// </summary>
    /// <returns>A new sentence with all chained Nouns grouped into NounCollections.</returns>
    private void CollectNouns()
    {
        List<INode> newList = new List<INode>();
        List<Noun> nounList = new List<Noun>();
        for (int i = 0; i < baseList.Count; i++)
        {
            // start/continue chain, increment 2 to skip to next Noun
            if (i + 2 < baseList.Count && baseList[i] is Noun && baseList[i + 1] is Particle && ((Particle)baseList[i + 1]).Lemma == "and" && baseList[i + 2] is Noun)
            {
                nounList.Add((Noun)baseList[i]);
                i++;
            }
            // add noun to existing chain, then terminate
            else if (nounList.Count > 0 && baseList[i] is Noun)
            {
                nounList.Add((Noun)baseList[i]);
                newList.Add(new NounCollection(nounList.ToArray()));
                nounList.Clear();
            }
            // no chain, just add word to sentence and go to next
            else
                newList.Add(baseList[i]);
        }
        baseList = new List<INode>(newList);
    }

    /// <summary>
    /// Attempts to comprehend a constructed sentence.
    /// </summary>
    public string Comprehend()
    {
        for (int i = 0; i < baseList.Count; i++)
        {
            if (baseList[i] is Particle && ((Particle)baseList[i]).Lemma == "and")
                continue;

            else if (baseList[i] is Command command)
                command.ActionDelegate();

            else if (baseList[i] is Verb verb)
            {
                List<VerbSyntax> syntaxList = verb.Syntaxes.ToList();
                // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
            }

            else if (baseList[i] is UnknownWord)
            {
                return "I don't understand the word \"" + baseList[i].OrigToken + "\".";
            }

            else
            {
                return "You lost me at \"" + baseList[i].OrigToken + "\".";
            }
        }
        return "Something weird happened. Maybe try that again?";
    }
}
