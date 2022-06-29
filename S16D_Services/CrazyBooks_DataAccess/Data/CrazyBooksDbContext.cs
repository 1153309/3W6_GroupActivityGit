using CrazyBooks_Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyBooks_DataAccess.Data
{
    public class CrazyBooksDbContext:DbContext
    {
        public CrazyBooksDbContext(DbContextOptions<CrazyBooksDbContext> options) : base(options)
        {

        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorDetail> AuthorsDetail { get; set; }
        public DbSet<AuthorBook> AuthorsBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuration fluent API

            //composite key
            modelBuilder.Entity<AuthorBook>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            //Générer des données de départ
            modelBuilder.GenerateData();
        }
    }
}
