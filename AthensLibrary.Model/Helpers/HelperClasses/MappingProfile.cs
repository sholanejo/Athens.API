using System.Collections.Generic;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AutoMapper;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<LibraryUserDTO, LibraryUser>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreationDTO, Category>().ReverseMap();
            CreateMap<LibraryUser, LibraryUserCreationDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<BookCreationDTO,Book>();
            CreateMap<IEnumerable<BookCreationDTO>,IEnumerable<Book>>();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
