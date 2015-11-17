﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using System.Configuration;

using System.IO;
using System.Text;
//using System.Web.UI.DataVisualization.Charting;
using uStore.Common.BLL;
using uStoreAPI;
using System.Data.SqlClient;
using System.Data;
public partial class CierantRABRetailer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Session["UserID"].ToString());      
        String oid = Request.QueryString["oid"] == null ? "" : Request.QueryString["oid"].ToString();
        //string uid = "6";
        //Session["StoreID"] = storeid;//the session set caused problem!
        //Session["UserID"] = uid;
        if (oid != "")
        {
            randb objrandb = new randb();
            randbXmpie objXmpie = new randbXmpie();
            DataSet exopid = objXmpie.CierantRandBGetOrderProductIDsfromOrder(Convert.ToInt32(oid));
            DataTable tblopid = exopid.Tables[0];
            int userid = uStore.Common.BLL.CustomerInfo.Current.UserID;
            decimal total = 0;        
            string availablebudget = "";
            string store_id = Session["StoreID"].ToString();
            if (tblopid.Rows.Count > 0)
            {
                for (int o = 0; o < tblopid.Rows.Count; o++)
                {
                    decimal dstartingbudget = 0;
                    decimal dremaining = 0;
                    decimal davailbudget = 0; 
                    string staringdatetime = "";
                    string startingbudget = "";
                    DataRow opRow = tblopid.Rows[o];
                    string opid = opRow["OrderProductID"] == null ? "" : opRow["OrderProductID"].ToString();
                    string EncryptedOrderId = opRow["EncryptedOrderId"] == null ? "" : opRow["EncryptedOrderId"].ToString();
                    string orderid = opRow["OrderID"] == null ? "" : opRow["OrderID"].ToString();
                    string CostCenter = opRow["CostCenter"] == null ? "" : opRow["CostCenter"].ToString();
                    string TotalPrice = opRow["TotalPrice"] == null ? "" : opRow["TotalPrice"].ToString();
                    decimal subtotal = Decimal.Parse(TotalPrice);
                    DataSet dsstaringbudget = objrandb.RandBGetStartingBudget(CostCenter);
                    DataTable tblstaringbudget = dsstaringbudget.Tables[0];
                    if (tblstaringbudget.Rows.Count > 0)
                    {
                        DataRow startingbudgetRow = tblstaringbudget.Rows[0];
                        startingbudget = startingbudgetRow["startingbudget"] == null ? "" : startingbudgetRow["startingbudget"].ToString();
                        if (startingbudget != "" && startingbudget != "0.00" && startingbudget != "0")
                            dstartingbudget = Math.Round(Convert.ToDecimal(startingbudget), 2);
                        staringdatetime = startingbudgetRow["staringdatetime"] == null ? "" : startingbudgetRow["staringdatetime"].ToString();
                        Response.Write(staringdatetime);
                    }
                    DataSet dsab = objrandb.RandBGetAvailableBudgetCheckOut(CostCenter, dstartingbudget, staringdatetime, Convert.ToInt32(store_id), Convert.ToInt32(orderid));
                    DataTable tblab = dsab.Tables[0];
                    if (tblab.Rows.Count > 0)
                    {
                        DataRow ABRow = tblab.Rows[0];
                        availablebudget = ABRow["availablebudget"] == null ? "" : ABRow["availablebudget"].ToString();
                        if (availablebudget != "" && availablebudget != "0.00" && availablebudget != "0")
                            davailbudget = Math.Round(Convert.ToDecimal(availablebudget), 2);
                        Response.Write(orderid + "," + davailbudget.ToString() + "," );
                        //Response.Write(availablebudget + ";" + davailbudget);
                    }
                    total = total + subtotal;
                    //dremaining = davailbudget - total;
                    dremaining = davailbudget - total;
                    Response.Write(opid + "," + dremaining.ToString() + "," + oid + ",total:" + total);
                    objrandb.RandBUpdateOrderProduct(Convert.ToInt32(opid), dremaining, Convert.ToInt32(oid));//different from ooh because this time the ordercost already calculated by RandBGetAvailableBudget
                }
            }
        }
    }
}
