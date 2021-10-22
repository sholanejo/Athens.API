using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        Task<Author> UpdateAuthor();
        AuthorDTO GetAuthorById(Guid id);
        Task <Author> GetAuthorByName(string name);
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Author> GetAuthorsByEmail(string email);
        void Delete(Guid id);

    }
}
