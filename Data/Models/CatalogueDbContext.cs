using FinalProjectCatalogue.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCatalogue.Data.Models
{
    internal class CatalogueDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public CatalogueDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Curs .net\FinalProjectCatalogue\Data\Catalogue.mdf"";Integrated Security=True");
        }
    }
}
