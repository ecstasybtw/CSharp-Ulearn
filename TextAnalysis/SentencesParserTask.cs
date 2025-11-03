using System.Text.RegularExpressions;
using NUnit.Framework;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        char[] sentenceSeparators = { '.', '!', '?', ';', ':', '(', ')' };
        var sentences = text.Split(sentenceSeparators);
        foreach (var sentence in sentences)
        {
            var words = new List<string>();
            var word = "";
            foreach (var character in sentence)
            {
                if (char.IsLetter(character) || character == '\'')
                    word += char.ToLower(character);

                else if (word.Length > 0)
                {
                    words.Add(word);
                    word = "";
                }
            }
            if (word.Length > 0)
                    words.Add(word);

            if (words.Count > 0)
                sentencesList.Add(words);
        }
        return sentencesList;
    }
}