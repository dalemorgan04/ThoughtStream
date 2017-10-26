using System.Collections.Generic;
using Tasks.Service.Tasks.Dto;

namespace Tasks.Service.Tasks
{
    public interface ITaskService
    {
        IList<TaskDto> GetTasks();
        TaskDto GetTaskById(int taskId);        
        void Save(TaskDto taskDto);
        void Delete(int taskId);
        //bool TaskExists(int taskId);
        //To do - write a get task with a search filter
    }
}