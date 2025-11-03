using System.Text;
using NUnit.Framework;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
	[TestCase("\"abcd\"", 0, "abcd", 6)]
	[TestCase("\"abcd", 0, "abcd", 5)]
	[TestCase("abcd\"abcd\\\\a\"", 4, "abcd\\a", 9)]
	[TestCase("b \"a'\"", 2, "a'", 4)]
	[TestCase(@"'a\' b'", 0, "a' b", 7)]
	[TestCase("'a'b", 0, "a", 3)]
	[TestCase("a'b'", 1, "b", 3)]
	public void Test(string line, int startIndex, string expectedValue, int expectedLength)
	{
		var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
		Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
	}

	
}

class QuotedFieldTask
{
	public static Token ReadQuotedField(string line, int startIndex)
	{
		int resultLength = 1;
		StringBuilder sb = new StringBuilder("");
		for (int i = startIndex + 1; i < line.Length; i++)
		{
			if (line[i] == '\\')
			{
				resultLength++;
				sb.Append(line[i + 1]);
				continue;
			}

			if ((line[i] == '\"' || line[i] == '\'') && line[i - 1] != '\\')
			{
				resultLength++;
				break;
			}
			
			sb.Append(line[i]);
			resultLength++;
		}
		return new Token(sb.ToString(), startIndex, resultLength);
	}
}