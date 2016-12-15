using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Security.Principal;
using System.Text;
using System.IO;
using System.Data;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{

    public DataTable GetUserDetails(ref DataTable dtData)
    {
        // string[] domains = new string[] { "LDAP://HO.com" };
       // string[] domains = new string[] { "LDAP://internationaltextile.com" };
        string[] domains = new string[] { "LDAP://HO.com", "LDAP://internationaltextile.com" };
        dtData.Columns.Add("Name");
        dtData.Columns.Add("mail");
        dtData.Columns.Add("displayname");
        dtData.Columns.Add("sn");
        dtData.Columns.Add("company");

        for (int i = 0; i < domains.Length; i++)
        {
            DirectoryEntry entry = new DirectoryEntry(domains[i]);
            DirectorySearcher dSearch = new DirectorySearcher(entry);
            dSearch.Filter = "(objectClass=user)";
           // DataTable  dtData = new DataTable();
               
            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                if (sResultSet.Properties["samaccountname"].Count > 0 && sResultSet.Properties["mail"].Count > 0
                    && sResultSet.Properties["displayname"].Count > 0)
                {
                    //ddlEmailApproval.Items.Add(new ListItem(sResultSet.Properties["samaccountname"][0].ToString(), sResultSet.Properties["mail"][0].ToString()));
                    //ddlEmailApproval.Items.Remove(ddlEmailApproval.Items.FindByValue(Session["User_Name"].ToString()));

                    //ddlEmailReviwer.Items.Add(new ListItem(sResultSet.Properties["samaccountname"][0].ToString(), sResultSet.Properties["mail"][0].ToString()));
                    //ddlEmailMDA.Items.Add(new ListItem(sResultSet.Properties["samaccountname"][0].ToString(), sResultSet.Properties["mail"][0].ToString()));
                    DataRow dr = dtData.NewRow();
                    dr[0] = sResultSet.Properties["samaccountname"][0].ToString();
                    dr[1] = sResultSet.Properties["mail"][0].ToString();
                    dr[2] = sResultSet.Properties["displayname"][0].ToString();
                    dr[3] = sResultSet.Properties["sn"][0].ToString();
                    dr[4] = sResultSet.Properties["company"][0].ToString();
                    dtData.Rows.Add(dr);
                   
                }

            }
           
        }
        return dtData;
    
	}
}