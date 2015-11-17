using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class randbXmpie
{
    DBUtility objDB;
    public randbXmpie()
    {
        //
        // TODO: Add constructor logic here
        //
        string Host = HttpContext.Current.Request.ServerVariables["server_name"].ToUpper();
        if (Host.IndexOf("LOCALHOST") != -1)
            objDB = new DBUtility("server=54.165.106.74;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else if (Host.IndexOf("XMPIEDEV") != -1)
            objDB = new DBUtility("server=172.17.0.39;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else if (Host.IndexOf("STAGING") != -1)
            objDB = new DBUtility("server=172.17.0.59;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
        else
            objDB = new DBUtility("server=172.17.0.38;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    }
    //DBUtility objDB = new DBUtility("server=172.17.0.38;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;"); 
    //DBUtility objDB = new DBUtility("server=54.164.238.154;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=172.17.0.39;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=54.165.106.74;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=52.5.43.203;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    //DBUtility objDB = new DBUtility("server=172.17.0.59;database=uStore;uid=xmpieustore;pwd=uStore1;MultipleActiveResultSets=True;");
    public DataSet CierantRandBGetOrderProductIDsfromOrder(int encrypedorderid)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
                {                   
                   DBUtility.GetInParameter("@encrypedorderid",encrypedorderid)
                };
            return objDB.ExecuteDataSet("CierantRandBGetOrderProductIDsfromOrder", sqlParams);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
