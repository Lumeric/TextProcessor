using DatabaseImplementations;
using DownloaderBusinessLogic;

namespace TextProcessorDownloader
{
	public class TextProcessorDownloadManager
	{
		private readonly IDownloadManager _downloadManager;

		public TextProcessorDownloadManager()
		{
			var contextFactory = new WordsDbContextFactory();
			var wordsContext = contextFactory.CreateDbContext(null);
			var repository = new WordsRepository(wordsContext);

			_downloadManager = new DownloadManager(repository);
		}

		public async Task Start()
		{
			await _downloadManager.DownloadFile();
		}
	}
}
