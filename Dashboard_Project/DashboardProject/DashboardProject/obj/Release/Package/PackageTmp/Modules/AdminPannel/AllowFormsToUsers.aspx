<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AllowFormsToUsers.aspx.cs" Inherits="ITLDashboard.Modules.AdminPannel.AllowFormsToUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <%--<link href="../../Content/bootstrap.min.css" rel="stylesheet" />--%>

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>





    <script type="text/javascript">
        function pageLoad() {
            $('[id*=ddlUserName]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        }

    </script>

    <script type="text/javascript">
        function ConfirmOnDelete() {
            if (confirm("Do you really want to delete?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(function () {

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA],[id$=btnApproved]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();
        });
    </script>



    <script type="text/javascript">
        $(function () {
            /*
             * this swallows backspace keys on any non-input element.
             * stops backspace -> back
             */
            var rx = /INPUT|SELECT|TEXTAREA/i;

            $(document).bind("keydown keypress", function (e) {
                if (e.which == 8) { // 8 == backspace
                    if (!rx.test(e.target.tagName) || e.target.disabled || e.target.readOnly) {
                        e.preventDefault();
                    }
                }
            });
        });


    </script>

    <style type="text/css">
        input[type=checkbox], input[type=radio] {
            margin: 10px 9px 0;
            margin-top: 1px 9px;
            line-height: normal;
        }
    </style>

    <style type="text/css">
        .fixed-panel {
            min-height: 10%;
            max-height: 10%;
            overflow-y: scroll;
        }

        .form-control {
        }

        .footable {
        }

        .btn-primary {
            height: 36px;
        }
        .pagination-ys {
    /*display: inline-block;*/
    padding-left: 0;
    margin: 20px 0;
    border-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td {
    display: inline;
}
 
.pagination-ys table > tbody > tr > td > a,
.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;
    color: #dd4814;
    background-color: #ffffff;
    border: 1px solid #dddddd;
    margin-left: -1px;
}
 
.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;    
    margin-left: -1px;
    z-index: 2;
    color: #aea79f;
    background-color: #f5f5f5;
    border-color: #dddddd;
    cursor: default;
}
 
.pagination-ys table > tbody > tr > td:first-child > a,
.pagination-ys table > tbody > tr > td:first-child > span {
    margin-left: 0;
    border-bottom-left-radius: 4px;
    border-top-left-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td:last-child > a,
.pagination-ys table > tbody > tr > td:last-child > span {
    border-bottom-right-radius: 4px;
    border-top-right-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td > a:hover,
.pagination-ys table > tbody > tr > td > span:hover,
.pagination-ys table > tbody > tr > td > a:focus,
.pagination-ys table > tbody > tr > td > span:focus {
    color: #97310e;
    background-color: #eeeeee;
    border-color: #dddddd;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" style="width: 100%; margin-top: 20px;">

        <div class="alert alert-success" id="sucess" runat="server" visible="false">
            <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="alert alert-danger" id="error" runat="server" visible="false">
            <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">User Authorization Form</p>

            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading"></div>
            <div class="panel-body">

                <span class="help-block"></span>

                <div class="row">
                    <div class="col-sm-3" runat="server" id="dvTransactionNo">
                        Transaction No
                                 <asp:Label ID="lblMaxTransactionNo" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="col-sm-3" runat="server" id="dvFormID" visible="false">
                        Form ID
                                 <asp:Label ID="lblMaxTransactionID" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                </div>
                <span class="help-block"></span>
                <%--<%# Eval("Lenght") %>--%>
            </div>

        </div>

        <div class="panel-group" id="dvGD" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Form Authorization
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-8">
                                Select Entry Type
                                 
                                     <asp:RadioButtonList ID="RBList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBList_SelectedIndexChanged">
                                         <asp:ListItem Value="tblForm_Restricted_Check">Form Restricted Check</asp:ListItem>
                                         <asp:ListItem Value="tblAllow_Forms">Allow Forms To Users</asp:ListItem>
                                     </asp:RadioButtonList>
                            </div>
                             <div class="col-sm-1"></div>
                              <div class="col-sm-3">                            
                                    Display by Form
                                     <asp:DropDownList ID="ddlSearchForm"  runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSearchForm_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>

                        <div class="row" id="dvAllowForm" runat="server" visible="false">
                            <div class="col-sm-4">
                                User Name 
                                   <asp:ListBox ID="ddlUserName" runat="server" CssClass="form-control"></asp:ListBox>
                            </div>
                            <div class="col-sm-4">
                                Form Name
                                    <asp:DropDownList ID="ddlFormName" runat="server" CssClass="form-control" CausesValidation="True"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Status
                                   <br />
                                <asp:RadioButtonList ID="rbStatus" runat="server" CausesValidation="True" Height="34px" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">Active</asp:ListItem>
                                    <asp:ListItem>De-Active</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <span class="help-block"></span>

                        <div class="row" id="dvRestricted" runat="server" visible="false">
                            <div class="col-sm-3">
                                Form Name
                                    &nbsp;<asp:DropDownList ID="ddlFormNameOther" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                Restricted
                      Restricted
                                <br />
                                <asp:RadioButtonList ID="rbRestricted" runat="server" CausesValidation="True" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Allow To Every One</asp:ListItem>
                                    <asp:ListItem Value="1">Allow To Specific Person</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:HiddenField ID="hfCode" runat="server" />
                                <asp:GridView ID="grdData" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False" AllowPaging="True" OnPageIndexChanging="grdData_PageIndexChanging" OnSelectedIndexChanging="grdData_SelectedIndexChanging" OnRowDeleting="grdData_RowDeleting" OnRowDataBound="grdData_RowDataBound">
                                   <PagerStyle CssClass="pagination-ys" />
                                      <EmptyDataTemplate>
                                       No records found for the search criteria.
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkSelect" Text="Select" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="delete" Text="Delete" OnClientClick="return ConfirmOnDelete();"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                            <HeaderStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFormName" Text='<%# Bind("FormName") %>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle Width="14%" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Form ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFormID" Text='<%# Bind("Form_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="14%" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="14%" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblActive" Text='<%# Bind("Active") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <%--<%# Eval("Lenght") %>--%>
    </div>

    <%--<%# Eval("Lenght") %>--%>

    <div class="panel panel-default" style="text-align: center;" runat="server" id="dvlbl">
        <div class="panel-body">
            <span class="help-block"></span>
            <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                <%--<%# Eval("Lenght") %>--%>
                <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
            </div>
        </div>
        <span class="help-block"></span>
    </div>


    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="grpSa" Width="100px" OnClick="btnUpdate_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
        <asp:Label ID="lbltest" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
    </div>

    <div class="panel-body fixed-panel">
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="grdWStatus" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False">
                    <EmptyDataTemplate>
                        No Data</td>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="User Name" SortExpression="User_name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("RoughtingUserID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="category">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbcategory" Text='<%# Bind("HierarchyCat") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Time" SortExpression="DateTime">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateTime" Text='<%# Bind("DateTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
