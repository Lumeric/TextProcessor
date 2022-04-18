using DatabaseInterfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace UploaderBusinessLogic
{
	public class UploadManager : IUploadManager
	{
		private const string ENTER_FILE_PATH_MESSAGE = "Enter a file path.";

		private readonly IFileReader _fileReader;
		private readonly IWordsValidator _fileValidator;
		private readonly IWordManager _wordManager;
		private readonly IRepository<Word> _repository;

		public UploadManager(IFileReader fileReader,
			IWordsValidator fileValidator,
			IWordManager wordManager,
			IRepository<Word> repository)
		{
			_fileReader = fileReader;
			_fileValidator = fileValidator;
			_wordManager = wordManager;
			_repository = repository;
		}

		public async Task UploadFile()
		{
			try
			{
				Console.WriteLine(ENTER_FILE_PATH_MESSAGE);

				string filePath = Console.ReadLine();
				var splittedWords = _fileReader.ReadAndSplit(filePath);
				var validatedWords = _fileValidator.ValidateWords(splittedWords);
				var countedWords = _wordManager.GetWords(validatedWords);
				var wordEnties = CreateWordEntities(countedWords);
				wordEnties = await FormRecords(wordEnties);

				await _repository.UpdateRangeAsync(wordEnties);
				await SaveChanges();

				_repository.Dispose();
			}
			catch (Exception)
			{
				await UploadFile();
			}
		}


		private List<Word> CreateWordEntities(Dictionary<string, int> countedWords)
		{
			List<Word> wordEntities = new List<Word>();

			foreach (var word in countedWords)
			{
				Word wordItem = new Word
				{
					Text = word.Key,
					Count = word.Value,
				};

				wordEntities.Add(wordItem);
			}

			return wordEntities;
		}

		private async Task<List<Word>> FormRecords(List<Word> inputWords)
		{
			var modifiedWords = new List<Word>();

			foreach (var word in inputWords)
			{
				Word storedWord = await GetStoredWord(word);

				if (storedWord != null)
				{
					UpdateWordCounter(storedWord, word.Count);
					modifiedWords.Add(storedWord);
				}
				else
				{
					modifiedWords.Add(word);
				}
			}

			return modifiedWords;
		}

		private async Task<Word> GetStoredWord(Word inputWord)
		{
			return await _repository.GetAsync(word => word != null && word.Text == inputWord.Text);
		}

		private void UpdateWordCounter(Word word, int addingValue)
		{
			word.Count += addingValue;
		}

		private async Task SaveChanges()
		{
			var saved = false;

			while (!saved)
			{
				try
				{
					await _repository.SaveAsync();
					saved = true;
				}
				catch (DbUpdateConcurrencyException ex)
				{
					Console.WriteLine($"Failed to save changes. {ex.Message}");
				}
			}
		}
	}
}
