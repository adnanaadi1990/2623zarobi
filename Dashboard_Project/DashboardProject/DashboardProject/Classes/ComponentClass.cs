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

namespace ITLDashboard.Classes
{
     public class ComponentClass
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            public DataSet GetTransactionID()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    //cmd.CommandText = "SELECT COALESCE(MAX(MeterialNo), 0) +1 as TransactionID from tbl_SYS_MaterialMaster";
                    cmd.CommandText = "EXEC [SP_MaintainTrans]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MaterialMaxID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet getUserDetail(string _UserNameID)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"select DisplayName,Department,Designation from tbluser where user_name = @UserID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@UserID", _UserNameID.ToString());
                        adp.Fill(ds, "tbluser_DisplayName");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet getUserHOD(string UserName)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"SP_getHOD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@username", UserName.ToString());
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "getUserHOD");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet getPlantDistinct()
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select Distinct PlantId, PlantId + ' ' + Description as Description from tblPlants where PlantId != '000'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "getPlantDistinct");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindTCode()
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select TCode, TCode + ' ' + Description as Description from tbl_TCode";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "TCode");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet GetStatusHierachyCategoryControl(string user_name, string TransID, string FormID, string HierarchyCateguory)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                {
                    using (SqlCommand cmdInsert = new SqlCommand())//
                    {
                        try
                        {
                            cmdInsert.CommandText = "";
                            cmdInsert.CommandText = @"sysControls";
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            cmdInsert.Connection = connection;
                            adp.SelectCommand = cmdInsert;
                            cmdInsert.Parameters.AddWithValue("@RoughtingUserID", user_name.ToString());
                            cmdInsert.Parameters.AddWithValue("@TransactionID", TransID.ToString());
                            cmdInsert.Parameters.AddWithValue("@FormID", FormID.ToString());
                            cmdInsert.Parameters.AddWithValue("@HierachyCategory", HierarchyCateguory.ToString());
                            adp.Fill(ds, "tbl_SysHierarchyControl");
                        }
                        catch (Exception ex)
                        { ex.ToString(); }
                        finally
                        { connection.Close(); }
                        return ds;
                    }
                }

            }

            public DataSet BindMovementtype()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Movementtypecode,Movementtypecode + ' ' + Movementtypedescription as  Description FROM tbl_Movementtype";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_Movementtype");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindOrdertype()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Ordertypecode,Ordertypecode + ' ' + OrdertypeDescription as  Description FROM tbl_OrderType";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_OrderType");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet CheckSapID(string username)
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select SAPID from tbluser where user_name like '%" + username.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "SAPID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet GetTransactionMaxPettyCash(string FORMID)
            {
                //Changes
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    //cmd.CommandText = "SELECT COALESCE(MAX(MeterialNo), 0) +1 as TransactionID from tbl_SYS_MaterialMaster";
                    cmd.CommandText = "EXEC [SP_MaintainTrans]" + "@FormID='" + FORMID.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MaterialMaxID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet GetTransactionMax()
            {
                //Changes
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    //cmd.CommandText = "SELECT COALESCE(MAX(MeterialNo), 0) +1 as TransactionID from tbl_SYS_MaterialMaster";
                    cmd.CommandText = "SELECT COALESCE(MAX(TransactionID), 0)  +1 as TransactionID from tbl_SYS_MaterialMaster";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MaterialMasterTrID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindTransactionID()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT (MeterialNo) from tbl_SYS_MaterialMaster order by MeterialNo asc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MeterialNo");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet getSAPMaterialCode()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"select SAPMaterialCode, isnull(SAPMaterialCode +'  | '+ Description,SAPMaterialCode) as Description from tbl_SYS_MaterialMaster
                                    where SAPMaterialCode is not null and SAPMaterialCode != '' and Status = 'N'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "SAPMaterialCode");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet createuser(string UserId, string username, string pass, string email, string designation, string dept)
            {
                try
                {
                    //string query = "Exec SP_signup" + " @user_name='" + username + "', " +
                    //     " @user_password='" + pass + "', " +
                    //      " @user_email='" + email + "', " +
                    //       " @designation='" + designation + "', " +
                    //        " @department_id='" + dept + "', " + "'";
                    cmd.CommandText = "Exec SP_signup" + " @user_id='" + UserId + "', " +
                        " @user_name='" + username + "', " +
                         " @user_password='" + pass + "', " +
                          " @user_email='" + email + "', " +
                           " @designation='" + designation + "', " +
                            " @department_id='" + dept + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");


                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindDropDownDept()
            {

                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindGridDept";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);


                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindDropDownDesignation()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindGridDesignation";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataTable Userlogin(string user_id, string passcode)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_userlogin" + " @user_id='" + user_id + "', " +
                            " @passcode='" + passcode + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(dt);
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return dt;
            }
            // MASTER DETAIL METERIAL COMBO//
            public DataSet BindMaterialType()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindMaterialType";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MaterialType");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataTable BindMaterialMaster()
            {
                try
                {
                    dt.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindMaterialMaster";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(dt);
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return dt;
            }
            public DataSet BindStorageLocation()
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_StorageLocation";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "StorageLocation");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindPlant()
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindPlant";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Plant");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindPlantMtype(string MaterialTypePlant)
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT PlantId ,PlantId + ' ' + Description as Description FROM tblPlants where MaterialType LIKE '%" + MaterialTypePlant.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindPlantMtype");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindMaterialgroup()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "EXEC SP_Materialgroup";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Materialgroup");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindMaterialgroupMtype(string MaterialType)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Materialgrpcode,[Materialgrpcode] + ' ' + Description as Description FROM [dbo].[tblMaterialgrp] where MaterialType  like '%" + MaterialType.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindMaterialgroupMtype");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindBaseUnitOfMeasure()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BaseUnitOfMeasure";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BaseUnitOfMeasure");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindBaseUnitOfMeasureMTYPE(string MTYPEBOM)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT BaseuomSNo ,Baseuom FROM tblBaseunitofmeasure where MaterialType like '%" + MTYPEBOM.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindBaseUnitOfMeasureMTYPE");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindLenght()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT [Lenghtcode],[Lenghtcode] +' '+[Lenghtdescription] as Description FROM [dbo].[tblLenght]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Lenght");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindUser()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT * FROM tbluserApproval where FormName = 'DCW'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblUser");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindSplitValueation()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_ValuationType";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ValuationType");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindProfitCenter()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_tblProfitCenter";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ProfitCenter");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindValuationCategory()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_ValuationCategory";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ValuationCategory");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindMRPtypeMTYPE(string MaterialType)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT mrptypecode ,mrptypecode + ' ' + Description as Description FROM tblmrptype where MaterialTypecode like '%" + MaterialType.ToString() + "%' ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindMRPtypeMTYPE");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindSplitValueation(string ValuationCatg)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT ValuationType FROM tblValuationType where ValuationCatg like '%" + ValuationCatg.ToString() + "%'   order by ValuationTypeSNo asc ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindSplitValueation");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindValuationCategoryMTYPE(string MaterialTypePlant)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT ValuationCategorySNo ,ValuationCategorycode ,ValuationCategorycode + ' ' + Description as Description FROM tblValuationCategory where MaterialTypecode like '%" + MaterialTypePlant.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ValuationCategoryMTYPE");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindPurchasingGroup()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_PurchasingGroup";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "PurchasingGroup");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindPurchasingGroupMTYPE(string MaterialTypePG)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT [PurchasingGroupSNo] ,[PurchasingGroupcode] ,[PurchasingGroupcode]+ ' ' + Description as Description FROM [dbo].[tblPurchasingGroup]   where MaterialTypecode like '%" + MaterialTypePG.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindPurchasingGroupMTYPE");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindMRPController()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_mrpController";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "mrpController");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindLotSize()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_LotSize";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "LotSize");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindMRPType()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_MRPType";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "MRPType");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindMRPTypeMtype(string MaterialType)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT [mrptypecode] ,[mrptypecode]+ ' ' + Description as Description FROM [dbo].[tblmrptype] where MaterialTypecode like '%" + MaterialType.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindMRPTypeMtype");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindAvailabilitycheck()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Availabilitycheck";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Availabilitycheck");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindRebateCategoryRate()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_RebateCategoryRate";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "RebateCategoryRate");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindPeriodIndicator()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_PeriodIndicator";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "PeriodIndicator");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindStrategygroup()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Strategygroup";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Strategygroup");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindQMControlKey()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_QMControlKey";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "QMControlKey");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindRate()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Rate";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Rate");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindDistributionChannel()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_DistributionChannel";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "DistributionChannel");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindDeliveringplant()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Deliveringplant";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Deliveringplant");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindLoadingGroup()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_LoadingGroup";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "LoadingGroup");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindSalesTax()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_SalesTax";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "SalesTax");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindValuationClass()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_ValuationClass";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ValuationClass");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindValuationClassMtype(string ValuationClassMtype)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT [ValuationClassSNo],[ValuationClasscode] ,[ValuationClasscode]+ ' ' + Description as Description FROM tblValuationClass where MaterialTypecode like '%" + ValuationClassMtype.ToString() + "%'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindValuationClassMtype");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindProdnsupervisor()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Prodnsupervisor";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Prodnsupervisor");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindProdSchedProfile()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_ProdSchedProfile";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ProdSchedProfile");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindTasklistusage()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_Tasklistusage";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Tasklistusage");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            // MASTER DETAIL METERIAL COMBO//
            //Metrial////

            public DataSet createMeterial(string MetrialNo, string Desc, string color, string size, string weight)
            {
                try
                {
                    //string query = "Exec SP_signup" + " @user_name='" + username + "', " +
                    //     " @user_password='" + pass + "', " +
                    //      " @user_email='" + email + "', " +
                    //       " @designation='" + designation + "', " +
                    //        " @department_id='" + dept + "', " + "'";
                    cmd.CommandText = "Exec SP_createMeterial" + " @MeterialOldNo='" + MetrialNo + "', " +
                         " @Desc='" + Desc + "', " +
                          " @color='" + color + "', " +
                           "@size='" + size + "', " +
                            "@weight='" + weight + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds);


                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            /////////////////////////////////////////////////////////////Insert Merterial Mater//////////////////////////////////////////////
            public DataSet CreateMaterialMaster(string action, string MeterialNo, string MaterialType, string SAPMaterialCode, string Plant, string ExToOtherPlant, string Description, string BaseUnitofMeasure, string MaterialGroup,
     string MaterialSubGroup, string GrossWeight, string NetWeight, string WeightUni, string Volume, string VolumeUnit, string OldMaterailNo, string Size_Dimension,
               string Packeging_Material_Catg, string BatchManagmet, string ProductHierarchy, string DistributionChannel, string SalesOrg, string SalesUnit, string Division, string TaxClasification,
               string Item_Catg_Group, string LoomType, string RoomReady, string SubDivision, string NOS, string Availabilitycheck, string TransportaionGroup, string LoadingGroup,
               string ProfitCenter, string SalesOrderTax, string Material_Rebate_Rate, string Rebate_Catg, string Purchasing_Group, string OrderingUnit, string PurchaseOrderText,
               string MRPType, string MRP_Group, string ReoderPoint, string MRPController, string BackFlush, string Planned_Delivery_Time_In_Days, string In_House_Production_Time_In_Days,
               string Gr_Processing_Time_In_Days, string Safety_Stock, string Production_Unit_Of_Measure, string UnitOfIssue, string Prodsupervisor, string ProdScheduleProfile,
               string Storage_Location, string Under_Delivery_Tollerance, string Ove_Delivery_Tollerance, string TaskListUsage, string ValuationClass, string ValuationCategory,
               string ValuationType, string Packaging_Material_Categuory, string Packaging_Material_Type, string Allowed_Packaging_Weight,
               string AllowedPackagingWeightUnit, string AllowedPackagingVolme, string AllowedPackagingVolmeUnit, string ExcessWeightTolerance, string ExcessVolumnTolerance, string APPROVAL, string REVIEWER, string MDA,
                string ClosedBox, string CreatedBy, string DelFlag, string FormID)
            {
                try
                {
                    string procedure = "";


                    if (action == "Extanding")
                    {
                        procedure = "EXEC SP_SYS_ExtendingMaterialMaster";
                    }
                    else if (action == "New")
                    {
                        procedure = "EXEC SP_SYS_MaterialMaster3";
                    }
                    else if (action == "Changing")
                    {
                        procedure = "EXEC SP_SYS_ExtendingChangeMaster";
                    }


                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "" + procedure.ToString() + "" + " @MeterialNo  ='" + MeterialNo + "', " +
             " @MaterialType  ='" + MaterialType + "', " +
            " @SAPMaterialCode ='" + SAPMaterialCode + "', " +
            " @Plant ='" + Plant + "', " +
             " @ExToOtherPlant ='" + ExToOtherPlant + "', " +
            " @Description ='" + Description + "', " +
            " @BaseUnitofMeasure ='" + BaseUnitofMeasure + "', " +
            " @MaterialGroup='" + MaterialGroup + "', " +
            " @MaterialSubGroup ='" + MaterialSubGroup + "', " +
            " @GrossWeight ='" + GrossWeight + "', " +
            " @NetWeight ='" + NetWeight + "', " +
            " @WeightUni ='" + WeightUni + "', " +
            " @Volume ='" + Volume + "', " +
            " @VolumeUnit ='" + VolumeUnit + "', " +
            " @OldMaterailNo ='" + OldMaterailNo + "', " +
            " @Size_Dimension ='" + Size_Dimension + "', " +
            " @Packeging_Material_Catg ='" + Packeging_Material_Catg + "', " +
            " @BatchManagmet ='" + BatchManagmet + "', " +
            " @ProductHierarchy ='" + ProductHierarchy + "', " +
            " @DistributionChannel ='" + DistributionChannel + "', " +
            " @SalesOrg ='" + SalesOrg + "', " +
            " @SalesUnit ='" + SalesUnit + "', " +
            " @Division ='" + Division + "', " +
            " @TaxClasification ='" + TaxClasification + "', " +
            " @Item_Catg_Group ='" + Item_Catg_Group + "', " +
            " @LoomType ='" + LoomType + "', " +
            " @RoomReady ='" + RoomReady + "', " +
            " @SubDivision ='" + SubDivision + "', " +
            " @NOS ='" + NOS + "', " +
            " @Availabilitycheck ='" + Availabilitycheck + "', " +
            " @TransportaionGroup ='" + TransportaionGroup + "', " +
            " @LoadingGroup ='" + LoadingGroup + "', " +
            " @ProfitCenter ='" + ProfitCenter + "', " +
            " @SalesOrderTax ='" + SalesOrderTax + "', " +
            " @Material_Rebate_Rate ='" + Material_Rebate_Rate + "', " +
            " @Rebate_Catg ='" + Rebate_Catg + "', " +
            " @Purchasing_Group ='" + Purchasing_Group + "', " +
            " @OrderingUnit ='" + OrderingUnit + "', " +
            " @PurchaseOrderText ='" + PurchaseOrderText + "', " +
            " @MRPType ='" + MRPType + "', " +
            " @MRP_Group ='" + MRP_Group + "', " +
            " @ReoderPoint ='" + ReoderPoint + "', " +
            " @MRPController ='" + MRPController + "', " +
            " @BackFlush ='" + BackFlush + "', " +
            " @Planned_Delivery_Time_In_Days ='" + Planned_Delivery_Time_In_Days + "', " +
            " @In_House_Production_Time_In_Days ='" + In_House_Production_Time_In_Days + "', " +
            " @Gr_Processing_Time_In_Days ='" + Gr_Processing_Time_In_Days + "', " +
            " @Safety_Stock ='" + Safety_Stock + "', " +
            " @Production_Unit_Of_Measure ='" + Production_Unit_Of_Measure + "', " +
            " @UnitOfIssue ='" + UnitOfIssue + "', " +
            " @Prodsupervisor ='" + Prodsupervisor + "', " +
            " @ProdScheduleProfile ='" + ProdScheduleProfile + "', " +
            " @Storage_Location ='" + Storage_Location + "', " +
            " @Under_Delivery_Tollerance ='" + Under_Delivery_Tollerance + "', " +
            " @Ove_Delivery_Tollerance ='" + Ove_Delivery_Tollerance + "', " +
            " @TaskListUsage ='" + TaskListUsage + "', " +
            " @ValuationClass ='" + ValuationClass + "', " +
            " @ValuationCategory ='" + ValuationCategory + "', " +
            " @ValuationType ='" + ValuationType + "', " +
            " @Packaging_Material_Categuory ='" + Packaging_Material_Categuory + "', " +
            " @Packaging_Material_Type ='" + Packaging_Material_Type + "', " +
            " @Allowed_Packaging_Weight ='" + Allowed_Packaging_Weight + "', " +
            " @AllowedPackagingWeightUnit ='" + AllowedPackagingWeightUnit + "', " +
            " @AllowedPackagingVolme ='" + AllowedPackagingVolme + "', " +
            " @AllowedPackagingVolmeUnit ='" + AllowedPackagingVolmeUnit + "', " +
            " @ExcessWeightTolerance ='" + ExcessWeightTolerance + "', " +
            " @ExcessVolumnTolerance ='" + ExcessVolumnTolerance + "', " +
            " @APPROVAL ='" + APPROVAL + "', " +
            " @REVIEWER ='" + REVIEWER + "', " +
            " @MDA ='" + MDA + "', " +
            " @ClosedBox ='" + ClosedBox + "', " +
            " @CreatedBy ='" + CreatedBy + "', " +
            " @DelFlag ='" + DelFlag + "', " +
            " @FormID ='" + FormID + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    ds.Clear();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Messsage");

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            //////////////////////////////////////////
            public DataSet DeleteMaterialMaster(string TransactionID, string SAPMaterialCode, string CreatedBy, string DelFlag, string APPROVAL, string REVIEWER, string MDA, string FormID)
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "EXEC SP_SYS_DeleteMaster" + " @TransactionMain  ='" + TransactionID + "', " +
            " @SAPMaterialCode ='" + SAPMaterialCode + "', " +
                        " @CreatedBy ='" + CreatedBy + "', " +
            " @DelFlag ='" + DelFlag + "', " +
            " @APPROVAL ='" + APPROVAL + "', " +
            " @REVIEWER ='" + REVIEWER + "', " +
            " @MDA ='" + MDA + "', " +
            " @FormID ='" + FormID + "'";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Messsage");

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            /////////////////////////////////////////
            public DataSet CreateMaterialMasterConversion(string MeterialNo, string AltUnitOfMeasureCode, string Numerator, string Denumerator, string Lenght, string Width
        , string height, string UOM)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"INSERT INTO [dbo].[tblAltUnitOfMeasure]
           ([TransactionID]
           ,[AltUnitOfMeasureCode]
           ,[Numerator]
           ,[Denumerator]
           ,[Lenght]
           ,[Width]
           ,[height]
           ,[UOM])
     VALUES
          (
        '" + MeterialNo.ToString() + "'," +
               "'" + AltUnitOfMeasureCode.ToString() + "', " +
                    "'" + Numerator.ToString() + "', " +
                    "'" + Denumerator.ToString() + "', " +
                    "'" + Lenght.ToString() + "', " +
                    "'" + Width.ToString() + "', " +
                    "'" + height.ToString() + "', " +
                    "'" + UOM.ToString() + "')";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("TransactionID", MeterialNo.ToString());
                    cmd.Parameters.AddWithValue("AltUnitOfMeasureCode", AltUnitOfMeasureCode.ToString());
                    cmd.Parameters.AddWithValue("Numerator", Numerator.ToString());
                    cmd.Parameters.AddWithValue("Denumerator", Denumerator.ToString());
                    cmd.Parameters.AddWithValue("Lenght", Lenght.ToString());
                    cmd.Parameters.AddWithValue("Width", Width.ToString());
                    cmd.Parameters.AddWithValue("height", height.ToString());
                    cmd.Parameters.AddWithValue("UOM", UOM.ToString());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;


            }

            public DataSet UpdateMaterial(string MetrialNo, string SAPCode, string MLock)
            {
                try
                {

                    cmd.CommandText = "EXEC SP_UpdateMaterial" + " @TransactionID  ='" + MetrialNo.ToString() + "', " +
                           " @Materiallock ='" + MLock.ToString() + "', " +
                           " @SAPCode ='" + SAPCode.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet InsertsysApplicationStatus(string FormID, string TransactionID, string HierachyCategory,
             string RoughtingUserID, string Status, string Remarks)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";

                    cmd.CommandText = "";
                    cmd.CommandText = "SP_InsertsysApplicationStatus";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                    cmd.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                    cmd.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                    cmd.Parameters.AddWithValue("@RoughtingUserID", RoughtingUserID.ToString());
                    cmd.Parameters.AddWithValue("@Status", Status.ToString());
                    cmd.Parameters.AddWithValue("@Remarks", Remarks.ToString());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet UpdatesysApplicationStatus(string FormID, string TransactionID, string HierachyCategory,
                string RoughtingUserID, string Status, string Remarks, string TransferredTo, string SerialNo, string Sequance)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";

                    cmd.CommandText = "";
                    cmd.CommandText = @"update sysworkflow set Status = @Status, StatusDateTime = @StatusDateTime ,TransferredTo = @TransferredTo
                 where FormID = @FormID and TransactionID = @TransactionID  and SerialNo = @SerialNo and RoughtingUserID like @RoughtingUserID and Sequance = @Sequance";//SP_InsertsysApplicationStatus
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                    cmd.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                    cmd.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                    cmd.Parameters.AddWithValue("@RoughtingUserID", RoughtingUserID.ToString() + '%');
                    cmd.Parameters.AddWithValue("@Status", Status.ToString());
                    cmd.Parameters.AddWithValue("@Remarks", Remarks.ToString());
                    cmd.Parameters.AddWithValue("@StatusDateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TransferredTo", TransferredTo.ToString());
                    cmd.Parameters.AddWithValue("@SerialNo", TransferredTo.ToString());
                    cmd.Parameters.AddWithValue("@Sequance", Sequance.ToString());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }



            public DataSet InsertsysApplicationStatusSpecificPerson(string FormID, string TransactionID, string HierachyCategory,
             string RoughtingUserID, string Status, string Remarks)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";

                    cmd.CommandText = "";
                    cmd.CommandText = @"INSERT INTO sysApplicationStatus (FormID,TransactionID,HierachyCategory,RoughtingUserID,DateTime,Status,Remarks) VALUES
                                     (@FormID1, @TransactionID1,@HierachyCategory1,@RoughtingUserID1,@Date1,@Status1,@Remarks1)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@FormID1", FormID.ToString());
                    cmd.Parameters.AddWithValue("@TransactionID1", TransactionID.ToString());
                    cmd.Parameters.AddWithValue("@HierachyCategory1", HierachyCategory.ToString());
                    cmd.Parameters.AddWithValue("@RoughtingUserID1", RoughtingUserID.ToString());
                    cmd.Parameters.AddWithValue("@Date1", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@Status1", Status.ToString());
                    cmd.Parameters.AddWithValue("@Remarks1", Remarks.ToString());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            //////////////////////////////////////////////////////////////Meterial Master Insertion End////////////////////////////////////////////////////

            public DataSet GetHarachy(string user_name, string HID)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select top(1) TransactionID,RoughtingUserID, HierachyCategory from [sysWorkFlow]
                RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + HID + "'  order by  HierachyCategory asc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "HID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet GetHarachyPettyCash(string user_name, string HID, string FormID)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select top(1) TransactionID,RoughtingUserID, HierachyCategory from [sysWorkFlow]
                RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + HID + "' and FormID = '" + FormID + "'   order by  HierachyCategory asc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "HID");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindgetDeleteList(string TransactionId)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec getDeleteList" + " @TransactionID ='" + TransactionId + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindgetDeleteList");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindsysApplicationStatus(string TransactionId, string FormId)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_BindsysApplicationStatus" + " @TransactionID ='" + TransactionId + "', " +
                         " @FormID='" + FormId + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "BindsysApplicationStatus");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }












            //////////////////////////////////////////////////////////Vendor Master///////////////////////////////////////////////////////////////////////////////////////
            public DataSet BindShortKey()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    // cmd.CommandText = "SELECT SortKeyNo,SortKeyDescription as Description FROM tblVMSortKey";
                    cmd.CommandText = "SELECT SortKeyNo,Convert(varchar,SortKeyNo)+ ' ' + SortKeyDescription  AS Description  FROM tblVMSortKey";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMSortKey");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindReconAccount()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT ReconAccountNo,Convert(varchar,ReconAccountNo)+ ' ' + ReconAccountDescription  AS Description  FROM tblVMReconAccount";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMReconAccount");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindTermsofpayment()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Termsofpayment,Termsofpayment + ' ' + TermsofpaymentDes as  Description FROM tblVMTermsofpayment";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMTermsofpayment");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindWHTaxType()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT WHTaxType,WHTaxType + ' ' + WHTaxTypeDes as  Description FROM tblVMWHTaxType";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMWHTaxType");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindAccountGroup()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Accountgroup,Accountgroup + ' ' + Accountgroupdescription as  Description FROM tblVMAccountGroup";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMAccountGroup");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet BindPurchasingOrg()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT PurchasingOrganization,PurchasingOrganization + ' ' + PurchasingOrganizationDes as  Description FROM tblVMPurchasingOrganization";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblVMPurchasingOrganization");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet getPettyCashDetail(string TransID)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"SELECT TransactionID as [Form ID] ,ChequeNo as [Cheque No],convert(varchar,cast(Amount as money),1) as Amount,Description,FileName as [File Name] ,Replace(CreatedBy, '.' ,' ')  as [Created By]
,CONVERT(VARCHAR(10),CreatedDateTime,103) as Date FROM tbl_FI_PettyCash  where TransactionID =  '" + TransID.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblPettyCash");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet getDCWDetail(string TransID)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"SELECT TransactionID as [Form ID],FileName as [File Name],Description,CreatedBy as [Created By],CreatedDateTime As [Created Date]
  FROM tbl_FI_DeliveryChallanWorkflow where TransactionID = '" + TransID.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_FI_DeliveryChallanWorkflow");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet getIWFDetail(string TransID)
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = @"SELECT TransactionID as [Form ID],FileName,CreatedBy,CreatedDateTime As [Created Date]
  FROM tbl_FI_InvoiceWorkflow where TransactionID  = '" + TransID.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_FI_InvoiceWorkflow");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            /////////////////////////////////////////////////////Vendor Master///////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////Secuirty/////////////////////////////////////////////////////////
            public DataSet AllowForms(string UserName, string FormName)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = "Exec SP_AllowForms" + " @User_Name ='" + UserName + "', " +
                            " @Form_Name ='" + FormName + "'";
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

            /////////////////////////////////////////////////////////////Email Method/////////////////////////////////////////////////////////

            public DataSet MailForwardUserToReviwer(string TransactionNo, string FormID)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT top(1)[FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] 
        FROM [dbo].[sysWorkFlow] b left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory = 3 and FormId= '" + FormID.ToString() + "'" +
           "and TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardUserToReviwer");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            //Update By Adnan khan
            public DataSet MailForwardUserToApprover(string TransactionNo, string FormID)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT top(1)[FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] 
        FROM [dbo].[sysWorkFlow] b left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory = 2 and FormId= '" + FormID.ToString() + "'" +
           "and TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardUserToApprover");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardFormApprover(string UserID, string TransactionNo, string FormID)
            {
                try
                {
                    string cmd = @"SELECT top(1)[FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] 
                        FROM [dbo].[sysWorkFlow] b left outer  join tbluser a on a.user_name=b.RoughtingUserID 
                        where b.Sequance > (select max(Sequance)  from [sysWorkFlow] where RoughtingUserID ='" + UserID.ToString() + "'  and TransactionID = '" + TransactionNo.ToString() + "' and  FormId= '" + FormID.ToString() + "' ) and  b.FormId= '" + FormID.ToString() + "' and b.HierachyCategory = 2" +
        " and b.TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory asc, Sequance asc";

                    //                string cmd = @"SELECT top(1)[FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,
                    //                                [HierachyCategory],[RoughtingUserID],[Sequance] 
                    //                                FROM [dbo].[sysWorkFlow] b left outer  join tbluser a on a.user_name=b.RoughtingUserID 
                    //                                where  RoughtingUserID > '" + UserID.ToString() + "'  and TransactionID = '" + TransactionNo.ToString() + "'" +
                    //                                " and  FormId= '" + FormID.ToString() + "' and b.HierachyCategory = 2  order by b.HierachyCategory asc, Sequance asc";


                    SqlDataAdapter adpt = new SqlDataAdapter(cmd, conn);
                    adpt.Fill(ds, "MailForwardFormApprover");
                    if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                    {
                        return ds;
                    }
                    else
                    {
                        string comtRM = @"SELECT [FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance]FROM [dbo].[sysWorkFlow] b 
                left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory in ('3','4')  and  FormId= '" + FormID.ToString() + "' and  TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                        SqlDataAdapter adpt1 = new SqlDataAdapter(comtRM, conn);
                        adpt1.Fill(ds, "MailForwardFormApprover");
                    }
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToUserOnRejection(string TransactionNo, string FormID)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT top(1)[FormID],[TransactionID],[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] FROM [dbo].[sysWorkFlow] b 
                               left outer  join tbluser a on a.user_name=b.RoughtingUserID where b.HierachyCategory = 1 and b.FormId= '" + FormID.ToString() +
                                   "' and TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardToUserOnRejection");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToAllFromMDA(string TransactionNo, string FormID, string HierachyCategory)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT [FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] FROM [dbo].[sysWorkFlow] b 
                               left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory not in ('" + HierachyCategory.ToString() + "') and TransactionID= '" + TransactionNo.ToString() +
                                   "' and  FormId= '" + FormID.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardToAllFromMDA");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet MailForwardFromReviwerToMDA(string TransactionNo, string FormID, string HierachyCategory)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT top(1)[FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] 
                     FROM [dbo].[sysWorkFlow] b left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory = '" + HierachyCategory.ToString() + "'   and b.FormId= '" + FormID.ToString() + "'" +
                         " and TransactionID= '" + TransactionNo.ToString() + "' order by b.HierachyCategory desc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardToAllFromMDA");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToUserToMDA(string TransactionNo, string FormID, string HierachyCategory)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT [FormID],TransactionID,[CreatedBy],Replace(isnull(a.DisplayName,b.RoughtingUserID), '.' ,' ') as user_name,a.user_email,[HierachyCategory],[RoughtingUserID],[Sequance] FROM [dbo].[sysWorkFlow] b 
                               left outer  join tbluser a on a.user_name=b.RoughtingUserID where  b.HierachyCategory = '" + HierachyCategory.ToString() + "' and TransactionID= '" + TransactionNo.ToString() +
                                   "' and  FormId= '" + FormID.ToString() + "' order by b.HierachyCategory asc,b.Sequance asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardToUserToMDA");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToSpecificPerson(string FormID)
            {
                try
                {
                    ds.Clear();
                    string com = @"SELECT distinct s.user_email,s.DisplayName as user_name,w.RoughtingUserID,s.FormID,Status FROM tbl_EmailToSpecificPerson as S
                                LEFT OUTER JOIN tbluser U ON S.user_name = u.user_name 
                                LEFT OUTER JOIN sysWorkFlow W ON S.user_name = W.RoughtingUserID 
                                 where S.FormID = '" + FormID.ToString() + "' and S.Status = '0'";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "tbl_EmailToSpecificPerson");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToAdminstrator(string UserName)
            {
                try
                {
                    ds.Clear();
                    string com = @"select user_name,user_email,DisplayName from tbluser where user_name = '" + UserName.ToString() + "'";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                    adpt.Fill(ds, "MailForwardToAdminstrator");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet MailForwardToForwarder(string UserID)
            {
                try
                {
                    string cmd = @"select * from tbluser where user_name = '" + UserID + "'";

                    SqlDataAdapter adpt = new SqlDataAdapter(cmd, conn);
                    adpt.Fill(ds, "MailForwardToForwarder");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            /////////////////////////////////////////////////////////////Email Method/////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////Customer Master///////////////////////////////////////////////////////////////////////////////////////

            public DataSet BindAccountGrp()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    //cmd.CommandText = "SELECT Accountgroup,Accountgroup + ' ' + Accountgroupdescription as  Description FROM tblVMAccountGroup";
                    cmd.CommandText = "select * from tblCMAccountGrp";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMAccountGrp");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindSalesOrganization()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT SalesOrganization,SalesOrganization + ' ' + SalesOrganizationDes as  Description FROM tblCMSalesOrganization";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMSalesOrganization");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindDistributionChannl()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT DistributionChannel,DistributionChannel + ' ' + DistributionChannelDes as  Description FROM tblCMDistributionChannel";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMDistributionChannel");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet BindDivision()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Division,Division + ' ' + DivisionDes as  Description FROM tblCMDivision";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMDivision");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindCountry()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Country,Country + ' ' + CountryDes as  Description FROM tblCMCountry";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMCountry");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindRegion()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Region,Region + ' ' + RegionDes as  Description FROM tblCMRegion";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMRegion");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet BindReconAccnt()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT ReconAccount,ReconAccount + ' ' + ReconAccountDes as  Description FROM tblCMReconAccount";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMReconAccount");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindTermsofpaymnt()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT TermsofPayment,TermsofPayment + ' ' + TermsofPaymentDes as  Description FROM tblCMTermsofPayment";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMTermsofPayment");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindPaymentBlock()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT PaymentBlock,PaymentBlock + ' ' + PaymentBlockDes as  Description FROM tblCMPaymentBlock";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMPaymentBlock");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindSalesdistrict()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT SalesDistrict,SalesDistrict + ' ' + SalesDistrictDes as  Description FROM tblCMSalesDistrict";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMSalesDistrict");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindCurrency()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Currency,Currency + ' ' + CurrencyDes as  Description FROM tblCMCurrency";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMCurrency");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet BindCustpricproc()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Custpricproc,Custpricproc + ' ' + CustpricprocDes as  Description FROM tblCMCustPricProc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMCustPricProc");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet BindIncoterms()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Incoterms,Incoterms + ' ' + IncotermsDes as  Description FROM tblCMIncoterms";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMIncoterms");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }



            public DataSet BindTax()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT Tax,Tax + ' ' + TaxDes as  Description FROM tblCMTax ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblCMTax");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindCostCenter()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select CostCentercode, CostCentercode + ' ' + CostCenterDesc as CostCenterDesc from tbl_AssetCostCenter";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_AssetCostCenter");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet UpdateAssetNo(string TransactionID, string AssetNo)
            {
                try
                {

                    cmd.CommandText = "EXEC SP_UpdateAssetNo" + " @TransactionID  ='" + TransactionID.ToString() + "', " + " @AssetNo ='" + AssetNo.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindLocation()
            {
                try
                {
                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select CurrentLocationcode, CurrentLocationcode + ' ' + CurrentLocationdesc as CurrentLocationdesc from tbl_Assetlocation";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tbl_Assetlocation");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet BindAssetStorageLocation()
            {
                try
                {

                    ds.Clear();
                    cmd.CommandText = "";
                    cmd.CommandText = "select StorageLocationcode, StorageLocationcode + '  ' + Description as Description from tblstoragelocation";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "tblstoragelocation");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet GetHarachyCustomerMaster(string user_name, string HID, string FormID)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select top(1) SerialNo,Status,TransactionID,RoughtingUserID, HierachyCategory,Sequance,CreatedBy from [sysWorkFlow]
                RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + HID + "' and FormID = '" + FormID + "'   order by  HierachyCategory asc,SerialNo desc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "HID");

                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet controlFowardControl(string FormID, string TID, string HirCtg, string Status, string UserName)
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                {
                    using (SqlCommand cmdInsert = new SqlCommand())//
                    {
                        try
                        {
                            cmd.CommandText = "";
                            cmd.CommandText = @"select top(1) RoutingID from tbl_SysHierarchyControl where FormID = '" + FormID.ToString() + "' and TransactionID = '" + TID.ToString() + "' and HierarchyCateguory = '" + HirCtg.ToString() + "'" +
                             "and Status = '" + Status.ToString() + "'  and RoutingID LIKE '%" + UserName + "%'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = conn;
                            adp.SelectCommand = cmd;
                            adp.Fill(ds, "controlFowardControl");

                        }
                        catch (Exception ex)
                        { ex.ToString(); }
                        finally
                        { conn.Close(); }
                        return ds;
                    }
                }
            }
            public DataSet GetHarachyNextData(string user_name, string TransactionID, string FormID, string HID)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select * from [sysWorkFlow]
                RoughtingUserID where TransactionID = '" + TransactionID + "' and FormID ='" + FormID + "' and SerialNo > (select SerialNo from [sysWorkFlow] RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + TransactionID + "' and FormID = '" + FormID + "') order by HierachyCategory,Sequance asc";


                    //cmd.CommandText = @"select * from [sysWorkFlow]
                    //RoughtingUserID where TransactionID = '" + TransactionID + "' and FormID = '" + FormID + "' and Sequance >= (select Sequance from [sysWorkFlow] RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + TransactionID + "' and FormID = '" + FormID + "' ) and HierachyCategory = '" + HID + "' order by HierachyCategory,Sequance asc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "GetHarachyNextData");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet GetStatusHierachyCategory(string user_name, string TransID, string FormID)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select top(1) TransactionID,RoughtingUserID, HierachyCategory,Status from sysApplicationStatus
                                    where RoughtingUserID = '" + user_name.ToString() + "' and TransactionID = '" + TransID.ToString() + "' and FormID = '" + FormID.ToString() + "'   order by  HierachyCategory asc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "StatusHierachyCategory");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }
            public DataSet GetStatusHierachyCategoryControl(string user_name, string TransID, string FormID, string HierarchyCateguory, string Serial, string Status)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"sysControls";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@RoughtingUserID", user_name.ToString());
                    cmd.Parameters.AddWithValue("@TransactionID", TransID.ToString());
                    cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                    cmd.Parameters.AddWithValue("@HierachyCategory", HierarchyCateguory.ToString());
                    adp.Fill(ds, "tbl_SysHierarchyControl");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

            public DataSet GetStatusHierachyCategoryControl(string user_name, string TransID, string FormID, string SerialNo, string Sequance)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"select top(1) TransactionID,RoughtingUserID, HierachyCategory,Status from sysworkflow
                                    where RoughtingUserID = '" + user_name.ToString() + "' and TransactionID = '" + TransID.ToString() + "' and FormID = '" + FormID.ToString() + "' and SerialNo = '" + SerialNo.ToString() + "' and Sequance = '" + Sequance.ToString() + "'  order by SerialNo asc,Sequance desc";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "GetStatusHierachyCategoryControl");
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }


            public DataSet insertEmail(string user_name, string UserEmail, string EmailSubject, string Body, string datetime, string sessionuser)
            {
                try
                {
                    cmd.CommandText = "";
                    cmd.CommandText = @"INSERT INTO tblEmailContentSending
           (UserName,UserEmail,EmailSubject,EmailBody,DateTime,SessionUser) VALUES ('" + user_name.ToString() + "','" + UserEmail.ToString() + "','" + EmailSubject.ToString() + "','" + Body.ToString() + "','" + datetime.ToString() + "','" + sessionuser.ToString() + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                { ex.ToString(); }
                finally
                { conn.Close(); }
                return ds;
            }

    }
}