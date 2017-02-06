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

namespace ITLDashboard
{
    public partial class SingleLogin : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        string connstring = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // string IP = Request.UserHostName;
                //string compName = DetermineCompName(IP);

                //  string s = Request.ServerVariables["AUTH_USER"];
                //   C.Text = s.ToString();
                //  D.Text = HttpContext.Current.Request.LogonUserIdentity.Name.ToString();
                string user = System.Web.HttpContext.Current.User.Identity.Name;

                //string principal = this.Context.User.Identity.Name;
                //string username = Convert.ToString(Request.LogonUserIdentity.Name.ToString());
                //A.Text = Request.ServerVariables["LOGON_USER"];
                //D.Text = User.Identity.Name;

                //C.Text = Environment.UserName.ToString();
                //D0.Text = Request.LogonUserIdentity.Name.ToString();
                // String u1 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //  B.Text = u1.ToString();
                //D1.Text = user.ToString();
                //D2.Text = principal.ToString();
                //D3.Text = username.ToString();
                //D4.Text = Request.ServerVariables.Get("AUTH_USER");



                if (Session["Test"] != null)
                {
                    if (user.StartsWith("ITL"))
                    {
                        Session["User_Name"] = "Dashboard.1";
                        Response.Redirect(Session["Test"].ToString());
                    }
                    else
                    {
                        Response.Redirect("ITLLogin.aspx");
                    }
                }

                else
                {
                    if (user.StartsWith("ITL"))
                    {
                        //Session["User_Name"] = Environment.UserName.ToString();
                        Session["User_Name"] = "Dashboard.1";
                        Response.Redirect("Main.aspx");
                    }

                    else
                    {
                        Response.Redirect("ITLLogin.aspx");
                    }
                    //}
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //string user = System.Web.HttpContext.Current.User.Identity.Name;
                    //String u1 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //string principal = this.Context.User.Identity.Name;
                    //string username = Convert.ToString(Request.LogonUserIdentity.Name.ToString());
                    //if (Session["Test"] != null)
                    //{
                    //    Session["User_Name"] = Environment.UserName.ToString();
                    //    Response.Redirect(Session["Test"].ToString());
                    //}
                    //else
                    //{
                    //    using (SqlConnection conn = new SqlConnection(connstring))
                    //    {
                    //        string UserName = Environment.UserName.ToString();

                    //        string query = "SELECT user_name,user_email,Server FROM tbluser where user_name = 'faraz.quddusi'";
                    //        conn.Open();
                    //        SqlCommand cmd = new SqlCommand(query, conn);
                    //        cmd.Connection = conn;
                    //        SqlDataReader dr = cmd.ExecuteReader();
                    //        if (dr.HasRows)
                    //        {
                    //            dr.Read();
                    //            Session["User_Name"] = dr[0].ToString();
                    //            Response.Redirect("Main.aspx");
                    //        }
                    //        else
                    //        {
                    //            Response.Redirect("ITLLogin.aspx");
                    //        }
                    //    }
                    //    //Response.Redirect("ITLLogin.aspx");
                    //}
                    ////////////////////////////////////////////////        /////////////////////////////////////////////////////
                    //if (Session["User_Name"] == null)
                    //{
                    //    Session["User_Name"] = "";
                    //    HttpContext con = HttpContext.Current;
                    //    con.Request.Url.ToString();
                    //    if (user.StartsWith("HO") || (user.StartsWith("internationaltextile")))
                    //    {
                    //        if (Session["Test"] != null)
                    //        {
                    //            Session["User_Name"] = Environment.UserName.ToString();
                    //            //Session["Test"] = con.Request.Url.ToString();
                    //            //A.Text = Request.ServerVariables["LOGON_USER"];
                    //            Response.Redirect(Session["Test"].ToString());


                    //        }
                    //        else
                    //        {
                    //            //A.Text = Request.ServerVariables["LOGON_USER"];
                    //            Session["User_Name"] = Environment.UserName.ToString();
                    //            Response.Redirect("Main.aspx");
                    //        }

                    //    }

                    //    else
                    //    {
                    //        //B.Text = Request.ServerVariables["LOGON_USER"];
                    //        Response.Redirect("ITLLogin.aspx");

                    //        if (Session["Test"] == null)
                    //        {
                    //            //Session["User_Name"] = Environment.UserName.ToString();
                    //         //   Session["Test"] = con.Request.Url.ToString();
                    //           // Response.Redirect(Session["Test"].ToString());

                    //        }
                    //        else
                    //        {
                    //            Session["User_Name"] = Environment.UserName.ToString();

                    //            Response.Redirect("Main.aspx");
                    //        }



                    //    }
                    //}
                    //if (Request.ServerVariables["LOGON_USER"].StartsWith("HO\\")  || (Request.ServerVariables["LOGON_USER"].StartsWith("internationaltextile\\")))           
                    //{
                    //    Session["User_Name"] = Environment.UserName.ToString();
                    //    Response.Redirect("Main.aspx");
                    //}

                    //else
                    //{
                    // Response.Redirect("ITLLogin.aspx");
                    //}


                }

            }
        }

    }
}