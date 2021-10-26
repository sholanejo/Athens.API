using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class CustomUserManager
    {        
        protected readonly UserManager<User> _userManager;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        public CustomUserManager(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {            
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        protected async Task<(bool success, string message, string userId)> CreateUserAsync(UserRegisterDTO model, string role )
        {
            var userEntity = _mapper.Map<User>(model);
            userEntity.UserName = model.Email;
            userEntity.IsActive = true;
            userEntity.BorrowerId = RandomItemGenerators.GenerateBorrowerId();
            await _userManager.UpdateAsync(userEntity);
            var createUserResult = await _userManager.CreateAsync(userEntity, model.Password);
            if (!createUserResult.Succeeded) return (false, "Registration failed, User not created!!", null);
            var addRoleResult = await _userManager.AddToRoleAsync(userEntity, role);
            if (!addRoleResult.Succeeded)
            {
                await deleteUser(model.Email);
                return (false, "Role Add failed", null); //delete created user!
            }
            return (true, "User Created", userEntity.Id);
        }
        protected async Task deleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
        }      
    }
}
