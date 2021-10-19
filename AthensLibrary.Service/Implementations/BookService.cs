using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<Book> _bookRepository;
        private readonly IServiceFactory _serviceFactory;

        public BookService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory)
        {
            _unitofWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _bookRepository = _unitofWork.GetRepository<Book>();
        }

        public Task<Book> BorrowBook()
        {
            throw new NotImplementedException();
        }

        public Book CreateBook(Book book)
        {
            _bookRepository.Add(book);
            _unitofWork.SaveChanges();
            return book;
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

