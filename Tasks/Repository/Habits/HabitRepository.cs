using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Tasks.Repository.Core;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository.Habits
{
    public class HabitRepository : DataAccess, IHabitRepository
    {
        protected readonly DataAccess dataAccess;

        public HabitRepository(string connectionString)
            :base(connectionString)
        {
            dataAccess = new DataAccess(connectionString);
        }

        public List<int> GetHabitOccurrences(DateTime date)
        {
            DataTable table = GetHabitOccurrencesOnDateTable(date);
            List<int> list = new List<int>();
            list = (from DataRow row in table.Rows
                    select Convert.ToInt32(row["HabitId"])).ToList();
            return list;            
        }

        public List<HabitList> GetHabitOccurrences(DateTime fromDate, DateTime toDate)
        {
            DataTable table = GetHabitOccurrencesBetweenDatesTable(fromDate, toDate);
            List<HabitList> list = new List<HabitList>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(new HabitList()
                {
                    Date = Convert.ToDateTime(table.Rows[i]["Date"].ToString()),
                    HabitId = Convert.ToInt32(table.Rows[i]["HabitId"].ToString())
                });
            }
            return list;
        }

        private DataTable GetHabitOccurrencesOnDateTable(DateTime date)
        {
            DataTable table = null;
            try
            {
                table =
                    this.dataAccess.ReturnDataTable
                        (
                            "udf_GetHabitsFromDate",
                            CommandType.StoredProcedure,
                            new SqlParameter("@date", date)                            
                        );
            }
            catch
            {
                throw;
            }
            return table;
        }

        private DataTable GetHabitOccurrencesBetweenDatesTable(DateTime fromDate, DateTime toDate)
        {
            DataTable table = null;
            try
            {
                table =
                    this.dataAccess.ReturnDataTable
                        (
                            "udf_GetHabitsFromDateRange",
                            CommandType.StoredProcedure,
                            new SqlParameter("@fromDate",fromDate),
                            new SqlParameter("@toDate", toDate)
                        );
            }
            catch
            {
                throw;
            }
            return table;
        }
    }
}