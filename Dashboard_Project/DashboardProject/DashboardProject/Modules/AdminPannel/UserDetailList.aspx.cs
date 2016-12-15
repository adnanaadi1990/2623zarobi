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


namespace ITLDashboard.Modules.AdminPannel
{
    public partial class UserDetailList : System.Web.UI.Page
    {
        public string FormID = "UDL01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public int a;

        public string User_ID = "";
        public string UserName = "";
        public string FormName = "";
        public string Form_ID = "";
        public string value = "";
        public string Remarks = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDepartment();
                getDesignation();
                getHOD();
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
                //Session["User_Name"] = Environment.UserName.ToString();

                //string url = HttpContext.Current.Request.Url.AbsoluteUri + "?User=" + Session["User_Name"].ToString();
                if (Request.QueryString["User"] != null)
                {

                    string UN = Session["User_Name"].ToString();
                    cmd.CommandText = "";
                    cmd.CommandText = @"Select top(1) *  FROM tblUser where user_name = @UN";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@UN", UN.ToString().Trim());
                    adp.SelectCommand = cmd;

                    adp.Fill(ds, "tblUser");
                    if (ds.Tables["tblUser"].Rows.Count > 0)
                    {
                        lblMaxTransactionNo.Text = ds.Tables["tblUser"].Rows[0]["SNo"].ToString().Trim();
                        txtUserNameOther.Text = ds.Tables["tblUser"].Rows[0]["user_name"].ToString().Trim();
                        txtEmailIDOther.Text = ds.Tables["tblUser"].Rows[0]["user_email"].ToString().Trim();
                        txtDisplayName.Text = ds.Tables["tblUser"].Rows[0]["DisplayName"].ToString().Trim();
                        ddlLocation.SelectedValue = ds.Tables["tblUser"].Rows[0]["Location"].ToString().Trim();
                        ddlDepartment.SelectedValue = ds.Tables["tblUser"].Rows[0]["Department"].ToString().Trim();
                        ddlDesignation.SelectedValue = ds.Tables["tblUser"].Rows[0]["Designation"].ToString().Trim();
                        txtMobileNo.Text = ds.Tables["tblUser"].Rows[0]["MobileNo"].ToString().Trim();
                        txtExtensionNo.Text = ds.Tables["tblUser"].Rows[0]["Ext"].ToString().Trim();
                        txtSAPID.Text = ds.Tables["tblUser"].Rows[0]["SAPID"].ToString().Trim();
                        ddlHOD.SelectedValue = ds.Tables["tblUser"].Rows[0]["HOD"].ToString().Trim();
                        btnUpdate.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtUserNameOther.Enabled = false;
                    }
                    else
                    {
                        txtUserNameOther.Text = Environment.UserName.ToString();
                        lblError.Text = "No Data Found.! Kindly provide your complete data ";
                        btnUpdate.Visible = false;
                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                        txtUserNameOther.Enabled = false;
                        dvPnl.Visible = false;
                    }

                }

                else
                {
                    btnUpdate.Visible = false;
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                    GetTransactionID();


                }


            }
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

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
        protected void getDesignation()
        {
            string strQuery = @"select Distinct Designation from tbluser where Designation is not null and Designation != ''";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "getDesignation");

            ddlDesignation.DataTextField = ds.Tables["getDesignation"].Columns["Designation"].ToString().Trim(); ; // text field name of table dispalyed in dropdown
            ddlDesignation.DataValueField = ds.Tables["getDesignation"].Columns["Designation"].ToString();             // to retrive specific  textfield name 
            ddlDesignation.DataSource = ds.Tables["getDesignation"];      //assigning datasource to the dropdownlist
            ddlDesignation.DataBind();  //binding dropdownlist
            ddlDesignation.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }
        protected void getHOD()
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

            ddlHOD.DataTextField = ds.Tables["getHOD"].Columns["Description"].ToString().Trim(); ; // text field name of table dispalyed in dropdown
            ddlHOD.DataValueField = ds.Tables["getHOD"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
            ddlHOD.DataSource = ds.Tables["getHOD"];      //assigning datasource to the dropdownlist
            ddlHOD.DataBind();  //binding dropdownlist
            ddlHOD.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }


        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();


                ClearInputs(ctrl.Controls);
            }
            Page.MaintainScrollPositionOnPostBack = false;
            txtUserNameOther.Text = Environment.UserName.ToString();
        }

        void ClearInputscolor(ControlCollection ctrlss)
        {
            foreach (Control ctrlsss in ctrlss)
            {
                if (ctrlsss is TextBox)
                    ((TextBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is DropDownList)
                    ((DropDownList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is ListBox)
                    ((ListBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is RadioButtonList)
                    ((RadioButtonList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                ClearInputscolor(ctrlsss.Controls);
            }
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////
        protected void ddlDisplayNameOther_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClearInputscolor(Page.Controls);
                if (txtDisplayName.Text == "")
                {
                    txtDisplayName.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Full Name should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (txtEmailIDOther.Text == "")
                {
                    txtEmailIDOther.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Email ID should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (ddlLocation.SelectedValue == "0")
                {
                    ddlLocation.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any location";
                    error.Visible = true;
                    return;

                }
                else if (ddlDepartment.SelectedValue == "0")
                {
                    ddlDepartment.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any Department";
                    error.Visible = true;
                    return;

                }
                else if (ddlDesignation.SelectedValue == "0")
                {
                    ddlDepartment.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any Designation";
                    error.Visible = true;
                    return;

                }

                else if (txtSAPID.Text == "")
                {
                    txtSAPID.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "SAP ID should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (ddlHOD.SelectedValue == "")
                {
                    ddlHOD.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select Head of department";
                    error.Visible = true;
                    return;

                }
                else
                {
                    insertuser();
                    lblError.Text = "";
                    error.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearInputscolor(Page.Controls);
                if (txtDisplayName.Text == "")
                {
                    txtDisplayName.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Full Name should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (txtEmailIDOther.Text == "")
                {
                    txtEmailIDOther.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Email ID should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (ddlLocation.SelectedValue == "")
                {
                    ddlLocation.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any location";
                    error.Visible = true;
                    return;

                }
                else if (ddlDepartment.SelectedValue == "")
                {
                    ddlDepartment.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any Department";
                    error.Visible = true;
                    return;

                }
                else if (ddlDesignation.SelectedValue == "")
                {
                    ddlDepartment.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select any Designation";
                    error.Visible = true;
                    return;

                }
                else if (txtSAPID.Text == "")
                {
                    txtSAPID.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "SAP ID should not be left blank";
                    error.Visible = true;
                    return;

                }
                else if (ddlHOD.SelectedValue == "")
                {
                    ddlHOD.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Select Head of department";
                    error.Visible = true;
                    return;

                }
                else
                {
                    updateuser();
                    lblError.Text = "";
                    error.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
        }

        protected void updateuser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdUpdateUser = new SqlCommand())
                {

                    try
                    {
                        cmdUpdateUser.Connection = connection;
                        cmdUpdateUser.CommandType = CommandType.Text;
                        cmdUpdateUser.CommandText = @"UPDATE tbluser SET user_name = @user_name ,user_email = @user_email ,DisplayName = @DisplayName ,Location = @Location,Department = @Department,Designation = @Designation,MobileNo = @MobileNo,Ext = @Ext,SAPID = @SAPID,HOD = @HOD WHERE SNo = @SNo";

                        cmdUpdateUser.Parameters.AddWithValue("SNo", lblMaxTransactionNo.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@user_name", Environment.UserName.ToString());
                        cmdUpdateUser.Parameters.AddWithValue("@user_email", txtEmailIDOther.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue.ToString());
                        cmdUpdateUser.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue.ToString());
                        cmdUpdateUser.Parameters.AddWithValue("@Designation", ddlDesignation.SelectedValue.ToString());
                        cmdUpdateUser.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@Ext", txtExtensionNo.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@SAPID", txtSAPID.Text);
                        cmdUpdateUser.Parameters.AddWithValue("@HOD", ddlHOD.SelectedValue.ToString());
                        connection.Open();
                        cmdUpdateUser.ExecuteNonQuery();
                        lblmessage.Text = "User updated sucessfully";
                        lblmessage.ForeColor = System.Drawing.Color.Green;
                        sucess.Visible = true;
                        lblmessage.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        btnUpdate.Enabled = false;
                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        protected void insertuser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertUser = new SqlCommand())
                {

                    try
                    {
                        cmdInsertUser.Connection = connection;
                        cmdInsertUser.CommandType = CommandType.Text;
                        cmdInsertUser.CommandText = @"INSERT INTO tbluser
           (user_name ,user_email,DisplayName,Location,Department,Designation,MobileNo,Ext,SAPID,HOD) VALUES
           (@user_name,@user_email,@DisplayName,@Location,@Department,@Designation,@MobileNo,@Ext,@SAPID,@HOD)";

                        cmdInsertUser.Parameters.AddWithValue("@user_name", Environment.UserName.ToString());
                        cmdInsertUser.Parameters.AddWithValue("@user_email", txtEmailIDOther.Text);
                        cmdInsertUser.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text);
                        cmdInsertUser.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue.ToString());
                        cmdInsertUser.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue.ToString());
                        cmdInsertUser.Parameters.AddWithValue("@Designation", ddlDesignation.SelectedValue.ToString());
                        cmdInsertUser.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                        cmdInsertUser.Parameters.AddWithValue("@Ext", txtExtensionNo.Text);
                        cmdInsertUser.Parameters.AddWithValue("@SAPID", txtSAPID.Text);
                        cmdInsertUser.Parameters.AddWithValue("@HOD", ddlHOD.SelectedValue.ToString());
                        connection.Open();
                        cmdInsertUser.ExecuteNonQuery();
                        lblmessage.Text = "User insert sucessfully";
                        lblmessage.ForeColor = System.Drawing.Color.Green;
                        sucess.Visible = true;
                        lblmessage.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;

                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
    }
}