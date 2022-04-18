using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DatabaseImplementations
{
	public class WordsDbContextFactory : IDesignTimeDbContextFactory<WordsContext>
	{
		public WordsContext CreateDbContext(string[] args)
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");

			var config = builder.Build();
			string connectionString = config.GetConnectionString("DbConnection");
			var optionsBuilder = new DbContextOptionsBuilder<WordsContext>();

			optionsBuilder.UseSqlServer(connectionString);

			return new WordsContext(optionsBuilder.Options);
		}
	}
}
