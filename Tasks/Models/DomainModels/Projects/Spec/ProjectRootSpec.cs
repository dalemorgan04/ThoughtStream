using System;
using System.Linq.Expressions;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Projects.Spec
{
    public class ProjectRootSpec : ExpressionSpecificationBase<Project>
    {
        public override Expression<Func<Project, bool>> SpecExpression
        {
            get
            {   return project => project.ParentProject == null;    }
        }
    }
}