<%@ Page Title="My Staff Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MyStaffingRequests.aspx.cs" Inherits="TPS.MyStaffingRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="RequestDataSource" runat="server"
        OnContextCreating="RequestDataSource_ContextCreating" EntitySetName="Requests"
        Include="Contract,Staff1,Staff2,Staff3" EnableDelete="true" EnableFlattening="false" />

    <asp:GridView ID="RequestGridView" runat="server"
        DataSourceID="RequestDataSource" DataKeyNames="RequestID" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# Eval("Status") %>
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
            <asp:TemplateField HeaderText="Choice(s)">
                <ItemTemplate>
                    <%# Eval("Staff1") == null ? "" : string.Format("{0}, {1}<br />", Eval("Staff1.LastName"), Eval("Staff1.FirstName")) %>
                    <%# Eval("Staff2") == null ? "" : string.Format("{0}, {1}<br />", Eval("Staff2.LastName"), Eval("Staff2.FirstName")) %>
                    <%# Eval("Staff3") == null ? "" : string.Format("{0}, {1}<br />", Eval("Staff3.LastName"), Eval("Staff3.FirstName")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
