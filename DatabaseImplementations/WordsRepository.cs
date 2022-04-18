using DatabaseInterfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseImplementations
{
	public class WordsRepository : IRepository<Word>
	{
		private readonly WordsContext _wordsContext;

		private bool _disposedValue;

		public WordsRepository(WordsContext wordsContext)
		{
			_wordsContext = wordsContext;
		}

		public async Task<List<Word>> GetAllAsync(Expression<Func<Word, bool>> condition)
		{
			return await _wordsContext.WordsSheet.Where(condition).ToListAsync();
		}

		public async Task<Word> GetAsync(Expression<Func<Word, bool>> condition)
		{
			return await _wordsContext.WordsSheet.FirstOrDefaultAsync(condition);
		}

		public async Task<List<Word>> GetFirstOrderedAsync(Expression<Func<Word, bool>> condition, int limit)
		{
			return await _wordsContext.WordsSheet.Where(condition)
				.OrderByDescending(word => word.Count)
				.ThenBy(word => word.Text)
				.Take(limit)
				.ToListAsync();
		}

		public Task UpdateRangeAsync(List<Word> items)
		{
			_wordsContext.UpdateRange(items);
			return Task.CompletedTask;
		}

		public async Task SaveAsync()
		{
			await _wordsContext.SaveChangesAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				_wordsContext.Dispose();
				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}