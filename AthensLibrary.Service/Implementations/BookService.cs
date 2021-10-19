using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class BookService : IBookService
    {
        public Task<Book> BorrowBook()
        {
            throw new NotImplementedException();
        }

        public Task<Book> CreateBook()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksByAnAuthor()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksByCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksByIsbn()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksByYear()
        {
            throw new NotImplementedException();
        }

        public Task<Book> ReturnBook()
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBook()
        {
            throw new NotImplementedException();
        }
    }
}

