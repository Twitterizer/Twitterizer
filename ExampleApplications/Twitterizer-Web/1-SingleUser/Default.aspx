<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_1_SingleUser_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBodyContentPlaceHolder" Runat="Server">
If your application is interacting with a single Twitter account, there probably is no need for your application to perform the authorization process within your application.<br />
    There are two simple options:<br />
    <ol>
    <li>
    If the single account is the owner of the OAuth application, you can obtain your 
    access token by visiting <a href="http://dev.twitter.com">dev.twitter.com</a>. Once there, login as that account, click 'View Apps' at the top, click on the application you wish to use, then on the right you will see a button for "My Access Token." You can then simply copy and paste the values into your code.
    </li>
        <li>
            Perform the authorization process in a helper or example application. Simply run 
            through one of the examples here and copy the access token values given.</li>
    </ol>
    <p>
        If you absolutely must run through the authorization process, check out the 
        other examples.</p>
</asp:Content>

