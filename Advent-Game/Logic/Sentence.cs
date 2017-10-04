﻿using System;
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
    /// <summary>The ordered <see cref="Node"/> objects representing words in the <see cref="Sentence"/>.</summary>
    public List<Node> NodeList { get; private set; }

    /// <summary>
    /// Create a new <see cref="Sentence"/> from <paramref name="inputString"/>, using the specified array of removable words and a <see cref="Glossary"/>.
    /// </summary>
    /// <param name="inputString">The string input by the player.</param>
    /// <param name="removableWords">Words that can be removed from <paramref name="inputString"/> before building the <see cref="Sentence"/>.</param>
    /// <param name="glossary">Used to look up and validate each word in <paramref name="inputString"/>.</param>
    /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
    public Sentence(string inputString, string[] removableWords, Glossary glossary, out string errorMessage)
    {
        NodeList = new List<Node>();

        // TOKENIZATION -- Split input string into a list of strings, while removing invalid characters and unnecessary words.
        List<string> tokenList = Tokenize(inputString, removableWords, out errorMessage);
        if (errorMessage != null)
            return;

        // PARSING -- Construct a sentence out of tokens by validating words, assigning data to them, and organizing them syntactically.
        foreach (string token in tokenList)
        {
            NodeList.Add(CreateNodeFromToken(token, glossary));
        }
        CollectAdjectives();
        CollectNouns();
    }

    /// <summary>
    /// Removes invalid characters from <paramref name="inputString"/>, splits it into a list, and removes unnecessary words.
    /// </summary>
    /// <param name="inputString">The string input by the player.</param>
    /// <param name="removableTokens">The array of removable words.</param>
    /// <param name="errorMessage">Null if no error occurs, otherwise a string that describes the problem.</param>
    /// <returns>A list of tokens, which are acceptable strings that represent words.</returns>
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
        // Input passed validation
        errorMessage = null;
        return outputList;
    }

    /// <summary>
    /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>, using <paramref name="glossary"/> for verification.
    /// </summary>
    /// <param name="token">A word input by the player.</param>
    /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
    private Node CreateNodeFromToken(string token, Glossary glossary)
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
        foreach (Tuple<string[], DirCodes> entry in glossary.Directions)
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
    /// Collects <see cref="Adjective"/> objects in <see cref="NodeList"/> contiguous to each <see cref="Noun"/>,
    /// and replaces the <see cref="Noun"/> with one containing the <see cref="Adjective"/> objects.
    /// </summary>
    private void CollectAdjectives()
    {
        List<Adjective> adjectiveList = new List<Adjective>();
        for (int i = 0; i < NodeList.Count; i++)
        {
            if (NodeList[i] is Noun)
            {
                // collect preceding Adjectives
                if (i > 0)
                {
                    for (int p = i - 1; p >= 0; p--)
                    {
                        if (NodeList[p] is Adjective)
                        {
                            adjectiveList.Add((Adjective)NodeList[p]);
                            // make adjective a null object
                            NodeList[p] = null;
                        }
                        else break;
                    }
                }
                // collect subsequent Adjectives
                if (i < NodeList.Count - 1)
                {
                    for (int s = i + 1; s < NodeList.Count; s++)
                    {
                        if (NodeList[s] is Adjective)
                        {
                            adjectiveList.Add((Adjective)NodeList[s]);
                            NodeList[s] = null;
                        }
                        else break;
                    }
                }
                Noun noun = (Noun)NodeList[i];
                noun.AddAdjectives(adjectiveList.ToArray());
                adjectiveList.Clear();
            }
        }
        // remove null items (collected Adjectives) from sentence
        for (int i = NodeList.Count - 1; i >= 0; i--)
        {
            if (NodeList[i] == null)
                NodeList.RemoveAt(i);
        }
    }

    /// <summary>
    /// Collects chained <see cref="Noun"/> objects in <see cref="NodeList"/> (those with conjunction
    /// <see cref="Particle"/> objects between them) into a new <see cref="NounCollection"/> object.
    /// </summary>
    /// <returns>A new sentence with all chained Nouns grouped into NounCollections.</returns>
    private void CollectNouns()
    {
        List<Node> newList = new List<Node>();
        List<Noun> nounList = new List<Noun>();
        for (int i = 0; i < NodeList.Count; i++)
        {
            // start/continue chain, increment 2 to skip to next Noun
            if (i + 2 < NodeList.Count && NodeList[i] is Noun && NodeList[i + 1] is Particle && ((Particle)NodeList[i + 1]).Lemma == "and" && NodeList[i + 2] is Noun)
            {
                nounList.Add((Noun)NodeList[i]);
                i++;
            }
            // add noun to existing chain, then terminate
            else if (nounList.Count > 0 && NodeList[i] is Noun)
            {
                nounList.Add((Noun)NodeList[i]);
                newList.Add(new NounCollection(nounList.ToArray()));
                nounList.Clear();
            }
            // no chain, just add word to sentence and go to next
            else
                newList.Add(NodeList[i]);
        }
        NodeList = new List<Node>(newList);
    }
}
