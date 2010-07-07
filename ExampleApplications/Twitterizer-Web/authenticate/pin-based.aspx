<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pin-based.aspx.cs" Inherits="authenticate_pin_based" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Wizard ID="AuthWizard" runat="server" DisplaySideBar="false" 
            OnActiveStepChanged="AuthWizard_ActiveStepChanged">
            <WizardSteps>
                <asp:WizardStep ID="StartView" runat="server" AllowReturn="true" StepType="Start">
                    This 2 step wizard (can that even be called a wizard?) will guide you through granting
                    our application access to your twitter information.<br />
                    To begin, move to the next step.
                </asp:WizardStep>
                <asp:WizardStep ID="ClickLinkStep" runat="server" StepType="Step">
                    In this first step, you will need to click the following link. This will open twitter.com
                    in a new window and you may be asked to login. You'll be asked if you would like
                    to allow our application to access to your Twitter account. Once allowed, you will
                    be provided a PIN number, please keep this for the next step.<br />
                    <br />
                    <asp:HyperLink ID="RequestHyperLink" runat="server" Text="Click here to go to Twitter.com"
                        Target="_blank" />
                    <br />
                    When you have the PIN, continue to the next step.
                </asp:WizardStep>
                <asp:WizardStep ID="RequestAccessStep" runat="server" StepType="Step">
                    Please give us the PIN:
                    <asp:TextBox ID="PIN" runat="server" />
                    <asp:Button ID="GetRequestToken" runat="server" Text="Verify PIN" 
                        OnClick="GetRequestToken_Click" />
                </asp:WizardStep>
                <asp:WizardStep ID="DoneStep" runat="server" StepType="Complete">
                    Congrats! You have finished the wizard!<br />
                    Access Token:
                    <asp:Label ID="AccessTokenLabel" runat="server" /><br />
                    Access Secret:
                    <asp:Label ID="AccessSecretLabel" runat="server" />
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
    </form>
</body>
</html>
