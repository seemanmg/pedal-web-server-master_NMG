<%@ Page Language="c#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UserWeb.Index" %>

<script runat="server">
	protected void Page_Load(object sender, System.EventArgs e)
	{
		lblVersion.Text = "Your server is running ASP.NET and the versio is " + System.Environment.Version.ToString();
	}
</script>


<html>
<head>
    <title>ASP.NET Version</title>
</head>
<body>
    <form id="form1" runat="server">
		<asp:Label ID="lblVersion" runat="server"></asp:Label>
    </form>
</body>
</html>
