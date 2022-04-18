using System.Linq.Expressions;

namespace DatabaseInterfaces
{
	public interface IRepository<T> : IDisposable
		where T : class
	{
		Task<T> GetAsync(Expression<Func<T, bool>> condition);

		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> condition);

		Task<List<T>> GetFirstOrderedAsync(Expression<Func<T, bool>> condition, int limit);

		Task UpdateRangeAsync(List<T> items);

		Task SaveAsync();
	}
}
