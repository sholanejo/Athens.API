using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface IBookService
    {
        Task<Book> BorrowBook();
        Task<(bool, string)> CreateBook(BookCreationDTO book);
        Task<(bool, string)> CreateListOfBooks(IEnumerable<BookCreationDTO> books);
        Task<(bool, string)> UpdateBook(Guid bookId, BookUpdateDTO model);
        //Get
        Book GetABookByIsbn(Guid Id);
        PagedList<Book> GetAllBooks(BookParameters bookParameters);
        IEnumerable<Book> GetAllBooksByAnAuthor(Guid authorId, BookParameters bookParameters);
        IEnumerable<Book> GetAllBooksByAnAuthor(string identifier, BookParameters bookParameters);
        IEnumerable<Book> GetAllBooksInACategory(string categoryName, BookParameters bookParameters);
        IEnumerable<Book> GetAllBooksPublishedInAYear(int publishYear, BookParameters bookParameters);
        IEnumerable<Book> GetBooksByTitle(string bookTitle, BookParameters bookParameters);
        //post
        Task<(bool success, string msg)> CheckOutABook(string BorrowerId, CheckOutABookDTO model);
        Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId);
        Task<(bool, string)> UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model);
        Task<(bool success, string msg)> RequestABook(UserBookRequestDTO model);
        Task<(bool success, string msg)> RequestABookDelete(UserBookDeleteRequestDTO model, string email);
        void Delete(Guid bookId);
    }
}
