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

namespace ITLDashboard.App_Code
{
    public class FieldValidationCode
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public DataTable checking(string MTYPE, ref DataTable tbl)
        {

            DataRow row;
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ComponentID";
            tbl.Columns.Add(column);


            if (MTYPE == "ROH")
            {
                //BD

                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PROD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PACKAGING
                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialCateguory";
                //tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialType";
                //tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

            }
            else if (MTYPE == "HALB")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //SD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatg";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub1";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub2";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlSalesUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlItemCateguoryGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlAvailabilitycheck";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProfitCenter";
                tbl.Rows.Add(row);

                //PROD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

            }

            else if (MTYPE == "FERT")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //SD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatg";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub1";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub2";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlSalesUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlItemCateguoryGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlAvailabilitycheck";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProfitCenter";
                tbl.Rows.Add(row);

                //PROD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);


                //PACKAGING
                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialCateguory";
                //tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialType";
                //tbl.Rows.Add(row);

            }


            else if (MTYPE == "ERSA")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

            }

            else if (MTYPE == "ABF")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);


                //SD

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatg";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub1";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProdCatgsub2";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlSalesUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlItemCateguoryGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlAvailabilitycheck";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlProfitCenter";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

            }

            else if (MTYPE == "VERP")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);


                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //ACCOUNTING

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PACKAGING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPackagingMaterialCateguory";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlPackagingMaterialType";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

            }

            else if (MTYPE == "FHMI")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PACKAGING
                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialCateguory";
                //tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "ddlPackagingMaterialType";
                //tbl.Rows.Add(row);

            }

            else if (MTYPE == "PROC")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);


            }

            else if (MTYPE == "WERB")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

            }

            else if (MTYPE == "NLAG")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);
            }

            else if (MTYPE == "UNBW")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //PROD
                row = tbl.NewRow();
                row["ComponentID"] = "ddlProductionunit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlUnitOfIssue";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

            }
            else if (MTYPE == "HAWA")
            {
                //BD
                row = tbl.NewRow();
                row["ComponentID"] = "txtDescription";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMMBaseUnitOfMeasure";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMSG";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "chkBatchManagement";
                tbl.Rows.Add(row);

                //PURCHASING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlPurchasingGroup";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlOrderingUnit";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMrpType";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlMRPGroup";
                tbl.Rows.Add(row);

                //ACCOUNTING
                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationClass";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "ddlValuationCategory";
                tbl.Rows.Add(row);

            }



            return tbl;
        }



        public DataTable GROUPVALIDATION(string MTYPE, ref DataTable tbl)
        {
            DataRow row;
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ComponentID";
            tbl.Columns.Add(column);

            if (MTYPE == "ROH")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "PROD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "Pack";
                //tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);


            }
            if (MTYPE == "HALB")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "SD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);


            }
            if (MTYPE == "FERT")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "SD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "Pack";
                //tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "ERSA")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "ABF")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "SD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "VERP")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Pack";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "FHMI")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                //row = tbl.NewRow();
                //row["ComponentID"] = "Pack";
                //tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);
            }
            if (MTYPE == "PROC")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "WERB")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);
            }
            if (MTYPE == "NLAG")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);
            }
            if (MTYPE == "UNBW")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Prod";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            if (MTYPE == "HAWA")
            {
                row = tbl.NewRow();
                row["ComponentID"] = "BD";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "CF";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Purch";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "Account";
                tbl.Rows.Add(row);

                row = tbl.NewRow();
                row["ComponentID"] = "divEmail";
                tbl.Rows.Add(row);

            }
            return tbl;
        }

        public DataTable checkingPetty(ref DataTable tbl)
        {

            DataRow row;
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ComponentID";
            tbl.Columns.Add(column);




            row = tbl.NewRow();
            row["ComponentID"] = "ddlEmailApproval";
            tbl.Rows.Add(row);

            row = tbl.NewRow();
            row["ComponentID"] = "ddlEmailApproval2nd";
            tbl.Rows.Add(row);

            row = tbl.NewRow();
            row["ComponentID"] = "ddlEmailMDA";
            tbl.Rows.Add(row);

            return tbl;

        }
        public DataSet GROUPVALIDATIONDATABASE(string MTYPE)
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "select * from sys_MType_MM_GroupValidation where MTYPE = '" + MTYPE.ToString() + "' and status = 0";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "sys_MType_MM_GroupValidation");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        public DataSet FieldVALIDATIONDATABASE(string MTYPE)
        {
            try
            {
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "SP_SYS_FIELDLISTINGRequired";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("@matTYPE", MTYPE.ToString());
                adp.Fill(ds, "SYS_FIELDLISTINGRequired");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }


        /////////////////////////////////////////////////////////////Secuirty/////////////////////////////////////////////////////////
        public DataSet AllowForms(string UserName, string FormName)
        {
            try
            {
                cmd.CommandText = "";
                cmd.CommandText = "Exec SP_AllowForms_Restricted" + " @User_Name ='" + UserName + "', " +
                        " @Form_ID ='" + FormName + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "AllowForm");
            }
            catch (Exception ex)
            { ex.ToString(); }
            finally
            { conn.Close(); }
            return ds;
        }
        /////////////////////////////////////////////////////////////Secuirty/////////////////////////////////////////////////////////
    }
}