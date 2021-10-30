using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Implementations
{
    public class UserService : CustomUserManager, IUserService
    {        
        private readonly IServiceFactory _serviceFactory;        
        
        public UserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, UserManager<User> userManager, IMapper mapper) : base(userManager, mapper, unitOfWork)
        {            
            _serviceFactory = serviceFactory;
        }       

        public async Task<ReturnModel> EnrollAuthor(UserRegisterDTO model)
        {
            var (success, message, Id) = await CreateUserAsync(model, Roles.Author.ToString());
            if (!success) return new ReturnModel { Success = false, Message = "User not created" };
            var author = new Author
            {               
                IsDeleted = false,               
                UserId = Id
            };
            var repo = _unitOfWork.GetRepository<Author>();
            repo.Add(author);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows < 1)
            {
                await deleteUser(model.Email);
               return new ReturnModel { Success = false, Message = "Internal Db error, registration failed" };               
            }
            return new ReturnModel { Success = true, Message = "Author added successfully" };
        }
        
        public async Task<ReturnModel> UpdateUser(string identifier, JsonPatchDocument<UserUpdateDTO> model)
        {
            var userEntity = await _userManager.FindByEmailAsync(identifier);
            userEntity ??= await _userManager.FindByIdAsync(identifier);
            if (userEntity is null) return new ReturnModel { Success = false, Message = "user not found" };
            var userToPatch = _mapper.Map<UserUpdateDTO>(userEntity);
            model.ApplyTo(userToPatch);
            _mapper.Map(userToPatch, userEntity);
            return new ReturnModel { Success = true, Message = "update successfully" };
        }      
    }
}