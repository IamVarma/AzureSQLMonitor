<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacultyDashboard.aspx.cs" Inherits="FacultyDashboard" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <table>
        <tr>
        <td>
            <div style="OVERFLOW-Y:scroll; WIDTH:500px; HEIGHT:200px"> 
            <asp:Label ID="Label1" backcolor="YellowGreen" runat="server" Text="Course"></asp:Label>
            <asp:CheckBoxList ID="chkCourse" runat="server">
            </asp:CheckBoxList><br />
            <asp:Label ID="lblcourse" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </td>
        <td>
                 <asp:Label ID="lblcolor" BackColor="yellowGreen" runat="server" Text="Back Ground Color" ></asp:Label><br />
                 <asp:DropDownList ID="ddl_color" runat="server">
                     <asp:ListItem Text="Black" Value="Black" Selected="true"></asp:ListItem>
                     <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                       <asp:ListItem Text="Plum" Value="Plum"></asp:ListItem>
                       <asp:ListItem Text="Blue" Value="CornFlowerBlue"></asp:ListItem>
                     <asp:ListItem Text="Green" Value="LightGreen"></asp:ListItem>
                     <asp:ListItem Text="Orange" Value="Orange"></asp:ListItem>
                 </asp:DropDownList>
        </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="View Report" OnClick="btnSubmit_Click" /></td>
        </tr>

     </table><br /><br />
        <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
