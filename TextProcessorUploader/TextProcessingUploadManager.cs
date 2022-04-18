using DatabaseImplementations;
using DatabaseInterfaces;
using Entities;
using UploaderBusinessLogic;

namespace TextProcessorUploader
{
	internal class TextProcessingUploadManager
	{
		private readonly IUploadManager _fileUploader;

		public TextProcessingUploadManager()
		{
			var contextFactory = new WordsDbContextFactory();
			var context = contextFactory.CreateDbContext(null);

			var reader = new FileReader();
			var validator = new WordsValidator();
			var wordManager = new WordManager();

			IRepository<Word> repository = new WordsRepository(context);
			_fileUploader = new UploadManager(reader, validator, wordManager, repository);
		}

		public async Task Start()
		{
			await _fileUploader.UploadFile();
		}
	}
}
