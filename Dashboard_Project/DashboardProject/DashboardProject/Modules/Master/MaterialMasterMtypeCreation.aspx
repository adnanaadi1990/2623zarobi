<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MaterialMasterMTypeCreation.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="ITLDashboard.Modules.Master.MaterialMasterMTypeCreation" %>
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
        function ConfirmOnDelete() {
            if (confirm("Do you really want to delete?") == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $("#divExtandToOtherPlant").hide();

            $('[id*=ddlTaxes],[id*=ddlMerchandiser],[id*=ddlEmailApproval] ,[id*=ddlEmailReviwer],[id*=ddlEmailMDA],[id*=ddlExtandPlant],[id*=ddlPlant],[id*=ddlExtOtherPlant],[id*=ddlStorageLocation],[id*=ddlValuationType],[id=*ddlSearchMC],[id=*ddlImmediateHead],[id*=ddlTransferUser]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });

       

        }

        $(function () {
            $("#divremarks").hide();
            $('[id*=chkApproved]').click(function () {
                if ($(this).attr("value") == "Approved") {
                    $("#divremarks").hide();

                }
                if ($(this).attr("value") == "Reject") {
                    $("#divremarks").show();

                }
            })
            $('[id*=ddlNotification],[id*=ddlTransferUser]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        });
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
        function pageLoad() {
            $('[id*=txtStandardPrice] ,[id*=txtNumerator] ,[id*=txtDenominator],[id*=txtVolume],[id*=txtLenght],[id*=txtWidth],[id*=txtheight],[id*=txtGROSSWEIGHT] ,[id*=txtNETWEIGHT],[id*=txtVolume],[id*=txtNumeratorValue],[id*=txtDenominatorValue],[id*=txtReoderPoint],[id*=txtPlannedDeliveryTimeInDays],[id*=txtInHouseProductionTimeInDays],[id*=txtGRPROCESSINGTIMEINDAYS],[id*=txtSafetyStock],[id*=txtOverDeliveryTollerance],[id*=txtExcessWeightTolerance],[id*=txtUnderDeliveryTollerance],[id*=txtExcessVolumeTolerance],[id*=txtSMC]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
            $('[id*=ddlTaxes],[id*=ddlMerchandiser],[id*=ddlEmailApproval] ,[id*=ddlEmailReviwer],[id*=ddlEmailMDA],[id*=ddlExtandPlant],[id*=ddlPlant],[id*=ddlExtOtherPlant],[id*=ddlStorageLocation],[id*=ddlValuationType],[id*=ddlSearchMC],[id*=ddlImmediateHead],[id*=ddlTransferUser],[id*=ddlNotification]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
            $(".accordion2").collapse();
            $('#accordion2').collapse({ hide: false });
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
         history.pushState(null, null, document.URL);
         window.addEventListener('popstate', function () {
             history.pushState(null, null, document.URL);
         });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#abctest').css('display', 'none');
            $(".accordion2").collapse();
            $('#accordion2').collapse({ hide: false });

        });
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

        .form-control {
            height: 34px;
        }
        .chkbox-disabled
        {
            display:block;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Create Finished Material</p>
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


                <div class="row" id="MeterialType">
                    <div class="col-sm-4">
                        Material Type
                        <asp:DropDownList ID="ddlMaterialType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMaterialType_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="0">------Select------</asp:ListItem>
                            <asp:ListItem Value="FERT">FERT Finished Product</asp:ListItem>
                            <asp:ListItem Value="FERT">HAWA Trading Goods</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-3" id="divSMC" runat="server">
                        <asp:Label runat="server" ID="lblSap" Text="SAP Material Code" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtSMC" runat="server" CssClass="form-control" Visible="false" placeholder="SAP Material Code" MaxLength="10"></asp:TextBox>

                    </div>
                    <div class="col-sm-5" runat="server" visible="false" id="dvLock">
                        <br />
                        <asp:CheckBox ID="chkLock" runat="server" Text="Material is locked"></asp:CheckBox>
                        <asp:RadioButtonList ID="cbML" runat="server" RepeatDirection="Horizontal" Enabled="False" Visible="False">
                           <asp:ListItem Selected="True" Value="1">Material is locked</asp:ListItem>
                           <asp:ListItem Value="0">Material is un-locked</asp:ListItem>
                       </asp:RadioButtonList>

                    </div>
                </div>
                <span class="help-block"></span>


                <div class="row" runat="server" id="dvSMC" visible="false">
                    <div class="col-sm-6">
                        Search Existing Material
                        <asp:ListBox ID="ddlSearchMC" runat="server"></asp:ListBox>
                    </div>
                    <div class="col-sm-2">
                        <br />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"></asp:Button>
                    </div>

                </div>
                <br />
            </div>
        </div>

        <div class="panel-group" id="accordion1">
            <div class="panel panel-default" runat="server" id="BD" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion1" href="#collapseOne">Basic Data</a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">


                        <div class="row">
                            <div class="col-sm-1">
                                <h5><u>Business Division </u></h5>
                            </div>
                            <div class="col-sm-4">
                                Plant
                              <asp:DropDownList runat="server" ID="ddlPlant" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                  </div>
                            <div class="col-sm-7">
                                Storage Location
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                             <ContentTemplate>
                                 <asp:ListBox ID="ddlStorageLocation" runat="server" CssClass="form-control" SelectionMode="Multiple" AppendDataBoundItems="True"></asp:ListBox>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddlPlant" EventName="SelectedIndexChanged" />
                             </Triggers>
                         </asp:UpdatePanel>
                                <asp:TextBox ID="txtStLocation" runat="server" CssClass="form-control" Visible="false"> </asp:TextBox>
                            </div>

                            <div class="col-sm-5" id="divExtandToOtherPlant" runat="server" visible="false">
                                Extend To Other Plant
                            <asp:ListBox ID="ddlExtOtherPlant" runat="server" SelectionMode="Multiple">
                                <asp:ListItem Value="1000">1000&nbsp; MJS</asp:ListItem>
                                <asp:ListItem Value="2000">2000&nbsp; Terry</asp:ListItem>
                                <asp:ListItem Value="3000">3000&nbsp; Garments</asp:ListItem>
                                <asp:ListItem Value="4000">4000&nbsp; Head Office</asp:ListItem>
                                <asp:ListItem Value="5000">5000&nbsp; Port</asp:ListItem>
                            </asp:ListBox>
                            </div>

                        </div>
                        <span class="help-block"></span>

                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-2">
                                Description(40 char)
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="( e.g 12/1 POLY MJS FABRIC REWIND GREIGH )"
                                    MaxLength="40"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>

                        <div class="row">

                            <div class="col-sm-4">
                                Base Unit of Measure
                                    &nbsp;<asp:DropDownList ID="ddlMMBaseUnitOfMeasure" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="col-sm-4">
                                Material Group
                                    <asp:DropDownList ID="ddlMG" runat="server" CssClass="form-control" OnSelectedIndexChanged="MG_SelectedIndexChanged" AutoPostBack="True" CausesValidation="True">
                                    </asp:DropDownList>
                            </div>

                            <div class="col-sm-4">
                                Material Sub Group
                                <asp:DropDownList ID="ddlMSG" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtMSG" Visible="false" CssClass="form-control" Wrap="False"></asp:TextBox>
                            </div>
                        </div>




                        <span class="help-block"></span>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Gross Weight
                                    <asp:TextBox ID="txtGROSSWEIGHT" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Net Weight
                                    <asp:TextBox ID="txtNETWEIGHT" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Weight Unit
                                    <asp:DropDownList ID="ddlWeightunitBD" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Volume
                                    <asp:TextBox ID="txtVolume" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Volume Unit
                                    <asp:DropDownList ID="ddlVOLUMEUNIT" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Old Material Number
                        <asp:TextBox ID="txtOldMaterialNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Size / Dimensions<asp:TextBox ID="txtSizeDimensions" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                Packaging Material Category<asp:DropDownList ID="ddlBasicDataPackagingMaterialCateguory" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Batch Management
                       <asp:RadioButtonList ID="chkBatchManagement" runat="server" RepeatDirection="Horizontal">
                           <asp:ListItem Selected="True">Yes</asp:ListItem>
                           <asp:ListItem>No</asp:ListItem>
                       </asp:RadioButtonList>
                            </div>

                        </div>

                        <span class="help-block"></span>
                    </div>

                </div>
            </div>
        </div>

        <%--    <%# Eval("AltUnitOfMeasureCode") %>--%>

        <div class="ConvertionFactor" id="ConvertionFactor" runat="server">
            <div class="panel-group" id="accordion2">
                <div class="panel panel-default" runat="server" id="CF" visible="false">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapseTwo">Conversion Factor</a>
                        </h4>
                    </div>
                    <div id="collapseTwo" class="panel-collapse collapse in">
                        <div class="panel-body fixed-panel">
                            <div class="row">
                                <div class="col-sm-12">

                                    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered footable" runat="server" AutoGenerateColumns="false"
                                        AlternatingRowStyle-BackColor="#f05f40" HeaderStyle-BackColor="" ShowFooter="true" OnRowDeleting="GridView1_RowDeleting">
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



                                            <asp:TemplateField HeaderText="Alt Unit Of Measure">
                                                <ItemTemplate>
                                                    <%--    <%# Eval("AltUnitOfMeasureCode") %>--%>
                                                    <asp:Label runat="server" ID="lblAltUnitOfMeasureCode" Text='<%# Bind("AltUnitOfMeasureCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasureCode" CssClass="form-control" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="Baseuom" DataValueField="Baseuom" Width="130px">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ITLConnection %>" SelectCommand="SELECT [Baseuom] FROM [tblBaseunitofmeasure]"></asp:SqlDataSource>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Numerator">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblNumerator" Text='<%# Bind("Numerator") %>'></asp:Label>
                                                    <%--<%# Eval("Numerator") %>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNumerator" runat="server" Width="95px" CssClass="form-control" />
                                                </FooterTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Denominator">
                                                <ItemTemplate>
                                                    <%--   <%# Eval("Denominator") %>--%>
                                                    <asp:Label runat="server" ID="lblDenominator" Text='<%# Bind("Denominator") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDenominator" runat="server" Width="95px" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="length">
                                                <ItemTemplate>
                                                    <%--<%# Eval("Lenght") %>--%>
                                                    <asp:Label runat="server" ID="lblLenght" Text='<%# Bind("Lenght") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtLenght" runat="server" Width="95px" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Width">
                                                <ItemTemplate>
                                                    <%--<%# Eval("Width") %>--%>
                                                    <asp:Label runat="server" ID="lblWidth" Text='<%# Bind("Width") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtWidth" runat="server" Width="95px" CssClass="form-control"> </asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="height">
                                                <ItemTemplate>
                                                    <%--<%# Eval("height") %>--%>
                                                    <asp:Label runat="server" ID="lblheight" Text='<%# Bind("height") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtheight" runat="server" Width="95px" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit of Measure">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    <%--<%# Eval("UOM") %>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" Width="130px" Visible="false">
                                                        <asp:ListItem Value="Baseuom">Baseuom</asp:ListItem>
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

                                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ITLConnection %>" SelectCommand="SELECT [Weightunitcode] FROM [tblWeightunit]"></asp:SqlDataSource>


                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
            <ItemTemplate>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="btnAdd" runat="server" UseSubmitBehavior="true" Text="Add" OnClick="Add" CommandName = "Footer" CausesValidation="true" />
            </FooterTemplate>
        </asp:TemplateField>--%>
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
                                                <th>Alt Unit Of Measure</th>
                                                <th>Numerator </th>
                                                <th>Denominator </th>
                                                <th>length </th>
                                                <th>Width </th>
                                                <th>height </th>
                                                <th style="display:none;">Unit Of Measure </th>

                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnAdd" runat="server" Text="Add" OnClick="Add" UseSubmitBehavior="true" CommandName="EmptyDataTemplate" value="Reset" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAltUnitOfMeasureCode" CssClass="form-control" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="Baseuom" DataValueField="Baseuom">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNumerator" runat="server" Width="95px" CssClass="form-control" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDenominator" runat="server" Width="95px" CssClass="form-control" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLenght" runat="server" Width="95px" CssClass="form-control" />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtWidth" runat="server" Width="95px" CssClass="form-control" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtheight" runat="server" Width="95px" CssClass="form-control" />
                                                    </td>
                                                    <td style="display:none;">
                                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" Width="130px">
                                                            <asp:ListItem Value="Baseuom">Baseuom</asp:ListItem>
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

                                                </tr>
                                                <tr>

                                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ITLConnection %>" SelectCommand="SELECT [Weightunitcode] FROM [tblWeightunit]"></asp:SqlDataSource>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ITLConnection %>" SelectCommand="SELECT [Baseuom] FROM [tblBaseunitofmeasure]"></asp:SqlDataSource>
                                        </EmptyDataTemplate>

                                    </asp:GridView>
                                    <asp:Label ID="lblgridError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="sales" id="sales">
            <div class="panel-group" id="accordion3">
                <div class="panel panel-default" runat="server" id="SD" visible="false">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion1" href="#collapse3">Sales Data</a>
                        </h4>
                    </div>
                    <div id="collapse3" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <div class="row">
                                <%--<%# Eval("Numerator") %>--%>
                                <div class="col-sm-4">
                                    Product Hierarchy
                            <asp:DropDownList ID="ddlProdCatg" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProdCatg_SelectedIndexChanged" ViewStateMode="Enabled">
                            </asp:DropDownList>

                                </div>
                                <div class="col-sm-4">
                                    Product Hierarchy Sub Class 1
                                    <%--   <%# Eval("Denominator") %>--%>
                                    <asp:DropDownList ID="ddlProdCatgsub1" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProdCatgsub1_SelectedIndexChanged">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                    </asp:DropDownList>

                                    <%--<%# Eval("Lenght") %>--%>
                                </div>

                                <div class="col-sm-4">
                                    Product Hierarchy Sub Class 2                                
                                    <%--<%# Eval("Width") %>--%>
                                    <asp:DropDownList ID="ddlProdCatgsub2" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<%# Eval("height") %>--%>
                                </div>
                                <%--<%# Eval("UOM") %>--%>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-3">
                                    Sales Org
                                    &nbsp;<asp:DropDownList ID="ddlSalesOrg" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">------Select------</asp:ListItem>
                                        <asp:ListItem Value="1000">1000 MJS</asp:ListItem>
                                        <asp:ListItem Value="2000">2000 Terry</asp:ListItem>
                                        <asp:ListItem Value="3000">3000 Garment</asp:ListItem>
                                        <asp:ListItem Value="4000">4000 Head Office</asp:ListItem>
                                        <asp:ListItem Value="5000">5000 Port</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Distribution Channel
                                    &nbsp;<asp:DropDownList ID="ddlDistributionChannel" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Sales Unit
                                    &nbsp;<asp:DropDownList ID="ddlSalesUnit" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Division
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-3">
                                    Tax Classification
                       <asp:DropDownList ID="ddlTaxClassification" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Item Category Group
                                    &nbsp;<asp:DropDownList ID="ddlItemCateguoryGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Loom Type
                       <asp:DropDownList ID="ddlLoomType" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Room Ready
                       <asp:DropDownList ID="ddlRoomReady" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-3">
                                    Sub Division
                       <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Nos
                       <asp:DropDownList ID="ddlNOS" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">
                                    Transportion Group
                                    &nbsp;<asp:DropDownList ID="ddlTransportionGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    Loading Group
                        <asp:DropDownList ID="ddlLoadingGroup" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    Profit Center
                                    &nbsp;<asp:DropDownList ID="ddlProfitCenter" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-9">
                                    Sales Order Text
                                    &nbsp;<asp:TextBox ID="txtSalesodertext" runat="server" CssClass="form-control"> </asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    Material Rebate Rate
                                    &nbsp;<asp:DropDownList ID="ddlRate" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-6">
                                    Rebate Category
                                    &nbsp;<asp:DropDownList ID="ddlRebatecategoryRate" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="panel-group" id="accordion4">
            <div class="panel panel-default" runat="server" id="Purch" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion4" href="#collapse4">Purchasing</a>
                    </h4>
                </div>
                <div id="collapse4" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-sm-3">
                                Purchasing Group
                                    &nbsp;<asp:DropDownList ID="ddlPurchasingGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Ordering Unit
                                    &nbsp;<asp:DropDownList ID="ddlOrderingUnit" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                Purchase Order Text
                        <asp:TextBox ID="txtPurchaseOrderText" runat="server" CssClass="form-control" MaxLength="255"> </asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Back Flush
                        <asp:DropDownList ID="ddlBackFlush" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Planned Delivery Time In Days
                        <asp:TextBox ID="txtPlannedDeliveryTimeInDays" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                In House Production Time In Days
                        <asp:TextBox ID="txtInHouseProductionTimeInDays" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Gr Processing Time In Days
                        <asp:TextBox ID="txtGRPROCESSINGTIMEINDAYS" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Safety Stock
                        <asp:TextBox ID="txtSafetyStock" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-5">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion5">
            <div class="panel panel-default" runat="server" id="Prod" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion5" href="#collapse5">Production</a>
                    </h4>
                </div>
                <div id="collapse5" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-sm-3">
                                Production Unit Of Measure
                                    &nbsp;<asp:DropDownList ID="ddlProductionunit" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Unit Of Issue
                                    &nbsp;<asp:DropDownList ID="ddlUnitOfIssue" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Production Supervisior
                                    &nbsp;<asp:DropDownList ID="ddlProdsupervisor" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Production Schedulling Profile
                        <asp:DropDownList ID="ddlProdScheduleProfile" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Under Delivery Tollerance
                        <asp:TextBox ID="txtUnderDeliveryTollerance" runat="server" CssClass="form-control" MaxLength="100"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Over Delivery Tollerance
                        <asp:TextBox ID="txtOverDeliveryTollerance" runat="server" CssClass="form-control" MaxLength="100"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Task List Usage&nbsp;<asp:DropDownList ID="ddlTaskListUsage" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion6">
            <div class="panel panel-default" runat="server" id="Account" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion6" href="#collapse6">Accounting</a>
                    </h4>
                </div>
                <div id="collapse6" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-3">
                                Valuation Class
                                    &nbsp;<asp:DropDownList ID="ddlValuationClass" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlValuationClass_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Valuation Category
                                    &nbsp;<asp:DropDownList ID="ddlValuationCategory" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlValuationCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Valuation Type
                                   <asp:ListBox ID="ddlValuationType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                               <div class="col-sm-3" >
                                Standard Price
                                   <asp:TextBox ID="txtStandardPrice" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion7">
            <div class="panel panel-default" runat="server" id="Pack" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion7" href="#collapse7">Packaging</a>
                    </h4>
                </div>
                <div id="collapse7" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Packaging Material Category<asp:DropDownList ID="ddlPackagingMaterialCateguory" runat="server" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Packaging Material Type
                   <asp:DropDownList ID="ddlPackagingMaterialType" runat="server" CssClass="form-control">
                   </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Allowed Packaging Weight
                        <asp:TextBox ID="txtAllowedPackagingWeight" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Weight Unit
                        &nbsp;<asp:DropDownList ID="ddlWeightUnit" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Allowed Packaging Volme 
                        <asp:TextBox ID="txtAllowedPackagingVolme" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Volme  Unit
                        &nbsp;<asp:DropDownList ID="ddlVolumUnit" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Excess Weight Tolerance
                      <asp:TextBox ID="txtExcessWeightTolerance" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Excess Volume Tolerance
                      <asp:TextBox ID="txtExcessVolumeTolerance" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Closed Box
                       <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                           <asp:ListItem Selected="True">Yes</asp:ListItem>
                           <asp:ListItem>No</asp:ListItem>
                       </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="accordion8">
            <div class="panel panel-default" runat="server" id="QM" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion8" href="#collapse8">Quality Management</a>
                    </h4>
                </div>
                <div id="collapse8" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-5">
                                QM Control Key<asp:DropDownList ID="ddlQMControlKey" runat="server" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-3">
                                    <br />
                                    <asp:CheckBox ID="chkInspectionSetup" runat="server" Text="Inspection Setup"></asp:CheckBox>
                                </div>
                                <div class="col-sm-3">
                                    <br />
                                    <asp:CheckBox ID="chkQmProcActive" runat="server" Text="QM proc.active"></asp:CheckBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:TemplateField>
            <ItemTemplate>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="btnAdd" runat="server" UseSubmitBehavior="true" Text="Add" OnClick="Add" CommandName = "Footer" CausesValidation="true" />
            </FooterTemplate>
        </asp:TemplateField>--%>
        <div class="panel-group" id="accordion10">
            <div class="panel panel-default" runat="server" id="MRP" visible="false">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion10" href="#collapse10">MRP</a>
                    </h4>
                </div>
                <div id="collapse10" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                MRP TYPE
                                    &nbsp;<asp:DropDownList ID="ddlMrpType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                MRP Group
                                    &nbsp;<asp:DropDownList ID="ddlMRPGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Reoder Point
                       <asp:TextBox ID="txtReoderPoint" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                MRP Controller
                        <asp:DropDownList ID="ddlMRPController" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Availability check
                                    &nbsp;&nbsp;<asp:DropDownList ID="ddlAvailabilitycheck" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Minimum Lot Size
                       <asp:TextBox ID="txtMinimumLotSize" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Maximum Lot Size
                       <asp:TextBox ID="txtMaximumLotSize" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Maximum stock level
                       <asp:TextBox ID="txtMaximumstocklevel" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Strategy group
                                    &nbsp;&nbsp;<asp:DropDownList ID="ddlStrategygroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Lot size
                       <asp:DropDownList ID="ddlLotsize" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                SchedMargin key
                       <asp:TextBox ID="TxtSchedMarginkey" runat="server" CssClass="form-control"> </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Period Indicator
                       <asp:DropDownList ID="ddlPeriodIndicator" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Panel -->

        <div id="divEmail" runat="server" visible="false">

            <asp:Panel ID="pnlemail" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Email Approval
                    </div>
                    <div class="panel-body">
                        <div class="row" style="text-align: center;">
                            <div class="col-sm-3">
                                Merchandiser
                                      <asp:DropDownList ID="ddlMerchandiser" runat="server" CssClass="form-control" ></asp:DropDownList>
                            </div>
                              <div class="col-sm-3">
                                Taxes
                                      <asp:DropDownList ID="ddlTaxes" runat="server" CssClass="form-control" ></asp:DropDownList>
                            </div>
                           <div class="col-sm-3">
                                Merchandiser HOD
                                      <asp:DropDownList ID="ddlMHOD" runat="server" CssClass="form-control" ></asp:DropDownList>
                            </div>
                                   <div class="col-sm-3">
                                       Marketing HOD
                                      <asp:DropDownList ID="ddlMarketingHOD" runat="server" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div id="Div1" class="col-sm-3" runat="server" visible="false">
                                Review By
                                        &nbsp;<asp:DropDownList ID="ddlEmailReviwer" runat="server"></asp:DropDownList>
                            </div>

                               <div class="col-sm-3">
                                Specific (Finance)
                                       <asp:DropDownList ID="ddlNotificationFI" runat="server"></asp:DropDownList>
                            </div>
                                                        <div class="col-sm-3">
                                                            Specific (IS)
                                       <asp:DropDownList ID="ddlNotificationMIS" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Master Data Administrator
                                        <asp:DropDownList ID="ddlEmailMDA" runat="server"></asp:DropDownList>
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
                        <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="False"></asp:TextBox>
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
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="return AllowOneClick()" OnClick="btnSave_Click" ValidationGroup="grpSa" Width="60px"></asp:Button>
        <asp:Button ID="btnSaveSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save / Submit" OnClick="btnSaveSubmit_Click" Width="120px" Visible="False" ValidationGroup="grpSave" CausesValidation="False"></asp:Button>
        <asp:LinkButton ID="btnTransfer" runat="server" class="btn btn-primary" data-target="#TansferModel" data-toggle="modal" Text="Transfer Form" Visible="False" CssClass="btn btn-primary"></asp:LinkButton>
        <asp:Button ID="btnApprover" runat="server" OnClick="btnApprover_Click" CssClass="btn btn-primary" Text="Approved" Visible="False" CausesValidation="False" />
        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
        <%--   <%# Eval("Denominator") %>--%>

        <asp:Button ID="btnReviewed" runat="server" CssClass="btn btn-primary" Text="Reviewed" CausesValidation="False" OnClick="btnReviewed_Click" Width="100px" Visible="False"></asp:Button>

        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" Width="80px" Visible="False"></asp:Button>

        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CausesValidation="False" Width="80px" Visible="False" OnClick="btnUpdate_Click"></asp:Button>
          <asp:Button ID="btnFUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CausesValidation="False" Width="80px" Visible="False" OnClick="btnFUpdate_Click" ></asp:Button>
        <asp:Button ID="btnTUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CausesValidation="False" Width="80px" Visible="False" OnClick="btnTUpdate_Click"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" OnClick="btnCancel_Click" Width="100px"></asp:Button>
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
                           <asp:TemplateField HeaderText="Transferred To" SortExpression="TransferredTo">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTransferred" Text='<%# Bind("TransferredTo") %>'></asp:Label>
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
                      <asp:DropDownList ID="ddlTransferUser" runat="server" >
                        </asp:DropDownList>                      
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnForward" runat="server" CssClass="btn btn-primary" Text="Forward"  Width="100px" CausesValidation="False" OnClick="btnForward_Click"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
