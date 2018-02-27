using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Service.Projects.Dto;

namespace Tasks.Service.Projects
{
    public class ProjectDtoMap : Profile
    {
        public ProjectDtoMap()
        {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
        }
    }
}