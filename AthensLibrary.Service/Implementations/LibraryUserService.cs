﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Service.Implementations
{
    public class LibraryUserService : CustomUserManager,ILibraryUserService 
    {        
        private readonly IServiceFactory _serviceFactory;
        private readonly IRepository<LibraryUser> _libraryUserRepo;
        private readonly IRepository<User> _userRepo;

        public LibraryUserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, UserManager<User> userManager, IMapper mapper):base(userManager,mapper, unitOfWork)
        {           
            _serviceFactory = serviceFactory;
            _libraryUserRepo = _unitOfWork.GetRepository<LibraryUser>();
            _userRepo = _unitOfWork.GetRepository<User>();
        }

        public ReturnModel Delete(Guid id) =>
            _libraryUserRepo.SoftDelete(id);

        public async Task<ReturnModel> Register(UserRegisterDTO model)
        {
            //should a person get a token immediately after registering or they will need to login!! 
            var (success, message, Id) = await CreateUserAsync(model, Roles.LibraryUser.ToString());
            if (!success) return new ReturnModel (false, "User not created");
            var libraryUser = new LibraryUser { UserId = Id };           
            _libraryUserRepo.Add(libraryUser);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows < 1)
            {
                await deleteUser(model.Email);
                return new ReturnModel(false, "Internal Db error, registration failed");
            }
            return new ReturnModel(true, "Registration successfully");
        }

        public IEnumerable<LibraryUser> GetAllLibraryUsers()
        {
            return _libraryUserRepo.GetAll().ToList();
        }        

        public LibraryUserDTO GetLibraryUserByName(string name)
        {          
            var user = _userRepo.GetSingleByCondition(a => a.FullName == name);
            var libraryUser = _libraryUserRepo.GetByCondition(a => a.UserId == user.Id).SingleOrDefault();
            var libraryUserToReturn= _mapper.Map<LibraryUserDTO>(libraryUser);
            return libraryUserToReturn;
        }

        public LibraryUserDTO GetLibraryUserById(Guid id)
        {
            var libraryUser = _libraryUserRepo.GetById(id);
            var libraryUserToReturn = _mapper.Map<LibraryUserDTO>(libraryUser);
            return libraryUserToReturn;
        }     
    }
}
