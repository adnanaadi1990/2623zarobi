<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BOMApproval.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DashboardProject.Modules.Master.BOMApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <%--<link href="../../Content/bootstrap.min.css" rel="stylesheet" />--%>

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>

    <script type="text/javascript">
        function ShowPopup() {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Material Master",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true,
                    height: 650,
                    width: 500,
                    left: 0,
                });
            });
        };

        function pageLoad() {
            $('[id*=txtQuantity]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
            $('[id*=ddlNotification],[id*=ddlPlant],[id*=ddlStorageLocation]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });

        }

        $(function () {
            $('[id*=txtQuantity]').keyup(function () {
                if (this.value.match(/[^,.0-9- ]/g)) {
                    this.value = this.value.replace(/[^,.0-9- ]/g, '');
                }
            });
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
        $(function () {
            $('[id*=grdWStatus]').footable();
     
            $('[id*=ddlNotification]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        });

    </script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            height: 500px;
            overflow: auto;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            overflow: auto;
            background-color: #FFFFFF;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                margin-bottom: 5px;
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
    </style>
    <style type="text/css">
        .rwStatusbarRow {
            display: none !important;
            height: auto;
        }

        /*.AutoShrink {
            width: 240px !important;
        }*/
    </style>
    <style type="text/css">
        .rgPageFirst, .rgPagePrev, .rgPageNext, .rgPageLast {
            display: none;
        }
        .btn-primary {}
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="dvH" style="font-family: inherit; display: none; font-size: 30px !important; font-weight: bold; color: black; text-align: center;">
        Dead Stock Approva Report<br />
        <br />
    </p>
    <div class="container" style="margin-top: 20px; width: 100%;">
        <div class="container-fluid">
            <div id="pnlPC">
                <div class="alert alert-success" id="sucess" runat="server" visible="false">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
                </div>

                <div class="alert alert-danger" id="error" runat="server" visible="false">
                    <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                </div>

                <div class="row">

                    <div class="col-sm-7" id="pnlHD">
                        <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">BOM Approval</p>
                    </div>
                </div>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion6" href="#collapse6"></a>
                        </h4>
                    </div>
                    <div id="collapse6" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-3" runat="server" id="dvTransactionNo">
                                    Transaction No
                                 <asp:Label ID="lblMaxTransactionNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-sm-3" runat="server" id="dvFormID" visible="false">
                                    Form ID
                                 <asp:Label ID="lblMaxTransactionID" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                          <div class="col-sm-3" runat="server" id="dvBillOfMaterial">
                                   Bill Of Material
                                 <asp:TextBox ID="txtBillOfMaterial" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row" runat="server" id="Div1">
                                <div class="col-sm-4">
                                    Material No
                                
                             <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-sm-8">
                                    Material Description
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row" runat="server" id="dvCheque">
                                <div class="col-sm-5">
                                    Plant
                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="col-sm-7">
                                    Storage Location
                                <asp:DropDownList ID="ddlStorageLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row" runat="server">
                                <div class="col-sm-4">
                                    Production  Lot Size From
                                <asp:TextBox ID="txtProductionLotSizefrom" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    Production  Lot Size To
                                <asp:TextBox ID="txtProductionLotSizeTo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    Production Version
                                <asp:TextBox ID="txtProductionVersion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div id="Div3" class="row" runat="server">
                                <div class="col-sm-4">
                                    Production Version Description
                                <asp:TextBox ID="txtProductionVersionDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    BOM Valid From
                                <asp:TextBox ID="txtBOMValidFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    BOM Valid To
                                <asp:TextBox ID="txtBOMValidTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div id="Div4" class="row" runat="server">
                                <div class="col-sm-4">
                                    Base Quantity
                                <asp:TextBox ID="txtBaseQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
               

                            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                       Item List
                    </h4>
                </div>
                <div id="Div5" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row fixed-panel">
                         <div class="col-sm-12">

                                                        <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered footable" runat="server" AutoGenerateColumns="false" Width="1200px"
                                                            AlternatingRowStyle-BackColor="#f05f40" HeaderStyle-BackColor="" ShowFooter="true" OnRowDataBound="OnRowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="delete" Text="Delete" OnClientClick="return ConfirmOnDelete();"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="btnAdd" runat="server" UseSubmitBehavior="true" Text="Add" OnClick="Add" CommandName="Footer" />
                                                                    </FooterTemplate>
                                                                    <ItemStyle Width="1%" />
                                                                    <HeaderStyle Width="1%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Component Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblComponentType" Text='<%# Bind("ComponentType") %>'></asp:Label>
                                                                        <%--<%# Eval("Numerator") %>--%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlComponentType" runat="server" CssClass="form-control" >
                                                                            <asp:ListItem Value="">---Select---</asp:ListItem>
                                                                            <asp:ListItem>Input Material</asp:ListItem>
                                                                            <asp:ListItem>Additive Material</asp:ListItem>
                                                                            <asp:ListItem>Scrap Material</asp:ListItem>
                                                                            <asp:ListItem>Packaging Material</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material No">
                                                                    <ItemTemplate>
                                                                        <%--   <%# Eval("Denominator") %>--%>
                                                                        <asp:Label runat="server" ID="lblMaterial" Text='<%# Bind("Material") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Description">
                                                                    <ItemTemplate>
                                                                        <%--<%# Eval("Lenght") %>--%>
                                                                        <asp:Label runat="server" ID="lblMaterialDescription" Text='<%# Bind("MaterialDescription") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <%--<%# Eval("Width") %>--%>
                                                                        <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtQuantity" runat="server" Width="95px" CssClass="form-control"> </asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit of Measure">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("UOM") %>'></asp:Label>
                                                                        <%--<%# Eval("UOM") %>--%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" Width="80px">
                                                                            <asp:ListItem Value="">------Select------</asp:ListItem>
                                                                            <asp:ListItem Value="BAG">BAG</asp:ListItem>
                                                                            <asp:ListItem Value="BOX">BOX</asp:ListItem>
                                                                            <asp:ListItem Value="CN">CN</asp:ListItem>
                                                                            <asp:ListItem Value="DR">DR</asp:ListItem>
                                                                            <asp:ListItem Value="DZ">DZ</asp:ListItem>
                                                                            <asp:ListItem Value="EA">EA</asp:ListItem>
                                                                            <asp:ListItem Value="FT">FT</asp:ListItem>
                                                                            <asp:ListItem Value="FT2">FT2</asp:ListItem>
                                                                            <asp:ListItem Value="G">G</asp:ListItem>
                                                                            <asp:ListItem Value="GLL">GLL</asp:ListItem>
                                                                            <asp:ListItem Value="IN">IN</asp:ListItem>
                                                                            <asp:ListItem Value="KAN">KAN</asp:ListItem>
                                                                            <asp:ListItem Value="KAR">KAR</asp:ListItem>
                                                                            <asp:ListItem Value="KG">KG</asp:ListItem>
                                                                            <asp:ListItem Value="KI">KI</asp:ListItem>
                                                                            <asp:ListItem Value="L">L</asp:ListItem>
                                                                            <asp:ListItem Value="LB">LB</asp:ListItem>
                                                                            <asp:ListItem Value="M">M</asp:ListItem>
                                                                            <asp:ListItem Value="OZ">OZ</asp:ListItem>
                                                                            <asp:ListItem Value="PAA">PAA</asp:ListItem>
                                                                            <asp:ListItem Value="PAK">PAK</asp:ListItem>
                                                                            <asp:ListItem Value="PC">PC</asp:ListItem>
                                                                            <asp:ListItem Value="ROL">ROL</asp:ListItem>
                                                                            <asp:ListItem Value="SET">SET</asp:ListItem>
                                                                            <asp:ListItem Value="STR">STR</asp:ListItem>
                                                                            <asp:ListItem Value="TIN">TIN</asp:ListItem>
                                                                            <asp:ListItem Value="YD">YD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Store Location">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblStLoc" Text='<%# Bind("StoreLocation") %>'></asp:Label>
                                                                        <%--<%# Eval("UOM") %>--%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlStLoc" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="S.No" Visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("sno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="13%" />
                                                                    <ItemStyle Width="11%" />

                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <EmptyDataTemplate>
                                                                <tr>
                                                                    <th></th>
                                                                    <th>Component Type</th>
                                                                    <th>Material</th>
                                                                    <th>Material Description</th>

                                                                    <th>Quantity</th>
                                                                    <th>UOM</th>
                                                                    <th>Store Location</th>

                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnAdd" runat="server" Text="Add" OnClick="Add" UseSubmitBehavior="true" CommandName="EmptyDataTemplate" value="Reset" />
                                                                        </td>
                                                                        <td>
                                                                          <asp:DropDownList ID="ddlComponentType" runat="server" CssClass="form-control" >
                                                                            <asp:ListItem Value="">---Select---</asp:ListItem>
                                                                            <asp:ListItem>Input Material</asp:ListItem>
                                                                            <asp:ListItem>Additive Material</asp:ListItem>
                                                                            <asp:ListItem>Scrap Material</asp:ListItem>
                                                                            <asp:ListItem>Packaging Material</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="form-control" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />

                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control">
                                                                                 <asp:ListItem Value="">------Select------</asp:ListItem>
                                                                                <asp:ListItem Value="BAG">BAG</asp:ListItem>
                                                                                <asp:ListItem Value="BOX">BOX</asp:ListItem>
                                                                                <asp:ListItem Value="CN">CN</asp:ListItem>
                                                                                <asp:ListItem Value="DR">DR</asp:ListItem>
                                                                                <asp:ListItem Value="DZ">DZ</asp:ListItem>
                                                                                <asp:ListItem Value="EA">EA</asp:ListItem>
                                                                                <asp:ListItem Value="FT">FT</asp:ListItem>
                                                                                <asp:ListItem Value="FT2">FT2</asp:ListItem>
                                                                                <asp:ListItem Value="G">G</asp:ListItem>
                                                                                <asp:ListItem Value="GLL">GLL</asp:ListItem>
                                                                                <asp:ListItem Value="IN">IN</asp:ListItem>
                                                                                <asp:ListItem Value="KAN">KAN</asp:ListItem>
                                                                                <asp:ListItem Value="KAR">KAR</asp:ListItem>
                                                                                <asp:ListItem Value="KG">KG</asp:ListItem>
                                                                                <asp:ListItem Value="KI">KI</asp:ListItem>
                                                                                <asp:ListItem Value="L">L</asp:ListItem>
                                                                                <asp:ListItem Value="LB">LB</asp:ListItem>
                                                                                <asp:ListItem Value="M">M</asp:ListItem>
                                                                                <asp:ListItem Value="OZ">OZ</asp:ListItem>
                                                                                <asp:ListItem Value="PAA">PAA</asp:ListItem>
                                                                                <asp:ListItem Value="PAK">PAK</asp:ListItem>
                                                                                <asp:ListItem Value="PC">PC</asp:ListItem>
                                                                                <asp:ListItem Value="ROL">ROL</asp:ListItem>
                                                                                <asp:ListItem Value="SET">SET</asp:ListItem>
                                                                                <asp:ListItem Value="STR">STR</asp:ListItem>
                                                                                <asp:ListItem Value="TIN">TIN</asp:ListItem>
                                                                                <asp:ListItem Value="YD">YD</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                        <asp:DropDownList ID="ddlStLoc" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                            </EmptyDataTemplate>

                                                        </asp:GridView>
                                                        <asp:Label ID="lblgridError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                                                    </div>
                        </div>
                        <span class="help-block"></span>
                            <div class="row" runat="server">
                                 <div class="col-sm-2"></div>
                                  <div class="col-sm-2"></div>
                                  <div class="col-sm-2"></div>
                                  <div class="col-sm-2"></div>
                                  <div class="col-sm-4">Total Base Quantity: <asp:Label ID="lblSum" runat="server" CssClass="form-control"></asp:Label></div>
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
                                        Head Of Deparment
                                       <asp:Label ID="lblHOD" runat="server" CssClass="form-control"></asp:Label>
                                    </div>



                                 <%--   <div class="col-sm-4">
                                        Specific (Approver)  
                                       <asp:ListBox ID="ddlNotification" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                    </div>--%>
                                    <div class="col-sm-4">
                                        IS Representative  
                                       <asp:DropDownList ID="ddlEmailMDA" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                    </div>
                                       <div class="col-sm-4">
                                         Notification  
                                       <asp:ListBox ID="ddlNotification" SelectionMode="Multiple"  runat="server">
                                         </asp:ListBox>
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
                        <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                            <%--   <%# Eval("Denominator") %>--%>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                        </div>
                    </div>
                    <span class="help-block"></span>
                </div>

                <span class="help-block"></span>
                <div id="dvimage" runat="server" class="col-sm-12">
                    <span class="help-block"></span>
                    <div id="pnlbtnerror">
                        <div class="panel panel-default" style="text-align: center;" id="DVERROR" runat="server">
                            <div class="panel-body">

                                <div id="Div2" class="col-sm-12" style="text-align: left;" runat="server">
                                    <%--<%# Eval("Lenght") %>--%>

                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                                    <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB" Font-Size="Medium"></asp:Label>
                                    <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>

                                </div>
                            </div>
                            <span class="help-block"></span>

                        </div>

                        <div class="col-sm-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save" Width="100px" OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnSaveSubmit" runat="server" CssClass="btn btn-primary" Text="Save / Submit" Width="100px" Visible="False" ValidationGroup="grpSave" OnClientClick="return AllowOneClick()" CausesValidation="False" OnClick="btnSaveSubmit_Click"></asp:Button>
                            <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" Text="Approve" OnClick="btnApproved_Click" Width="100px" OnClientClick="return AllowOneClick()"></asp:Button>
                            <%--<%# Eval("Width") %>--%>
                            <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
                            <asp:Button ID="btnMDA" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" OnClick="btnMDA_Click" Text="Submit" Width="100px" Visible="False" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" Width="100px" OnClick="btnCancel_Click"></asp:Button>


                            <%--<%# Eval("UOM") %>--%><%--<%# Eval("UOM") %>--%>
                        </div>
                    </div>


                </div>
            </div>
            <div class="panel-body fixed-panel " id="grd">
                <span class="help-block"></span>
                <span class="help-block"></span>
                <div class="row">
                    <div class="col-sm-12 fixed-panel">
                        <asp:GridView ID="grdWStatus" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" Visible="False">
                            <EmptyDataTemplate>
                                No Data</td>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Form ID" SortExpression="TransactionID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTransactionID" Text='<%# Bind("TransactionID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User" SortExpression="User_name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("RoughtingUserID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Category" SortExpression="category">

                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbcategory" Text='<%# Bind("HierarchyCat") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Time" SortExpression="DateTime">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDateTime" Text='<%# Bind("DateTime") %>'></asp:Label>
                                    </ItemTemplate>
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
