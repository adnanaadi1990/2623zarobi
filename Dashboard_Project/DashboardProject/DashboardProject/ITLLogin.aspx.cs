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


namespace ITLDashboard
{
    public partial class ITLLogin : System.Web.UI.Page
    {
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        string connstring = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowPopup_Click(object sender, EventArgs e)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            try
            {
                if (ddlServer.SelectedValue == "0")
                {
                    lblError.Text = "Please select any server!";
                    return;
                }

                Boolean boolresult = ValidateUser(txtloginID.Text, txtloginpass.Text);
                if (boolresult)
                {
                    //string userName1 = txtloginID.Text.ToString().Replace('.', ' ');
                    string userName1 = txtloginID.Text.ToString();
                    txtloginID.Text = userName1.ToString();
                    //Session["User_Name"] = txtloginID.Text.ToString().Replace('.', ' ');
                    Session["User_Name"] = userName1.ToString();
                    lblError.Text = "Successful";
                    if (Session["Test"] == null)
                    {
                        Session["Test"] = "";
                        Response.Redirect("Main.aspx");
                    }
                    else
                    {
                        string URL = Session["Test"].ToString();
                        Response.Redirect(URL);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                    lblmessage.Text = "Sorry! Invalid User Name and Password";
                }
            }
            catch (Exception ex)
            { ex.ToString(); }


        }
        public Boolean ValidateUser(string userName, string password)
        {
            string path = ddlServer.SelectedValue.ToString();
            // string path = "LDAP://HO.com";
            //string path = "LDAP://internationaltextile.com";
            DirectoryEntry dirEntry = new DirectoryEntry(path, userName, password, AuthenticationTypes.Secure);
            try
            {
                DirectorySearcher dirSearcher = new DirectorySearcher(dirEntry);

                dirSearcher.FindOne();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}