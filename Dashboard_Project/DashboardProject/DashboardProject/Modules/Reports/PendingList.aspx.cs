using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITLDashboard.Modules.Reports
{
    public partial class PendingList : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand();
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
                getFormName();

            }


        }


        protected void getFormName()
        {
            cmd.CommandText = @"select FormIDCode,FormName from tblFormsDetail";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlApplication.DataSource = cmd.ExecuteReader();
            ddlApplication.DataTextField = "FormName";
            ddlApplication.DataValueField = "FormIDCode";
            ddlApplication.DataBind();
            conn.Close();
            ddlApplication.Items.Insert(0, new ListItem("------------Select------------", ""));
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                // txtfromID.BackColor = System.Drawing.Color.White;
                //txtToID.BackColor = System.Drawing.Color.White;
                //if (ddlApplication.SelectedValue == "0")
                //{
                //    lblError.Text = "Fill all required field!.";
                //    ddlApplication.BackColor = System.Drawing.Color.Red;
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    return;
                //}
                //else if (txtfromID.Text == "")
                //{
                //    lblError.Text = "Fill all required field!.";
                //    txtfromID.BackColor = System.Drawing.Color.Red;
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    return;
                //}
                //if (txtfromID.Text == "")
                //{
                //    //lblError.Text = "Fill all required field!.";
                //   // txtfromID.BackColor = System.Drawing.Color.Red;
                //    //Page.MaintainScrollPositionOnPostBack = false;
                //    //sreturn;
                //}

                //else
                //{

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