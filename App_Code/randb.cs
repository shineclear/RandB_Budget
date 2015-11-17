using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class randb
{
    DBUtility objDB;
    public randb()
    {
        //
        // TODO: Add constructor logic here
        //
        string Host = HttpContext.Current.Request.ServerVariables["server_name"].ToUpper();
        if (Host.IndexOf("LOCALHOST") != -1)
            objDB = new DBUtility("server=54.165.106.74;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else if (Host.IndexOf("XMPIEDEV") != -1)
            objDB = new DBUtility("server=172.17.0.39;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else if (Host.IndexOf("STAGING") != -1)
            objDB = new DBUtility("server=172.17.0.59;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else
            objDB = new DBUtility("server=172.17.0.38;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    }
    //DBUtility objDB = new DBUtility("server=172.17.0.38;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;"); 
    //DBUtility objDB = new DBUtility("server=54.164.238.154;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=172.17.0.39;database=CIE_RandBFoods;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=54.165.106.74;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=52.5.43.203;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=172.17.0.59;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    public DataSet RandBUpdateOrderProduct(int orderproductid, decimal AvailableBudget, int orderid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@orderproductid",orderproductid),
                   DBUtility.GetInParameter("@AvailableBudget",AvailableBudget),
                   DBUtility.GetInParameter("@orderid",orderid)
                };
            return objDB.ExecuteDataSet("RandBUpdateOrderProduct", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetStartingBudget(string retailername)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailername",retailername)
                };
            return objDB.ExecuteDataSet("RandBGetStartingBudget", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetProductBudgetInfo(string retailername, string type, string startdate, string enddate, int userid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailername",retailername),
                   DBUtility.GetInParameter("@type",type),
                   DBUtility.GetInParameter("@startdate",startdate),
                   DBUtility.GetInParameter("@enddate",enddate),
                   DBUtility.GetInParameter("@userid",userid)
                };
            return objDB.ExecuteDataSet("RandBGetProductBudgetInfo", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetAvailableBudget(string retailername, decimal startingbudget, string startingdatetime, int storeid, int opid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailername",retailername),
                   DBUtility.GetInParameter("@startingbudget",startingbudget),
                   DBUtility.GetInParameter("@startingdatetime",startingdatetime),
                   DBUtility.GetInParameter("@storeid",storeid),
                   DBUtility.GetInParameter("@opid",opid)
                };
            return objDB.ExecuteDataSet("RandBGetAvailableBudget", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetAllRetailer(int userid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@userid",userid)
                };
            return objDB.ExecuteDataSet("RandBGetAllRetailer", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetLastUpdate(string retailername, int userid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailername",retailername),
                   DBUtility.GetInParameter("@userid",userid)
                };
            return objDB.ExecuteDataSet("RandBGetLastUpdate", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetEarliestStartingBudget(int userid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@userid",userid)
                };
            return objDB.ExecuteDataSet("RandBGetEarliestStartingBudget", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetRetailerName(int retailerid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailerid",retailerid)
                };
            return objDB.ExecuteDataSet("RandBGetRetailerName", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet RandBGetAvailableBudgetCheckOut(string retailername, decimal startingbudget, string startingdatetime, int storeid, int orderid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {   
                   DBUtility.GetInParameter("@retailername",retailername),
                   DBUtility.GetInParameter("@startingbudget",startingbudget),
                   DBUtility.GetInParameter("@startingdatetime",startingdatetime),
                   DBUtility.GetInParameter("@storeid",storeid),
                   DBUtility.GetInParameter("@orderid",orderid)
                };
            return objDB.ExecuteDataSet("RandBGetAvailableBudgetCheckOut", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
}