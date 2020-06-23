using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace AttendanceManagementSystem.AppCode
{
    public class Global
    {
        public SqlConnection conn = null;
        public SqlCommand cmd = null;
        public DataTable dt = null;

        public object Session { get; private set; }

        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["attenms"].ToString();
        }
        public SqlConnection GetConnection()
        {
            conn = new SqlConnection(GetConnectionString());
            conn.Open();
            return conn;
        }

        public void CloseSqlConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public DataTable GetData(string query)
        {
            GetConnection();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public int ExecuteNonQuery(string query)
        {
            GetConnection();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public string ExecuteNonQueryForSignup(string query, string name, string email, string mobile, string password)
        {
            string message;
            GetConnection();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@USERNAME", name);
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@MOBILE", mobile);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 200);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                message = (string)cmd.Parameters["@MESSAGE"].Value;
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        
    }
}