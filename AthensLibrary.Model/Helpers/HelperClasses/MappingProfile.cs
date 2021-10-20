using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AutoMapper;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<LibraryUser, LibraryUserCreationDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<Book, BookCreationDTO>();
            CreateMap<BookUpdateDTO, Book>();
        }
    }
}
