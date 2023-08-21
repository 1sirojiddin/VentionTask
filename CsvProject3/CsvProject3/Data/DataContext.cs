using CsvProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace CsvProject3.Data
{
public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }       
}
}
