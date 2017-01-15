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
            $('[id*=txtPostalCode],[id*=txtAssetCode]').keyup(function () {
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Assets Disposal Form</p>
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
                        Assets Disposal Form
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
                                        <asp:TextBox ID="txtAssetCode" Width="150px" CssClass="form-control" runat="server">                                          
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description & Category of Asset">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" Width="300px" CssClass="form-control" runat="server">                                          
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </asp:TemplateField>                                                 
                                <asp:TemplateField HeaderText="Date of Disposal">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDateofDisposal" Width="190px" CssClass="form-control" runat="server" TextMode="Date" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reasons / Justification for Disposal">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReasonsJustificationforDisposal" Width="350px" CssClass="form-control" runat="server" />
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
                        <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="False" BackColor="AliceBlue"></asp:TextBox>
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
        <asp:Button ID="btnReviewed" runat="server" CssClass="btn btn-primary" Text="Submit" Width="100px" Visible="False" OnClick="btnReviewed_Click"></asp:Button>
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject"  Width="100px" Visible="False" CausesValidation="False" OnClick="btnReject_Click"></asp:Button>
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

</asp:Content>
