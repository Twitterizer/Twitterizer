<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="callback.aspx.cs" Inherits="_2_MultipleUser_callback" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
<h1>
        Basic Web Flow (cont.)</h1>
    <h2 class="style1">
        Step 3 - The user returns</h2>
    &nbsp;As you can see, you&#39;ve authenticated with Twitter and you&#39;ve returned
    to example. If you look up at the address bar you will notice there are two values
    on the querystring: <em>oauth_token</em> and <em>oauth_verifier</em>. These values
    will always be appended to the callback url. The <em>oauth_token</em> value is the
    same request token that we obtained in Step 1 and provided on the querystring in
    Step 2. The <em>oauth_verifier</em> is an additional value that is added for additional
    security that will be required for the next step.<br />
    <h2>
        Step 4 - Token Exchange</h2>
    It&#39;s a fair assumption that since the user has arrived at our callback url with
    a token and verifier that they have successfully authenticated with Twitter, so
    we can proceed in obtaining the access token.
    <div class="codesample">
        <div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
            <p style="margin: 0px;">
                <span style="color: #2b91af;">OAuthTokenResponse</span> accessTokenResponse = <span
                    style="color: #2b91af;">OAuthUtility</span>.GetAccessToken(consumerKey, consumerSecret,</p>
            <p style="margin: 0px;">
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Request.QueryString[<span style="color: #a31515;">&quot;oauth_token&quot;</span>],</p>
            <p style="margin: 0px;">
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Request.QueryString[<span style="color: #a31515;">&quot;oauth_verifier&quot;</span>]);</p>
            <p style="margin: 0px;">
                AccessTokenLabel.Text = accessTokenResponse.Token;</p>
            <p style="margin: 0px;">
                AccessSecretLabel.Text = accessTokenResponse.TokenSecret;</p>
            <p style="margin: 0px;">
                ScreenNameLabel.Text = accessTokenResponse.ScreenName;</p>
            <p style="margin: 0px;">
                UserIdLabel.Text = accessTokenResponse.UserId.ToString();</p>
        </div>
    </div>
    You can also use our GetAccessTokenDuringCallback method to perform the exchange.
    This will automatically pull the values it needs from the querystring.
    <div class="codesample">
        <div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
            <p style="margin: 0px;">
                <span style="color: #2b91af;">OAuthTokenResponse</span> accessTokenResponse = <span
                    style="color: #2b91af;">OAuthUtility</span>.GetAccessTokenDuringCallback(consumerKey,
                consumerSecret);</p>
            <p style="margin: 0px;">
                AccessTokenLabel.Text = accessTokenResponse.Token;</p>
            <p style="margin: 0px;">
                AccessSecretLabel.Text = accessTokenResponse.TokenSecret;</p>
            <p style="margin: 0px;">
                ScreenNameLabel.Text = accessTokenResponse.ScreenName;</p>
            <p style="margin: 0px;">
                UserIdLabel.Text = accessTokenResponse.UserId.ToString();</p>
        </div>
    </div>
    And here are the results:
    <br />
    Access Token:
    <asp:Label ID="AccessTokenLabel" runat="server" />
    <br />
    Access Secret:
    <asp:Label ID="AccessSecretLabel" runat="server" />
    <br />
    Screen Name:
    <asp:Label ID="ScreenNameLabel" runat="server" />
    <br />
    User ID:
    <asp:Label ID="UserIdLabel" runat="server" />
    <br />
    <h2>
        Step 5 - Store the results</h2>
    You should now store the access token and the user details. Keep in mind that the
    only way an access token will become invalid is if the user revokes access by logging
    into Twitter. Otherwise, those values will grant you access to that user&#39;s data
    forever.
</asp:Content>
