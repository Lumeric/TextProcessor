using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploaderBusinessLogic
{
	public class WordManager : IWordManager
	{
		private const int MINIMAL_WORD_COUNTER = 4;

		private Dictionary<string, int> _selectedWords;

		public WordManager()
		{
			_selectedWords = new Dictionary<string, int>();
		}

		public Dictionary<string, int> GetWords(List<string> words)
		{
			for (int i = 0; i < words.Count; i++)
			{
				AddOrUpdateWord(words[i]);
			}

			return FilterWords(_selectedWords);
		}

		private void AddOrUpdateWord(string word)
		{
			if (_selectedWords.TryGetValue(word, out int counter))
				_selectedWords[word]++;
			else
				_selectedWords.Add(word, ++counter);
		}

		private Dictionary<string, int> FilterWords(Dictionary<string, int> words)
		{
			var filteredWords = new Dictionary<string, int>();

			foreach (var word in words)
			{
				if (word.Value >= MINIMAL_WORD_COUNTER)
					filteredWords.Add(word.Key, word.Value);
			}

			return filteredWords;
		}
	}
}
