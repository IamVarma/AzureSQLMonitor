<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentDashboard.aspx.cs" Inherits="_Default" %>

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
            <td style="width:100px;height:50px">
                 <div id="Filters" >
                        <asp:Label ID="lbl_CourseStatus" BackColor="YellowGreen" runat="server" Text="Course Status" ></asp:Label>
                        <asp:CheckBoxList ID="cbl_CourseStatus" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="cbl_CourseStatus_SelectedIndexChanged">               
                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="InActive" Value="0"></asp:ListItem>                
                    </asp:CheckBoxList>
                 </div>
            </td>
            <td></td>
        <td>
             <div style="OVERFLOW-Y:scroll; WIDTH:500px; HEIGHT:200px"> 
            <asp:Label ID="Label1" backcolor="YellowGreen" runat="server" Text="Course"></asp:Label>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">               
                             
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
    </tr></table><br /><br />
   <div>

       <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" 
           Font-Names="Verdana" Font-Size="8pt" Width="100%" Height="100%" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
           <LocalReport ReportPath="StudentDashboard.rdlc">
               <DataSources>
                   <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="RegisteredCourse" />
               </DataSources>
           </LocalReport>
       </rsweb:ReportViewer>
       <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MECProdDataSetTableAdapters.rpt_GetScorePercentageTableAdapter">
           <SelectParameters>
               <asp:Parameter DefaultValue="11501" Name="Student" Type="String" />
               <asp:ControlParameter ControlID="CheckBoxList1" DefaultValue="27001" Name="COURSE" PropertyName="SelectedValue" Type="String" />
           </SelectParameters>
       </asp:ObjectDataSource>
       <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MECProdDataSetTableAdapters.rpt_GetRegisteredCompletedCoursesTableAdapter">
           <SelectParameters>
               <asp:Parameter DefaultValue="11501" Name="Student" Type="String" />
           </SelectParameters>
       </asp:ObjectDataSource>
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
   </div>
       
        
    </form>
</body>
</html>
