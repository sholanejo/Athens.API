using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<BorrowDetail> BorrowDetail { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
    }
}
