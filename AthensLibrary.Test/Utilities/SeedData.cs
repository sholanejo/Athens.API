using System;
using System.Collections.Generic;
using System.Text;
using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.Builder;

namespace AthensLibrary.Test.Utilities
{
    public class SeedData
    {
        public static void SeedinitialData(AthensDbContext context)
        {
            context.Books.AddRange(new Book
            {
                ID = Guid.Parse("D78697EE-3A6B-4DDF-8898-F092E821FD4D"),
                Title = "TestTitle",
                AuthorId = Guid.NewGuid(),
                CategoryName = "Fiction",
                InitialBookCount = 10,
                CurrentBookCount = 10,
                PublicationYear = DateTime.Now,
            },
            new Book
            {
                ID = Guid.Parse("D78697EE-3A6B-4DDF-8898-F092E821FD6D"),
                Title = "TestTitle",
                AuthorId = Guid.NewGuid(),
                CategoryName = "Fiction",
                InitialBookCount = 10,
                CurrentBookCount = 10,
                PublicationYear = DateTime.Now,
            });

            context.SaveChanges();
        }
    }
}
