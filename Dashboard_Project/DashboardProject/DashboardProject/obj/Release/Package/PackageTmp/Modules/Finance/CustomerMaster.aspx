<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerMaster.aspx.cs" Inherits="ITLDashboard.Modules.Finance.CustomerMaster" %>

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
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, document.URL);
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

    <script type="text/javascript">
        function pageLoad() {
            $('[id*=txtPostalCode],[id*=txtcustomerCode]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
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

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnApproved],[id$=btnReject],[id$=btnMDA]').click(function () {
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
        function pageLoad() {

            $('[id*=ddlEmailApproval],[id*=ddlCountry],[id*=ddlSortKey],[id*=ddlReconAccount],[id*=ddlAccountGroup],[id*=ddlCurrency]').multiselect({
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

        .panel-heading {
            font-weight: 700;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Customer Master</p>

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
                <div class="row" runat="server" visible="false" id="dvCustomerCode">
                    <div class="col-sm-3">
                        Customer Master Code
                            <asp:TextBox ID="txtcustomerCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>

                </div>

            </div>
        </div>

        <div class="panel-group" id="accordion1">
            <div class="panel panel-default" runat="server" id="dvGD">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion1" href="#collapse1">General Data(To be filled by user)</a></h4>
                </div>
                <div id="collapse1" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4">
                                Title
                                    &nbsp;<asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                        <asp:ListItem Value="Company">Company</asp:ListItem>
                                        <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                        <asp:ListItem Value="Mr. and Mrs.">Mr. and Mrs.</asp:ListItem>
                                        <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                    </asp:DropDownList>
                            </div>

                            <div class="col-sm-8">
                                Name
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Sales Organization
                                    <asp:DropDownList ID="ddlSalesOrganization" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Company Code
                                    <asp:DropDownList ID="ddlCompanyCode" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                        <asp:ListItem Value="1100">1100    International Textile Limited</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Distribution Channel
                                    <asp:DropDownList ID="ddlDistributionChannel" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Division
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                House Number
                                    <asp:TextBox ID="txtHouseNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
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
                         <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                TaxPayer CNIC
                         <asp:TextBox ID="txtTaxPayerCNIC" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                TaxPayer NTN
                         <asp:TextBox ID="txtTaxPayerNTN" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                GST No.
                         <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Telephone
                         <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Email
                         <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion2">
            <div class="panel panel-default" runat="server" id="dvCCD" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2">Company Code Data(To be filled by Finance)</a>
                    </h4>
                </div>
                <div id="collapse2" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                        </div>
                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-4">
                                Sort Key
                                    &nbsp;<asp:DropDownList ID="ddlSortKey" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>

                            <div class="col-sm-4">
                                Recon. Account
                                    <asp:DropDownList ID="ddlReconAccount" runat="server" CssClass="form-control" CausesValidation="True">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>

                            <div class="col-sm-4">
                                Terms of payment
                                <asp:DropDownList ID="ddlADTermsofpayment" runat="server" CssClass="form-control">
                                    <asp:ListItem>------Select------</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Payment Methods
                                    <asp:TextBox ID="txtPaymentMethods" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Payment Block
                                    <asp:DropDownList ID="ddlPaymentBlock" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <br />
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Single payment"></asp:CheckBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion3">
            <div class="panel panel-default" runat="server" id="dvWHT" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion3" href="#collapse3">With Holding Tax(To be filled by Finance Depart)</a>
                    </h4>
                </div>
                <div id="collapse3" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body fixed-panel">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered footable" AutoGenerateColumns="False" ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderStyle-Width="1%">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="deleteRowEvent" OnClientClick="return ConfirmOnDelete();" Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="AddRowBtn" runat="server" OnClick="AddRowEvent" Text="Add"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle Width="1%" />

                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="wth.t.type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlwthttype" Width="100px" AppendDataBoundItems="true" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="w/tax code">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlwtaxcode" Width="100px" AppendDataBoundItems="true" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="w/tax">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Cbwtax" runat="server" Checked='<%# Convert.ToBoolean((Eval("w/tax").ToString() == "1" ? true : false)) %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oblig.from">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObligfrom" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oblig.to">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObligto" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="W/tax.number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWtaxnumber" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exemption number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptionnumber" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exemption rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptionrate" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exemption.reas">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptionreas" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exempt.from">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptfrom" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exempt To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptTo" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtName" Width="150px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="GridView4" runat="server" CssClass="table table-striped table-bordered footable" AutoGenerateColumns="true" ShowFooter="false" Visible="false" Width="100%">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion4">
            <div class="panel panel-default" runat="server" id="dvSAD" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion4" href="#collapse4">Sales Area Data(To be filled by Finance)</a>
                    </h4>
                </div>
                <div id="collapse4" class="panel-collapse collapse in" role="tabpanel">
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
                                Currency
                                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control" CausesValidation="True">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Sales district
                                    &nbsp;<asp:DropDownList ID="ddlSalesdistrict" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Incoterms
                                    <asp:DropDownList ID="ddlIncoterms" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Terms of payment
                                    <asp:DropDownList ID="ddlTermsofpayment" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Tax
                                    <asp:DropDownList ID="ddlTax" runat="server" CssClass="form-control">
                                        <asp:ListItem>------Select------</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Cust.pric.proc.
                
                                <asp:DropDownList ID="ddlCustpricproc" runat="server" CssClass="form-control">
                                    <asp:ListItem>------Select------</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <span class="help-block"></span>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion5">
            <div class="panel panel-default" runat="server" id="dvSADPF" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion5" href="#collapse5">Sales Area Data(Partner Functions)</a>
                    </h4>
                </div>
                <div id="collapse5" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body fixed-panel">
                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered footable" AutoGenerateColumns="False" ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="deleteRowEvent2" OnClientClick="return ConfirmOnDelete();" Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="AddRowBtn" runat="server" OnClick="AddRowEvent2" Text="Add"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle Width="1%" />
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PF">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPF" Width="150px" AppendDataBoundItems="true" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PartnerFunction">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPartnerFunction" Width="160px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />

                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumber" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtName" Width="160px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PartnerDescription">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPartnerDescription" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Default">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CbwDefault" runat="server" Checked='<%# Convert.ToBoolean((Eval("Default").ToString() == "1" ? true : false)) %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-striped table-bordered footable" AutoGenerateColumns="true" ShowFooter="false" Visible="false" Width="100%">
                        </asp:GridView>
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
                            <div class="col-sm-2"></div>
                            <div class="col-sm-4">
                                H.O.D
                                       <asp:Label ID="lblHOD" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                (Finance Department)
                                      <asp:DropDownList ID="ddlEmailMDA" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-2"></div>
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
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnMDA" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save / Submit" Width="120px" ValidationGroup="grpSave" CausesValidation="False" OnClick="btnMDA_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Approve" CausesValidation="False" Width="100px" Visible="False" OnClick="btnApproved_Click"></asp:Button>

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
