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
    public partial class MtypeAdminForm : System.Web.UI.Page
    {
        public string FormID = "AMTYPE01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string value = "";
        public string Status = "";

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
                BindMaterialType();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                cmd.CommandText = "";
                cmd.CommandText = @"SP_UpdateAdminMTYPEandMandat";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@GroupName", txtPanelName.Text);
                cmd.Parameters.AddWithValue("@MTYPE", ddlMtypegrd.SelectedValue);
                cmd.Parameters.AddWithValue("@SNO", hdf.Value);
                if (CbStatus != null && CbStatus.Checked)
                {
                    Status = "0";
                }
                else
                {
                    Status = "1";
                }
                cmd.Parameters.AddWithValue("@Status", Status.ToString());
                cmd.Parameters.AddWithValue("@PanelDescription", txtPanelDescription.Text.ToString());



                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();

                lblmessage.Text = message.ToString();

                //EmailWorkSendFirstApproval();
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                btnSave.Visible = false;
                btnNew.Visible = true;
                ClearInputs(Page.Controls);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnReject_Click(object sender, EventArgs e)
        {

        }
        private void BindMaterialType()
        {

            ds = obj.BindMaterialType();
            ddlMtype.DataTextField = ds.Tables["MaterialType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlMtype.DataValueField = ds.Tables["MaterialType"].Columns["MaterialTypecode"].ToString();             // to retrive specific  textfield name 
            ddlMtype.DataSource = ds.Tables["MaterialType"];      //assigning datasource to the dropdownlist
            ddlMtype.DataBind();  //binding dropdownlist
            ddlMtype.Items.Insert(0, new ListItem("------Select------", "0"));

            ddlMtypegrd.DataTextField = ds.Tables["MaterialType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlMtypegrd.DataValueField = ds.Tables["MaterialType"].Columns["MaterialTypecode"].ToString();             // to retrive specific  textfield name 
            ddlMtypegrd.DataSource = ds.Tables["MaterialType"];      //assigning datasource to the dropdownlist
            ddlMtypegrd.DataBind();  //binding dropdownlist
            ddlMtypegrd.Items.Insert(0, new ListItem("------Select------", "0"));

        }
        protected void getData()
        {

            cmd.CommandText = @"SELECT SNo,MTYPE,PanelName,PanelDescription,
                            CASE Status
                            WHEN 0 THEN 'Active'  
                            ELSE 'De-active' 
                            END as Status 
                            FROM sys_MType_MM_GroupValidation where Mtype = @MTYPEget";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@MTYPEget", ddlMtype.SelectedValue);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            adp.SelectCommand = cmd;
            adp.Fill(ds, "sys_MType_MM_GroupValidation");
            grdData.DataSource = ds.Tables["sys_MType_MM_GroupValidation"];
            grdData.DataBind();
            grdData.Visible = true;


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ddlMtype.BackColor = System.Drawing.Color.White;
            if (ddlMtype.SelectedValue == "0")
            {
                sucess.Visible = false;
                lblmessage.Text = "";
                lblUpError.Text = "Please Select any M-Type.";
                error.Visible = true;
                ddlMtype.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else
            {
                lblUpError.Text = "";
                error.Visible = false;
                sucess.Visible = false;
                lblmessage.Text = "";
                getData();
            }
        }
        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            btnSave.Visible = true;
            btnNew.Visible = false;
            var SNo = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblSNo")).Text;
            var MTYPE = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblMTYPE")).Text;
            var PanelName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblPanelName")).Text;
            var PanelDescription = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblPanelDescription")).Text;
            var Status = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblStatus")).Text;

            hdf.Value = SNo.ToString();
            ddlMtypegrd.SelectedValue = MTYPE.ToString();
            txtPanelName.Text = PanelName.ToString();
            txtPanelDescription.Text = PanelDescription.ToString();
            value = Status.ToString();


            if (value == "Active")
            {
                CbStatus.Checked = true;
            }
            else
            {
                CbStatus.Checked = false;
            }

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string data = "";
            if (CbStatus != null && CbStatus.Checked)
            {
                data = "0";
            }
            else
            {
                data = "1";
            }
            cmd.CommandText = @"INSERT INTO sys_MType_MM_GroupValidation
           (SNo
           ,MTYPE
           ,PanelName
           ,PanelDescription
           ,Status)
     VALUES
           ((SELECT COALESCE(MAX(SNo), 0)  +1 as SNo from sys_MType_MM_GroupValidation)
           ,@MTYPE
           ,@GroupName
           ,@PanelDescription
           ,@Status)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@MTYPE", ddlMtypegrd.SelectedValue);
            cmd.Parameters.AddWithValue("@GroupName", txtPanelName.Text);
            cmd.Parameters.AddWithValue("@PanelDescription", txtPanelDescription.Text);
            cmd.Parameters.AddWithValue("@Status", data.ToString());

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();
            int aa = cmd.ExecuteNonQuery();
            if (aa > 0)
            {
                lblmessage.Text = "Record created sucessfully!";
                sucess.Visible = true;
            }
            conn.Close();
            ClearInputs(Page.Controls);
        }

        void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                if (ctrl is DropDownList)
                    ((DropDownList)ctrl).SelectedIndex = 0;
                if (ctrl is ListBox)
                    ((ListBox)ctrl).SelectedIndex = -1;
                ClearInputs(ctrl.Controls);

            }
            CbStatus.Checked = false;

        }
    }
}