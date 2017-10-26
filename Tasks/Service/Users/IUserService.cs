using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Service.Users.Dto;

namespace Tasks.Service.Users
{
    public interface IUserService
    {
        UserDto GetUser(int id);
    }
}