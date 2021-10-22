using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _authorRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitOfWork unitofWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _authorRepository = unitofWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
            _mapper = mapper;
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
            //first used the usermanger to find the user by name,
            //then find an author that has a corresponding id with user found above
            var authors = _authorRepository.GetByCondition(a => a.User.FullName == name);
            return authors;
        }

        public AuthorDTO GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetById(id);
            var authorM = _mapper.Map<AuthorDTO>(author);
            return authorM;
        }

        public Task<Author> UpdateAuthor()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            _authorRepository.SoftDelete(id);
            _unitofWork.SaveChanges();
        }
    }
}
