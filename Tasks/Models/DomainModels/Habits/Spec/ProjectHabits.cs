using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Spec
{
    public class ProjectHabits : ExpressionSpecificationBase<Habit>
    {
        private readonly Project project;

        public ProjectHabits(Project project)
        {
            this.project = project;
        }

        public override Expression<Func<Habit, bool>> SpecExpression
        {
            get { return habit => habit.Project == this.project; }
        }
        
    }
}