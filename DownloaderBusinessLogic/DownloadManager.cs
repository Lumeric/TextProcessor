using DatabaseInterfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderBusinessLogic
{
	public class DownloadManager : IDownloadManager
	{
		private const string ENTER_FILTER_TEXT_MESSAGE = "Enter a string to display matches.";
		private const int LIMIT_VALUE = 5;

		private readonly IRepository<Word> _repository;

		public DownloadManager(IRepository<Word> repository)
		{
			_repository = repository;
		}

		public async Task DownloadFile()
		{
			Console.WriteLine(ENTER_FILTER_TEXT_MESSAGE);

			string query = Console.ReadLine();

			var records = await _repository.GetFirstOrderedAsync(word => word.Text.StartsWith(query), LIMIT_VALUE);

			PrintToConsole(records);
		}

		private void PrintToConsole(List<Word> records)
		{
			var stringBuilder = new StringBuilder();

			foreach (var record in records)
			{
				stringBuilder.Append(record.Text).Append(" - ").AppendLine(record.Count.ToString());
			}

			Console.WriteLine(stringBuilder);
		}
	}
}
