using System;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILibraryUserService _LibraryUserService;
        private readonly IRepository<User> userRepository;

        public LibraryUserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper):base(userManager,mapper)
        {
            _unitOfWork = unitOfWork;           
        }

        

        public async Task<(bool success, string msg)> Register(UserRegisterDTO model)
        {
            //should a person get a token immediately after registering or they will need to login!! 
            var (success, message, Id) = await CreateUserAsync(model);
            if (!success) return (false, "User not created");
            var libraryUser = new LibraryUser
            {
                BorrowerId = RandomItemGenerators.GenerateBorrowerId(),
                UserId = Id
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

       
    }
}
