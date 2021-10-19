using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AutoMapper;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibraryUser, LibraryUserCreationDTO>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}
