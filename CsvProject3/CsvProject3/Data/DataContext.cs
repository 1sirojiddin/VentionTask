using CsvProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace CsvProject3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
       
    }
}
