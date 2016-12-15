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
using Telerik.Web.UI;
using ITLDashboard.Classes;


namespace DashboardProject.Modules.Reports
{
    public partial class DeadStockReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string FormID = "601";
        public int Coint = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Name"] == null)
                {
                    Response.Redirect("~/SingleLogin.aspx");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (txtFormIDto.Text == "")
                {
                    txtFormIDto.Text = txtFormIDfrom.Text;
                }
                RadGrid1.Visible = true;
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = @"SP_DeadStockReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@FormIDFrom", txtFormIDfrom.Text);
                cmd.Parameters.AddWithValue("@FormIDto", txtFormIDto.Text);
                cmd.Parameters.AddWithValue("@userName", txtUN.Text);

                adp.SelectCommand = cmd;
                adp.Fill(dt);
                ViewState["data"] = dt;
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
                RadGrid1.Visible = true;
                ClearInputs(Page.Controls);
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            { conn.Close(); }
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
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("20"));
                PageSizeCombo.FindItemByText("20").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("200"));
                PageSizeCombo.FindItemByText("200").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
        {
            dt = (DataTable)ViewState["data"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            dt = (DataTable)ViewState["data"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string str = item["FormID"].Text;

                    RadWindow win = new RadWindow();
                    win.ID = "window1";
                    win.VisibleOnPageLoad = true;
                    win.Width = 1000;
                    win.EnableViewState = false;
                    win.NavigateUrl = "~/StatusDetail.aspx?TransactionNo=" + str.ToString() + "&FormName=" + FormID.ToString() + "";
                    win.InitialBehaviors = WindowBehaviors.Maximize;
                    RadWindowManager1.Controls.Add(win);
                }

            }

        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                GridCommandItem cmdItem = (GridCommandItem)RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;
                ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {

            foreach (TableCell cell in e.Item.Cells)
            {
                cell.Style["font-size"] = "12px";
                cell.Style["font-family"] = "Arial";//"Courier New"; 
                cell.Style["vertical-align"] = "middle";
                cell.Style["font-weight"] = "normal";
                cell.Style["text-align"] = "left";
                cell.Style["background-color"] = "#f0f7f0";
                cell.Style["color"] = "Black";
                cell.Style["text-decoration"] = "none";
            }

            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                headerItem.Style["font-size"] = "14px";
                headerItem.Style["font-family"] = "Arial";//"Courier New"; 
                headerItem.Style["vertical-align"] = "middle";
                headerItem.Style["font-weight"] = "bold";
                headerItem.Style["text-align"] = "left";
                headerItem.Style["background-color"] = "#f0f7f0";
                headerItem.Style["color"] = "Black";
                headerItem.Style["text-decoration"] = "none";

                foreach (TableCell cell in headerItem.Cells)
                {
                    cell.Style["font-weight"] = "normal";
                    cell.Style["text-align"] = "left";
                    cell.Style["font-size"] = "12px";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["background-color"] = "#f0f7f0";
                    cell.Style["color"] = "Black";
                    cell.Style["text-decoration"] = "none";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}