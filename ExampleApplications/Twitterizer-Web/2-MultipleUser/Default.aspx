<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_2_MultipleUser_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    <h1>
        Basic Web Flow</h1>
    <h2>
        Step 1 - Request Token</h2>
    The request token is like a session id: it is used to keep track of the user during
    a process. When you ask for a request token, you identify your application and the
    callback url (where the user will return to your site later). So, from this single
    value, the Twitter knows where a user came from and where they should return. Here's
    how it happens.
    <div class="codesample">
        <div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
            <p style="margin: 0px;">
                <span style="color: blue;">string</span> callbackUrl = <span style="color: #a31515;">
                    &quot;http://localhost:59813/Twitterizer-Web/2-MultipleUser/callback.aspx&quot;</span>;</p>
            <p style="margin: 0px;">
                <span style="color: blue;">string</span> requestToken = <span style="color: #2b91af;">
                    OAuthUtility</span>.GetRequestToken(consumerKey, consumerSecret, callbackUrl).Token;</p>
            <p style="margin: 0px;">
                RequestTokenLabel.Text = requestToken;</p>
        </div>
    </div>
    <br />
    Your request token for this example is <em>
        <asp:Label ID="RequestTokenLabel" runat="server" Text="" /></em>
    <h2>
        Step 2 - Send the user to Twitter</h2>
    At this point, the user will actually leave your website and go to twitter.com and
    asked to login. After they have logged in, they will be provided with the name of
    your application and asked if they wish to grant your application access to this
    data. It is the user's choice afterall.
    <br />
    <br />
    The URL that the user is sent to is important. To help, Twitterizer gives you a
    method to quickly build the URL.
    <div class="codesample">
        <div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
            <p style="margin: 0px;">
                <span style="color: #2b91af;">Uri</span> authenticationUri = <span style="color: #2b91af;">
                    OAuthUtility</span>.BuildAuthorizationUri(requestToken);</p>
            <p style="margin: 0px;">
                AuthenticationUrlLink.Text = authenticationUri.AbsoluteUri;</p>
            <p style="margin: 0px;">
                AuthenticationUrlLink.NavigateUrl = authenticationUri.AbsoluteUri;</p>
        </div>
    </div>
    Here is the result:
    <asp:HyperLink ID="AuthenticationUrlLink" runat="server" />
    <br />
    <br />
    As you can see, the request token (that we aquired in step 1) is supplied on the
    querystring.
    <br />
    <br />
    To proceed with this example, click on the authentication url.
</asp:Content>
