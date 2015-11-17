<%@ Page Language="c#" CodeBehind="CheckOutComplete.aspx.cs" AutoEventWireup="True" Inherits="uStore.CheckOutComplete" %>
<asp:Content ID="cntCheckOutComplete" runat="server" ContentPlaceHolderID="cphMainContent">	
    <iframe style="display:none" src="" id="ifr"></iframe>
	<table cellspacing="0" cellpadding="5" width="600" border="0">
		<tr>
			<td class="normalbold" align="left"></td>
		</tr>
		<tr>
			<td class="NormalBold" align="left">
				<asp:Literal id="Literal1" runat="server" Text="Your order has been received successfully."></asp:Literal>
				<asp:label id="LblCustomerName" runat="server" Visible="False"></asp:label>
			</td>
		</tr>
		<tr>
			<td class="Normal" noWrap align="left">
				<asp:Literal id="Literal2" runat="server" Text="Order number:"></asp:Literal>
				&nbsp; <font class="SubHead CheckoutCompleteOrderNumber" >
					<asp:label id="OrderNumber" runat="server"></asp:label></font></td>
		</tr>
		<tr>
			<td class="Normal" align="left"></td>
		</tr>
		<tr>
			<td class="Normal" align="left">
				<asp:Label id="LblMessage" runat="server"></asp:Label></td>
		</tr>					
		<div style="display: none"><asp:TextBox id="txtSecureQueryParams" runat="server" class=""></asp:TextBox>&nbsp;</div>
	</table>
	<script type="text/javascript">
	    function OpenWin() {
	        var query = document.getElementById("<%=txtSecureQueryParams.ClientID%>").value;
	        printerFriend('OrderPrint.aspx?' + query);
	    }
	    var id = document.getElementById("ctl00_cphMainContent_OrderNumber").innerHTML;
	    document.getElementById("ifr").setAttribute("src", "CierantRABRetailer.aspx?oid=" + id);
	</script>
</asp:Content>
<asp:Content ID="cntButtons" runat="server" ContentPlaceHolderID="cphMainContentFooter">
	<div>
		<center>
			<uStoreControls:XmpImageButton id="btnPrint" SkinID="CheckoutComplete_Print" runat="server"/>
			<uStoreControls:XmpImageButton id="btnContinue" SkinID="CheckoutComplete_Continue" runat="server"/>
		</center>
    </div>
</asp:Content>
