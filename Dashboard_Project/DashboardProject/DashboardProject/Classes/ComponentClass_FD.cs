using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.DirectoryServices;
using System.Security.Principal;
using System.Web.UI.HtmlControls;
namespace ITLDashboard.Classes
{
    public class ComponentClass_FD
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


        public DataSet InsertAllHODS(string FormID, string TransactionID, string RoughtingUserID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_NEWWorkFlowTest";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                        cmd.Parameters.AddWithValue("@RoughtingUserID", RoughtingUserID.ToString());

                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "getPlantDistinct");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet getUserSequanceWise(string FormID) //string TransactionID, string RoughtingUserID
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SELECT * FROM tblEmailSequanceWise where FormID = @FormID order by Sequance asc";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "getPlantDistinct");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }
    }
}