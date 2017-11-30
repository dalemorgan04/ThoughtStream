using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Projects.Spec
{
    public class ProjectChildrenSpec : ExpressionSpecificationBase<Project>
    {
        private readonly Project project;
        public ProjectChildrenSpec(Project project)
        {
            this.project = project;
        }
        public override Expression<Func<Project, bool>> SpecExpression
        {
            get { return child => child.ParentProject == this.project; }
        }
    }
}