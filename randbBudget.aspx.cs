﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using AjaxControlToolkit;
using uStore;
using uStoreAPI;
using System.Net;

namespace CierantRAB
{
    public partial class randbBudget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            randb objOoh = new randb();
            randbXmpie objOohustore = new randbXmpie();
            int Uid = Convert.ToInt32(uStore.Common.BLL.CustomerInfo.Current.UserID.ToString());
            retailername.Text = "All";
            hiduserid.Value = Uid.ToString();
            string lastupdatedate = "";
            string startingbudget = "";
            string startingbudgetdate = "";
            decimal dstartingbudget = 0;
            DataSet dsearliestdate = objOoh.RandBGetEarliestStartingBudget(Uid);
            DataTable tblearliestdate = dsearliestdate.Tables[0];
            if (tblearliestdate.Rows.Count > 0)
            {
                DataRow EarliestRow = tblearliestdate.Rows[0];
                string startdate = EarliestRow["earliestdate"] == null ? "" : EarliestRow["earliestdate"].ToString();
                if (startdate != "")
                {
                    string[] words = startdate.Split(' ');
                    startingbudgetdate = words[0];
                }
            }
            DataSet dslastupdate = objOoh.RandBGetLastUpdate("", Uid);
            DataTable tbllastupdate = dslastupdate.Tables[0];
            if (tbllastupdate.Rows.Count > 0)
            {
                DataRow LastupdateRow = tbllastupdate.Rows[0];
                lastupdatedate = LastupdateRow["lastupdate"] == null ? "" : LastupdateRow["lastupdate"].ToString();
            }
            lastupdate.Text = lastupdatedate;
            //annualbudget.Text = dstartingbudget.ToString();
            //Response.Write(Uid.ToString());
            DataSet dsdistributor = objOoh.RandBGetAllRetailer(Uid);
            DataTable tbldistributor = dsdistributor.Tables[0];
            string htmldistributor = "<select id='ddldistributor' onchange='changeretailer(this.value);'><option value='0'>All</option>";
            int count = tbldistributor.Rows.Count;
            if (count > 0)
            {
                for (int c = 0; c < count; c++)
                {
                    DataRow DistributorRow = tbldistributor.Rows[c];
                    string retailer = DistributorRow["CostCenter"] == null ? "" : DistributorRow["CostCenter"].ToString();
                    string retailerid = DistributorRow["CostCenterID"] == null ? "" : DistributorRow["CostCenterID"].ToString();
                    htmldistributor = htmldistributor + "<option value='" + retailerid + "'>" + retailer + "</option>";
                }
            }
            htmldistributor = htmldistributor + "</select>";
            lbdistributor.Text = htmldistributor;
            var today = DateTime.Today.ToString("MM/dd/yyyy");
            var startdateformat = "";
            var previousDate = "";
            if (startingbudgetdate != "")
            {
                DateTime myDateTime = DateTime.Parse(startingbudgetdate);
                startdateformat = myDateTime.ToString("MM/dd/yyyy");
                StartDate.Value = startdateformat;
                previousDate = startdateformat;
            }
            else
            {
                DateTime.Now.Year.ToString();
                startdateformat = "01/01/" + DateTime.Now.Year.ToString();
                StartDate.Value = startdateformat;
                previousDate = startdateformat;            
            }
            DataSet dsbudgetinfo = objOoh.RandBGetProductBudgetInfo("", "all", previousDate, today, Uid);
            DataTable tblbudgetinfo = dsbudgetinfo.Tables[0];
            int budgetcountrow = tblbudgetinfo.Rows.Count;
            string infohtml = "<table id='data' class='simpletable nopad'><thead><tr><th>Date</th><th>Retailer</th><th>Ordered by</th><th>Transaction</th><th>Order #</th><th>Item #</th><th>Description</th><th>Amount</th><th>Available Balance</th></tr></thead><tbody>";
            //Response.Write(budgetcountrow.ToString() + "," + previousDate + "," + today + "," + Uid);
            if (budgetcountrow > 0)
            {
                for (int b = 0; b < budgetcountrow; b++)
                {
                    DataRow BudgetInfoRow = tblbudgetinfo.Rows[b];
                    string orderedby = "";
                    string costcentername = BudgetInfoRow["RetailerName"] == null ? "" : BudgetInfoRow["RetailerName"].ToString();
                    string FirstName = BudgetInfoRow["FirstName"] == null ? "" : BudgetInfoRow["FirstName"].ToString();
                    string LastName = BudgetInfoRow["LastName"] == null ? "" : BudgetInfoRow["LastName"].ToString();
                    string amount = BudgetInfoRow["amount"] == null ? "" : BudgetInfoRow["amount"].ToString();
                    string Transaction = BudgetInfoRow["Transaction"] == null ? "" : BudgetInfoRow["Transaction"].ToString();
                    string Description = BudgetInfoRow["Description"] == null ? "" : BudgetInfoRow["Description"].ToString();
                    string TransactionDate = BudgetInfoRow["TransactionDate"] == null ? "" : BudgetInfoRow["TransactionDate"].ToString();
                    string OrderId = BudgetInfoRow["EncryptedOrderId"] == null ? "" : BudgetInfoRow["EncryptedOrderId"].ToString();
                    string view = "";
                    if (OrderId == "0")
                        OrderId = "-";
                    string OrderProductID = BudgetInfoRow["OrderProductID"] == null ? "" : BudgetInfoRow["OrderProductID"].ToString();
                    if (OrderProductID == "0")
                        OrderProductID = "-";
                    else
                        view = "<a href='#' onclick=\"window.parent.location='../../OrderHistory' \">View</a>";
                    decimal damount = 0;
                    decimal dAvailableBudget = 0;
                    string amountmoney = "";
                    string AvailableBudgetmoney = "";
                    string amountstyle = "";
                    string AvailableBudgetstyle = "";
                    if (amount != "0" && amount != "0.00" && amount != "" && amount != "0.00000")
                        damount = Math.Round(Convert.ToDecimal(amount), 2);
                    if (damount < 0)
                    {
                        amountmoney = "-$" + (damount * -1).ToString();
                        amountstyle = "style='color:red'";
                    }
                    else
                        amountmoney = "$" + damount.ToString();
                    string AvailableBudget = BudgetInfoRow["AvailableBudget"] == null ? "" : BudgetInfoRow["AvailableBudget"].ToString();
                    if (AvailableBudget != "0" && AvailableBudget != "0.00" && AvailableBudget != "")
                        dAvailableBudget = Math.Round(Convert.ToDecimal(AvailableBudget), 2);
                    if (dAvailableBudget < 0)
                    {
                        AvailableBudgetmoney = "-$" + (dAvailableBudget * -1).ToString();
                        AvailableBudgetstyle = "style='color:red'";
                    }
                    else
                        AvailableBudgetmoney = "$" + dAvailableBudget.ToString();
                    if (Transaction == "Order")
                        orderedby = FirstName + " " + LastName;
                    infohtml = infohtml + "<tr><td>" + TransactionDate + "</td><td>" + costcentername + "</td><td>" + orderedby + "</td><td>" + Transaction + "</td><td>" + OrderId + "</td><td>" + OrderProductID + "</td><td>" + Description + "</td>" +
                        "<td " + amountstyle + ">" + amountmoney + "</td><td " + AvailableBudgetstyle + ">" + AvailableBudgetmoney + "</td>";
                }
            }
            infohtml = infohtml + "</tbody></table>";
            tblbudget.InnerHtml = infohtml;
        }
        [WebMethod]
        public static string[] ChangeFilter(string retailerid, string startdate, string enddate, string userid)
        {
            string[] result = new string[5];
            randb objOoh = new randb();
            DataSet dsbudgetinfo;
            DataSet dslastupdate;
            string annualbudget = "";
            string retailername = "";
            string lastupdatedate = "";
            if (retailerid == "0")
            {
                dsbudgetinfo = objOoh.RandBGetProductBudgetInfo("", "all", startdate, enddate, Convert.ToInt32(userid));
                retailername = "All";
                annualbudget = "";
                dslastupdate = objOoh.RandBGetLastUpdate("", Convert.ToInt32(userid));
            }
            else
            {
                DataSet dsretailername = objOoh.RandBGetRetailerName(Convert.ToInt32(retailerid));
                DataTable tblretailername = dsretailername.Tables[0];               
                if (tblretailername.Rows.Count > 0)
                {
                    DataRow RetailerNameRow = tblretailername.Rows[0];
                    retailername = RetailerNameRow["CostCenter"] == null ? "" : RetailerNameRow["CostCenter"].ToString();
                }
                DataSet dsannualbudget = objOoh.RandBGetStartingBudget(retailername);
                DataTable tblannualbudget = dsannualbudget.Tables[0];
                if (tblannualbudget.Rows.Count > 0)
                {
                    DataRow AnnualBudgetRow = tblannualbudget.Rows[0];
                    annualbudget = AnnualBudgetRow["startingbudget"] == null ? "" : AnnualBudgetRow["startingbudget"].ToString();
                }
                dsbudgetinfo = objOoh.RandBGetProductBudgetInfo(retailername, "", startdate, enddate, Convert.ToInt32(userid));
                dslastupdate = objOoh.RandBGetLastUpdate(retailername, Convert.ToInt32(userid));
            }          
            DataTable tbllastupdate = dslastupdate.Tables[0];
            if (tbllastupdate.Rows.Count > 0)
            {
                DataRow LastupdateRow = tbllastupdate.Rows[0];
                lastupdatedate = LastupdateRow["lastupdate"] == null ? "" : LastupdateRow["lastupdate"].ToString();
            }
            DataTable tblbudgetinfo = dsbudgetinfo.Tables[0];
            int budgetcountrow = tblbudgetinfo.Rows.Count;
            string infohtml = "<table id='data' class='simpletable nopad'><thead><tr><th>Date</th><th>Retailer</th><th>Ordered by</th><th>Transaction</th><th>Order #</th><th>Item #</th><th>Description</th><th>Amount</th><th>Available Balance</th></tr></thead><tbody>";
            if (budgetcountrow > 0)
            {
                for (int b = 0; b < budgetcountrow; b++)
                {
                    DataRow BudgetInfoRow = tblbudgetinfo.Rows[b];
                    string orderedby = "";
                    string costcentername = BudgetInfoRow["RetailerName"] == null ? "" : BudgetInfoRow["RetailerName"].ToString();
                    string FirstName = BudgetInfoRow["FirstName"] == null ? "" : BudgetInfoRow["FirstName"].ToString();
                    string LastName = BudgetInfoRow["LastName"] == null ? "" : BudgetInfoRow["LastName"].ToString();
                    string amount = BudgetInfoRow["amount"] == null ? "" : BudgetInfoRow["amount"].ToString();
                    string Transaction = BudgetInfoRow["Transaction"] == null ? "" : BudgetInfoRow["Transaction"].ToString();
                    string Description = BudgetInfoRow["Description"] == null ? "" : BudgetInfoRow["Description"].ToString();
                    string TransactionDate = BudgetInfoRow["TransactionDate"] == null ? "" : BudgetInfoRow["TransactionDate"].ToString();
                    string OrderId = BudgetInfoRow["EncryptedOrderId"] == null ? "" : BudgetInfoRow["EncryptedOrderId"].ToString();
                    string view = "";
                    if (OrderId == "0")
                        OrderId = "-";
                    string OrderProductID = BudgetInfoRow["OrderProductID"] == null ? "" : BudgetInfoRow["OrderProductID"].ToString();
                    if (OrderProductID == "0")
                        OrderProductID = "-";
                    else
                        view = "<a href='#' onclick=\"window.parent.location='../../OrderHistory' \">View</a>";
                    decimal damount = 0;
                    decimal dAvailableBudget = 0;
                    string amountmoney = "";
                    string AvailableBudgetmoney = "";
                    string amountstyle = "";
                    string AvailableBudgetstyle = "";
                    if (amount != "0" && amount != "0.00" && amount != "" && amount != "0.00000")
                        damount = Math.Round(Convert.ToDecimal(amount), 2);
                    if (damount < 0)
                    {
                        amountmoney = "-$" + (damount * -1).ToString();
                        amountstyle = "style='color:red'";
                    }
                    else
                        amountmoney = "$" + damount.ToString();
                    string AvailableBudget = BudgetInfoRow["AvailableBudget"] == null ? "" : BudgetInfoRow["AvailableBudget"].ToString();
                    if (AvailableBudget != "0" && AvailableBudget != "0.00" && AvailableBudget != "")
                        dAvailableBudget = Math.Round(Convert.ToDecimal(AvailableBudget), 2);
                    if (dAvailableBudget < 0)
                    {
                        AvailableBudgetmoney = "-$" + (dAvailableBudget * -1).ToString();
                        AvailableBudgetstyle = "style='color:red'";
                    }
                    else
                        AvailableBudgetmoney = "$" + dAvailableBudget.ToString();
                    if (Transaction == "Order")
                        orderedby = FirstName + " " + LastName;
                    infohtml = infohtml + "<tr><td>" + TransactionDate + "</td><td>" + costcentername + "</td><td>" + orderedby + "</td><td>" + Transaction + "</td><td>" + OrderId + "</td><td>" + OrderProductID + "</td><td>" + Description + "</td>" +
                        "<td " + amountstyle + ">" + amountmoney + "</td><td " + AvailableBudgetstyle + ">" + AvailableBudgetmoney + "</td>";
                }
            }
            infohtml = infohtml + "</tbody></table>";
            result[0] = infohtml;
            result[1] = retailername;
            result[2] = retailerid;
            result[3] = annualbudget;
            result[4] = lastupdatedate;
            return result;
        }
    }
}