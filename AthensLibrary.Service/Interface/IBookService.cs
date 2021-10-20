using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IBookService
    {
         Task<Book> BorrowBook();
         Book CreateBook(Book book);
         Task<IEnumerable<Book>> GetAllBooks();
         Task<IEnumerable<Book>> GetAllBooksByAnAuthor();
         Task<IEnumerable<Book>> GetAllBooksByCategory();
         Task<IEnumerable<Book>> GetAllBooksByIsbn();
         Task<IEnumerable<Book>> GetAllBooksByYear();
         Task<(bool, string)> UpdateBook(Guid bookId, BookUpdateDTO model);
         Task<Book> ReturnBook();

    }
}
