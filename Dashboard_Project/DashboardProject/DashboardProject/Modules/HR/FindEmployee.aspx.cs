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
using ITLDashboard.Classes;
namespace ITLDashboard.Modules.HR
{
    public partial class FindEmployee : System.Web.UI.Page
    {
        public string FormID = "FEMP";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtRemarksReview.Visible = false;
                if (Session["User_Name"] == null)
                {
                    HttpContext con = HttpContext.Current;
                    con.Request.Url.ToString();

                    Session["Test"] = con.Request.Url.ToString();

                    //  Response.Redirect("~/SingleLogin.aspx");
                }

                if (Session["User_Name"] == null)
                {
                    Response.Redirect("~/SingleLogin.aspx");
                }
                getUser();
                getDepartment();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                getData();

            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }
        protected void getData()
        {

            cmd.CommandText = @"SELECT user_name
      ,user_email
      ,DisplayName
	  ,Location
      ,Department
      ,Designation
      ,MobileNo
      ,Ext
  FROM tbluser WHERE (ISNULL(@name,'')='' OR user_name = @name)
							AND (ISNULL(@Location,'')='' OR Location = @Location)
                            AND (ISNULL(@Department,'')='' OR Department = @Department)
                            order by SNo asc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@name", ddlDisplayName.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            adp.SelectCommand = cmd;
            adp.Fill(ds, "tblFindUser");
            grdData.DataSource = ds.Tables["tblFindUser"];
            grdData.DataBind();
            dt = ds.Tables["tblFindUser"];
            ViewState["paging"] = dt;
            grdData.Visible = true;
            ddlDisplayName.SelectedIndex = -1;
            ddlLocation.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            abc.Visible = true;

        }
        protected void getUser()
        {
            string strQuery = @"select Distinct user_name, DisplayName + ' | ' + Department + ' | ' + Designation  as Description from tbluser where Designation is not null and Designation != ''";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "getHOD");

            ddlDisplayName.DataTextField = ds.Tables["getHOD"].Columns["Description"].ToString().Trim(); ; // text field name of table dispalyed in dropdown
            ddlDisplayName.DataValueField = ds.Tables["getHOD"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
            ddlDisplayName.DataSource = ds.Tables["getHOD"];      //assigning datasource to the dropdownlist
            ddlDisplayName.DataBind();  //binding dropdownlist
            ddlDisplayName.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }
        protected void getDepartment()
        {
            string strQuery = @"select Distinct Department from tbluser where Department is not null and Department != ''";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "getDepartment");

            ddlDepartment.DataTextField = ds.Tables["getDepartment"].Columns["Department"].ToString().Trim(); ; // text field name of table dispalyed in dropdown
            ddlDepartment.DataValueField = ds.Tables["getDepartment"].Columns["Department"].ToString();             // to retrive specific  textfield name 
            ddlDepartment.DataSource = ds.Tables["getDepartment"];      //assigning datasource to the dropdownlist
            ddlDepartment.DataBind();  //binding dropdownlist
            ddlDepartment.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }
        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            DataTable dtpaging = (DataTable)ViewState["paging"];
            grdData.DataSource = dtpaging;   // 6 feb 2014
            grdData.DataBind();
        }
    }
}