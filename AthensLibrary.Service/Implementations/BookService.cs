using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Book> _bookRepository;       
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;
        private const int maxCheckoutValue = 10;


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
            // A person shouldnt be able to add a publish date that is in the future
            // current count at this point must always equal initial count, shouldnt be in DTO
            var bookEntity = _mapper.Map<Book>(book);
            var authorService = _serviceFactory.GetServices<IAuthorService>();
            var categoryService = _serviceFactory.GetServices<ICategoryService>();
            var author = authorService.GetAuthorById(book.AuthorId);
            var category = categoryService.GetCategoryByName(book.CategoryName);
            if (author == null || author.IsDeleted == true) return (false, $"Author with id:{book.AuthorId} does not exist, is inactive or is deleted");
            if (category == null || category.CategoryName.ToLower() != book.CategoryName.ToLower()) return (false, $"Category with name:{book.CategoryName} does not exist. Please enter a valid category name");
            _bookRepository.Add(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Book creation failed") : (true, "Book Created successfully");
        }         

        public async Task<(bool, string)> UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model)
        {
            var bookEntity = _bookRepository.GetById(bookId);
            if (bookEntity is null) return (false, $"Book with Id {bookId} not found");
            bookEntity.UpdatedAt = DateTime.Now;
            var bookToPatch = _mapper.Map<BookUpdateDTO>(bookEntity);
            model.ApplyTo(bookToPatch);
            _mapper.Map(bookToPatch, bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "Book update successfully");
        }       

        public async Task<(bool, string)> CreateListOfBooks(IEnumerable<BookCreationDTO> books)
        {
            //check that the category name actually exist
            //check that the author that is being registerd with this bbok is in the db, is not deleted, he is active
            var bookEntity = _mapper.Map<IEnumerable<Book>>(books);           
            _bookRepository.AddRange(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book Created successfully");
        }
      
        public Book GetABookByIsbn(Guid Id)
        {
            return _unitOfWork.GetRepository<Book>().GetSingleByCondition(a => a.ID == Id);
        }

        public PagedList<Book> GetAllBooks(BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetAll().OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public PagedList<Book> GetAllBooksByAnAuthor(Guid authorId, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.AuthorId == authorId).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public PagedList<Book> GetAllBooksByAnAuthor(string identifier, BookParameters bookParameters)
        {
            var user = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.FullName == identifier || a.Email == identifier || a.Id == identifier);
            var author = _unitOfWork.GetRepository<Author>().GetSingleByCondition(a => a.UserId == user.Id);
            var books = GetAllBooksByAnAuthor(author.Id, bookParameters).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public PagedList<Book> GetAllBooksInACategory(string categoryName, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.CategoryName.ToLower() == categoryName.ToLower()).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);

        }

        public PagedList<Book> GetAllBooksPublishedInAYear(int publishYear, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.PublicationYear.Year == publishYear).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public PagedList<Book> GetBooksByTitle(string bookTitle, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.Title == bookTitle).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<(bool success, string msg)> CheckOutABook(Guid bookId, CheckOutABookDTO model)
        {
            var _userManager = _serviceFactory.GetServices<UserManager<User>>();
            var borrower = await _userManager.FindByEmailAsync(model.Email);
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            var book = _unitOfWork.GetRepository<Book>().GetSingleByCondition(b => b.ID == bookId);
            if (borrower is null) return (false, "User not found");
            if (!borrower.IsActive) return (false, "User account is not Active, please contact AthensLibrary admin for more info!!");
            if (borrower.BorrowCount++ > maxCheckoutValue) return (false, "Maximum number of books that can be checkedout has been reached!\nReturn a book and try again!");
            if (book.IsDeleted) return (false, "Book unavailable");
            if (borrowDetailRepo.Any(a => a.BorrowerId ==borrower.BorrowerId && a.BookId == bookId)) return (false, "You have already checked out this book");
            //borrower.BorrowCount++;
            var AddBCresult = await _userManager.UpdateAsync(borrower);
            if (!AddBCresult.Succeeded) return (false, "User borrow count update failed!! Checkout failed");
            var borrowDETAIL = new BorrowDetail
            {
                BookId = bookId,
                BorrowedOn = DateTime.Now,
                BorrowerId = borrower.BorrowerId,
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
            };
            borrowDetailRepo?.Add(borrowDETAIL);
            book.CurrentBookCount--;
            var affectedRow = await _unitOfWork.SaveChangesAsync();
            if (affectedRow < 1)
            {
                borrower.BorrowCount--;
                var reduceBCresult = await _userManager.UpdateAsync(borrower);
                return (false, "Internal DB error, Checkout failed!!");
            }
            return (true, "Checkout succesful");
        }

        public async Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId)
        {
            //use his borrowId and book id to get a borrow detail to update 

            //what if am trying to return a book that has been deleted
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            var borrowDetailToUpdate = borrowDetailRepo.GetById(borrowDetailId);
            var book = _unitOfWork.GetRepository<Book>().GetSingleByCondition(b => b.ID == borrowDetailToUpdate.BookId);
            if (borrowDetailToUpdate is null) return (false, "Borrow Detail not found");
            borrowDetailToUpdate.ReturnDate = DateTime.Now;
            borrowDetailToUpdate.IsDeleted = true;
            book.CurrentBookCount++;
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book returned successfully");
        }

        public void Delete(Guid bookId)
        {
            _bookRepository.SoftDelete(bookId);
            _unitOfWork.SaveChanges();
        }            

        public async Task<(bool success, string msg)> RequestABook(UserBookRequestDTO model)
        {
            var userRequest = new UserBookRequest
            {
                AuthorName = model.AuthorName,
                BookTitle = model.BookTitle,
                RequestType = RequestType.AddBookRequest.ToString()
            };
            var userBookRequestRepo = _unitOfWork.GetRepository<UserBookRequest>();
            userBookRequestRepo?.Add(userRequest);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
        }

        public async Task<(bool success, string msg)> RequestABookDelete(UserBookDeleteRequestDTO model, string email)
        {
            var _userManager = _serviceFactory.GetServices<UserManager<User>>();
            var userEntity = await _userManager.FindByEmailAsync(email);
            var _books = _serviceFactory.GetServices<IBookService>().GetAllBooksByAnAuthor(email, new Model.RequestFeatures.BookParameters()).ToList();
            var _bookrepo = _unitOfWork.GetRepository<Book>();
            var _authorrepo = _unitOfWork.GetRepository<Author>();

            if (userEntity is null) return (false, "user not found");
            if (!_books.Any(a => a.Title == model.BookTitle)) return (false, "You currently do not have any book with that title");
            var userRequest = new UserBookRequest
            {
                AuthorName = userEntity.FullName,
                BookTitle = model.BookTitle,
                RequestType = RequestType.DeleteBookRequest.ToString()
            };
            var userBookRequestRepo = _unitOfWork.GetRepository<UserBookRequest>();
            userBookRequestRepo?.Add(userRequest);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");

        }

        
    }
}

