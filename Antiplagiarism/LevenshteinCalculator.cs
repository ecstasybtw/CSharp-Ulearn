using System;
using System.Collections.Generic;
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var result = new List<ComparisonResult>();
            for (int i = 0; i < documents.Count; i++)
                for (int j = i + 1; j < documents.Count; j++)
                    result.Add(Comparer(documents[i], documents[j]));
            return result;
        }

        ComparisonResult Comparer(DocumentTokens first, DocumentTokens second)
        {
            if (first == null)
                throw new ArgumentNullException();
            if (second == null)
                throw new ArgumentNullException();

            var firstCount = first.Count;
            var secondCount = second.Count;

            var previous = new double[secondCount + 1];
            var current = new double[secondCount + 1];

            for (int j = 0; j <= secondCount; j++)
                previous[j] = j;

            for (int i = 1; i <= firstCount; i++)
            {
                current[0] = i;
                for (int j = 1; j <= secondCount; j++)
                {
                    double cost = (first[i - 1] == second[j - 1])
                        ? 0
                        : TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]);

                    current[j] = Math.Min(Math.Min(previous[j] + 1, current[j - 1] + 1), previous[j - 1] + cost);
                }
                var temp = previous;
                previous = current;
                current = temp;
            }
            return new ComparisonResult(first, second, previous[secondCount]);
        }
    }
}
