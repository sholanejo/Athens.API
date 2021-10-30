using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface IBookService
    {       
        Task<ReturnModel> CreateBook(BookCreationDTO book);
        Task<ReturnModel> CreateListOfBooks(IEnumerable<BookCreationDTO> books);        
        //Get
        Book GetABookByIsbn(Guid Id);
        PagedList<Book> GetAllBooks(BookParameters bookParameters);
        PagedList<Book> GetAllBooksByAnAuthor(Guid authorId, BookParameters bookParameters);
        PagedList<Book> GetAllBooksByAnAuthor(string identifier, BookParameters bookParameters);
        PagedList<Book> GetAllBooksInACategory(string categoryName, BookParameters bookParameters);
        PagedList<Book> GetAllBooksPublishedInAYear(int publishYear, BookParameters bookParameters);
        PagedList<Book> GetBooksByTitle(string bookTitle, BookParameters bookParameters);
        //post
        Task<ReturnModel> CheckOutABook(Guid bookId, CheckOutABookDTO model);
        ReturnModel ReturnABook(Guid borrowDetailId);
        ReturnModel UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model);
        Task<ReturnModel> RequestABook(UserBookRequestDTO model);
        Task<ReturnModel> RequestABookDelete(UserBookDeleteRequestDTO model, string email);
        void Delete(Guid bookId);
    }
}
