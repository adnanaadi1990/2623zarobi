using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardProject
{
    public partial class PowerLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
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
    }
}