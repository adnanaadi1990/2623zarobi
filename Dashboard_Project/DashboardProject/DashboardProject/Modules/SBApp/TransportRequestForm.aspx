<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TransportRequestForm.aspx.cs" Inherits="ITLDashboard.Modules.SBApp.TransportRequestForm" %>

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
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, document.URL);
        });

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
            $('[id$=btnSave],[id$=btnSubmit],[id$=btnApprover],[id$=Button1],[id$=btnSaveSubmit],[id$=btnReviewed]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=grdWStatus]').footable();
        });
    </script>

    <script type="text/javascript">

        /////////////////////////////////////////////////////////////////////////////////////////////////
        function pageLoad() {
            $('[id*=ddlTransportTo],[id*=ddlSAPID],[id*=ddlMovementType],[id*=ddlOrderType],[id*=ddlRoles]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });

        }
        var clicked = false;
        function AllowOneClick() {
            if (!clicked) {
                clicked = true;
                return true;
            }
            return false;
        }
        //}
    </script>
    <style type="text/css">
        input[type=checkbox], input[type=radio] {
            margin: 10px 9px 0;
            margin-top: 1px 9px;
            line-height: normal;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Transport Request Form</p>
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
                    Transport Request Form
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <%--                        <div class="row">
                            <div class="col-sm-4">
                                SAP ID 
                                   <asp:Label ID="txtSAPID" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-sm-4" runat="server" id="dvType">
                                Select any Type
                                   <asp:RadioButtonList ID="rbAction" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdAction_SelectedIndexChanged">
                                       <asp:ListItem Selected="True">T-Code</asp:ListItem>
                                       <asp:ListItem>Report</asp:ListItem>
                                   </asp:RadioButtonList>
                            </div>
                        </div>--%>
                        <span class="help-block"></span>

                        <div class="row" id="dvAllowForm" runat="server">
                            <div class="col-sm-4">
                                Applicable Area
                                     <asp:DropDownList ID="ddlApplicableArea" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlApplicableArea_SelectedIndexChanged">
                                         <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                         <asp:ListItem Value="MM">MM Material Management</asp:ListItem>
                                         <asp:ListItem Value="PP">PP Production Planing</asp:ListItem>
                                         <asp:ListItem Value="SD">SD Sales Distribution</asp:ListItem>
                                         <asp:ListItem Value="QM">QM Quality Management</asp:ListItem>
                                         <asp:ListItem Value="FICO">FICO (Financial Accounting) and CO (Controlling)</asp:ListItem>
                                     </asp:DropDownList>
                            </div>
                            <div class="col-sm-4" runat="server" id="dvTransportTo">
                                Transport To                     
                                 <asp:ListBox ID="ddlTransportTo" runat="server" CssClass="form-control" SelectionMode="Multiple" AppendDataBoundItems="True"></asp:ListBox>
                            </div>
                            <div class="col-sm-4" runat="server" id="dvTRNo">
                                 Transport No
                                   <asp:TextBox ID="TxtTRNo" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>

                        </div>
                         <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-8" runat="server" id="DvDes">
                                Description
                                   <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                        </div>
                         <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-8" runat="server" id="DvLT">
                                Long Text
                                   <asp:TextBox ID="txtLT" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                    </div>
                </div>
            </div>
        </div>

        <%--<%# Eval("Lenght") %>--%>

        <!-- Panel -->

        <div id="divEmail" runat="server" visible="true">
            <asp:Panel ID="pnlemail" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Email Approval
                    </div>
                    <div class="panel-body">
                        <div class="row" style="text-align: center;">

                            <div class="col-sm-3">
                                Head Of Deparment
                                       <asp:Label ID="lblHOD" runat="server" CssClass="form-control"></asp:Label>
                            </div>

                            <div class="col-sm-3">
                                Reviewer
                                        <asp:DropDownList ID="ddlReviewer" runat="server" CssClass="form-control" CausesValidation="True"></asp:DropDownList>
                            </div>

                            <div class="col-sm-3">
                                Function Consultant
                                       <asp:DropDownList ID="ddlFC" SelectionMode="Multiple" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                SAP Basis Administrator
                                        <asp:DropDownList ID="ddlEmailMDA" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="False" BackColor="AliceBlue"></asp:TextBox>
                        </div>
                    </div>
                    <span class="help-block"></span>
                    <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                        <%--<%# Eval("Numerator") %>--%>
                        <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                        <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                        <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                    </div>
                </div>
                <span class="help-block"></span>
            </div>
        </div>

        <div class="col-sm-12" style="text-align: center;">

            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
            <asp:Button ID="btnApprover" runat="server" CssClass="btn btn-primary" Text="Approve" Visible="False" Width="100px" CausesValidation="False" OnClick="btnApprover_Click" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" CausesValidation="False" OnClick="btnSubmit_Click" Width="100px" Visible="False"></asp:Button>
            <asp:Button ID="btnSubmitFC" runat="server" CssClass="btn btn-primary" Text="Submit" CausesValidation="False" Width="100px" Visible="False" OnClick="btnSubmitFC_Click"></asp:Button>
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
            <asp:Label ID="lbltest" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
        </div>
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
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div id="TansferModel" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Select Any Action</h4>
                </div>
                <div class="modal-body" style="height: 125px;">

                    <div class="col-sm-4"><b>Select any user</b></div>
                    <div class="col-sm-5" style="text-align: center;">
                        <asp:DropDownList ID="ddlTransferUser" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnForward" runat="server" CssClass="btn btn-primary" Text="Forward" Width="100px" CausesValidation="False" OnClick="btnForward_Click"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
