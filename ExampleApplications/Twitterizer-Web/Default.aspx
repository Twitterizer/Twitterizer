<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPage.master" Theme="DefaultTheme" %>

<%@ MasterType TypeName="Twitterizer.ExampleWeb.MasterPage" %>
<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <h2>
        Home</h2>
    This site will provide you with an overview of the OAuth process, as it applies
    to Twitterizer, and give you a few examples of how you can use Twitterizer to guide
    your users through the authorization/authentication process.
    <br />
    <br />
    The process, in extremely simple terms, progresses as follows:<br />
    <ol>
        <li>Your application obtains a &quot;request token.&quot; This token is discarded at
            the end of the process. </li>
        <li>The user leaves your site and visits the authentication url. This will probably
            be done through a response.redirect, but there are a few options for this.</li>
        <li>The user arrives back at your site at a callback url that your application provides
            in step 1.</li>
        <li>Your application exchanges the request token for an access token.</li>
    </ol>
    <p>
        In order to execute any examples, we&#39;ll need your consumer token:<br />
        Consumer Key: <asp:TextBox ID="ConsumerKeyTextBox" runat="server" Width="259px"></asp:TextBox>
        <br />
        Consumer Secret: <asp:TextBox ID="ConsumerSecretTextBox" runat="server" Width="259px"></asp:TextBox>
        <asp:Button ID="SaveConsumerTokenButton" runat="server" Text="Save" 
            onclick="SaveConsumerTokenButton_Click" />
    </p>
    <p>
        You should NEVER give these values out. In fact, I highly suggest you inspect 
        the source code of this example website before you provide your token and run 
        any examples. You don&#39;t know who I am!</p>
    <p>
        If you click on an example link and are given a 401 exception, come back here 
        and double check your consumer token value.</p>
    
</asp:Content>
