using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Enum;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Models.DomainModels.Tasks.Spec;
using Tasks.Repository.Core;
using Tasks.Repository.Projects;
using Tasks.Service.Projects.Dto;

namespace Tasks.Service.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly ISpecificationRepository<Project, int> projectRepository;
        private readonly IProjectRepository projectSqlRepository;
        private readonly ISpecificationRepository<Task, int> taskRepository;
        private readonly IUnitOfWork unitOfWork;
        public ProjectService(
            ISpecificationRepository<Project, int> projectRepository,
            IProjectRepository projectSqlRepository,
            ISpecificationRepository<Task, int> taskRepository,
            IUnitOfWork unitOfWork)
        {
            this.projectRepository = projectRepository;
            this.projectSqlRepository = projectSqlRepository;
            this.taskRepository = taskRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<ProjectWithItemsDto> GetRootProjects()
        {
            List<ProjectWithItemsDto> rootProjects = new List<ProjectWithItemsDto>();
            List<int> rootProjectIds = new List<int>();
            DataTable dataTable = projectSqlRepository.GetProjectDescendants(0, 1);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                rootProjectIds.Add(Convert.ToInt32(dataTable.Rows[i]["ProjectId"].ToString()));
            }
            foreach (int rootProjectId in rootProjectIds)
            {
                rootProjects.Add(GetProject(rootProjectId));
            }
            return rootProjects;
        }

        public ProjectWithItemsDto GetProject(int projectId)
        {
            Project rootProject = projectRepository.Get(projectId);
            ProjectWithItemsDto projectWithItemsDto = new ProjectWithItemsDto()
            {
                Id = rootProject.Id,
                Description = rootProject.Description
            };

            

            //Tasks
            List<Task> tasks = taskRepository.Find(new ProjectTasksSpec(rootProject)).ToList();
            foreach (Task task in tasks)
            {
                ProjectItemDto projectItem = new ProjectItemDto()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Type = ItemType.Task
                };
                projectWithItemsDto.Items.Add(projectItem);
            }

            //Habits

            //Sub Projects
            DataTable table = projectSqlRepository.GetProjectDescendants(projectId, 1);
            List<int> subProjectIds = new List<int>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                subProjectIds.Add(Convert.ToInt32(table.Rows[i]["ProjectId"].ToString()));
            }

            List<Project> subProjects = projectRepository.Get(subProjectIds).ToList();
            foreach (Project subProject in subProjects)
            {
                ProjectWithItemsDto project = new ProjectWithItemsDto();
                project = GetProject(subProject.Id);
                projectWithItemsDto.SubProjects.Add(project);
            }

            return projectWithItemsDto;
        }

        public void AppendTask( int projectId, int taskId)
        {
            Project project = projectRepository.Get(projectId);
            Task task = taskRepository.Get(taskId);
            //task.Update(project);
            unitOfWork.Commit();
        }

        public void Save(ProjectWithItemsDto projectWithItemsDto)
        {
            throw new System.NotImplementedException();
        }

        public void Save(ProjectDto projectDto)
        {
            Project project = Mapper.Map<ProjectDto, Project>(projectDto);
            projectRepository.Add(project);
            unitOfWork.Commit();
        }
    }
}
