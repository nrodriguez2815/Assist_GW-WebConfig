<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assist_WebConfig.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Client Argentina SA</title>
    <link rel="shortcut icon" type="image/png" href="~/Root/img/LogoSmall.jpg" />
    <link rel="stylesheet" href="~/Content/Login.css" />
</head>
<body>
    <div class="container">
        <div class="card">
            <div class="card-image">	
            </div>
            <form class="card-form" id="form1" runat="server">
                <div class="input">
                    <asp:TextBox ID="txtUserName" class="input-field" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" class="input-label" runat="server" Text="Mail"></asp:Label>
                </div>
                <div class="input">
                    <asp:TextBox ID="txtPassword" class="input-field" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="Label2" class="input-label" runat="server" Text="Password"></asp:Label>
                </div>
                <div class="action">
                    <asp:Button ID="btnLogin" class="action-button" runat="server" Text="Login" OnClick="btnLogin_Click"></asp:Button>
                    <asp:Label ID="lblErrorMsg" runat="server" Text="Incorrect username or password." ForeColor="Red"></asp:Label>                    
                </div>
            </form>
            <div class="card-info">
			    <a class="card-info" href="/AssistWebConfig/User/StartRecovery">Forgot your password?</a>
		    </div>
        </div>
    </div>
</body>
</html>
