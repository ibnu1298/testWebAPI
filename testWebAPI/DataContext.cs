using Microsoft.EntityFrameworkCore;
using testWebAPI.Model;

namespace testWebAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<DataPolindrome> DataPolindromies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=checkPolDB")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
            Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();
        }
    }
}