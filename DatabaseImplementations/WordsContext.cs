using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseImplementations
{
    public class WordsContext : DbContext
    {
        public DbSet<Word> WordsSheet { get; set; }

        public WordsContext(DbContextOptions<WordsContext> options)
            :base(options)
        {
            Database.Migrate();
        }
    }
}
