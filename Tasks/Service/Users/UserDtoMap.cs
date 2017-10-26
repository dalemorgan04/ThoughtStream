using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Service.Users.Dto;

namespace Tasks.Service.Users
{
    public class UserDtoMap : Profile
    {
        public UserDtoMap()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}