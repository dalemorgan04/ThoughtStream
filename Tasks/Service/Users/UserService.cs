using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.Core;
using Tasks.Models.DomainModels;
using AutoMapper;
using Tasks.Service.Users.Dto;

namespace Tasks.Service.Users
{
    public class UserService : IUserService
    {
        private readonly ISpecificationRepository<User, int> userRepository;

        public UserService(
            ISpecificationRepository<User, int> userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserDto GetUser(int id)
        {
            return Mapper.Map<User, UserDto>(userRepository.Get(id));
        }
    }
}