<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="VendorMaster.aspx.cs" Inherits="ITLDashboard.Modules.Finance.VendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, document.URL);
        });

    </script>


    <script type="text/javascript">
        $(function () {
            $('[id*=txtPostalCode],[id*=txtVendorCode]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });

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
            $('[id*=txtPostalCode],[id*=txtVendorCode]').keyup(function () {
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

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA],[id$=btnApproved]').click(function () {
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
        function ShowPopup2(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Confirmation",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true,
                    width: 450
                });
            });
        };
        function ShowPopupApproved(message) {
            $(function () {
                $("#dialogApproved").html(message);
                $("#dialogApproved").dialog({
                    title: "Confirmation Request",

                    modal: true,
                    width: 350
                });
            });
        };

        /////////////////////////////////////////////////////////////////////////////////////////////////


        //}
    </script>

    <script type="text/javascript">
        function pageLoad() {

            $('[id$=ddlEmailApproval],[id$=ddlCountry],[id$=ddlWHTaxType],[id$=ddlWHTaxCountry],[id$=ddlAccountGroup],[id$=ddlSortKey],[id$=ddlReconAccount],[id$=ddlWHTaxType]').multiselect({
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

        var clicked = false;
        function AllowOneClick() {
            if (!clicked) {
                clicked = true;
                return true;
            }
            return false;
        }
    </script>
    <style type="text/css">
        input[type=radio] {
            margin: 0px 7px 7px;
            display: inline-block;
        }
    </style>
    <style type="text/css">
        .fixed-panel {
            min-height: 10%;
            max-height: 10%;
            overflow-y: scroll;
        }



        .listContainer {
            background-color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="width: 100%; margin-top: 20px;">

        <div class="alert alert-success" id="sucess" runat="server" visible="false">
            <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
        </div>
        <div class="alert alert-info" id="email" runat="server" visible="false">
            <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
        </div>
        <div class="alert alert-danger" id="error" runat="server" visible="false">
            <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Vendor Master</p>

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
                <div class="row" runat="server" visible="false" id="dvVendorCode">
                    <div class="col-sm-3">
                        Vendor Code
                            <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>

                </div>

            </div>
        </div>
        <div class="panel-group" id="dvGD" runat="server">
            <div id="Div1" class="panel panel-default" runat="server">
                <div class="panel-heading">
                    General Data(To be filled by user)             
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4">
                                Title
                                    &nbsp;<asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                        <asp:ListItem>Company</asp:ListItem>
                                        <asp:ListItem>Mr.</asp:ListItem>
                                        <asp:ListItem>Mr. and Mrs.</asp:ListItem>
                                        <asp:ListItem>Ms.</asp:ListItem>
                                    </asp:DropDownList>
                            </div>

                            <div class="col-sm-4">
                                Name
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                Purchasing Organization
                                    <asp:DropDownList ID="ddlPurchasingOrganization" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-4">
                                Company Code
                                    <asp:DropDownList ID="ddlCompanyCode" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="1100">1100    International Textile Limited</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4" runat="server" id="dvHNo" visible="false">
                                House Number
                                    <asp:TextBox ID="txtHouseNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-8">
                                Street
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-4">
                                Country
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                City
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Region
                         <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control">
                             <asp:ListItem>------Select------</asp:ListItem>
                         </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-4">
                                Postal Code
                                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                TaxPayer CNIC
                         <asp:TextBox ID="txtTaxPayerCNIC" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4" runat="server" id="dvPNo" visible="false">
                                Passport No
                         <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                TaxPayer NTN
                         <asp:TextBox ID="txtTaxPayerNTN" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">


                            <div class="col-sm-4">
                                CDC Number
                         <asp:TextBox ID="txtCDCNumber" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                GST No
                         <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Telephone
                         <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-4">
                                Nature of Vendor
                         <asp:TextBox ID="txtNatureofVendor" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Email
                         <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="dvPD" runat="server" visible="false">
            <div id="Div2" class="panel panel-default">
                <div class="panel-heading">
                    Purchasing Data
                </div>
                <div id="Div3" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">

                        <div class="row">

                            <div class="col-sm-4">
                                Order Currency
                                    &nbsp;<asp:TextBox ID="txtOrderCurrency" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                Minimum Order Value
                                    <asp:TextBox ID="txtMinimumOrderValue" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                Terms of payment
                                    <%--<%# Eval("Lenght") %>--%>
                                <asp:DropDownList ID="ddlPDTermsOfPayment" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Schema Group Vendor
                                    <asp:DropDownList ID="ddlSchemaGroupVendor" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                        <asp:ListItem Value="1">1   Import Vendor</asp:ListItem>
                                        <asp:ListItem Value="2">2   Local Vendor</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                GR Check
                       <asp:RadioButtonList ID="rbGrCheck" runat="server" RepeatDirection="Horizontal">
                           <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                           <asp:ListItem Value="0">No</asp:ListItem>
                       </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="dvAD" runat="server" visible="false">
            <div id="Div5" class="panel panel-default" runat="server">
                <div class="panel-heading">
                    Accounting Data(To be filled by Finance)
                </div>
                <div id="Div6" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Account group
                                    <%--<%# Eval("Lenght") %>--%>
                                <asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>


                            <div class="col-sm-4">
                                Recon. Account
                                    <asp:DropDownList ID="ddlReconAccount" runat="server" CssClass="form-control" CausesValidation="True">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Sort Key
                                    &nbsp;<asp:DropDownList ID="ddlSortKey" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Terms of payment
                                    <%--<%# Eval("Lenght") %>--%>
                                <asp:DropDownList ID="ddlADTermsofpayment" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Payment Methods
                                    <asp:TextBox ID="txtPaymentMethods" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Previous Account
                                    <asp:TextBox ID="txtPreviousAccount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Liable Check
                                    <asp:TextBox ID="txtLiableCheck" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Exemption Certificate
                                    <asp:TextBox ID="txtExemptionCertificate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Exemption Reasons
                                    <asp:DropDownList ID="ddlExemptionReasons" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="10">PK (10) Exemption Certificate</asp:ListItem>

                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Exemption From Date
                        <asp:TextBox ID="txtExemptionFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Exemption To Date
                                    <asp:TextBox ID="txtExemptionToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="dvWTR" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Withholding Tax Related Information(To be filled by Finance)
                </div>
                <div id="Div9" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-sm-4">
                                WH Tax Country
                                    <asp:DropDownList ID="ddlWHTaxCountry" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                WH Tax Type
                                    <asp:ListBox ID="ddlWHTaxType" runat="server" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
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
                    <div class="panel-body">
                        <div class="row" style="text-align: center;">
                            <div class="col-sm-4">
                                H.O.D
                                       <asp:Label ID="lblHOD" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                Procurement Department (Authority)
                                       <asp:DropDownList ID="ddlEmailApproval2nd" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                MDA(Finance Department)
                                        <asp:DropDownList ID="ddlEmailMDA" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                            </div>
                        </div>

                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

    <div class="panel panel-default" style="text-align: center;" runat="server" id="dvlbl">
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-12" style="text-align: center;">
                    <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" placeholder="Remarks" Visible="true"></asp:TextBox>
                </div>
            </div>
            <span class="help-block"></span>
            <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                <%--<%# Eval("Lenght") %>--%>
                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
            </div>
        </div>
        <span class="help-block"></span>
    </div>

    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
        <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" Text="Approve" OnClick="btnApproved_Click" Width="100px" Visible="False" OnClientClick="return AllowOneClick()"></asp:Button>
        <asp:Button ID="btnMDA" runat="server" CssClass="btn btn-primary" Text="Save / Submit" OnClientClick="return AllowOneClick()" Width="120px" ValidationGroup="grpSave" CausesValidation="False" OnClick="btnMDA_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
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
</asp:Content>
