using System;
using System.Data;
using System.Data.Common;

namespace Tasks.Models.Core
{
    public class DataAccess
    {
        //Don't need types. ypes are only needed for a generic data access. In this case we will only use SQL so just build this class for that
        public string ConnectionString { get; set; }

        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void ExecuteStoredProcedure(IDbCommand storedProcedure)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                storedProcedure.Connection = connection;
                try
                {
                    connection.Open();
                    storedProcedure.ExecuteNonQuery();
                }
                finally 
                {
                    connection.Close();
                }
            }   
        }

        public void ExecuteStoredProcedure(string Command, params IDataParameter[] Params)
        {
            CreateCommandAndExecuteDynamicMethod(ExecuteStoredProcedure, Command, CommandType.StoredProcedure, 60,Params);
        }

        private void CreateCommandAndExecuteDynamicMethod(Action<IDbCommand> dynamicMethod, string Command,
            CommandType commandType, int commandTimeout, params IDataParameter[] Params)
        {
            using (IDbCommand command = new System.Data.SqlClient.SqlCommand())
            {
                command.CommandText = Command;
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout;

                foreach (IDataParameter param in Params)
                {
                    command.Parameters.Add(param);
                }
                dynamicMethod.Invoke(command);
            }
        }
    }
}