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
    public class ComponentClass_FK
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public DataSet BindTransportTo()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "SELECT TransportTo,TransportTo + ' ' + Description as  Description FROM tbl_TransportTo";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tbl_TransportTo");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }



        public DataSet BindServiceCategory()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select ServiceCategoryCode, ServiceCategoryCode +' '+ ServiceCategoryDesc as Description  from tbl_ServiceCategory";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tbl_ServiceCategory");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }

        public DataSet BindBaseUnit()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select Baseuom from tblBaseunitofmeasure";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tblBaseunitofmeasure");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }

        public DataSet BindMaterialService()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select Materialgrpcode,Materialgrpcode + ' ' + Description as Description from tblMaterialgrp";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tblMaterialgrp");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        public DataSet BindDivision()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select Divisioncode,Divisioncode +' '+ Description as Description from tblDivision";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tblDivision");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        public DataSet BindValuation()
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select  ValuationClasscode,ValuationClasscode +' '+ Description as Description from tblValuationClass";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "tblValuationClass");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }


        public DataSet UpdateServiceMaster(string MetrialNo, string SAPCode,string VC)
        {
            try
            {

                cmd.CommandText = "EXEC SP_UpdateServiceMasterCode" + " @TransactionID  ='" + MetrialNo.ToString() + "', " + " @ServiceMasterCode ='" + SAPCode.ToString() + "'," + " @VC ='" + VC.ToString() + "'";                    
                       
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
    }
}