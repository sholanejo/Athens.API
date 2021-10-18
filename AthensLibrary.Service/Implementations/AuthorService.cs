using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IRepository<Author> _authorRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitofWork unitofWork, IServiceFactory serviceFactory)
        {
            _unitofWork = unitofWork;
            _authorRepository = unitofWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
        }

        public void Create(string name, string email)
        {
            var author = new Author {  Name = name, Email = email, };

            _authorRepository.Insert(author);
        }

        public IEnumerable<Author> GetAll()
        {
            var authors = _authorRepository.GetByCondition();
            return authors;
        }

        public IEnumerable<Author> GetAuthorsByEmail(string email)
        {
            var authors = _authorRepository.GetByCondition(a => a.Email == email);
            return authors;
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            var authors = _authorRepository.GetByCondition(a => a.Name == name);
            return authors;
        }

        public Author GetById(Guid id)
        {
            var author = _authorRepository.GetById(id);

            return author;
        }

        public Task<Author> Login()
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
