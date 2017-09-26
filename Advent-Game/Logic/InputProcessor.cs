using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Sentence;
using Lexicon;

/// <summary>
/// Parses and interprets player input.
/// </summary>
static class InputProcessor
{
    /// <summary>
    /// Removes invalid characters from inputString, splits it into a List, and removes unnecessary words.
    /// </summary>
    /// <param name="inputString">The player's raw input as a string.</param>
    /// <param name="outputList">A filtered and tokenized list derived from inputString.</param>
    /// <returns>True if inputString is valid, else false.</returns>
    public static bool Tokenize(string inputString, string[] removableTokens, out List<string> outputList)
    {
        // Check for empty input
        if (inputString == "")
        {
            Console.WriteLine("Speak up, please.");
            outputList = null;
            return false;
        }
        // Remove invalid characters
        StringBuilder strBuilder = new StringBuilder();
        foreach (char letter in inputString)
        {
            if ((letter >= 'A' && letter <= 'z') || (letter >= '0' && letter <= '9') || letter == ' ' || letter == '&' || letter == '?')
                strBuilder.Append(letter);
        }
        // Check if any valid characters remain
        string temp = strBuilder.ToString();
        if (temp.Length == 0)
        {
            Console.WriteLine("Try using actual words.");
            outputList = null;
            return false;
        }
        // Split inputString into outputList -- a list of string tokens, each representing one word
        outputList = temp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        // Remove unnecessary words
        for (int i = outputList.Count() - 1; i >= 0; i--)
        {
            if (removableTokens.Contains(outputList[i]))
                outputList.Remove(outputList[i]);
        }
        // Check if any potentially valid tokens remain
        if (outputList.Count == 0)
        {
            Console.WriteLine("I'm pretty sure that isn't a sentence.");
            outputList = null;
            return false;
        }
        // Input passed initial validation
        return true;
    }

    /// <summary>
    /// Creates and returns a list of INodes representing a sentence, derived from tokens.
    /// </summary>
    /// <param name="token">A word input by the player.</param>
    /// <returns>An INode that represents the token.</returns>
    public static INode CreateNodeFromToken(string token, Glossary glossary)
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
    public static List<INode> CollectAdjectives(List<INode> inputSentence)
    {
        List<Adjective> adjectiveList = new List<Adjective>();
        for (int i = 0; i < inputSentence.Count; i++)
        {
            if (inputSentence[i] is Noun)
            {
                // collect preceding Adjectives
                if (i > 0)
                {
                    for (int p = i - 1; p >= 0; p--)
                    {
                        if (inputSentence[p] is Adjective)
                        {
                            adjectiveList.Add((Adjective)inputSentence[p]);
                            // make adjective a null object
                            inputSentence[p] = null;
                        }
                        else break;
                    }
                }
                // collect subsequent Adjectives
                if (i < inputSentence.Count - 1)
                {
                    for (int s = i + 1; s < inputSentence.Count; s++)
                    {
                        if (inputSentence[s] is Adjective)
                        {
                            adjectiveList.Add((Adjective)inputSentence[s]);
                            inputSentence[s] = null;
                        }
                        else break;
                    }
                }
                Noun noun = (Noun)inputSentence[i];
                noun.AddAdjectives(adjectiveList.ToArray());
                adjectiveList.Clear();
            }
        }
        // remove null items (collected Adjectives) from sentence
        for (int i = inputSentence.Count - 1; i >= 0; i--)
        {
            if (inputSentence[i] == null)
                inputSentence.RemoveAt(i);
        }
        return inputSentence;
    }

    /// <summary>
    /// Collects chained Nouns (those with single "and" Particles between them) into a NounCollection.
    /// </summary>
    /// <returns>A new sentence with all chained Nouns grouped into NounCollections.</returns>
    public static List<INode> CollectNouns(List<INode> inputSentence)
    {
        List<INode> newSentence = new List<INode>();
        List<Noun> nounList = new List<Noun>();
        for (int i = 0; i < inputSentence.Count; i++)
        {
            // start/continue chain, increment 2 to skip to next Noun
            if (i + 2 < inputSentence.Count && inputSentence[i] is Noun && inputSentence[i + 1] is Particle && ((Particle)inputSentence[i + 1]).Lemma == "and" && inputSentence[i + 2] is Noun)
            {
                nounList.Add((Noun)inputSentence[i]);
                i++;
            }
            // add noun to existing chain, then terminate
            else if (nounList.Count > 0 && inputSentence[i] is Noun)
            {
                nounList.Add((Noun)inputSentence[i]);
                newSentence.Add(new NounCollection(nounList.ToArray()));
                nounList.Clear();
            }
            // no chain, just add word to sentence and go to next
            else
                newSentence.Add(inputSentence[i]);
        }
        return newSentence;
    }

    /// <summary>
    /// Attempts to comprehend a constructed sentence.
    /// </summary>
    public static void Comprehend(List<INode> inputSentence)
    {
        for (int i = 0; i < inputSentence.Count; i++)
        {
            if (inputSentence[i] is Particle && ((Particle)inputSentence[i]).Lemma == "and")
                continue;

            else if (inputSentence[i] is Command command)
                command.ActionDelegate();

            else if (inputSentence[i] is Verb verb)
            {
                List<VerbSyntax> syntaxList = verb.Syntaxes.ToList();
                // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
            }

            else if (inputSentence[i] is UnknownWord)
            {
                Console.WriteLine("I don't understand the word \"" + inputSentence[i].OrigToken + "\".");
                break;
            }

            else
            {
                Console.WriteLine("You lost me at \"" + inputSentence[i].OrigToken + "\".");
                break;
            }
        }
    }
}
