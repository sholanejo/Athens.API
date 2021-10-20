using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private const int maxCheckoutValue = 10;        

        public UserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<(bool success, string msg)> CheckOutABook(string borrowerId, CheckOutABookDTO model)
        {             
            var libraryUser = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.BorrowerId == borrowerId);
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            if (libraryUser is null) return (false, "User not found");
            if (!libraryUser.IsActive) return (false, "User account is not Active, please contact AthensLibrary admin for more info!!");
            if (libraryUser.BorrowCount++ > maxCheckoutValue) return (false, "Maximum number of books that can be checkedout has been reached!\nReturn a book and try again!");
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
            var borrowDetailRepo = _unitOfWork.GetRepository<BorrowDetail>();
            var BorrowDetailToUpdate = borrowDetailRepo.GetById(borrowDetailId);
            if (BorrowDetailToUpdate is null) return (false, "Borrow Detail not found");
            BorrowDetailToUpdate.ReturnDate = DateTime.Now;
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, return book failed") : (true, "Book returned successfully");
        }
        

        public void EnrollUser(UserRegisterDTO model, string role)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Book> GetAllBooksByAnAuthor(string authorName)
        {
           var user = _unitOfWork.GetRepository<User>().GetSingleByCondition(a => a.FullName == authorName);
           var author = _unitOfWork.GetRepository<Author>().GetSingleByCondition(a => a.UserId == user.Id);
           return _unitOfWork.GetRepository<Book>().GetByCondition(a => a.AuthorId == author.Id).OrderBy(a => a.CreatedAt);
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
        public async Task<(bool, string)> UpdateUser(string email, UserUpdateDTO model)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity is null) return (false, "user not found");
            _mapper.Map(model, userEntity);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "update successfully");
        }

        public Task<(bool, string)> UpdateUser(UserUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
