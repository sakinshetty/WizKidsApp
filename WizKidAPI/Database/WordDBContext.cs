using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WizKidAPI.Database.DBModel;

namespace WizKidAPI.Database
{
    public class WordDBContext : DbContext
    {
        public WordDBContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"Data Source = Dictionary.db;");
        }

        public DbSet<WordsDB>? Words { get; set; }

        
    }
}

