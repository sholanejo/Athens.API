using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<Author> _authorRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitOfWork unitofWork, IServiceFactory serviceFactory)
        {
            _unitofWork = unitofWork;
            _authorRepository = unitofWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
        }

        public void Create(string name, string email)
        {
            var author = new Author { };

            _authorRepository.Insert(author);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAll().ToList();

        }





        public IEnumerable<Author> GetAuthorsByEmail(string email)
        {
            var authors = _authorRepository.GetByCondition(a => a.User.Email == email);
            return authors;
        }

        public IEnumerable<Author> GetAuthorByName(string name)
        {
            var authors = _authorRepository.GetByCondition(a => a.User.FullName == name);
            return authors;
        }

        public Author GetById(Guid id)
        {
            var author = _authorRepository.GetById(id);
            return author;
        }

        public Task<Author> UpdateAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
