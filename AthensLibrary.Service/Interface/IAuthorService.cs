using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface IAuthorService
    {
        AuthorDTO GetAuthorById(Guid id);
        Author GetAuthorByName(string name);
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorByEmail(string email);
        ReturnModel Delete(Guid id);
    }
}
