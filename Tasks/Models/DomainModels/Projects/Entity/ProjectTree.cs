using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.Models.DomainModels.Projects.Entity
{
    public class ProjectTree
    {
        public ProjectTree()
        {
            ProjectNodeList = new List<ProjectNode>();
        }

        public IList<ProjectNode> ProjectNodeList { get; set; }
        
    }
}