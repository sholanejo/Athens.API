using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public Author GetAuthorsByEmail(string email)
        {
            var author = _authorRepository.GetSingleByCondition(a => a.User.Email == email);
            return author;
        }

        

        public async Task<Author>  GetAuthorByName(string name)
        {
            var userManager = _serviceFactory.GetServices<UserManager<User>>();
            var user = (await userManager.FindByNameAsync(name));
            var author = _authorRepository.GetSingleByCondition(a => a.UserId == user.Id);
            return author;
        }

        public AuthorDTO GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetById(id);
            var authorM = _mapper.Map<AuthorDTO>(author);
            return authorM;
        }        

        public void Delete(Guid id)
        {
            _authorRepository.SoftDelete(id);
            _unitofWork.SaveChanges();
        }
    }
}
