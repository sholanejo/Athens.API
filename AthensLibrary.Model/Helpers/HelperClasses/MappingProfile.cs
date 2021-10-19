﻿using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AutoMapper;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
