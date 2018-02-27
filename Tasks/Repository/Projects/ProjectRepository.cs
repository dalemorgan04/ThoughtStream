using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Repository.Core;

namespace Tasks.Repository.Projects
{
    public class ProjectRepository: DataAccess, IProjectRepository
    {
        protected readonly DataAccess dataAccess;
        private readonly TreeView projectTreeView;

        public ProjectRepository(string connectionString)
            :base(connectionString)
        {
            dataAccess = new DataAccess(connectionString);
        }


        public DataTable GetProjectDescendants(int projectId = 0, int getNoLevels = 1)
        {

            DataTable projectDataTable = null;
            try
            {
                projectDataTable =
                    this.dataAccess.ReturnDataTable
                    (
                        "usp_GetProjectDescendants",
                        CommandType.StoredProcedure,
                        new SqlParameter("@projectId", projectId),
                        new SqlParameter("@getNoLevels", getNoLevels)
                    );
            }
            catch
            {
                throw;
            }
            return projectDataTable;
        }

        public void CreateProject(int parentProjectId, int projectId)
        {
            try
            {
                dataAccess.ExecuteStoredProcedure(
                    "udf_CreateProject",
                    new SqlParameter("@parentProjectId", parentProjectId),
                    new SqlParameter("@projectId", projectId)
                );
            }
            catch
            {
                throw;
            }
        }

        public void RemoveProject(int projectId)
        {
            try
            {
                dataAccess.ExecuteStoredProcedure(
                    "udf_RemoveProject",
                    new SqlParameter("@projectId", projectId)
                );
            }
            catch
            {
                throw;
            }
        }

        public void MoveProject(int fromProjectId, int toParentId)
        {
            try
            {
                dataAccess.ExecuteStoredProcedure(
                    "udf_MoveProject",
                    new SqlParameter("@fromProjectId", fromProjectId),
                    new SqlParameter("@toProjectId", toParentId)
                );
            }
            catch
            {
                throw;
            }
        }
    }
}