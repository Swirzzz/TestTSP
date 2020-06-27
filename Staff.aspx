<%@ Page Title="Manage Staff" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="TPS.Staff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="StaffDataSource" runat="server"
        OnContextCreating="StaffDataSource_ContextCreating" EntitySetName="Users"
        Where="it.SecurityRole = 'Staff'" EnableUpdate="true" />

    <asp:GridView ID="StaffGridView" runat="server" DataSourceID="StaffDataSource" DataKeyNames="UserID"
        AutoGenerateColumns="false" AllowSorting="true">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <ItemTemplate>
                    <%# Eval("LastName") %>, <%# Eval("FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Education Level" SortExpression="EducationLevel">
                <ItemTemplate>
                    <%# Eval("EducationLevel") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EducationLevelDropDownList" runat="server" SelectedValue='<%# Bind("EducationLevel") %>'>
                        <asp:ListItem Value="High School" />
                        <asp:ListItem Value="Some College" />
                        <asp:ListItem Value="Undergraduate" />
                        <asp:ListItem Value="Graduate" />
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desired Salary" SortExpression="DesiredSalary">
                <ItemTemplate>
                    <%# Eval("DesiredSalary") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CompareValidator ID="SalaryValidator" ControlToValidate="DesiredSalaryTextBox" Type="Double" runat="server" ErrorMessage="Must be a number!!" Operator="DataTypeCheck"></asp:CompareValidator>
                    <asp:TextBox ID="DesiredSalaryTextBox" runat="server" Text='<%# Bind("DesiredSalary") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Can Relocate?" SortExpression="CanRelocate">
                <ItemTemplate>
                    <%# (bool)Eval("CanRelocate") ? "Yes" : "No" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CanRelocateCheckBox" runat="server" Checked='<%# Bind("CanRelocate") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desired Job Category" SortExpression="DesiredJobCategory">
                <ItemTemplate>
                    <%# Eval("DesiredJobCategory") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="JobCategoryDropDownList" runat="server" SelectedValue='<%# Bind("DesiredJobCategory") %>'>
                        <asp:ListItem Value="Administrative" />
                        <asp:ListItem Value="Information Technology" />
                        <asp:ListItem Value="Construction" />
                        <asp:ListItem Value="Medicine" />
                        <asp:ListItem Value="Veterinary" />
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
