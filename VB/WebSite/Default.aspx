<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom pager</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxe:ASPxComboBox ID="cbPagerPosition" runat="server" ValueType="System.String" AutoPostBack="true">
                <Items>
                    <dxe:ListEditItem Text="Top" Value="PosTop" />
                    <dxe:ListEditItem Text="Bottom" Value="PosBottom" Selected="true" />
                    <dxe:ListEditItem Text="Both" Value="PosBoth" />
                </Items>
            </dxe:ASPxComboBox>
            <dxwgv:ASPxGridView ID="grid" runat="server" DataSourceID="sds" KeyFieldName="ProductID"
                ClientInstanceName="grid" OnLoad="grid_Load" OnDataBound="grid_DataBound" Width="600px"
                OnCustomCallback="grid_CustomCallback" OnInit="grid_Init" OnBeforeGetCallbackResult="grid_BeforeGetCallbackResult">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SupplierID" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="QuantityPerUnit" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="UnitsInStock" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="UnitsOnOrder" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ReorderLevel" VisibleIndex="8">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="9">
                    </dxwgv:GridViewDataCheckColumn>
                </Columns>
                <Templates>
                    <PagerBar>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div style="text-align: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    Items per page:
                                                </td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cbRecords" runat="server" ToolTip="Items Per Page" ValueType="System.Int32"
                                                        Width="50px" OnInit="cbRecords_Init">
                                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { grid.PerformCallback(s.GetValue()); }"
                                                            Init="function (s, e) { s.SetValue(grid.cpPageSize); }" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxHyperLink ID="lnkFirstPage" runat="server" Text="<<" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { grid.GotoPage(0); }" />
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                                <td>
                                                    <dxe:ASPxHyperLink ID="lnkPrevPage" runat="server" Text="<" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { grid.PrevPage(); }" />
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cbPage" runat="server" ToolTip="Current Page" ValueType="System.Int32"
                                                        Width="50px" OnInit="cbPage_Init">
                                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { grid.GotoPage(parseInt(s.GetValue()) - 1); }"
                                                            Init="function (s, e) { s.SetValue(grid.cpPageIndex); }" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxHyperLink ID="lnkNextPage" runat="server" Text=">" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { grid.NextPage(); }" />
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                                <td>
                                                    <dxe:ASPxHyperLink ID="lnkLastPage" runat="server" Text=">>" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { grid.GotoPage(grid.cpPageCount - 1); }" />
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </PagerBar>
                </Templates>
                <SettingsPager AlwaysShowPager="true" />
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="sds" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
                SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
