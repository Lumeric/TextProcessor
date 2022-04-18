using Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseImplementations
{
	public class WordsContext : DbContext
	{
		public DbSet<Word> WordsSheet { get; set; }

		public WordsContext(DbContextOptions<WordsContext> options)
			: base(options)
		{
			Database.Migrate();
		}
	}
}
