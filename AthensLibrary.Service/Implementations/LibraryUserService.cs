using System;
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

        public async Task<(bool, string)> Delete(Guid id)
        {            
           var (EntityToDelete, message) = _libraryUserRepo.SoftDelete(id);
            if (EntityToDelete is null) return (false, message);           
            return (await _unitOfWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "Delete successful");
        }

        public async Task<(bool success, string msg)> Register(UserRegisterDTO model)
        {
            //should a person get a token immediately after registering or they will need to login!! 
            var (success, message, Id) = await CreateUserAsync(model, Roles.LibraryUser.ToString());
            if (!success) return (false, "User not created");
            var libraryUser = new LibraryUser { UserId = Id };           
            _libraryUserRepo.Add(libraryUser);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows < 1)
            {
                await deleteUser(model.Email);
                return (false, "Internal Db error, registration failed");
            }
            return (true, "Registration successfully");
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
