using Entities;
using DatabaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace UploaderBusinessLogic
{
	public class FileUploader : IFileUploader
	{
		private const string ENTER_FILE_PATH_MESSAGE = "Enter a file path.";

		private readonly IFileReader _fileReader;
		private readonly IFileValidator _fileValidator;
		private readonly IWordManager _wordManager;
		private readonly IRepository<Word> _repository;

		public FileUploader(IFileReader fileReader, 
			IFileValidator fileValidator, 
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

				// add method to ask user if they wanna add more file by check (y/n)

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

		// Check functionality!!!!!!!!!!!!!!!!
		private async Task SaveChanges()
		{
			var saved = false;

			while (!saved)
			{
				try
				{
					// Attempt to save changes to the database
					await _repository.SaveAsync();
					saved = true;
				}
				catch (DbUpdateConcurrencyException ex)
				{
					foreach (var entry in ex.Entries)
					{
						if (entry.Entity is Word)
						{
							var proposedValues = entry.CurrentValues;
							var databaseValues = entry.GetDatabaseValues();

							foreach (var property in proposedValues.Properties)
							{
								var proposedValue = proposedValues[property];
								var databaseValue = databaseValues[property];

								// TODO: decide which value should be written to database
								// proposedValues[property] = <value to be saved>;
							}

							// Refresh original values to bypass next concurrency check
							entry.OriginalValues.SetValues(databaseValues);
						}
						else
						{
							throw new NotSupportedException(
								"Don't know how to handle concurrency conflicts for "
								+ entry.Metadata.Name);
						}
					}
				}
			}
		}
	}
}
