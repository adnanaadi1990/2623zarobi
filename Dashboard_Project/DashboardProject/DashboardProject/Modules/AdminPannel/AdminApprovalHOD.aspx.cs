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
    public partial class AdminApprovalHOD : System.Web.UI.Page
    {
        public string FormID = "AAHOD01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();


        public string DisplayName = "";
        public string UserName = "";
        public string UserEmail = "";
        public string FormName = "";
        public string cmdInsert = "";
        public string cmdUpdate = "";
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
                GetTransactionID();
                methodCall();
            }

        }
        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

        }
        private void methodCall()
        {
            getUser();
            getFromName();
            getGridDetail();
        }


        protected void getUser()
        {
            conn.Close();
            string strQuery = @"Select user_name,user_email,DisplayName from tbluser";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblUser");

            ddlDisplayNameOther.DataTextField = ds.Tables["tblUser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
            ddlDisplayNameOther.DataValueField = ds.Tables["tblUser"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
            ddlDisplayNameOther.DataSource = ds.Tables["tblUser"];      //assigning datasource to the dropdownlist
            ddlDisplayNameOther.DataBind();  //binding dropdownlist
            ddlDisplayNameOther.Items.Insert(0, new ListItem("------Select------", "0"));
            conn.Close();
        }

        protected void getGridDetail()
        {
            conn.Close();
            string strQuery = @"Select * from tbluserApprovalHOD";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(dt);
            ViewState["paging"] = dt;
            grdData.DataSource = dt;
            grdData.DataBind();
            grdData.Visible = true;
        }
        protected void getFromName()
        {
            string strQuery = @"SELECT FormName,Form_ID from tblFormsDetail";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblFormsDetail");

            ddlFormNameOther.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlFormNameOther.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlFormNameOther.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlFormNameOther.DataBind();  //binding dropdownlist
            ddlFormNameOther.Items.Insert(0, new ListItem("------Select------", "0"));

            ddlSearchForm.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlSearchForm.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlSearchForm.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlSearchForm.DataBind();  //binding dropdownlist
            ddlSearchForm.Items.Insert(0, new ListItem("------Select------", "0"));

            conn.Close();
        }


        protected void ddlSearchForm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlDisplayNameOther.SelectedValue == "0")
            {

                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                ddlDisplayNameOther.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else if (txtUserNameOther.Text == "")
            {

                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtUserNameOther.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else if (txtEmailIDOther.Text == "")
            {

                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtEmailIDOther.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else if (ddlFormNameOther.SelectedValue == "0")
            {

                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                ddlFormNameOther.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else if (txtDesignation.Text == "0")
            {

                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtDesignation.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else
            {
                btnUpdate.Visible = true;

                cmdInsert = @"INSERT INTO tbluserApprovalHOD (DisplayName,user_name,Designation,FormID,EmailID)
                        VALUES  (@DisplayName,@user_name,@Designation,@FormID,@EmailID)";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdInsert;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@DisplayName", ddlDisplayNameOther.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@user_name", ddlDisplayNameOther.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
                cmd.Parameters.AddWithValue("@FormID", ddlFormNameOther.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@EmailID", txtEmailIDOther.Text.ToString());
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                conn.Close();
                if (a > 0)
                {
                    lblmessage.Text = "Record Inserted successfully";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    getGridDetail();
                    GetTransactionID();
                    ddlDisplayNameOther.SelectedIndex = -1;
                    ddlDisplayNameOther.SelectedIndex = -1;
                    txtDesignation.Text = "";
                    ddlFormNameOther.SelectedIndex = -1;
                    txtEmailIDOther.Text = "";

                }
                else
                {
                    lblmessage.Text = "Record can't Inserted";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            btnUpdate.Visible = true;
            cmdInsert = @"UPDATE tbluserApprovalHOD SET DisplayName = @DisplayName, user_name = @user_name,Designation = @Designation,FormID = @FormID,EmailID = @EmailID
                                 WHERE ID = @ID";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = cmdInsert;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@ID", hfIDupdate.Value.ToString());
            cmd.Parameters.AddWithValue("@DisplayName", ddlDisplayNameOther.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@user_name", txtUserNameOther.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@FormID", ddlFormNameOther.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@EmailID", txtEmailIDOther.Text.ToString());
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                lblmessage.Text = "Record Updated successfully";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                getGridDetail();

            }
            else
            {
                lblmessage.Text = "Record can't Updated";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                getGridDetail();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void grdData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string DeleteQuery = @"delete from tbluserApprovalHOD where ID = @DeleteID";
            var id = ((Label)grdData.Rows[e.RowIndex].FindControl("lblID")).Text;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = DeleteQuery;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@DeleteID", id.ToString());
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                lblmessage.Text = "Record deleted successfully";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                getGridDetail();

            }
            else
            {
                lblmessage.Text = "Record can't deleted";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                getGridDetail();
                Page.MaintainScrollPositionOnPostBack = false;
            }
        }
        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            var ID = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblID")).Text;
            var UserName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblUserName")).Text;
            var UserEmail = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lbluseremail")).Text;
            var DisplayName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblDisplayName")).Text;
            var FormName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblFormName")).Text;
            var Designation = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblDesignation")).Text;

            ddlDisplayNameOther.SelectedValue = UserName.ToString();
            hfIDupdate.Value = ID.ToString();
            txtEmailIDOther.Text = UserEmail.ToString();
            txtUserNameOther.Text = UserName.ToString();
            ddlFormNameOther.SelectedValue = FormName.ToString();
            txtDesignation.Text = Designation.ToString();

            txtEmailIDOther.Enabled = true;

            txtUserNameOther.Enabled = true;
        }
        protected void ddlDisplayNameOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedID = ddlDisplayNameOther.SelectedValue;
            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"Select user_name,user_email,DisplayName,Designation from tbluser where user_name = @DN";
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@DN", selectedID.ToString());
            conn.Open();
            using (SqlDataReader theReader = cmd.ExecuteReader())
            {
                if (theReader.HasRows)
                {
                    // Get the first row
                    theReader.Read();

                    // Set the text box values
                    txtUserNameOther.Text = theReader.GetString(0);
                    txtEmailIDOther.Text = theReader.GetString(1);
                    //  ddlDisplayNameOther.SelectedValue = theReader.GetString(0);

                }
            }
            conn.Close();
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {

        }
    }
}