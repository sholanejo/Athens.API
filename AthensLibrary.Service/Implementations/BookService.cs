using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;

namespace AthensLibrary.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public BookService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<(bool, string)> UpdateBook(Guid bookId, BookUpdateDTO model)
        {
            var bookEntity =  _bookRepository.GetById(bookId);
            if (bookEntity is null) return (false, "Book not found");
            _mapper.Map(model, bookEntity);
            return (await _unitofWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
        }
    }
}

