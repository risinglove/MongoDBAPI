<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MongoDB.aspx.cs" Inherits="MongoDB_WEB.MongoDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <table>
                          <%=html %>
                        </table>
            <br />
            <table>
                <tr>
                    <td>Name:</td>
                    <td>
                        <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Sex:</td>
                    <td>
                        <asp:TextBox ID="TXT_PAS" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="seve" runat="server" Text="Save" OnClick="seve_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
