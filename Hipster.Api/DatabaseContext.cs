using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hipster.Api;

public class DatabaseContext: DbContext
{
    public DbSet<Book> Books { get; init; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Uncle Bob",
                Year = 2000
            },
            new Book
            {
                Id = 2,
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt and David Thomas",
                Year = 2005
            }
        );
    }
}