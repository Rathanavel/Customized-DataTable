using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for CLVServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CLVServices : System.Web.Services.WebService
{

    public CLVServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Data(object parameters)
    {
        QuerySP query = new QuerySP();

        DataTable table = query.QueryData();

        var req = DataTableParameters.Get(parameters);

        DataView dv = table.DefaultView;
        //dv.Sort = "ID DESC";
        foreach (var col in req.Columns)
        {
            if (req.Order.ContainsKey(col.Key))
                dv.Sort = table.Columns[req.Order[col.Key].Column].ToString() + " " + req.Order[col.Key].Direction;
        }

        table = dv.ToTable();     

        var resultSet = new DataTableResultSet();
        resultSet.draw = req.Draw;
        resultSet.recordsTotal = table.Rows.Count;    
        resultSet.recordsFiltered = table.Rows.Count; 

        foreach (DataRow recordFromDb in table.Select().Skip(req.Start).Take(req.Length))
        {           
            //var columns = new List<string>(); // Working

            var columns = new List<string>();
            foreach (DataColumn col in table.Columns)
            {
                //columns.Add(recordFromDb[col.ColumnName].ToString()); - Working
                //columns.Add(recordFromDb[col.ColumnName] as LiteralControl);
                //columns.Add(((LiteralControl)recordFromDb[col.ColumnName]).Text);
                columns.Add(recordFromDb[col.ColumnName].ToString());
            }

            //columns.Add("<a href='https://google.com'>Link</a>");

            //columns.Add(recordFromDb[0].ToString());
            //columns.Add(recordFromDb[1].ToString());


            //columns.Add("first column value");
            //columns.Add("second column value");
            //columns.Add("third column value");
            /* you may add as many columns as you need. Each column is a string in the List<string> */
            resultSet.data.Add(columns);
        }
        
        SendResponse(HttpContext.Current.Response, resultSet);
    }

    private static void SendResponse(HttpResponse response, DataTableResultSet result)
    {
        response.Clear();
        //response.Headers.Add("X-Content-Type-Options", "nosniff");
        //response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        response.ContentType = "application/json; charset=utf-8";
        response.Write(result.ToJSON());
        response.Flush();
        response.End();
    }
}
