using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class _Default : System.Web.UI.Page
{
    DataTable table2 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        SetDataTable();
    }

    [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Data(object parameters)
    {
        DataTable table = new DataTable();

        table.Columns.Add("Link", typeof(HyperLink));
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));


        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 1, "Rathanavel");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 2, "Aish");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 3, "Navneet Rathanavel");

        table.Rows.Add(new HyperLink() { Target = "https://google.com", Text = "Google", NavigateUrl = "https://google.com" }, 1, "Rathanavel");
        //table.Rows.Add(new HyperLinkField() {  NavigateUrl = "https://google.com",Target = "Google" }, 1, "Rathanavel");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 2, "Aish");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 3, "Navneet Rathanavel");

        //table.Rows.Add(1, "Rathanavel");
        //table.Rows.Add(2, "Aish");
        //table.Rows.Add(3, "Navneet Rathanavel");

        //table.Rows.Add(4, "Rathanavel4");
        //table.Rows.Add(5, "Aish5");
        //table.Rows.Add(6, "Navneet Rathanavel6");

        //table.Rows.Add(7, "Rathanavel7");
        //table.Rows.Add(8, "Aish8");
        //table.Rows.Add(9, "Navneet Rathanavel9");

        //table.Rows.Add(10, "Rathanavel10");
        //table.Rows.Add(11, "Aish11");
        //table.Rows.Add(12, "Navneet Rathanavel12");

        //table.Rows.Add(7, "Rathanavel7");
        //table.Rows.Add(8, "Aish8");
        //table.Rows.Add(9, "Navneet Rathanavel9");

        //table.Rows.Add(10, "Rathanavel10");
        //table.Rows.Add(11, "Aish11");
        //table.Rows.Add(12, "Navneet Rathanavel12");

        //table.Rows.Add(7, "Rathanavel7");
        //table.Rows.Add(8, "Aish8");
        //table.Rows.Add(9, "Navneet Rathanavel9");

        //table.Rows.Add(10, "Rathanavel10");
        //table.Rows.Add(11, "Aish11");
        //table.Rows.Add(12, "Navneet Rathanavel12");

        var req = DataTableParameters.Get(parameters);

        DataView dv = table.DefaultView;
        //dv.Sort = "ID DESC";
        foreach (var col in req.Columns)
        {
            if (req.Order.ContainsKey(col.Key))
                dv.Sort = table.Columns[req.Order[col.Key].Column].ToString() + " " + req.Order[col.Key].Direction;
        }

        table = dv.ToTable();

        //...

        var resultSet = new DataTableResultSet();
        resultSet.draw = req.Draw;
        resultSet.recordsTotal = table.Rows.Count;    /* total number of records in table */
        resultSet.recordsFiltered = table.Rows.Count; /* number of records after search - box filtering is applied */

        foreach (DataRow recordFromDb in table.Select().Skip(req.Start).Take(req.Length))
        {
            /* this is pseudocode */
            //var columns = new List<string>(); // Working

            var columns = new List<LiteralControl>();
            foreach (DataColumn col in table.Columns)
            {
                //columns.Add(recordFromDb[col.ColumnName].ToString()); - Working
                columns.Add(recordFromDb[col.ColumnName] as LiteralControl);
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

        //var result = new
        //{
        //    iTotalRecords = table.Rows.Count

        //};

        SendResponse(HttpContext.Current.Response, resultSet);
    }

    public static void LoadData()
    {

    }

    private static void SendResponse(HttpResponse response, DataTableResultSet result)
    {
        response.Clear();
        response.Headers.Add("X-Content-Type-Options", "nosniff");
        response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        response.ContentType = "application/json; charset=utf-8";
        response.Write(result.ToJSON());
        response.Flush();
        response.End();
    }

    [WebMethod]
    public static string GetItems()
    {
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));

        table.Rows.Add(1, "Rathanavel");
        table.Rows.Add(2, "Aish");
        table.Rows.Add(3, "Navneet Rathanavel");

        return DataTableToJsonWithJavaScriptSerializer(table);
        //return "";
    }

    public static string DataTableToJsonWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    void SetDataTable()
    {
        table2.Columns.Add("ID", typeof(int));
        table2.Columns.Add("Name", typeof(string));

        table2.Rows.Add(1, "Rathanavel");
        table2.Rows.Add(2, "Aish");
        table2.Rows.Add(3, "Navneet Rathanavel");

    }


}

public class DataTableResultSet
{
    /// <summary>Array of records. Each element of the array is itself an array of columns</summary>
    //public List<List<string>> data = new List<List<string>>();
    public List<List<LiteralControl>> data = new List<List<LiteralControl>>();

    /// <summary>value of draw parameter sent by client</summary>
    public int draw;

    /// <summary>filtered record count</summary>
    public int recordsFiltered;

    /// <summary>total record count in resultset</summary>
    public int recordsTotal;

    public string ToJSON()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
//[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DataTableResultError : DataTableResultSet
{
    public string error;
}

public class DataTableParameters
{
    public Dictionary<int, DataTableColumn> Columns;
    public int Draw;
    public int Length;
    public Dictionary<int, DataTableOrder> Order;
    public bool SearchRegex;
    public string SearchValue;
    public int Start;

    private DataTableParameters()
    {
    }

    /// <summary>
    /// Retrieve DataTable parameters from WebMethod parameter, sanitized against parameter spoofing
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static DataTableParameters Get(object input)
    {
        return Get(JObject.FromObject(input));
    }

    /// <summary>
    /// Retrieve DataTable parameters from JSON, sanitized against parameter spoofing
    /// </summary>
    /// <param name="input">JToken object</param>
    /// <returns>parameters</returns>
    public static DataTableParameters Get(JToken input)
    {
        return new DataTableParameters
        {
            Columns = DataTableColumn.Get(input),
            Order = DataTableOrder.Get(input),
            Draw = (int)input["draw"],
            Start = (int)input["start"],
            Length = (int)input["length"],
            SearchValue =
                new string(
                    ((string)input["search"]["value"]).Where(
                        c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
            SearchRegex = (bool)input["search"]["regex"]
        };
    }
}

public class DataTableColumn
{
    public int Data;
    public string Name;
    public bool Orderable;
    public bool Searchable;
    public bool SearchRegex;
    public string SearchValue;

    private DataTableColumn()
    {
    }

    /// <summary>
    /// Retrieve the DataTables Columns dictionary from a JSON parameter list
    /// </summary>
    /// <param name="input">JToken object</param>
    /// <returns>Dictionary of Column elements</returns>
    public static Dictionary<int, DataTableColumn> Get(JToken input)
    {
        return (
            (JArray)input["columns"])
            .Select(col => new DataTableColumn
            {
                Data = (int)col["data"],
                Name =
                    new string(
                        ((string)col["name"]).Where(
                            c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
                Searchable = (bool)col["searchable"],
                Orderable = (bool)col["orderable"],
                SearchValue =
                    new string(
                        ((string)col["search"]["value"]).Where(
                            c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
                SearchRegex = (bool)col["search"]["regex"]
            })
            .ToDictionary(c => c.Data);
    }
}

public class DataTableOrder
{
    public int Column;
    public string Direction;

    private DataTableOrder()
    {
    }

    /// <summary>
    /// Retrieve the DataTables order dictionary from a JSON parameter list
    /// </summary>
    /// <param name="input">JToken object</param>
    /// <returns>Dictionary of Order elements</returns>
    public static Dictionary<int, DataTableOrder> Get(JToken input)
    {
        return (
            (JArray)input["order"])
            .Select(col => new DataTableOrder
            {
                Column = (int)col["column"],
                Direction =
                    ((string)col["dir"]).StartsWith("desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC"
            })
            .ToDictionary(c => c.Column);
    }
}