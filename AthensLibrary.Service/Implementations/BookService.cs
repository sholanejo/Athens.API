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
using Microsoft.AspNetCore.JsonPatch;

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
            var bookEntity = _mapper.Map<Book>(book);
            var authorService = _serviceFactory.GetServices<IAuthorService>();
            var categoryService = _serviceFactory.GetServices<ICategoryService>();
            var author = authorService.GetAuthorById(book.AuthorId);
            var category = categoryService.GetCategoryByName(book.CategoryName);
            if (author == null || author.IsActive == false || author.IsDeleted == true) return (false, $"Author with id:{book.AuthorId} does not exist, is inactive or is deleted");
            if (category == null || category.CategoryName != book.CategoryName) return (false, $"Category with name:{book.CategoryName} does not exist. Please enter a valid category name");
            _bookRepository.Add(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Book creation failed") : (true, "Book Created successfully");
        } 
        

        public async Task<(bool, string)> UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model)
        {
            var bookEntity = _bookRepository.GetById(bookId);
            if (bookEntity is null) return (false, $"Book with Id {bookId} not found");
            var bookToPatch = _mapper.Map<BookUpdateDTO>(bookEntity);
            model.ApplyTo(bookToPatch);
            _mapper.Map(bookToPatch, bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "Book update successfully");
        }
    }
}

