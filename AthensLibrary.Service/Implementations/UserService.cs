using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Service.Implementations
{
    class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory; 

        public UserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
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
       
        public void UpdateUser(UserUpdateEmailDTO model)
        {
            throw new NotImplementedException();
        } 
        public void EnrollUser(UserRegisterDTO model, string role)
        {
            throw new NotImplementedException();
        }
    }
}
