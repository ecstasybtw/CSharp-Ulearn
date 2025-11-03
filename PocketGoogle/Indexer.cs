using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private Dictionary<string, List<DocumentEntry>> index = new Dictionary<string, List<DocumentEntry>>();

        private class DocumentEntry
        {
            public int DocumentId { get; set; }
            public List<int> Positions { get; set; }
            
            public DocumentEntry(int documentId, List<int> positions)
            {
                DocumentId = documentId;
                Positions = positions;
            }
        }

        public void Add(int id, string documentText)
        {
            var words = SplitWords(documentText);
            
            foreach (var word in words)
            {
                if (!index.ContainsKey(word))
                {
                    index[word] = new List<DocumentEntry>();
                }

                var documentEntry = index[word].Find(entry => entry.DocumentId == id);
                if (documentEntry == null)
                {
                    documentEntry = new DocumentEntry(id, new List<int>());
                    index[word].Add(documentEntry);
                }
                
                documentEntry.Positions.Add(words.IndexOf(word));
            }
        }

        public List<int> GetIds(string word)
        {
            if (index.ContainsKey(word))
            {
                List<int> result = new List<int>();
                foreach (var entry in index[word])
                {
                    result.Add(entry.DocumentId);
                }
                return result;
            }
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (index.ContainsKey(word))
            {
                var entry = index[word].Find(e => e.DocumentId == id);
                if (entry != null)
                {
                    return entry.Positions;
                }
            }
            return new List<int>();
        }

        public void Remove(int id)
        {
            foreach (var word in index.Keys)
            {
                var documentEntry = index[word].Find(entry => entry.DocumentId == id);
                if (documentEntry != null)
                {
                    index[word].Remove(documentEntry);
                }
            }
        }

        private List<string> SplitWords(string documentText)
        {
            char[] delimiters = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
            string[] words = documentText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(words);
        }
    }
}