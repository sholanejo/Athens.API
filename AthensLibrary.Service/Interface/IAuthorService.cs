using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        Task<Author> UpdateAuthor();
        void Create(string name, string email );
        Author GetById(Guid id);
        IEnumerable<Author> GetAuthorByName(string name);
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Author> GetAuthorsByEmail(string email);

    }
}
