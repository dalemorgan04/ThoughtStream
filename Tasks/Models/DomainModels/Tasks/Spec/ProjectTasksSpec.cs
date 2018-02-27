using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Tasks.Spec
{
    public class ProjectTasksSpec : ExpressionSpecificationBase<Task>
    {
        private readonly Project project;
        public ProjectTasksSpec(Project project)
        {
            this.project = project;
        }

        public override Expression<Func<Task, bool>> SpecExpression
        {
            get { return task => task.Project == this.project; }
        }
    }
}