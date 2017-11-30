using System.Collections.Generic;
using System.Linq;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Models.DomainModels.Projects.Spec;
using Tasks.Repository.Core;
using Tasks.Service.Projects.Dto;

namespace Tasks.Service.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly ISpecificationRepository<Project, int> projectRepository;
        public ProjectService(ISpecificationRepository<Project, int> projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public List<ProjectDto> GetRootProjects()
        {
            List<Project> rootProjects = projectRepository
                                                .Find( new ProjectRootSpec())                                                
                                                .ToList();
            List<ProjectDto> projectList = new List<ProjectDto>();
            
            foreach (Project project in rootProjects)
            {
                //Overview
                ProjectDto projectDto = new ProjectDto()
                {
                    Id = project.Id,
                    Description = project.Description,
                    Items = new List<ProjectItemDto>()
                };                
                
                //Add habits, tasks, child projects as items and seen as equal

                //Habits
                foreach (HabitGoal habitGoal in project.HabitGoals)
                {
                    ProjectItemDto itemDto = new ProjectItemDto()
                    {
                            Id = habitGoal.Id,
                            Description = habitGoal.Description,
                            Type = "Habit"
                        };
                    projectDto.Items.Add(itemDto);
                }

                //Tasks
                foreach (Task task in project.Tasks)
                {
                    ProjectItemDto itemDto = new ProjectItemDto()
                    {
                        Id = task.Id,
                        Description = task.Description,
                        Type = "Task"
                    };
                    projectDto.Items.Add(itemDto);
                }

                //Child Projects
                foreach (Project childProject in GetChildProjects(project))
                {
                    ProjectItemDto itemDto = new ProjectItemDto()
                    {
                        Id = childProject.Id,
                        Description = childProject.Description,
                        Type = "Sub-Project"
                    };
                    projectDto.Items.Add(itemDto);
                }

                projectList.Add(projectDto);
            }            

            return projectList;
        }
        private List<Project> GetChildProjects(Project project)
        {
            return this.projectRepository
                                .Find( new ProjectChildrenSpec(project))
                                .ToList();
        }
    }
}
