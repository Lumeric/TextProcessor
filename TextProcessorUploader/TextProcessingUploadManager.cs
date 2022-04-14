using Entities;
using DatabaseImplementations;
using DatabaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UploaderBusinessLogic;

namespace TextProcessorUploader
{
    internal class TextProcessingUploadManager
    {
        private readonly IRepository<Word> _repository;

        private IFileUploader _fileUploader;

        public TextProcessingUploadManager()
        {
            var contextFactory = new WordsDbContextFactory();
            var context = contextFactory.CreateDbContext(null);

            // tempSolutionWithImplemetantionInitialization
            var reader = new FileReader();
            var validator = new FileValidator();
            var wordManager = new WordManager();

            _repository = new WordsRepository(context);

            _fileUploader = new FileUploader(reader, validator, wordManager, _repository);
        }

        public async Task Start()
        {
            await _fileUploader.UploadFile();
        }
    }
}
