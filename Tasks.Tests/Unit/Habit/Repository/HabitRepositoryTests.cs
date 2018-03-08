using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Repository.Habits;

namespace Tasks.Tests.Unit.Habit.Repository
{
    [TestClass]
    public class HabitRepositoryTests
    {
        [TestMethod]
        [TestCategory("Sql")]
        public void GetHabitOccurrencesBetweenDates_InputDates_GetListofHabits()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            HabitRepository repo = new HabitRepository(connectionString);
            DateTime dateFrom = new DateTime(2017,11,1);
            DateTime dateTo = new DateTime(2017,11,30);
            var results = repo.GetHabitOccurrences(dateFrom, dateTo);
        }

        [TestMethod]
        [TestCategory("Sql")]
        public void GetHabitOccurrencesOnDate_InputDate_GetListofHabits()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            HabitRepository repo = new HabitRepository(connectionString);
            DateTime date = new DateTime(2017, 11, 1);            
            var results = repo.GetHabitOccurrences(date) ;            
        }
    }
}
