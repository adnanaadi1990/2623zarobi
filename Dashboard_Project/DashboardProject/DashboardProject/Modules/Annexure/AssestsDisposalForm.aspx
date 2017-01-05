<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssestsDisposalForm.aspx.cs" Inherits="DashboardProject.Modules.Annexure.AssestsDisposalForm" %>
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

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviwer],[id$=btnReject],[id$=btnMDA],[id$=btnApproved]').click(function () {
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
            height: 34px;
        }

        .footable {
        }

        .btn-primary {
            height: 36px;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Assets Dispoal Form</p>
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

       <div class="panel-group" id="accordion3">
            <div class="panel panel-default" runat="server" id="dvADF">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Assets Dispoal Form
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

                                  <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                      <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asset Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAssetCode" Width="100px" CssClass="form-control" runat="server">                                          
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description & Category of Asset">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" Width="100px" CssClass="form-control" runat="server">                                          
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Purchase / Capitalization">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDateofPurchase" Width="150px" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost (PKR)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCost" Width="150px" CssClass="form-control" runat="server" TextMode="Number" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Depreciation (PKR)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAccumulatedDepreciation" Width="150px" CssClass="form-control" runat="server" TextMode="Number" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Book Value (NBV) PKR">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNetBookValue" Width="150px" CssClass="form-control" runat="server" TextMode="Number" />
                                    </ItemTemplate>

                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Disposal">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDateofDisposal" Width="150px" CssClass="form-control" runat="server" TextMode="Date" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mode of Disposal">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtModeofDisposal" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Proceeds (PKR)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSalesProceeds" Width="150px" CssClass="form-control" runat="server" TextMode="Number" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gain Or Loss on Disposal (PKR)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGainOrLossonDisposal" Width="150px" CssClass="form-control" runat="server" TextMode="Number" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reasons / Justification for Disposal">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReasonsJustificationforDisposal" Width="100px" CssClass="form-control" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>                             
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdDetail" runat="server" CssClass="table table-striped table-bordered footable" AutoGenerateColumns="true" ShowFooter="false" Visible="false" Width="100%">
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
                            <div class="col-sm-4">
                                Chief Opreating Officer
                                      <asp:DropDownList ID="ddlApproval1" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                               Chief Procurement Officer
                                      <asp:DropDownList ID="ddlApproval2" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                 Chief Financial Officer
                                      <asp:DropDownList ID="ddlApproval3" CssClass="form-control" runat="server"></asp:DropDownList>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
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
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click" ></asp:Button>
        <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" Text="Approval" CausesValidation="False" Width="100px" Visible="False" OnClick="btnApproved_Click"></asp:Button>

        <asp:Button ID="btnReviwer" runat="server" CssClass="btn btn-primary" Text="Approval" CausesValidation="False" Width="100px" Visible="False" OnClick="btnReviwer_Click"></asp:Button>

        <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" data-target="#myModal" data-toggle="modal" Text="Reject" Visible="false" CssClass="btn btn-primary" Width="100px" ></asp:LinkButton>

        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px"></asp:Button>
      
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
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject"  Width="100px" Visible="False" CausesValidation="False" OnClick="btnReject_Click"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
