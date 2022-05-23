using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIT_WebApp.DAL
{
    public class SQLHelper
    {
        private string _connection;


        public SQLHelper()
        {
            _connection = ConfigurationManager.ConnectionStrings["BIT"].ConnectionString;
        }
        public SQLHelper(string conn)
        {
            _connection = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
        }


        public DataTable ExecuteSQL(string sql, SqlParameter[] parameters = null, bool storedProcedure = false)
        {
            DataTable dataTable = new DataTable();

            // set the connection string
            SqlConnection dbConnection = new SqlConnection(_connection);

            // create a command object to execute the SQL
            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);

            // we must decide if the SqlCommand is executing a stored proc
            if (storedProcedure == true)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null)
            {
                AddParameters(dbCommand, parameters);
            }

            try
            {
                // open the DB Connection
                dbConnection.Open();

                // execute the SQL and then close then connection
                SqlDataReader drResults = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // load the reader results into the DataTable
                dataTable.Load(drResults);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return dataTable;
        }
        private void AddParameters(SqlCommand objCommand, SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                objCommand.Parameters.Add(parameters[i]);
            }
        }
        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null, bool storedProcedure = false)
        {
            int returnValue = -1;

            SqlConnection dbConnection = new SqlConnection(_connection);

            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);

            if (storedProcedure == true)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null)
            {
                AddParameters(dbCommand, parameters);
            }

            try
            {
                dbConnection.Open();
                returnValue = dbCommand.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }
        public Object ExecuteScalar(string sql, SqlParameter[] parameters = null, bool storedProcedure = false)
        {
            Object returnValue = null;

            SqlConnection dbConnection = new SqlConnection(_connection);

            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);

            if (storedProcedure == true)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null)
            {
                AddParameters(dbCommand, parameters);
            }

            try
            {
                dbConnection.Open();
                returnValue = dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }
    }
}