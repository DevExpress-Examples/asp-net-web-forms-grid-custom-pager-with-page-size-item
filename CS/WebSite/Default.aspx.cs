using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
    }
    protected void grid_Load(object sender, EventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        String position = String.Empty;
        if (cbPagerPosition.Value != null)
            position = cbPagerPosition.Value.ToString();
        switch (position) {
            case "PosTop":
                grid.SettingsPager.Position = PagerPosition.Top;
                break;
            case "PosBoth":
                grid.SettingsPager.Position = PagerPosition.TopAndBottom;
                break;
            case "PosBottom":
            default:
                grid.SettingsPager.Position = PagerPosition.Bottom;
                break;
        }
    }
    protected void grid_DataBound(object sender, EventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        grid.JSProperties["cpPageCount"] = grid.PageCount;        
        grid.JSProperties["cpPageSize"] = grid.SettingsPager.PageSize;
        grid.JSProperties["cpPageIndex"] = grid.PageIndex + 1;
    }
    protected void cbPage_Init(object sender, EventArgs e) {
        ASPxComboBox cb = sender as ASPxComboBox;
        GridViewPagerBarTemplateContainer container = cb.NamingContainer as GridViewPagerBarTemplateContainer;
        ASPxGridView grid = container.Grid;
        int pageSize = grid.SettingsPager.PageSize;
        int totalRows = grid.VisibleRowCount;
        Double pageCount = Math.Ceiling(Convert.ToDouble(totalRows) / Convert.ToDouble(pageSize));
        for (int i = 1; i <= grid.PageCount; i++)
            cb.Items.Add(i.ToString(), i);
    }
    protected void cbRecords_Init(object sender, EventArgs e) {
        Int32[] values = { 10, 25, 50, 100 };
        ASPxComboBox cb = sender as ASPxComboBox;
        for (int i = 0; i < values.Length; i++) {
            cb.Items.Add(values[i].ToString(), values[i]);
        }

        if (Session["GridCurrentPageSize"] != null) {
            cb.Value = Convert.ToString(Session["GridCurrentPageSize"]);
        }
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        int newPageSize;
        if (e != null) {
            if (!int.TryParse(e.Parameters, out newPageSize)) return;
            grid.SettingsPager.PageSize = newPageSize;
            Session["GridCurrentPageSize"] = newPageSize;
        }
    }
    protected void grid_Init(object sender, EventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;

        if (Session["GridCurrentPageSize"] != null)
            grid.SettingsPager.PageSize = (int)Session["GridCurrentPageSize"];
        else
            Session["GridCurrentPageSize"] = grid.SettingsPager.PageSize;

        grid.DataBind();
    }
    protected void grid_BeforeGetCallbackResult(object sender, EventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        grid.JSProperties["cpPageSize"] = grid.SettingsPager.PageSize;
        grid.JSProperties["cpPageIndex"] = grid.PageIndex + 1;
    }
}
