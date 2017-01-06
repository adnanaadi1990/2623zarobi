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
    public partial class Main : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpContext.Current.Session["Application"] = "";
                if (Session["User_Name"] == null)
                {
                    Response.Redirect("ITLLogin.aspx");

                }
                else
                {
                    ds = obj.getUserDetail(Session["User_Name"].ToString());
                    if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
                    {
                        lblUSerName.Text = ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                        //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                    }
                    else
                    {
                        lblUSerName.Text = Session["User_Name"].ToString();
                    }
                }
                if (DateTime.Now.Hour < 12)
                {
                    Label1.Text = "Good Morning ";
                }
                else if (DateTime.Now.Hour < 17)
                {
                    Label1.Text = "Good Afternoon ";
                }
                else
                {
                    Label1.Text = "Good Evening ";

                }

            }
            if (Session["User_Name"].ToString() == "faraz.quddusi" || Session["User_Name"].ToString() == "faraz" || Session["User_Name"].ToString() == "adnan.yousufzai" || Session["User_Name"].ToString() == "farrukh.aslam")
            {
                dvAdmin.Visible = true;
            }
            else
            {
                dvAdmin.Visible = false;
            }

        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {

            Session.Abandon();
            Response.Redirect("~/ITLLogin.aspx");

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["ProjectId"] = "1";
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMessage.Text == "")
                {
                    lblMessage.Text = "Please enter a message.!";
                    lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
                }
                else
                {
                    message = txtMessage.Text.ToString();
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "EXEC SP_InsertMessagedHelpDesk" + " @UserID ='" + Session["User_Name"] + "', " +
                                " @Messages='" + txtMessage.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a < 0)
                    {
                        ds = obj.MailForwardToAdminstrator(lblUSerName.Text.ToString());

                        ViewState["user_name"] = ds.Tables["MailForwardToAdminstrator"].Rows[0]["user_name"].ToString();
                        ViewState["user_email"] = ds.Tables["MailForwardToAdminstrator"].Rows[0]["user_email"].ToString();
                        ViewState["DisplayName"] = ds.Tables["MailForwardToAdminstrator"].Rows[0]["DisplayName"].ToString();
                        EmailSendToAdmin();

                    }

                }
            }


            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            txtMessage.Text = "";

        }
        protected void btnMM_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "MM";
            Response.Redirect("Modules/Master/MM_Main.aspx");
        }
        protected void btnFI_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "FI";
            Response.Redirect("Modules/Finance/Finance_Main.aspx");
        }
        protected void btnHR_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "HR";
            Response.Redirect("Modules/HR/HR_Main.aspx");
        }
        protected void btnAnnexure_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "Annexure";
            Response.Redirect("Modules/Annexure/Annexure_Main.aspx");
        }
        protected void btnAdmin_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "Admin";
            Response.Redirect("Modules/AdminPannel/Admin_Main.aspx");
        }

        protected void btnSAPBasis_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "SAPBasisApplication";
            Response.Redirect("Modules/SBApp/SAPBasis_Main.aspx");
        }


        ///////////////////////////////////////// Admin Email Working//////////////////
        protected void EmailSendToAdmin()
        {
            using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", "ithelpdesk.internationaltextil@gmail.com"))
            {

                mm.Subject = "Dashboard Helpdesk Query via – " + ViewState["DisplayName"].ToString() + "";
                //,<br> <br>   I have Following request against " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";


                mm.Body = "Dear Adminstrator <br> <br> " + ViewState["DisplayName"].ToString() + " Send you an email via dashboard Helpdesk" +
                    "<br> <br> <br>Message: " + message.ToString() +
               "<br><br><br>HELPDESK MIS ADMINISTRATION<br> Information Systems Dashboard";
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "Itldash$$");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                lblMessage.Text = "Your Message has been send to admin!";

            }
        }

        protected void btnIM_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "IM";
            Response.Redirect("Modules/Inventorymanagement/Inventorymanagement_Main.aspx");
        }

        protected void btnProcu_Click(object sender, ImageClickEventArgs e)
        {
         
                Session["Module"] = "Proc";
                Response.Redirect("Modules/Procurement/Procurment_Main.aspx");
        }

        protected void btnPP_Click(object sender, ImageClickEventArgs e)
        {
            Session["Module"] = "PP";
            Response.Redirect("Modules/PP/PP_Main.aspx");
        }
        ///////////////////////////////////////// Admin Email Working//////////////////

  
    }
}