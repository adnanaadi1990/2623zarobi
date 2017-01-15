<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MtypeAdminForm.aspx.cs" Inherits="ITLDashboard.Modules.AdminPannel.MtypeAdminForm" %>
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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "jQuery Dialog Popup",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
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

    <script type="text/javascript">
        function pageLoad() {
            $('[id*=txtPostalCode],[id*=txtcustomerCode]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
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
            $('[id*=grdData]').footable();
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">M-Type Entry Form</p>

            </div>
        </div>

        <div class="panel-group" id="dvGD" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    M-Type Entry Form
                    <asp:HiddenField ID="hdf" runat="server" />
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                          
                        </div>
          

                        
                        <span class="help-block"></span>

                        <div class="row" id="dvAllUser" runat="server">
                            <div class="col-sm-3">
                                M-Type
                                    &nbsp;<asp:DropDownList ID="ddlMtypegrd" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                           <div class="col-sm-3">
                                Panel Name
                                    <asp:TextBox ID="txtPanelName" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Panel Description
                                    <asp:TextBox ID="txtPanelDescription" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <br />
                                Status                                 
                                  <asp:CheckBox ID="CbStatus" runat="server" />
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <%--<%# Eval("Lenght") %>--%>

    </div>

    <%--<%# Eval("Lenght") %>--%>

    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="100px" OnClick="btnNew_Click"></asp:Button>
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="grpSa" Width="100px" OnClick="btnSave_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
        <asp:Label ID="lbltest" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
    </div>

    <div class="panel-body fixed-panel">
        <div class="row">
            <div class="col-sm-6">
            </div>
               <div class="col-sm-4">
                    <asp:DropDownList ID="ddlMtype" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                   </div>
             <div class="col-sm-2">
                   <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="grpSa" Width="80px" OnClick="btnSearch_Click" ></asp:Button>
            </div>
            </div>
          <span class="help-block"></span>
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="grdData" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False" OnSelectedIndexChanging="grdData_SelectedIndexChanging">
                    <EmptyDataTemplate>
                        No Data</td>
                    </EmptyDataTemplate>
                    <Columns>
                          <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkSelect" Text="Select" CommandName="Select" ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No" SortExpression="SNo">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSno" Text='<%# Bind("SNo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Type" SortExpression="MTYPE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMTYPE" Text='<%# Bind("MTYPE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Panel Name" SortExpression="PanelName">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPanelName" Text='<%# Bind("PanelName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Panel Description" SortExpression="PanelDescription">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPanelDescription" Text='<%# Bind("PanelDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Select Any Action</h4>
                </div>
                <div class="modal-body" style="height: 125px;">

                    <div class="col-sm-2"><b>Remarks</b></div>
                    <div class="col-sm-9" style="text-align: center;">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" Visible="False" CausesValidation="False"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
