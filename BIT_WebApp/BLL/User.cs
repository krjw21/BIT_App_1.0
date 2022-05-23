﻿using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIT_WebApp.BLL
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        private SQLHelper _db;


        public User()
        {
            _db = new SQLHelper();
        }


        public int CheckClient() // Method to check client login details
        {
            string sql = "SELECT Client_ID FROM Client WHERE Email = @Email AND Password = @Password";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@Email", DbType.String);
            objParameters[0].Value = this.Email;
            objParameters[1] = new SqlParameter("@Password", DbType.String);
            objParameters[1].Value = this.Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            return id;
        }
        public int CheckContractor() // Method to check contractor login details
        {
            {
                string sql = "SELECT Contractor_ID FROM Contractor WHERE Email = @Email AND Password = @Password";
                SqlParameter[] objParameters = new SqlParameter[2];
                objParameters[0] = new SqlParameter("@Email", DbType.String);
                objParameters[0].Value = this.Email;
                objParameters[1] = new SqlParameter("@Password", DbType.String);
                objParameters[1].Value = this.Password;
                DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

                int id = -1;
                if (dataTable.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dataTable.Rows[0][0]);
                }

                return id;
            }
        }
        public int CheckCoordinator() // Method to check coordinator login details
        {
            string sql = "SELECT Coordinator_ID FROM Coordinator WHERE Email = @Email AND Password = @Password";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@Email", DbType.String);
            objParameters[0].Value = this.Email;
            objParameters[1] = new SqlParameter("@Password", DbType.String);
            objParameters[1].Value = this.Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            return id;
        }
    }
}