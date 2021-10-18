using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        Task<Author> Login();
        Task<Author> UpdateAuthor();
        void Create(string name, string email );
        Author GetById(Guid id);
        IEnumerable<Author> GetAuthorsByName(string name);
        IEnumerable<Author> GetAll();
        IEnumerable<Author> GetAuthorsByEmail(string email);

    }
}
