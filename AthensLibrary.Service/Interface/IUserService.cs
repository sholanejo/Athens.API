using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Service.Interface
{
    public interface IUserService 
    {       
        void EnrollUser(UserRegisterDTO model, string role);
        Task<(bool, string)> UpdateUser(UserUpdateDTO model);
        Task<(bool, string)> UpdateUser(string userId, UserUpdateDTO model);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetAllBooksByAnAuthor(Guid authorId);
        IEnumerable<Book> GetAllBooksByAnAuthor(string authorName);
        IEnumerable<Book> GetAllBooksInACategory(string categoryName);   
        IEnumerable<Book> GetAllBooksPublishedInAYear(int year);
        Book GetABookByIsbn(Guid Id);
        IEnumerable<Book> GetBooksByTitle(string bookTitle);
        Task<(bool success, string msg)> CheckOutABook(string BorrowerId, CheckOutABookDTO model);
        Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId);
    }
}
