using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        AuthorDTO GetAuthorById(Guid id);
        IEnumerable<Author> GetAuthorByName(string name);
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorsByEmail(string email);
        void Delete(Guid id);

    }
}
