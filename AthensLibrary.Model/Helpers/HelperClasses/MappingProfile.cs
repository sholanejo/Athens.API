using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AutoMapper;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using System.Collections.Generic;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreationDTO, Category>().ReverseMap();
            CreateMap<LibraryUser, LibraryUserCreationDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<BookCreationDTO,Book>();
            CreateMap<IEnumerable<BookCreationDTO>,IEnumerable<Book>>();
            CreateMap<BookUpdateDTO, Book>();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
