using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Implementations
{
    public class UserService : CustomUserManager,IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;        
        private const int maxCheckoutValue = 10;        

        public UserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, UserManager<User> userManager, IMapper mapper) : base (userManager,mapper)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;           
        }

        public async Task<(bool success, string msg)> CheckOutABook(string borrowerId, CheckOutABookDTO model)
        {             
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
            //book.IsDeleted = false;
            book.CurrentBookCount++;
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book returned successfully");
        }

        public async Task<(bool, string)> EnrollAuthor(UserRegisterDTO model)
        {
            var (success, message, Id) = await CreateUserAsync(model);
            if (!success) return (false, "User not created");
            var author = new Author
            {
                IsActive = true,
                IsDeleted = false,                
                BorrowerId = RandomItemGenerators.GenerateBorrowerId(),
                UserId = Id
            };
            var repo = _unitOfWork.GetRepository<Author>();
            repo.Add(author);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows < 1)
            {
                await deleteUser(model.Email);
                return (false, "Internal Db error, registration failed");
            }
            return (true, "Author added successfully");
        }

        public Book GetABookByIsbn(Guid Id)
        {
           return _unitOfWork.GetRepository<Book>().GetSingleByCondition(a => a.ID == Id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
           return  _unitOfWork.GetRepository<Book>().GetAll();
        }

        public IEnumerable<Book> GetAllBooksByAnAuthor(Guid authorId)
        {
           return _unitOfWork.GetRepository<Book>().GetByCondition(a => a.AuthorId == authorId).OrderBy(a => a.CreatedAt);
        }

        public IEnumerable<Book> GetAllBooksByAnAuthor(string identifier)
        {
           var user = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.FullName == identifier || a.Email == identifier ||  a.Id == identifier);
           var author = _unitOfWork.GetRepository<Author>().GetSingleByCondition(a => a.UserId == user.Id);
            return GetAllBooksByAnAuthor(author.Id);
        }

        public IEnumerable<Book> GetAllBooksInACategory(string categoryName)
        {
           return  _unitOfWork.GetRepository<Book>().GetByCondition(a => a.CategoryName.ToLower() == categoryName.ToLower()).OrderBy(a => a.CreatedAt);
        }
        
        public IEnumerable<Book> GetAllBooksPublishedInAYear(int publishYear)
        {
            return _unitOfWork.GetRepository<Book>().GetByCondition(a => a.PublicationYear.Year == publishYear).OrderBy(a => a.CreatedAt);
        }

        public IEnumerable<Book> GetBooksByTitle(string bookTitle) 
        {
            return _unitOfWork.GetRepository<Book>().GetByCondition(a => a.Title == bookTitle).OrderBy(a => a.CreatedAt);
        }
        public async Task<(bool, string)> UpdateUser(string identifier, JsonPatchDocument<UserUpdateDTO> model)
        {
            var userEntity = await _userManager.FindByEmailAsync(identifier);
            userEntity ??= await _userManager.FindByIdAsync(identifier);
            if (userEntity is null) return (false, "user not found");
            var userToPatch = _mapper.Map<UserUpdateDTO>(userEntity);
            model.ApplyTo(userToPatch);
            _mapper.Map(userToPatch, userEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
        }
    }
}
