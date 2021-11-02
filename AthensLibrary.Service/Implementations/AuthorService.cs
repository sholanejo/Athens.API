using System;
using System.Collections.Generic;
using System.Linq;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;

namespace AthensLibrary.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = unitOfWork.GetRepository<Author>();
            _userRepository = unitOfWork.GetRepository<User>();
            _serviceFactory = serviceFactory;
            _mapper = mapper;
        }        

        public IEnumerable<Author> GetAllAuthors() =>
             _authorRepository.GetAll().ToList();
        

        public Author GetAuthorByEmail(string email) =>
             _authorRepository.GetSingleByCondition(a => a.User.Email == email);
 
        public Author GetAuthorByName(string name)
        {
            var user = _userRepository.GetSingleByCondition(a => a.FullName == name);

            return _authorRepository.GetSingleByCondition(a => a.UserId == user.Id);            
        }

        public AuthorDTO GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetById(id);

            return _mapper.Map<AuthorDTO>(author);         
        } 

        public ReturnModel Delete(Guid id) =>
            _authorRepository.SoftDelete(id);          
    }
}
