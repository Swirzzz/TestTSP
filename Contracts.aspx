<%@ Page Title="Contracts" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Contracts.aspx.cs" Inherits="TPS.Contracts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="ContractDataSource" runat="server"
        OnContextCreating="EntityDataSource_ContextCreating" EntitySetName="Contracts"
        Include="Client" EnableInsert="true" EnableUpdate="true" EnableDelete="true" />

    <asp:ValidationSummary ID="EditContractValidationSummary" runat="server"
        ShowSummary="false" ShowMessageBox="true" ValidationGroup="EditContractValidationGroup" />

    <asp:GridView ID="ContractGridView" runat="server"
        DataSourceID="ContractDataSource" DataKeyNames="ContractID"
        AllowSorting="true" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Client" SortExpression="Client.UserID">
                <ItemTemplate>
                    <%# Eval("Client") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position" SortExpression="Position">
                <ItemTemplate>
                    <%# Eval("Position") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditContractPositionTextBox" runat="server" Text='<%# Bind("Position") %>' />
                    <asp:RequiredFieldValidator ID="EditContractPositionRequiredValidator" runat="server"
                        ControlToValidate="EditContractPositionTextBox" Text="*" ErrorMessage="A position is required."
                        ValidationGroup="EditContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate">
                <ItemTemplate>
                    <%# Eval("StartDate", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditContractStartDateTextBox" runat="server" Text='<%# Bind("StartDate", "{0:MM/d/yyyy}") %>' />
                    <asp:RequiredFieldValidator ID="EditContractStartDateRequiredValidator" runat="server"
                        ControlToValidate="EditContractStartDateTextBox" Text="*" ErrorMessage="A start date is required."
                        ValidationGroup="EditContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                <ItemTemplate>
                    <%# Eval("EndDate", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditContractEndDateTextBox" runat="server" Text='<%# Bind("EndDate", "{0:MM/d/yyyy}") %>' />
                    <asp:RequiredFieldValidator ID="EditContractEndDateRequiredValidator" runat="server"
                        ControlToValidate="EditContractEndDateTextBox" Text="*" ErrorMessage="An end date is required."
                        ValidationGroup="EditContractValidationGroup" />
                    <asp:CompareValidator ID="EditContractDateValidator" runat="server"
                        ControlToValidate="EditContractEndDateTextBox" ControlToCompare="EditContractStartDateTextBox"
                        Operator="GreaterThan" Type="Date" Text="*" ErrorMessage="The end date must come after the start date."
                        ValidationGroup="EditContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ValidationGroup="EditContractValidationGroup" />
        </Columns>
    </asp:GridView>

    <h2>Add New Contract</h2>

    <asp:ValidationSummary ID="NewContractValidationSummary" runat="server"
        ShowSummary="false" ShowMessageBox="true" ValidationGroup="NewContractValidationGroup" />

    <asp:DetailsView ID="NewContractDetailsView" runat="server"
        DataSourceID="ContractDataSource" DefaultMode="Insert" AutoGenerateRows="false"
        OnDataBound="NewContractDetailsView_DataBound"
        OnItemInserting="NewContractDetailsView_ItemInserting" OnItemInserted="NewContractDetailsView_ItemInserted">
        <Fields>
            <asp:TemplateField HeaderText="Client">
                <EditItemTemplate>
                    <asp:DropDownList ID="NewContractClientDropDownList" runat="server" SelectedValue='<%# Bind("Client.UserID") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position">
                <EditItemTemplate>
                    <asp:TextBox ID="NewContractPositionTextBox" runat="server" Text='<%# Bind("Position") %>' />
                    <asp:RequiredFieldValidator ID="NewContractPositionRequiredValidator" runat="server"
                        ControlToValidate="NewContractPositionTextBox" Text="*" ErrorMessage="A position is required."
                        ValidationGroup="NewContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <EditItemTemplate>
                    <asp:TextBox ID="NewContractStartDateTextBox" runat="server" Text='<%# Bind("StartDate") %>' />
                    <asp:RequiredFieldValidator ID="NewContractStartDateRequiredValidator" runat="server"
                        ControlToValidate="NewContractStartDateTextBox" Text="*" ErrorMessage="A start date is required."
                        ValidationGroup="NewContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <EditItemTemplate>
                    <asp:TextBox ID="NewContractEndDateTextBox" runat="server" Text='<%# Bind("EndDate") %>' />
                    <asp:RequiredFieldValidator ID="NewContractEndDateRequiredValidator" runat="server"
                        ControlToValidate="NewContractEndDateTextBox" Text="*" ErrorMessage="An end date is required."
                        ValidationGroup="NewContractValidationGroup" />
                    <asp:CompareValidator ID="NewContractDateValidator" runat="server"
                        ControlToValidate="NewContractEndDateTextBox" ControlToCompare="NewContractStartDateTextBox"
                        Operator="GreaterThan" Type="Date" Text="*" ErrorMessage="The end date must come after the start date."
                        ValidationGroup="NewContractValidationGroup" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowInsertButton="true" ButtonType="Button" ValidationGroup="NewContractValidationGroup" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
