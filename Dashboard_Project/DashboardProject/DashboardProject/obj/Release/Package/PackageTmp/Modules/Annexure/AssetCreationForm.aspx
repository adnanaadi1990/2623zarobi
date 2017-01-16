<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="AssetCreationForm.aspx.cs" Inherits="ITLDashboard.Modules.Annexure.AssetCreationForm" %>

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

            $('[id*=ddlEmailApproval] ,[id*=ddlEmailReviwer],[id*=ddlEmailMDA],[id*=ddlExtandPlant],[id*=ddlPlant],[id*=ddlExtOtherPlant],[id*=ddlStorageLocation],[id*=ddlValuationType],[id=*ddlSearchMC],[id=*ddlImmediateHead],[id*=ddlTransferUser]').multiselect({
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
            $(' [id*=txtReservationNumber] , [id*=txtQuantity] , [id*=txtAUCNumber] ,[id*=txtNumerator] ,[id*=txtDenominator],[id*=txtVolume],[id*=txtLenght],[id*=txtWidth],[id*=txtheight],[id*=txtGROSSWEIGHT] ,[id*=txtNETWEIGHT],[id*=txtVolume],[id*=txtNumeratorValue],[id*=txtDenominatorValue],[id*=txtReoderPoint],[id*=txtPlannedDeliveryTimeInDays],[id*=txtInHouseProductionTimeInDays],[id*=txtGRPROCESSINGTIMEINDAYS],[id*=txtSafetyStock],[id*=txtOverDeliveryTollerance],[id*=txtExcessWeightTolerance],[id*=txtUnderDeliveryTollerance],[id*=txtExcessVolumeTolerance],[id*=txtSMC]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
            $('[id*=ddlEmailApproval] ,[id*=ddlEmailReviwer],[id*=ddlEmailMDA],[id*=ddlExtandPlant],[id*=ddlPlant],[id*=ddlExtOtherPlant],[id*=ddlStorageLocation],[id*=ddlValuationType],[id*=ddlSearchMC],[id*=ddlImmediateHead],[id*=ddlTransferUser],[id*=ddlNotification]').multiselect({
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
        //}
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container" style="width: 100%; margin-top: 20px;">

        <div class="alert alert-success" id="sucess" runat="server" visible="false">
            <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="alert alert-danger" id="error" runat="server" visible="false">
            <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Asset Creation Form</p>
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
                <div class="row" runat="server" visible="false" id="dvAssetNo">
                    <div class="col-sm-3">
                        Asset No
                            <asp:TextBox ID="txtAssetNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>

                </div>
            </div>
        </div>
        <div class="panel-group" id="accordion1">
            <div class="panel panel-default" runat="server" id="BD">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion1" href="#collapseOne">Basic Data</a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4" runat="server" id="dvType">
                                Select Any Type
                                   <asp:RadioButtonList ID="rbAction" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdAction_SelectedIndexChanged">
                                       <asp:ListItem Selected="True">Asset</asp:ListItem>
                                       <asp:ListItem>AUC</asp:ListItem>
                                   </asp:RadioButtonList>
                                &nbsp;
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Plant
                                    <asp:DropDownList ID="ddlPlant" runat="server">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-7">
                                Asset Description(40 char)
                                 <asp:TextBox ID="txtAssetDescription" runat="server" CssClass="form-control" placeholder="( e.g 12/1 POLY MJS FABRIC REWIND GREIGH )"
                                     MaxLength="40"></asp:TextBox>
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
                        <div class="row">
                            <div class="col-sm-3">
                                Quantity
                                    &nbsp;<asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Cost Center
                                    <asp:DropDownList ID="ddlCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control" CausesValidation="True">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Location
                                <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Reservation Number
                                    &nbsp;<asp:TextBox ID="txtReservationNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" runat="server" id="dvManufacturer" visible="false">
                                Manufacturer                    
                                 <asp:TextBox ID="txtManufacturer" runat="server" CssClass="form-control" SelectionMode="Multiple" AppendDataBoundItems="True"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" runat="server" id="DvSerialNumber" visible="false">
                                Serial Number                    
                                 <asp:TextBox ID="txtSerialNumber" runat="server" CssClass="form-control" SelectionMode="Multiple" AppendDataBoundItems="True"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" runat="server" id="DvAUCNumber" visible="false">
                               AUC Number                    
                                 <asp:TextBox ID="txtAUCNumber" runat="server" CssClass="form-control" SelectionMode="Multiple" AppendDataBoundItems="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <span class="help-block"></span>
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
                            Asset MDA
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
                    <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="true" Enabled="False"></asp:TextBox>
                </div>
            </div>
            <span class="help-block"></span>
            <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                <%--   <%# Eval("Denominator") %>--%>

                <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
            </div>
        </div>
        <span class="help-block"></span>
    </div>

    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="return AllowOneClick()" OnClick="btnSave_Click" ValidationGroup="grpSa" Width="60px"></asp:Button>
        <asp:Button ID="btnSaveSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save / Submit" OnClick="btnSaveSubmit_Click" Width="120px" Visible="False" ValidationGroup="grpSave" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnApprover" runat="server" OnClick="btnApprover_Click" CssClass="btn btn-primary" Text="Approve" Visible="False" CausesValidation="False" />
        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
        <%--   <%# Eval("Denominator") %>--%>

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
                        <asp:DropDownList ID="ddlTransferUser" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnForward" runat="server" CssClass="btn btn-primary" Text="Forward" Width="100px" CausesValidation="False"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

