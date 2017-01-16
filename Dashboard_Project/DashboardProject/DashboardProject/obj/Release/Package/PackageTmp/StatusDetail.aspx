<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusDetail.aspx.cs" Inherits="ITLDashboard.StatusDetail" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src=".Scripts/bootstrap.min.js"></script>
    <%--<link href="../../Content/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Style/style.css" rel="stylesheet" />
    <link href="Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/footable.min.js"></script>

    <script type="text/javascript" src="https://www.draw.io/js/embed-static.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('table').footable();
        });

    </script>
    <style type="text/css">
        .fixed-panel {
            min-height: 10%;
            max-height: 10%;
            overflow-x: scroll;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
           <div class="container" style="margin-top: 20px; width:100%;">
    <div class="panel panel-default" id="pnlPC">
            <div class="panel-body">
                 
                <div class="row">
                          <div id="dvDetail" runat="server" class="col-sm-12 fixed-panel">
    <asp:GridView ID="grdWStatus" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
        <EmptyDataTemplate>
            No Data</td>
        </EmptyDataTemplate>
        <Columns>

            <asp:TemplateField HeaderText="Form ID" SortExpression="User_name">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblTransactionID" Text='<%# Bind("TransactionID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="15%" />
                <HeaderStyle Width="15%" />
            </asp:TemplateField>


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

                        <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("Remarks") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="14%" />
                <HeaderStyle Width="14%" />
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
                              </div>
    </div>
                </div>
        </div>
        </div>
    </form>
</body>
</html>
