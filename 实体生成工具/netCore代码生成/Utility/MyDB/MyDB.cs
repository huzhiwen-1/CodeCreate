using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;            
using System.Xml;      
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web;       

namespace  Utility.Data
{
 

    public class MyDB
    {
        public static string GetSqlConnectionString()
        {          
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        /// <summary>
        /// 执行SQL，此方法会抛出异常
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static Int32 ExecuteSQL(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.CommandTimeout = 0;
                    Int32 i = cmd.ExecuteNonQuery();
                    conn.Close();
                    return i;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行SQL，此方法不会抛出异常，如果执行SQL时发生异常，返回 -1
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static Int32 ExecSQL(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.CommandTimeout = 0;
                    Int32 i = cmd.ExecuteNonQuery();
                    conn.Close();
                    return i == -1 ? 0 : i;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return -1;
                }
            }
        }

        /// <summary>
        /// 执行SQL获取字符串，此方法不会抛出异常，如果执行SQL时发生异常，返回 空字符串
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static string GetDataItemString(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.CommandTimeout = 0;
                    object objRtn = cmd.ExecuteScalar();
                    conn.Close();
                    return DBNull.Value.Equals(objRtn) ? String.Empty : objRtn.ToString();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return "";
                }
            }
        }

        /// <summary>
        /// 执行SQL获取Int类型数据，此方法不会抛出异常，如果执行SQL时发生异常，返回 0
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static Int32 GetDataItemInt(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    object objRtn = cmd.ExecuteScalar();
                    cmd.CommandTimeout = 0;
                    conn.Close();
                    return DBNull.Value.Equals(objRtn) ? 0 : (Int32)objRtn;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行SQL获取Int类型数据，此方法不会抛出异常，如果执行SQL时发生异常，返回 0
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.CommandTimeout = 0;
                    DataTable dtRtn = new DataTable();
                    SqlDataAdapter ada = new SqlDataAdapter(cmd);
                    ada.Fill(dtRtn);
                    conn.Close();
                    return dtRtn;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return null;
                }
            }
        }
    }
}

          