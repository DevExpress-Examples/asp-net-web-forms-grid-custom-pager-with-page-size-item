Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
	End Sub
	Protected Sub grid_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
		Dim position As String = String.Empty
		If cbPagerPosition.Value IsNot Nothing Then
			position = cbPagerPosition.Value.ToString()
		End If
		Select Case position
			Case "PosTop"
				grid.SettingsPager.Position = PagerPosition.Top
			Case "PosBoth"
				grid.SettingsPager.Position = PagerPosition.TopAndBottom
			Case Else
				grid.SettingsPager.Position = PagerPosition.Bottom
		End Select
	End Sub
	Protected Sub grid_DataBound(ByVal sender As Object, ByVal e As EventArgs)
		Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
		grid.JSProperties("cpPageCount") = grid.PageCount
		grid.JSProperties("cpPageSize") = grid.SettingsPager.PageSize
		grid.JSProperties("cpPageIndex") = grid.PageIndex + 1
	End Sub
	Protected Sub cbPage_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim cb As ASPxComboBox = TryCast(sender, ASPxComboBox)
		Dim container As GridViewPagerBarTemplateContainer = TryCast(cb.NamingContainer, GridViewPagerBarTemplateContainer)
		Dim grid As ASPxGridView = container.Grid
		Dim pageSize As Integer = grid.SettingsPager.PageSize
		Dim totalRows As Integer = grid.VisibleRowCount
		Dim pageCount As Double = Math.Ceiling(Convert.ToDouble(totalRows) / Convert.ToDouble(pageSize))
		For i As Integer = 1 To grid.PageCount
			cb.Items.Add(i.ToString(), i)
		Next i
	End Sub
	Protected Sub cbRecords_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim values() As Int32 = { 10, 25, 50, 100 }
		Dim cb As ASPxComboBox = TryCast(sender, ASPxComboBox)
		For i As Integer = 0 To values.Length - 1
			cb.Items.Add(values(i).ToString(), values(i))
		Next i

		If Session("GridCurrentPageSize") IsNot Nothing Then
			cb.Value = Convert.ToString(Session("GridCurrentPageSize"))
		End If
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
		Dim newPageSize As Integer = Nothing
		If e IsNot Nothing Then
			If Not Integer.TryParse(e.Parameters, newPageSize) Then
				Return
			End If
			grid.SettingsPager.PageSize = newPageSize
			Session("GridCurrentPageSize") = newPageSize
		End If
	End Sub
	Protected Sub grid_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)

		If Session("GridCurrentPageSize") IsNot Nothing Then
			grid.SettingsPager.PageSize = CInt(Math.Truncate(Session("GridCurrentPageSize")))
		Else
			Session("GridCurrentPageSize") = grid.SettingsPager.PageSize
		End If

		grid.DataBind()
	End Sub
	Protected Sub grid_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs)
		Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
		grid.JSProperties("cpPageSize") = grid.SettingsPager.PageSize
		grid.JSProperties("cpPageIndex") = grid.PageIndex + 1
	End Sub
End Class
