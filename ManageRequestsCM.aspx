<%@ Page Title="Manage Staff Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageRequestsCM.aspx.cs" Inherits="TPS.ManageRequestsCM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="RequestDataSource" runat="server"
        OnContextCreating="RequestDataSource_ContextCreating" EntitySetName="Requests"
        Include="Contract,Contract.Client,Contract.Staff,Staff1,Staff2,Staff3" EnableFlattening="false"
        EnableUpdate="true"/>

    <asp:GridView ID="RequestsGridView" runat="server" AutoGenerateColumns="false"
        DataSourceID="RequestDataSource" DataKeyNames="RequestID"
        OnRowDataBound="RequestsGridView_RowDataBound" OnRowUpdating="RequestsGridView_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Client">
                <ItemTemplate>
                    <%# Eval("Contract.Client.Organization") %> - <%# Eval("Contract.Client.LastName") %>, <%# Eval("Contract.Client.FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position">
                <ItemTemplate>
                    <%# Eval("Contract.Position") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate>
                    <%# Eval("Contract.StartDate", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate>
                    <%# Eval("Contract.EndDate", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("Status") %>' />
                    <%# Eval("Status").ToString() == "Approved" ?
                        string.Format(": {0}, {1}", Eval("Contract.Staff.LastName"), Eval("Contract.Staff.FirstName")) : "" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="StaffDropDownList" runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
