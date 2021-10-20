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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public BookService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _bookRepository = _unitOfWork.GetRepository<Book>();
        }

        public Task<Book> BorrowBook()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)>  CreateBook(BookCreationDTO book)
        {
            //check that the category name actually exist
            //check that the author that is being registerd with this bbok is in the db, is not deleted, he is active
            var bookEntity = _mapper.Map<Book>(book);
            _bookRepository.Add(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book Created successfully");
        } 
        
        public async Task<(bool, string)> UpdateBook(Guid bookId, BookUpdateDTO model)
        {
            var bookEntity =  _bookRepository.GetById(bookId);
            if (bookEntity is null) return (false, "Book not found");
            _mapper.Map(model, bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
        }
    }
}

