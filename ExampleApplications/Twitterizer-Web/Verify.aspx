<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Verify.aspx.cs" Inherits="Verify" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    <h1>
        Verify Credentials</h1>
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            Consumer Key:
            <asp:TextBox ID="CKTextBox" runat="server"></asp:TextBox>
            <br />
            Consumer Secret:
            <asp:TextBox ID="CSTextBox" runat="server"></asp:TextBox>
            <br />
            Access Key:
            <asp:TextBox ID="AKTextBox" runat="server"></asp:TextBox>
            <br />
            Access Secret:
            <asp:TextBox ID="ASTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="VerifyButton" runat="server" Text="Verify" OnClick="VerifyButton_Click" />
            <asp:Label ID="ResultLabel" runat="server" Text="ResultText" Visible="False"></asp:Label>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    Thinking ...
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
