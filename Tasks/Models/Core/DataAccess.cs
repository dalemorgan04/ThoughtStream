using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Tasks.Models.Core
{
    public class DataAccess
    {
        //Don't need types. ypes are only needed for a generic data access. In this case we will only use SQL so just build this class for that
        public string ConnectionString { get; set; }
        public void ExecuteStoredProcedure(string Command, params SqlParameter[] Params)
        {
            CreateCommandAndExecuteDynamicMethod(ExecuteStoredProcedure, Command, CommandType.StoredProcedure, 60, Params);
        }

        public DataTable ReturnDataTable(string Command, CommandType commandType, params SqlParameter[] Params)
        {
            return CreateCommandAndExecuteDynamicMethod<DataTable>(ReturnDataTable, Command, commandType, 60, Params);
        }
        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void ExecuteStoredProcedure(IDbCommand storedProcedure)
        {
            using (IDbConnection connection = new SqlConnection())
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
        public DataSet ReturnDataSet(IDbCommand storedProcedure)
        {
            DataSet dataSet = new DataSet();
            IDbDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = storedProcedure;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                storedProcedure.Connection = conn;

                try
                {
                    adapter.Fill(dataSet);
                }
                finally
                {
                    conn.Close();
                }
            }

            return dataSet;
        }

        public DataTable ReturnDataTable(IDbCommand storedProcedure)
        {
            DataSet dataset = new DataSet();
            dataset = ReturnDataSet(storedProcedure);
            DataTable dataTable = null;

            if (dataset.Tables.Count > 0)
            {
                dataTable = dataset.Tables[0];
            }

            return dataTable;
        }


        private void CreateCommandAndExecuteDynamicMethod(Action<IDbCommand> dynamicMethod, string Command,
            CommandType commandType, int commandTimeout, params IDataParameter[] Params)
        {
            using (IDbCommand command = new SqlCommand())
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

        private TData CreateCommandAndExecuteDynamicMethod<TData>
            (Func<IDbCommand, TData> dynamicMethod, string Command, CommandType commandType, int commandTimeout, params IDataParameter[] Params)
            where TData : class
        {
            TData data = null;
            using (IDbCommand comm = new SqlCommand())
            {
                comm.CommandText = Command;
                comm.CommandType = commandType;
                comm.CommandTimeout = commandTimeout;

                for (int i = 0; i < Params.Length; i++)
                {
                    comm.Parameters.Add(Params[i]);
                }

                data = dynamicMethod.Invoke(comm) as TData;
            }
            return data;
        }
    }
}