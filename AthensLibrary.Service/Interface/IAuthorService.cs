using AthensLibrary.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        Task<Author> Login();
        Task<Author> UpdateAuthor();
        

    }
}
