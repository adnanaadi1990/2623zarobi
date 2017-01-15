<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssetTransferFormApproval.aspx.cs" Inherits="ITLDashboard.Modules.Annexure.AssetTransferFormApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>
    <link href="../../Scripts/bootstrap-datetimepicker.min.js"
        rel="stylesheet" type="text/css" />
    <link href="../../Scripts/bootstrap-datetimepicker.js"
        rel="stylesheet" type="text/css" />
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
            $('[id*=txtQuantity]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
            $('[id*=txtDate]').datepicker({
                format: "dd/mm/yyyy"
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
        //function pageLoad() {
        //$("[id*=txtNetBookValue]").priceFormat({
        //        prefix: '',
        //        thousandsSeparator: ''
        //    });
        //}
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
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, document.URL);
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
            height: 34px;
        }

        .footable {
        }

        .btn-primary {
            height: 36px;
        }

        .HeaderStyle {
            border: solid 1px White;
            font-weight: bold;
            text-align: center;
        }

        .HeaderStyle2 {
            border: solid 1px White;
            background-color: aliceblue;
            font-weight: bold;
            text-align: center;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Assets Transfer Form</p>
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
            </div>
        </div>

        <div class="panel-group" id="accordion1">
            <div class="panel panel-default" runat="server" id="BD">
                <div class="panel-heading">
                    <h4 class="panel-title">Basic Information
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-3">
                                Date
                                    <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="m-wrap span12 date form_datetime form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-sm-5">
                                Description
                                  <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                From Current Location
                                  <asp:DropDownList ID="ddlFromCurrentLocation" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                From Store
                                  <asp:DropDownList ID="ddlFromStore" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                From Cost Center
                                  <asp:DropDownList ID="ddlFromCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                To New Location
                                  <asp:DropDownList ID="ddlToNewLocation" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                To Store
                                  <asp:DropDownList ID="ddlToStore" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                To Cost Center
                                  <asp:DropDownList ID="ddlToCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion2">
            <div class="panel panel-default" runat="server" id="Div2">
                <div class="panel-heading">
                    <h4 class="panel-title">Asset Details
                    </h4>
                </div>
                <div id="Div3" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-3">
                                Item Description
                                    <asp:TextBox ID="txtItemDescription" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                TagNo.
                                  <asp:TextBox ID="txtTagNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Serial No.
                                  <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Model No/FAR/Code/Part NO
                                  <asp:TextBox ID="txtModelNoFarCodePartNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Make
                                  <asp:TextBox ID="txtMake" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Unit Of Measure
                                  <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Quantity
                                  <asp:TextBox ID="txtQuantity" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Remarks
                                  <asp:TextBox ID="txtRemarksAD" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divEmail" runat="server">
            <asp:Panel ID="pnlemail" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Email Approval
                    </div>
                    <div class="panel-body" style="text-align: center;">
                        <div class="row">
                            <div class="col-sm-3">
                                H.O.D
                                <asp:Label ID="lblHOD" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                C.O.O
                                <asp:DropDownList ID="ddlCOO" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                C.F.O
                                <asp:DropDownList ID="ddlCFO" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Asset MDA
                                <asp:DropDownList ID="ddlReviewer" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="true" BackColor="AliceBlue"></asp:TextBox>
                    </div>
                </div>
                <span class="help-block"></span>
                <div class="col-sm-12" style="text-align: left;" runat="server" id="Div1">
                    <%--<%# Eval("Numerator") %>--%>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                </div>
            </div>
            <span class="help-block"></span>
        </div>

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
    </div>
    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
        <asp:Button ID="btnApprover" runat="server" CssClass="btn btn-primary" Text="Approve" CausesValidation="False" Width="100px" Visible="False" OnClick="btnApproved_Click"></asp:Button>
        <asp:Button ID="btnReviewed" runat="server" CssClass="btn btn-primary" Text="Reviewed" Width="100px" Visible="False" OnClick="btnReviewed_Click"></asp:Button>
        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
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
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
