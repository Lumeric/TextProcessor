using DatabaseImplementations;
using DatabaseInterfaces;
using DownloaderBusinessLogic;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessorDownloader
{
	public class TextProcessorDownloadManager
	{
		private readonly IRepository<Word> _repository;
		private readonly IDownloadManager _downloadManager;
		private readonly WordsContext _wordsContext;

		public TextProcessorDownloadManager()
		{
			var contextFactory = new WordsDbContextFactory();
			_wordsContext = contextFactory.CreateDbContext(null);

			_repository = new WordsRepository(_wordsContext);
			_downloadManager = new DownloadManager(_repository);
		}

		public async Task Start()
		{
			await _downloadManager.DownloadFile();
		}
	}
}
