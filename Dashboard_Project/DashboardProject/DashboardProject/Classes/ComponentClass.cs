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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet getUserDetail(string _UserNameID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_getUserDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        adp.SelectCommand = cmd;
                        cmd.Parameters.AddWithValue("@User_Name", _UserNameID.ToString());
                        adp.Fill(ds, "tbluser_DisplayName");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet getUserHOD(string UserName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet getPlantDistinct()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_getPlantDistinct";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

    


        public DataSet BindTCode()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindTCode";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindMovementtype";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindOrdertype()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindOrdertype";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet CheckSapID(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Check_SAPID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@user_name", "%" + username.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "SAPID");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet GetTransactionMaxPettyCash(string FORMID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                { //Changes
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
            }
        }

        public DataSet GetTransactionMax()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                { //Changes
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
            }
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_getSAPMaterialCode";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "SAPMaterialCode");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet createuser(string UserId, string username, string pass, string email, string designation, string dept)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet BindDropDownDept()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet BindDropDownDesignation()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataTable Userlogin(string user_id, string passcode)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        // MASTER DETAIL METERIAL COMBO//
        public DataSet BindMaterialType()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataTable BindMaterialMaster()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet BindStorageLocation()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_StorageLocation";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindPlant()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindPlant";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindPlantMtype(string MaterialTypePlant)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindPlantMtype";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@MaterialType", "%" + MaterialTypePlant.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindPlantMtype");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindMaterialgroup()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Materialgroup";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindMaterialgroupMtype(string MaterialType)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindMaterialgroupMtype";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@MaterialType", "%" + MaterialType.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindMaterialgroupMtype");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindBaseUnitOfMeasure()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BaseUnitOfMeasure";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BaseUnitOfMeasure");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindBaseUnitOfMeasureMTYPE(string MTYPEBOM)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindBaseUnitOfMeasureMTYPE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@MaterialType", "%" + MTYPEBOM.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindBaseUnitOfMeasureMTYPE");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindLenght()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindLenght";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindUser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindUser";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindSplitValueation()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_ValuationType";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindProfitCenter()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_tblProfitCenter";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindValuationCategory()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_ValuationCategory";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindMRPtypeMTYPE(string MaterialType)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindMRPtypeMTYPE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@MaterialTypecode", "%" + MaterialType.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindMRPtypeMTYPE");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindSplitValueation(string ValuationCatg)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindSplitValueation";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@ValuationCatg", "%" + ValuationCatg.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindSplitValueation");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindValuationCategoryMTYPE(string MaterialTypePlant)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindValuationCategoryMTYPE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@MaterialTypecode", "%" + MaterialTypePlant.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "ValuationCategoryMTYPE");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindPurchasingGroup()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_PurchasingGroup";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindPurchasingGroupMTYPE(string MaterialTypePG)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindPurchasingGroupMTYPE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@MaterialTypecode", "%" + MaterialTypePG.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindPurchasingGroupMTYPE");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindMRPController()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_mrpController";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindLotSize()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_LotSize";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindMRPType()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_MRPType";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindMRPTypeMtype(string MaterialType)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindMRPtypeMTYPE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@MaterialTypecode", "%" + MaterialType.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindMRPTypeMtype");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindAvailabilitycheck()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Availabilitycheck";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindRebateCategoryRate()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_RebateCategoryRate";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }


        public DataSet BindPeriodIndicator()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_PeriodIndicator";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindStrategygroup()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Strategygroup";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindQMControlKey()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_QMControlKey";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindRate()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Rate";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindDistributionChannel()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_DistributionChannel";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindDeliveringplant()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Deliveringplant";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindLoadingGroup()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_LoadingGroup";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindSalesTax()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_SalesTax";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindValuationClass()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_ValuationClass";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindValuationClassMtype(string ValuationClassMtype)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindValuationClassMtype";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@Materialtypcode", "%" + ValuationClassMtype.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "BindValuationClassMtype");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindProdnsupervisor()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Prodnsupervisor";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindProdSchedProfile()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_ProdSchedProfile";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindTasklistusage()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_Tasklistusage";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet UpdatesysApplicationStatus(string FormID, string TransactionID, string HierachyCategory,
        string RoughtingUserID, string Status, string Remarks, string TransferredTo, string SerialNo, string Sequance)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }


        public DataSet InsertsysApplicationStatusSpecificPerson(string FormID, string TransactionID, string HierachyCategory,
        string RoughtingUserID, string Status, string Remarks)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        //////////////////////////////////////////////////////////////Meterial Master Insertion End////////////////////////////////////////////////////

        public DataSet GetHarachy(string user_name, string HID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_GetHarachy";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet GetHarachyPettyCash(string user_name, string HID, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_GetHarachyPettyCash";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@RoughtingUserID", "%" + user_name.ToString() + "%");
                        cmd.Parameters.AddWithValue("@ransactionID", "%" + HID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@FormID", "%" + FormID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "HID");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet BindgetDeleteList(string TransactionId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        public DataSet BindsysApplicationStatus(string TransactionId, string FormId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }
        //////////////////////////////////////////////////////////Vendor Master///////////////////////////////////////////////////////////////////////////////////////

        public DataSet BindShortKey()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        // cmd.CommandText = "SELECT SortKeyNo,SortKeyDescription as Description FROM tblVMSortKey";
                        cmd.CommandText = "SP_BindShortKey";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindReconAccount()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindReconAccount";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }
        public DataSet BindTermsofpayment()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindTermsofpayment";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindWHTaxType()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindWHTaxType";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindAccountGroup()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindAccountGroup";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet BindPurchasingOrg()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_BindPurchasingOrg";
                        cmd.CommandType = CommandType.StoredProcedure;
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
            }
        }

        public DataSet getPettyCashDetail(string TransID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_getPettyCashDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "tblPettyCash");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet getDCWDetail(string TransID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {

                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_getDCWDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "tbl_FI_DeliveryChallanWorkflow");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet getIWFDetail(string TransID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_getIWFDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "tbl_FI_InvoiceWorkflow");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        /////////////////////////////////////////////////////Vendor Master///////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////Secuirty/////////////////////////////////////////////////////////

        public DataSet AllowForms(string UserName, string FormName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
        }

        /////////////////////////////////////////////////////////////Secuirty/////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////Email Method/////////////////////////////////////////////////////////

        public DataSet MailForwardUserToReviwer(string TransactionNo, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardUserToReviwer";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionNo.ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardUserToReviwer");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }
        //Update By Adnan khan
        public DataSet MailForwardUserToApprover(string TransactionNo, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "Exec SP_MailForwardUserToApprover" + " @TransactionID ='" + TransactionNo + "', " +
                                " @FormID ='" + FormID + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardUserToApprover");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardFormApprover(string UserID, string TransactionNo, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = "Exec SP_MailForwardFormApprover" + " @TransactionID ='" + TransactionNo + "', " +
                                " @UserID ='" + UserID + "', " +
                                " @FormID ='" + FormID + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardFormApprover");
                        if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                        {
                            return ds;
                        }
                        else
                        {
                            cmd.CommandText = "";
                            cmd.CommandText = "Exec SP_MailForwardFormApproverToMDAOrOthers" + " @TransactionID ='" + TransactionNo + "', " +
                                    " @FormID ='" + FormID + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = conn;
                            adp.SelectCommand = cmd;
                            adp.Fill(ds, "MailForwardFormApprover");
                        }
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }
        public DataSet MailForwardToUserOnRejection(string TransactionNo, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardToUserOnRejection";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionNo.ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToUserOnRejection");

                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardToAllFromMDA(string TransactionNo, string FormID, string HierachyCategory)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardToAllFromMDA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionNo.ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToAllFromMDA");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardFromReviwerToMDA(string TransactionNo, string FormID, string HierachyCategory)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardFromReviwerToMDA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionNo.ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToAllFromMDA");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardToUserToMDA(string TransactionNo, string FormID, string HierachyCategory)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardToUserToMDA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@TransactionID", TransactionNo.ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToUserToMDA");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardToSpecificPerson(string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardToSpecificPerson";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "tbl_EmailToSpecificPerson");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardToAdminstrator(string UserName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        ds.Clear();
                        cmd.CommandText = "SP_MailForwardToAdminstrator";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@Username", UserName.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToAdminstrator");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet MailForwardToForwarder(string UserID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {

                        ds.Clear();
                        cmd.CommandText = @"select * from tbluser where user_name = @UserID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@UserID", UserID.ToString());
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "MailForwardToForwarder");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        /////////////////////////////////////////////////////////////Email Method/////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////Customer Master///////////////////////////////////////////////////////////////////////////////////////

        public DataSet BindAccountGrp()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
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
            }
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

        public DataSet GetHarachyCustomerMaster(string _user_name, string _HID, string _FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())//
                {
                    try
                    {
                        cmdgetData.CommandText = "";
                        cmdgetData.CommandText = "SP_Get_HarcheyID";
                        cmdgetData.CommandType = CommandType.StoredProcedure;
                        cmdgetData.Connection = connection;
                        cmdgetData.Parameters.AddWithValue("@User_Name", _user_name.ToString());
                        cmdgetData.Parameters.AddWithValue("@TransactionID", _HID.ToString());
                        cmdgetData.Parameters.AddWithValue("@FormID", _FormID.ToString());
                        adp.SelectCommand = cmdgetData;
                        adp.Fill(ds, "HID");

                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
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
                        cmd.CommandText = @"SP_controlFowardControl";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@FormID", "%" + FormID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@HierarchyCateguory", "%" + HirCtg.ToString() + "%");
                        cmd.Parameters.AddWithValue("@Status", "%" + Status.ToString() + "%");
                        cmd.Parameters.AddWithValue("@RoutingID", "%" + UserName.ToString() + "%");
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsert = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_GetHarachyNextData";
                        //cmd.CommandText = @"select * from [sysWorkFlow]
                        //RoughtingUserID where TransactionID = '" + TransactionID + "' and FormID = '" + FormID + "' and Sequance >= (select Sequance from [sysWorkFlow] RoughtingUserID where RoughtingUserID like '" + user_name + "%' and TransactionID = '" + TransactionID + "' and FormID = '" + FormID + "' ) and HierachyCategory = '" + HID + "' order by HierachyCategory,Sequance asc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@RoughtingUserID", "%" + user_name.ToString() + "%");
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransactionID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@FormID", "%" + FormID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "GetHarachyNextData");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet GetStatusHierachyCategory(string user_name, string TransID, string FormID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsert = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_GetStatusHierachyCategory";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@RoughtingUserID", "%" + user_name.ToString() + "%");
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@FormID", "%" + FormID.ToString() + "%");
                        adp.SelectCommand = cmd;
                        adp.Fill(ds, "StatusHierachyCategory");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
        }

        public DataSet GetStatusHierachyCategoryControl(string user_name, string TransID, string FormID, string HierarchyCateguory, string Serial, string Status)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsert = new SqlCommand())//
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
            }
        }

        public DataSet GetStatusHierachyCategoryControl(string user_name, string TransID, string FormID, string SerialNo, string Sequance)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsert = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = @"SP_GetStatusHierachyCategoryControl";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        adp.SelectCommand = cmd;
                        cmd.Parameters.AddWithValue("@RoughtingUserID", "%" + user_name.ToString() + "%");
                        cmd.Parameters.AddWithValue("@TransactionID", "%" + TransID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@FormID", "%" + FormID.ToString() + "%");
                        cmd.Parameters.AddWithValue("@SerialNo", "%" + SerialNo.ToString() + "%");
                        cmd.Parameters.AddWithValue("@Sequance", "%" + Sequance.ToString() + "%");
                        adp.Fill(ds, "GetStatusHierachyCategoryControl");
                    }
                    catch (Exception ex)
                    { ex.ToString(); }
                    finally
                    { conn.Close(); }
                    return ds;
                }
            }
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