<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CLV-DataTable</title>
    <script src="jquery-1.12.4.min.js"></script>
    <script src="jquery.dataTables.min.js"></script>
    <link href="//cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Literal ID="ctrlLiteral" runat="server"></asp:Literal>
            <asp:Label ID="sdf" runat="server"></asp:Label>

            <input type="button" onclick="loadAll()" value="LoadAll" />
            <input type="button" onclick="BindAll()" value="Bind" />
            <table id="example" class="display" style="width: 100%">
                <thead>
                    <tr>
                        <th>Link</th>
                        <th name="NAME">ID</th>
                        <th>Name</th>
                    </tr>
                </thead>
            </table>
        </div>
    </form>
    <script>
        //$(document).ready(function () {
        //    $('#example').DataTable();
        //});

        function loadAll() {
            var da = new FormData();
            da.append('id', 5);

            $('#example').DataTable({


                //bSort: true,
                //bFilter: true,
                processing: true,
                serverSide: true,
                //data: JSON.stringify({ id: 5 }),
                ajax: {
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //contentType: "application/json",
                    //url: "Default.aspx/Data",
                    url: "CLVServices.asmx/Data",
                    data: function (d) {
                        d.webpartid = '5';
                        d.pageurl = 'https://google.com';

                        console.log(d)
                        return JSON.stringify({ parameters: d });
                    }
                }
            });
        }

        function BindAll() {
            $('#example').DataTable({
                "processing": true,
                "serverSide": true,
                "ajax": "Default.aspx/GetItems"
            });

            //$('#example').DataTable({
            //    bSort: true,
            //    bFilter: true,
            //    processing: true,
            //    bServerSide: true,
            //    sAjaxSource: "Default.aspx/Data"
            //});
        }

        function loadAll34() {

            //$('#example').DataTable({
            //    processing: true,
            //    serverSide: true,
            //    ajax: {
            //        //type: "POST",
            //        //contentType: "application/json; charset=utf-8",
            //        //contentType: "application/json",
            //        url: "Default.aspx/GetItems",
            //        data: function (d) {
            //            //d.myKey = "myValue"
            //            console.log(JSON.parse(JSON.stringify(d)));
            //            return JSON.parse(JSON.stringify(d));
            //        }
            //    }
            //    //});

            //    //$('#example').DataTable({
            //    //    "ajax": 'Default.aspx/GetItems'
            //    //});

            //})
        }

        function loadAll1() {
            //$.ajax({
            //    url: 'Default.aspx/GetItems',
            //    type: 'POST',
            //    dataType: 'json',
            //    success: function (data) {
            //        console.log(data);
            //    },
            //    error: function (data) {
            //        console.log(data);
            //    }
            //})

            //$.ajax({
            //    url: 'Default.aspx/GetItems',
            //    type: "POST",
            //    //data: formData,
            //    success: function (d) {
            //        alert(d);
            //    }
            //});

            //$.post("Default.aspx/GetItems", "", function (d) {
            //    alert(d);
            //});

            $.ajax({
                url: 'Default.aspx/GetItems',
                type: "POST",
                contentType: "application/json",
                //headers: { "Accept": "application/json; odata=verbose" },
                //beforeSend: function (XMLHttpRequest) {
                //    XMLHttpRequest.setRequestHeader("Accept", "application/json; odata=verbose");
                //},
                success: function (data) {
                    console.log(data);
                },
                error: function (error) {
                    alert(JSON.stringify(error));
                }
            });
        }

    </script>
</body>
</html>
