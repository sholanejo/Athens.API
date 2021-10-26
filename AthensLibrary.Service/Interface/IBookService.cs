using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
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
        //Get
        Book GetABookByIsbn(Guid Id);
        PagedList<Book> GetAllBooks(BookParameters bookParameters);
        PagedList<Book> GetAllBooksByAnAuthor(Guid authorId, BookParameters bookParameters);
        PagedList<Book> GetAllBooksByAnAuthor(string identifier, BookParameters bookParameters);
        PagedList<Book> GetAllBooksInACategory(string categoryName, BookParameters bookParameters);
        PagedList<Book> GetAllBooksPublishedInAYear(int publishYear, BookParameters bookParameters);
        PagedList<Book> GetBooksByTitle(string bookTitle, BookParameters bookParameters);
        //post
        Task<(bool success, string msg)> CheckOutABook(Guid bookId, CheckOutABookDTO model);
        Task<(bool success, string msg)> ReturnABook(Guid borrowDetailId);
        Task<(bool, string)> UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model);
        Task<(bool success, string msg)> RequestABook(UserBookRequestDTO model);
        Task<(bool success, string msg)> RequestABookDelete(UserBookDeleteRequestDTO model, string email);
        void Delete(Guid bookId);
    }
}
