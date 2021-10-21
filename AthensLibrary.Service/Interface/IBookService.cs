using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface IBookService
    {
        Task<Book> BorrowBook();
        Task<(bool, string)> CreateBook(BookCreationDTO book);        
        Task<(bool, string)> UpdateBook(Guid bookId, JsonPatchDocument<BookUpdateDTO> model);        
    }
}
