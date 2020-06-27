<%@ Page Title="View/Update My Information" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StaffInfo.aspx.cs" Inherits="TPS.StaffInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="StaffDataSource" runat="server"
        OnContextCreating="StaffDataSource_ContextCreating" EntitySetName="Users" EnableUpdate="true" />
    
    <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" Text="" />

    <asp:DetailsView ID="StaffDetailsView" runat="server" DataSourceID="StaffDataSource" DataKeyNames="UserID"
        DefaultMode="Edit" AutoGenerateRows="false" OnItemUpdating="StaffDetailsView_ItemUpdating">
        <EditRowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
        <Fields>
            <asp:TemplateField HeaderText="Photo">
                <EditItemTemplate>
                    <asp:Image ID="PhotoImage" runat="server"
                        ImageUrl='<%# "~/DownloadFile.ashx?type=photo&id=" + Eval("UserID") %>' /><br />
                    <asp:FileUpload ID="PhotoFileUpload" runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Résumé">
                <EditItemTemplate>
                    <asp:HyperLink ID="ResumeLink" runat="server" Text="View" Target="_blank"
                        NavigateUrl='<%# "~/DownloadFile.ashx?type=resume&id=" + Eval("UserID") %>' />
                    <asp:FileUpload ID="ResumeFileUpload" runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Education Level">
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
                <EditItemTemplate>
                    <asp:CompareValidator ID="SalaryValidator" ControlToValidate="DesiredSalaryTextBox" Type="Double" runat="server" ErrorMessage="Must be a number!!" Operator="DataTypeCheck"></asp:CompareValidator>
                    <asp:TextBox ID="DesiredSalaryTextBox" runat="server" Text='<%# Bind("DesiredSalary") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Can Relocate?" SortExpression="CanRelocate">
                <EditItemTemplate>
                    <asp:CheckBox ID="CanRelocateCheckBox" runat="server" Checked='<%# Bind("CanRelocate") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desired Job Category" SortExpression="DesiredJobCategory">
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
            <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ButtonType="Button" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
