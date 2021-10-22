using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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
            //check that the category name actually exist
            //check that the author that is being registerd with this bbok is in the db, is not deleted, he is active
            var bookEntity = _mapper.Map<Book>(book);
            _bookRepository.Add(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book Created successfully");
        }

        public async Task<(bool, string)> CreateListOfBooks(IEnumerable<BookCreationDTO> books)
        {
            //check that the category name actually exist
            //check that the author that is being registerd with this bbok is in the db, is not deleted, he is active
            var bookEntity = _mapper.Map<IEnumerable<Book>>(books);           
            _bookRepository.AddRange(bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book Created successfully");
        }

        public async Task<(bool, string)> UpdateBook(Guid bookId, BookUpdateDTO model)
        {
            var bookEntity =  _bookRepository.GetById(bookId);
            if (bookEntity is null) return (false, "Book not found");
            _mapper.Map(model, bookEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
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

        public IEnumerable<Book> GetAllBooksByAnAuthor(Guid authorId, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.AuthorId == authorId).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public IEnumerable<Book> GetAllBooksByAnAuthor(string identifier, BookParameters bookParameters)
        {
            var user = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.FullName == identifier || a.Email == identifier || a.Id == identifier);
            var author = _unitOfWork.GetRepository<Author>().GetSingleByCondition(a => a.UserId == user.Id);
            var books = GetAllBooksByAnAuthor(author.Id, bookParameters).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public IEnumerable<Book> GetAllBooksInACategory(string categoryName, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.CategoryName.ToLower() == categoryName.ToLower()).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);

        }

        public IEnumerable<Book> GetAllBooksPublishedInAYear(int publishYear, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.PublicationYear.Year == publishYear).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public IEnumerable<Book> GetBooksByTitle(string bookTitle, BookParameters bookParameters)
        {
            var books = _unitOfWork.GetRepository<Book>().GetByCondition(a => a.Title == bookTitle).OrderBy(a => a.Title);
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<(bool success, string msg)> CheckOutABook(string borrowerId, CheckOutABookDTO model)
        {
            var _userManager = _serviceFactory.GetServices<UserManager<User>>(); 
            var libraryUser = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.BorrowerId == borrowerId);
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            var book = _unitOfWork.GetRepository<Book>().GetSingleByCondition(b => b.ID == model.BookId);
            if (libraryUser is null) return (false, "User not found");
            if (!libraryUser.IsActive) return (false, "User account is not Active, please contact AthensLibrary admin for more info!!");
            if (libraryUser.BorrowCount++ > maxCheckoutValue) return (false, "Maximum number of books that can be checkedout has been reached!\nReturn a book and try again!");
            if (book.IsDeleted) return (false, "Book unavailable");
            if (borrowDetailRepo.Any(a => a.BorrowerId == borrowerId && a.BookId == model.BookId)) return (false, "You have already checked out this book");
            libraryUser.BorrowCount++;
            var AddBCresult = await _userManager.UpdateAsync(libraryUser);
            if (!AddBCresult.Succeeded) return (false, "User borrow count update failed!! Checkout failed");
            var borrowDETAIL = new BorrowDetail
            {
                BookId = model.BookId,
                BorrowedOn = DateTime.Now,
                BorrowerId = borrowerId,
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
            };
            borrowDetailRepo?.Add(borrowDETAIL);
            book.CurrentBookCount--;
            var affectedRow = await _unitOfWork.SaveChangesAsync();
            if (affectedRow < 1)
            {
                libraryUser.BorrowCount--;
                var reduceBCresult = await _userManager.UpdateAsync(libraryUser);
                return (false, "Internal DB error, Checkout failed!!");
            }
            return (true, "Checkout succesful");
        }

        public async Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId)
        {
            //what if am trying to return a book that has been deleted
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            var borrowDetailToUpdate = borrowDetailRepo.GetById(borrowDetailId);
            var book = _unitOfWork.GetRepository<Book>().GetSingleByCondition(b => b.ID == borrowDetailToUpdate.BookId);
            if (borrowDetailToUpdate is null) return (false, "Borrow Detail not found");
            borrowDetailToUpdate.ReturnDate = DateTime.Now;           
            book.CurrentBookCount++;
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book returned successfully");
        }
    }
}

