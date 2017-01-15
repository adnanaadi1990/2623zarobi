using Microsoft.Reporting.WebForms;
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
using ITLDashboard.Classes;


namespace ITLDashboard.Modules.Reports
{
    public partial class PendingList : System.Web.UI.Page
    {
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
                getFormNameByFormID();

            }


        }


        protected void getFormNameByFormID()
        {

            ds = obj.getFormNameByFormID(Session["Application"].ToString().Trim(),"");
            if (ds.Tables["SP_FormDetailByFormID"].Rows.Count > 0)
            {
                ddlApplication.DataTextField = ds.Tables["SP_FormDetailByFormID"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
                ddlApplication.DataValueField = ds.Tables["SP_FormDetailByFormID"].Columns["FormIDCode"].ToString();             // to retrive specific  textfield name 
                ddlApplication.DataSource = ds.Tables["SP_FormDetailByFormID"];      //assigning datasource to the dropdownlist
                ddlApplication.DataBind();  //binding dropdownlist
              //  ddlApplication.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlApplication.SelectedIndex = 1;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
              
                if (txtToID.Text == "")
                {
                    txtToID.Text = txtfromID.Text;
                }
                DataSet ds = new DataSet();

                ds.Clear();
                //DataTable DT = GetSPResult();
                string query = "";
                query = @"SP_UserPendingList";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@FormID", ddlApplication.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TransactionIDFrom", txtfromID.Text);
                cmd.Parameters.AddWithValue("@TransactionIDTO", txtToID.Text);
                ds.Clear();
                Adapter.Fill(ds, "PendingList");
                if (ds.Tables["PendingList"].Rows.Count > 0)
                {
                    ReportViewer1.Visible = true;
                    ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["PendingList"]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~\Modules\Reports\PendingList.rdlc");

                    string FORMName = ddlApplication.SelectedValue.ToString();
                    if (FORMName == "")
                    {
                        FORMName = "ALL";

                    }
                    else
                    {
                        FORMName = ds.Tables["PendingList"].Rows[0]["FormID"].ToString().Trim();
                    }
                    ReportParameter FORMID = new ReportParameter("FORMID", FORMName.ToString().Trim());
                    ReportViewer1.LocalReport.SetParameters(FORMID);

                    string TIDFrom = txtfromID.Text;
                    if (TIDFrom == "")
                    {
                        TIDFrom = "ALL";
                        txtfromID.Text = "ALL";
                    }
                    else
                    {
                        TIDFrom = txtfromID.Text;
                    }

                    ReportParameter TransactionFrom = new ReportParameter("FormIDFrom", TIDFrom.ToString().Trim());
                    ReportViewer1.LocalReport.SetParameters(TransactionFrom);

                    string TIDTO = txtToID.Text;
                    if (TIDTO == "")
                    {
                        TIDTO = txtfromID.Text;
                    }
                    else
                    {
                        TIDTO = txtToID.Text;
                    }

                    ReportParameter FormIDTo = new ReportParameter("FormIDTo", TIDTO.ToString().Trim());
                    ReportViewer1.LocalReport.SetParameters(FormIDTo);

                    //ReportParameter To = new ReportParameter("To", txtToID.Text);
                    //ReportViewer1.LocalReport.SetParameters(To);

                    //ReportParameter From = new ReportParameter("From", txtfromID.Text);
                    //ReportViewer1.LocalReport.SetParameters(From);

                    ReportViewer1.LocalReport.Refresh();

                    //byte[] bytes = ReportViewer1.LocalReport.Render("PDF");

                    ////display pdf in browser
                    //Response.AddHeader("Content-Disposition", "inline; filename=MyReport.pdf");
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(bytes);
                    //Response.End();

                    byte[] file = ReportViewer1.LocalReport.Render("PDF");

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "inline;filename=Test.pdf");
                    Response.Buffer = false;
                    Response.Clear();
                    Response.BinaryWrite(file);
                    ddlApplication.SelectedIndex = -1;
                    txtfromID.Text = "";
                    txtToID.Text = "";
                    //rbRptType.SelectedValue = "NormalDays";
                    lblError.Text = "";
                    Response.End();

                }

                else
                {
                    lblError.Text = "No Data Found!..";
                }

                //   }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());

        }
    }
}