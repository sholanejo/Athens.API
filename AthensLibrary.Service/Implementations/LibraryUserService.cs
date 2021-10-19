using System;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Service.Implementations
{
    public class LibraryUserService : ILibraryUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public LibraryUserService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
       

        public async Task<(bool success, string msg)> Register(UserRegisterDTO model)
        {
            //should a person get a token immediately after registering or they will need to login!! 
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                CreatedBy = "Shola nejo",
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.Now,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender
            };
            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded) return (false, "Registration failed, User not created!!");
            var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.LibraryUser.ToString());
            if (!addRoleResult.Succeeded)
            {
                await deleteUser(model.Email);
                return (false, "Role Add failed"); //delete created user!
            }
            var libraryUser = new LibraryUser
            {
                BorrowerId = RandomItemGenerators.GenerateBorrowerId(),
                UserId = user.Id
            };
            var repo = _unitOfWork.GetRepository<LibraryUser>();
            repo.Add(libraryUser);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows < 1)
            {
                await deleteUser(model.Email);
                return (false, "Internal Db error, registration failed");
            }
            return (true, "Registration successfully");
        }
        private async Task deleteUser (string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);           
        }
    }
}
