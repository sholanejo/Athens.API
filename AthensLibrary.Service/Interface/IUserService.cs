﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface IUserService 
    {
        Task<(bool, string)> EnrollAuthor(UserRegisterDTO model);
        Task<(bool, string)> UpdateUser(string identifier, JsonPatchDocument<UserUpdateDTO> model);        
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetAllBooksByAnAuthor(Guid authorId);
        IEnumerable<Book> GetAllBooksByAnAuthor(string identifier);
        IEnumerable<Book> GetAllBooksInACategory(string categoryName);   
        IEnumerable<Book> GetAllBooksPublishedInAYear(int year);
        Book GetABookByIsbn(Guid Id);
        IEnumerable<Book> GetBooksByTitle(string bookTitle);
        Task<(bool success, string msg)> CheckOutABook(string BorrowerId, CheckOutABookDTO model);
        Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId);
    }
}
