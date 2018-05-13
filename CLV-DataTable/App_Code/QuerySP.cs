using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QuerySP
/// </summary>
public class QuerySP
{
    public QuerySP()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public DataTable QueryData()
    {
        DataTable table = new DataTable();

        table.Columns.Add("Link", typeof(string));
        table.Columns.Add("Img", typeof(string));
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));

        table.Rows.Add("<a href='#' target='_blank' onclick='console.log(this);alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 1, "Rathanavel");
        table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 2, "Aish");
        table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 3, "Nivi");

        table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 4, "Rathanavel4");
        table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 5, "Aish5");
        table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 6, "Nivi6");

        table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 7, "Rathanavel7");
        table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 8, "Aish8");
        table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 9, "Nivi9");

        table.Rows.Add("<a href='#' target='_blank' onclick='alert(&#x27;hello&#x27;)'>Click Me</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 10, "Rathanavel10");
        table.Rows.Add("<a href='https://google.com' target='_blank'>Some Link</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 11, "Aish11");
        table.Rows.Add("<a href='https://yahoo.com' target='_blank'>File.pdf</a>", "<img src='https://placeholdit.co//i/30x30?&bg=333&fc=fff&text=IMG'>", 12, "Nivi12");


        return table;

    }
}