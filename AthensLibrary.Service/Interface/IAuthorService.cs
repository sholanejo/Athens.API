using AthensLibrary.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        Task<Author> Login();
        Task<IEnumerable<Book>> GetAllBooksByAnAuthor();
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetAllBooksByCategory();
        Task<IEnumerable<Book>> GetAllBooksByYear();
        Task<IEnumerable<Book>> GetAllBooksByIsbn();
        Task<Author> UpdateAuthor();
        Task<Book> CreateBook();
        Task<Book> UpdateBook();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetCategoryByName();
        Task<IEnumerable<Category>> GetCategoryById();
        Task<Book> BorrowBook();
        Task<Book> ReturnBook();

    }
}
