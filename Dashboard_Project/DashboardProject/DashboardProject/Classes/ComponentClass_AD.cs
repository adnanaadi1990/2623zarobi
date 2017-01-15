﻿using System.Web;
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
    public class ComponentClass_AD
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


        public DataSet getDeadStock(string TransID)
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = @"SELECT TransactionID as [Form ID]
      ,DocumentNo as [Document No]
      ,FileName as [File Name]
      ,Description as [Description]
      ,FilePath as [File Location] FROM tbl_DeadStock  where TransactionID =  '" + TransID.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "getDeadStock");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        public DataSet getInventoryadjustment(string TransID)
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = @"SELECT TransactionID as [Form ID]
      ,DocumentNo as [Document No]
      ,FileName as [File Name]
      ,Description as [Description] FROM tbl_Inventoryadjustment  where TransactionID =  '" + TransID.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "getInventoryadjustment");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        public DataSet AllowForms(string UserName, string FormName)
        {
            try
            {
                cmd.CommandText = "";
                cmd.CommandText = "Exec SP_AllowForms_Restricted" + " @User_Name ='" + UserName + "', " +
                        " @Form_ID ='" + FormName + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "AllowForm");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
    }
}