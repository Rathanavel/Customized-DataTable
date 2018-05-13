using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class _Default : System.Web.UI.Page
{   
    QuerySP query = new QuerySP();

    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateTable(query.QueryData());
    }

    public void GenerateTable(DataTable table)
    {
        /*
        DataTable table = new DataTable();

        table.Columns.Add("Link", typeof(string));
        table.Columns.Add("Img", typeof(string));
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));


        table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 1, "Rathanavel");
        table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 2, "Aish");
        table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 3, "Navneet Rathanavel");

        table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 1, "Rathanavel");
        table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 2, "Aishwarya");

        ctrlLiteral.Text = ConvertDataTableToHTMLTableInOneLine(table);
        */

        string renderScript = @"<script>$('#webpartid').DataTable({
                                            paging    : true,
                                            ordering  : true, // Global Sorting enable/disable
                                            info      : false,
                                            filter    : false,
                                            length    : false,
                                            processing: true,
                                            serverSide: true,
                                            pagingType: 'simple_numbers', // numbers | simple | simple_numbers | full | full_numbers | first_last_numbers - https://datatables.net/reference/option/pagingType
                                            ajax:
                                            {
                                                type: 'POST',
                                                contentType: 'application/json; charset=utf-8',
                                                url: 'CLVServices.asmx/Data',
                                                data: function(d) {
                                                    d.webpartid = '5';
                                                    d.pageurl = 'https://google.com';
                                                    console.log(d)
                                                    return JSON.stringify({ parameters: d });
                                                }
                                            },
                                            //scrollY:'200px',
                                            //scrollCollapse: true,
                                            order: [],
                                            columnDefs: [
                                               { 'targets': 3, 'name':'jgg', 'title': 'IDD', 'orderable': true, 'searchable': false, 'width': 'auto', 'defaultContent': 'sdfsf', 'render': function (data, type, full, meta) {             
                                                //console.log(data.length) 
                                                if(data.length != 0){
                                                                   return data;
                                                                }else{
                                                                   return '-';
                                                                }
                                                            } 
                                                }
                                              ]
                                        });</script>";
        ctrlLiteral.Text = ConvertDataTableToHTML(
            table,       // Actual DataTable
            "webpartid", //Webpart id
            "dataTable", //ClassName
            "border:5px solid red;border-top:10px solid #009688" // Custom style attributes 
            ) + renderScript;

    }

    public string ConvertDataTableToHTML(DataTable dt, string tableID, string tableClass, string tableStyle)
    {
        string html = "<table id='" + tableID + "' class='" + tableClass + "' style='" + tableStyle + "' >";
        //add header row
        html += "<thead>";
        for (int i = 0; i < dt.Columns.Count; i++)
            html += "<th>" + dt.Columns[i].ColumnName + "</th>";
        html += "</thead>";

        html += "<tbody>";
        //add rows
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count; j++)
                html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
            html += "</tr>";
        }
        html += "</tbody>";

        html += "</table>";
        return html;
    }

    #region Old Code
    /*
    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Data(object parameters)
    {
        DataTable table = new DataTable();

        //table.Columns.Add("Link", typeof(string));
        //table.Columns.Add("Img", typeof(string));
        //table.Columns.Add("ID", typeof(int));
        //table.Columns.Add("Name", typeof(string));


        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 1, "Rathanavel");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 2, "Aish");
        //table.Rows.Add(new HyperLinkColumn() { Target = "https://google.com", Text = "Google" }, 3, "Navneet Rathanavel");

        //table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 1, "Rathanavel");
        //table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 2, "Aishwarya");
        //table.Rows.Add(new LiteralControl("<a href='https://google.com'></a>"),1,"Rathanavel");
        //table.Rows.Add(new HyperLink() { Target = "https://google.com", Text = "Google", NavigateUrl = "https://google.com" }, 1, "Rathanavel");
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
        resultSet.recordsTotal = table.Rows.Count;    // total number of records in table 
        resultSet.recordsFiltered = table.Rows.Count; // number of records after search - box filtering is applied 

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

    //public DataTable QueryData()
    //{
    //    DataTable table = new DataTable();

    //    table.Columns.Add("Link", typeof(string));
    //    table.Columns.Add("Img", typeof(string));
    //    table.Columns.Add("ID", typeof(int));
    //    table.Columns.Add("Name", typeof(string));

    //    table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 1, "Rathanavel");
    //    table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 2, "Aish");
    //    table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 3, "Nivi");

    //    table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 4, "Rathanavel4");
    //    table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 5, "Aish5");
    //    table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 6, "Nivi6");

    //    table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 7, "Rathanavel7");
    //    table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 8, "Aish8");
    //    table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 9, "Nivi9");

    //    table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 10, "Rathanavel10");
    //    table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 11, "Aish11");
    //    table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 12, "Nivi12");


    //    return table;

    //}

    */
    #endregion

}

