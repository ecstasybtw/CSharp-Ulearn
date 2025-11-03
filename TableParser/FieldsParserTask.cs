using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
	public static void Test(string input, string[] expectedResult)
	{
		var actualResult = FieldsParserTask.ParseLine(input);
		Assert.AreEqual(expectedResult.Length, actualResult.Count);
		for (int i = 0; i < expectedResult.Length; ++i)
		{
			Assert.AreEqual(expectedResult[i], actualResult[i].Value);
		}
	}

	        [TestCase("text", new[] {"text"})]
            [TestCase("hello world", new[] {"hello", "world"})]
            [TestCase("a", new[] {"a"})]
			[TestCase("a b c", new[] {"a", "b", "c"})]
			[TestCase("a b", new[] {"a", "b"})]
			[TestCase("'a \"b\" c'", new[] {"a \"b\" c"})]
			[TestCase("\"a 'b' c\"", new[] {"a 'b' c"})]
			[TestCase("a   b", new[] {"a", "b"})]
			[TestCase("\"a b c\"", new[] {"a b c"})]
			[TestCase("\"\"", new[] {""})]
			[TestCase("", new string[0])]
			[TestCase("\"a b c", new[] {"a b c"})]
			[TestCase("a \"b c\"", new[] {"a", "b c"})]
			[TestCase("\"a b\" c", new[] {"a b", "c"})]
			[TestCase("'a \\'b\\' c'", new[] {"a 'b' c"})]
			[TestCase("a\"b\"c", new[] {"a", "b", "c"})]
			[TestCase("\"a \\\"b\\\" c\"", new[] {"a \"b\" c"})]
			[TestCase("\"a \\\\\"", new[] {"a \\"})]
			[TestCase("\"a \\\\ b\"", new[] {"a \\ b"})]
			[TestCase("  a b  ", new[] {"a", "b"})]
			[TestCase("\"a b \"", new[] {"a b "})]
			[TestCase("\"a b ", new[] {"a b "})]

            public static void RunTests(string input, string[] expectedOutput)
            {
            	Test(input, expectedOutput);
            }
}

public class FieldsParserTask
{
	
	public static List<Token> ParseLine(string line)
	{
		var tokens = new List<Token>();
		int index = 0;

		while(index < line.Length)
		{
			index = SkipWhitespaces(line, index);
			if (index >= line.Length)
				break;
			
			if (line[index] == '\"')
			{
				Token quotedToken = QuotedFieldTask.ReadQuotedField(line, index);
				tokens.Add(quotedToken);
				index = quotedToken.GetIndexNextToToken(); 
			}
			else
			{
				Token unquotedToken = ReadField(line, index);
				tokens.Add(unquotedToken);
				index = unquotedToken.GetIndexNextToToken();
			}
		}

		return tokens;
	}
        
	public static Token ReadField(string line, int startIndex)
	{
		var resultLength = 0;
		var sb = new StringBuilder();
		for (int i = startIndex; i < line.Length; i++)
		{
			if (char.IsWhiteSpace(line[i]) || line[i] == '\"' || line[i] == '\'')
				break;
			sb.Append(line[i]);
			resultLength++;
		}
		return new Token(sb.ToString(), startIndex, resultLength);
	}

	public static int SkipWhitespaces(string line, int index)
	{
		while (index < line.Length && char.IsWhiteSpace(line[index]))
			index++;
		return index;
	}

	public static Token ReadQuotedField(string line, int startIndex)
	{
		return QuotedFieldTask.ReadQuotedField(line, startIndex);
	}
}