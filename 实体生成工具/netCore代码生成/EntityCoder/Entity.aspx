<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entity.aspx.cs" Inherits="EntityCoder.Entity" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title> 
    <script src="jquery-1.10.2.js"></script>
	<style type="text/css">
        html,body{ padding:0px; margin:0px;width:100%; height:100%; font-family:"宋体"; font-size:12px; overflow:hidden}
        #div_tables{ overflow:auto}
        td { vertical-align:top; padding:20px}
    </style>
    <script type="text/javascript">
        function selectAll(obj) { 
            if ($(obj).is(':checked')) {
                $("#div_tables input[type='checkbox']").prop("checked", true);
            } else {
                $("#div_tables input[type='checkbox']").prop("checked", false);
            }
        }
    </script>
	</head>
<body>
    <form id="form1" runat="server">
      
    <table cellpadding="0" cellspacing="0" border="0" style="width:100%; height:100%">
        <tr>
            <td>  <input type="checkbox" id="checkAll" onchange="selectAll(this)" /> 全选</td>
        </tr>
        <tr>
            <td width="300">
                <h3>选择表：</h3>
                <div id="div_tables" style="overflow:y-scroll;height:500px;">
                <asp:Repeater ID="tableList" runat="server">
                    <ItemTemplate>
                       <input type="checkbox" id="<%# DataBinder.Eval(Container.DataItem,"tname")%>" value="<%# DataBinder.Eval(Container.DataItem,"tname")%>" name="table"/> <label for="<%# DataBinder.Eval(Container.DataItem,"tname")%>"><%# DataBinder.Eval(Container.DataItem,"tname")%></label><br />
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            </td>
               <td>
                命名空间：<asp:TextBox ID="txtNameSpace" runat="server" Width="150px"></asp:TextBox>                

            </td>
            <td>
                生成目录：<asp:TextBox ID="txtDir" runat="server" Width="400"></asp:TextBox> <br />（例如: D:\项目\cERP_1.0）<br /><br />
                <asp:Button ID="create" runat="server" Text="生成代码！" OnClick="CreateCode" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
