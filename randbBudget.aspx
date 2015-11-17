<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="randbBudget.aspx.cs" Inherits="CierantRAB.randbBudget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<title>title</title>
<link rel="stylesheet" href="pagestyles.css" type="text/css">
    <style>
        .ui-datepicker {
width: 14em;
height:14em;
} 
a.dp-choose-date {
	float: left;
	width: 16px;
	height: 16px;
	padding: 0;
	margin: 5px 3px 0;
	display: block;
	text-indent: -2000px;
	overflow: hidden;
	background: url(datepicker.jpg) no-repeat; 
}
        table td {text-align:center;
        } 
         .active { 
            background:rgb(238, 232, 142);
        }

        #data thead th:nth-child(8) {
            width:110px;
        }
    </style>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<link type="text/css" rel="stylesheet" href="http://fast.fonts.net/cssapi/20183b4a-4dc4-4ffa-afa6-89ea5dffc25f.css"/>
<script>

   
    function changeretailer(retailerid) {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var userid = $("#hiduserid").val();
        PageMethods.ChangeFilter(retailerid, startdate, enddate, userid,OnSucceeded, OnFailed);
    }
    function OnSucceeded(result, userContext, methodName) {
        if (methodName == "ChangeFilter") {
            //console.log(result);
            $("#tblbudget").html(result[0]);
            $("#retailername").html(result[1]);
            $("#hidretailerid").val(result[2]);
            $("#annualbudget").html(result[3]);
            $("#lastupdate").html(result[4]);
            $('#data').after('<div id="nav"></div>');
            var rowsShown = $("#hidrow").val();
            var rowsTotal = $('#data tbody tr').length;

            var numPages = rowsTotal / rowsShown;
            //console.log(rowsTotal + "," + numPages);
            if (numPages > 1) {
                $('#nav').attr("style", "display:block");
                for (i = 0; i < numPages; i++) {
                    var pageNum = i + 1;
                    $('#nav').append('<a href="#" rel="' + i + '">' + pageNum + '</a> ');
                }
            }
            else
                $('#nav').attr("style", "display:none");
            $('#data tbody tr').hide();
            $('#data tbody tr').slice(0, rowsShown).show();
            $('#nav a:first').addClass('active');
            $('#nav a').bind('click', function () {
                $('#nav a').removeClass('active');
                $(this).addClass('active');
                var currPage = $(this).attr('rel');
                var startItem = currPage * rowsShown;
                var endItem = startItem + rowsShown;
                $('#data tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                        css('display', 'table-row').animate({ opacity: 1 }, 300);
            });
        }
    }
    function OnFailed(error, userContext, methodName) {
        alert("error:" + error._message);
    }

    $(document).ready(function () {
        $("#ctl00_cphMainContent_ifrmCustomMain", parent.document).css("overflow-x", "hidden").animate({
            height: $("body").height() + 32
        }, 600);

        $('#data').after('<div id="nav"></div>');
        var rowsShown = $("#hidrow").val();
        var rowsTotal = $('#data tbody tr').length;

        var numPages = rowsTotal / rowsShown;
        //console.log(rowsTotal + "," + numPages);
        if (numPages > 1) {
            $('#nav').attr("style", "display:block");
            for (i = 0; i < numPages; i++) {
                var pageNum = i + 1;
                $('#nav').append('<a href="#" rel="' + i + '">' + pageNum + '</a> ');
            }
        }
        else
            $('#nav').attr("style", "display:none");
        $('#data tbody tr').hide();
        $('#data tbody tr').slice(0, rowsShown).show();
        $('#nav a:first').addClass('active');
        $('#nav a').bind('click', function () {
            $('#nav a').removeClass('active');
            $(this).addClass('active');
            var currPage = $(this).attr('rel');
            var startItem = currPage * rowsShown;
            var endItem = startItem + rowsShown;
            $('#data tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                    css('display', 'table-row').animate({ opacity: 1 }, 300);
            $("#ctl00_cphMainContent_ifrmCustomMain", parent.document).css("overflow-x", "hidden").animate({
                height: $("body").height() + 100
            }, 600);
        });

        $(".datepicker").datepicker({
            changeYear: true,
            showOn: 'both', buttonImageOnly: true, buttonImage: 'datepicker.jpg'
        });
        $(".ui-datepicker-trigger").each(function () {
            $(this).attr("style", "width:20px;vertical-align:middle;padding:0px 5px 5px 3px;");
            $(this).attr("alt", "Select Date");
            $(this).attr("title", "Select Date");
        });

        $("#StartDate").change(function () {
            var startdate = $(this).val();
            var enddate = $("#EndDate").val();
            var retailerid = $("#hidretailerid").val();
            var userid = $("#hiduserid").val();
            PageMethods.ChangeFilter(retailerid, startdate, enddate, userid, OnSucceeded, OnFailed);
        });
        $("#EndDate").change(function () {
            var userid = $("#hiduserid").val();
            var startdate = $("#StartDate").val();
            var enddate = $(this).val();
            var retailerid = $("#hidretailerid").val();
            PageMethods.ChangeFilter(retailerid, startdate, enddate, userid, OnSucceeded, OnFailed);
        });
        /*$("#ddldistributor").change(function () {
            var userid = $("#hiduserid").val();
            var distributorval = $(this).val();
            var startdate = $("#StartDate").val();
            var enddate = $("#EndDate").val();
            console.log(distributorval + "," + startdate + "," + enddate + "," + userid);
            PageMethods.ChangeFilter(ddldistributor, startdate, enddate, userid, OnSucceeded, OnFailed);
        });*/
        function OnSucceeded(result, userContext, methodName) {
            if (methodName == "ChangeFilter") {
                $("#tblbudget").html(result[0]);
                $("#retailername").html(result[1]);
                $("#hidretailerid").val(result[2]);
                $("#annualbudget").html(result[3]);
                $("#lastupdate").html(result[4]);
                $('#data').after('<div id="nav"></div>');
                var rowsShown = $("#hidrow").val();
                var rowsTotal = $('#data tbody tr').length;
                var numPages = rowsTotal / rowsShown;
                //console.log(rowsTotal + "," + numPages);
                if (numPages > 1) {
                    $('#nav').attr("style", "display:block");
                    for (i = 0; i < numPages; i++) {
                        var pageNum = i + 1;
                        $('#nav').append('<a href="#" rel="' + i + '">' + pageNum + '</a> ');
                    }
                }
                else
                    $('#nav').attr("style", "display:none");
                $('#data tbody tr').hide();
                $('#data tbody tr').slice(0, rowsShown).show();
                $('#nav a:first').addClass('active');
                $('#nav a').bind('click', function () {
                    $('#nav a').removeClass('active');
                    $(this).addClass('active');
                    var currPage = $(this).attr('rel');
                    var startItem = currPage * rowsShown;
                    var endItem = startItem + rowsShown;
                    $('#data tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                            css('display', 'table-row').animate({ opacity: 1 }, 300);
                });
            }
        }
        function OnFailed(error, userContext, methodName) {
            alert("error:" + error._message);
        }
        var today = new Date();
        var dd = today.getDate();
        var endmm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (dd < 10)
            dd = '0' + dd;
        var now = new Date();
        var earlier = new Date(yyyy, endmm - 4, dd)
        var startmonth = earlier.getMonth() + 1;

        if (startmonth < 10)
            startmonth = '0' + startmonth;

        if (endmm < 10)
            endmm = '0' + endmm;
        var enddateString = endmm + '/' + dd + '/' + yyyy;
        var startdateString = startmonth + '/' + dd + '/' + yyyy;
        $("#EndDate").val(enddateString);
        //$("#StartDate").val(startdateString);
    });
</script>
</head>
    <body><form id="form2" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
<div class="customcontent">
<!--Title--><h1 id="mastertitle">Retailer Budgets</h1><!--/Title-->
<!--IntroText-->
    <asp:HiddenField runat="server" id="hiduserid" Value=""></asp:HiddenField>
    <asp:HiddenField runat="server" id="hidretailerid" Value="0"></asp:HiddenField>
    <asp:HiddenField runat="server" id="hidrow" Value ="22"></asp:HiddenField>
    <div class="rightbudget">
<span>Retailer Name: </span><asp:Label runat="server" id="retailername"></asp:Label><br/>
<span>Annual Budget: </span><asp:Label runat="server" id="annualbudget"></asp:Label><br/>
<span>Last Update: </span><asp:Label runat="server" id="lastupdate"></asp:Label>
</div>
<div class="clearfix"></div>
<div class="tblUserControl wider height">
	 
	 <span class="lbl">Start Date:</span>
    <input name='StartDate' runat='server' type='text' readonly='readonly' id='StartDate' class='datepicker' style='width:80px;' />
	<span class="lbl">End Date:</span>
<input name='EndDate' type='text' readonly='readonly' id='EndDate' class='datepicker' style='width:80px;' />
        <div class="retailer">
        <span class="lbl">Retailer:</span>
	<asp:Label runat="server" id="lbdistributor"></asp:Label>
        </div>
</div>
<div class="clearfix"></div>
<div id="tblbudget" runat="server">
</div>
</div>
        </form></body>
<script type="text/javascript">
     $(document).ready(function() {
$("#ctl00_cphMainContent_ifrmCustomMain", parent.document).css("overflow-x", "hidden").animate({
                 height: $("body").height() + 70
}, 600);
 });
</script>
</html>

