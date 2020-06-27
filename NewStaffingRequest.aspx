<%@ Page Title="New Staff Request" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="NewStaffingRequest.aspx.cs" Inherits="TPS.NewStaffingRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="NoContractsLabel" runat="server" Visible="false" Text="You have no active contracts." />
    <asp:Panel ID="FormPanel" runat="server">
        <p>Contract: <asp:DropDownList ID="ContractDropDownList" runat="server" /></p>
    
        <asp:GridView ID="ChosenStaffGridView" runat="server" AutoGenerateColumns="false"
            DataKeyNames="UserID" OnRowDeleting="ChosenStaffGridView_RowDeleting">
            <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="StaffPhoto" runat="server"
                                ImageUrl='<%# "~/DownloadFile.ashx?type=photo&id=" + Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# Eval("LastName") %>, <%# Eval("FirstName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Education Level">
                        <ItemTemplate>
                            <%# Eval("EducationLevel") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <%# Eval("City") %>, <%# Eval("State") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Can Relocate?">
                        <ItemTemplate>
                            <%# (bool)Eval("CanRelocate") ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Desired Salary">
                        <ItemTemplate>
                            <%# Eval("DesiredSalary") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Résumé">
                        <ItemTemplate>
                            <asp:HyperLink ID="ViewResumeLink" runat="server" Target="_blank"
                                Text="View" NavigateUrl='<%# "~/DownloadFile.ashx?type=resume&id=" + Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                </Columns>
        </asp:GridView>
    
        <p><asp:Label ID="InstructionLabel" runat="server" Text="Search for and choose up to 3 staffers using the form below." /></p>

        <asp:Button ID="SubmitButton" runat="server" Text="Submit" Enabled="false" OnClick="SubmitButton_Click" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" /><br /><br />

        <fieldset>
            <legend>Staff Search</legend>
            Job Category:
            <asp:DropDownList ID="JobCategoryDropDownList" runat="server">
                <asp:ListItem Value="Any" />
                <asp:ListItem Value="Administrative" />
                <asp:ListItem Value="Information Technology" />
                <asp:ListItem Value="Construction" />
                <asp:ListItem Value="Medicine" />
                <asp:ListItem Value="Veterinary" />
            </asp:DropDownList>
            Minimum Education Level:
            <asp:DropDownList ID="EducationLevelDropDownList" runat="server">
                <asp:ListItem Value="High School" />
                <asp:ListItem Value="Some College" />
                <asp:ListItem Value="Undergraduate" />
                <asp:ListItem Value="Graduate" />
            </asp:DropDownList>
            Maximum Salary: <asp:TextBox ID="SalaryTextBox" runat="server" Width="50" /><br />
            City: <asp:TextBox ID="CityTextBox" runat="server" />
            State: <asp:TextBox ID="StateTextBox" runat="server" Width="30" />
            <asp:CheckBox ID="RelocateCheckBox" runat="server" /> Show only relocatable staff<br />
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" /><br />

            <asp:GridView ID="SearchResultsGridView" runat="server" AutoGenerateColumns="false"
                DataKeyNames="UserID" OnSelectedIndexChanged="SearchResultsGridView_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="StaffPhoto" runat="server"
                                ImageUrl='<%# "~/DownloadFile.ashx?type=photo&id=" + Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# Eval("LastName") %>, <%# Eval("FirstName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Education Level">
                        <ItemTemplate>
                            <%# Eval("EducationLevel") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <%# Eval("City") %>, <%# Eval("State") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Can Relocate?">
                        <ItemTemplate>
                            <%# (bool)Eval("CanRelocate") ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Desired Salary">
                        <ItemTemplate>
                            <%# Eval("DesiredSalary") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Résumé">
                        <ItemTemplate>
                            <asp:HyperLink ID="ViewResumeLink" runat="server" Target="_blank"
                                Text="View" NavigateUrl='<%# "~/DownloadFile.ashx?type=resume&id=" + Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="true" />
                </Columns>
            </asp:GridView>
        </fieldset>
    </asp:Panel>
</asp:Content>
