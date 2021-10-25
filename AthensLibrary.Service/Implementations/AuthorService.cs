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

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAll().ToList();
        }

        public Author GetAuthorByEmail(string email)
        {
            var author = _authorRepository.GetSingleByCondition(a => a.User.Email == email);
            return author;
        }        

        public Author  GetAuthorByName(string name)
        {
            var user = _userRepository.GetSingleByCondition(a => a.FullName == name);
            var author = _authorRepository.GetSingleByCondition(a => a.UserId == user.Id);
            return author;
        }

        public AuthorDTO GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetById(id);
            var authorM = _mapper.Map<AuthorDTO>(author);
            return authorM;
        } 

        public async Task<(bool, string)> Delete(Guid id)
        {
            //Check if an entity is already deleted!
            var (EntityToDelete, message) = _authorRepository.SoftDelete(id);
            if (EntityToDelete is null) return (false, message);
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "Delete successful");
        }
    }
}
