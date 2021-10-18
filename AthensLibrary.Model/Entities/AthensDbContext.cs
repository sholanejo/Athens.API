using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AthensLibrary.Model.Entities
{
    public class AthensDbContext : IdentityDbContext<User>
    {
        public AthensDbContext(DbContextOptions<AthensDbContext> options) : base(options)
        {
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BorrowDetail> BorrowDetail { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Category> Categories { get; set; }

        //add the day a book was added
        //an author should also be able to borrow a book
        //all tables should have created at and created by
        //add aadress
        //scope your project
        //category should not show as numbers on  vf,

        //Authentication
        //crud for all
        //

    }
}
