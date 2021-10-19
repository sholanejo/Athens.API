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
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<Author> _authorRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitOfWork unitofWork, IServiceFactory serviceFactory)
        {
            _unitofWork = unitofWork;
            _authorRepository = unitofWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
        }

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

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoryById()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoryByName()
        {
            throw new NotImplementedException();
        }

        public Task<Author> Login()
        {
            throw new NotImplementedException();
        }

        public Task<Book> ReturnBook()
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAuthor()
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBook()
        {
            throw new NotImplementedException();
        }
    }
}
