using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Tasks.Spec
{
    public class TaskOnDaySpec : ExpressionSpecificationBase<Task>
    {
        private readonly DateTime date;
        public TaskOnDaySpec(DateTime date) 
        {
            this.date = date;
        }
        public override Expression<Func<Task, bool>> SpecExpression
        {
            get { return task => task.TimeFrameDateTime.Day == date.Day ; }
        }
    }
}