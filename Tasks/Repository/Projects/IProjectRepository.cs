using System.Data;
using System.Web.UI.WebControls;

namespace Tasks.Repository.Projects
{
    public interface IProjectRepository
    {
        void CreateProject(int parentProjectId, int projectId);
        void RemoveProject(int projectId);
        void MoveProject(int fromProjectId, int toParentId);
        DataTable GetProjectDescendants(int projectId = 0, int getNoLevels = 1);
    }
}