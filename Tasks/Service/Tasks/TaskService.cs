using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tasks.Models.Core;
using Tasks.Models.DomainModels;
using Tasks.Service.Tasks.Dto;

namespace Tasks.Service.Tasks
{
    public class TaskService: ITaskService
    {
        private readonly ISpecificationRepository<Task, int> taskRepository;
        public TaskService(ISpecificationRepository<Task, int> taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        
        public void Delete(int taskId)
        {
            taskRepository.Remove(taskId);
        }

        public TaskDto GetTaskById(int taskId)
        {
            var task = this.taskRepository.Get(taskId);
            return Mapper.Map<Task, TaskDto>(task);
        }

        public IList<TaskDto> GetTasks()
        {
            List<Task> taskList = taskRepository.GetAll().ToList();
            List<TaskDto> taskDtoList = Mapper.Map<List<Task>, List<TaskDto>>(taskList);
            return taskDtoList;
        }

        public void Save(TaskDto taskDto)
        {
            Task task = Mapper.Map <TaskDto,Task> (taskDto);
            taskRepository.Add(task);
        }
    }
}
