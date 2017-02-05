using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class solution : System.Web.UI.Page
{
    string q_sl;
    public DataTable dt_slimg = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }

        q_sl = "select top 3 Thumb,*,CONCAT('sl',Sol_ProdNo) as ID from Solution_Products order by Sol_ProdNo DESC";
        dbo.get(q_sl, ref dt_slimg);
        //q="select * from Frame_Products";
        //Response.Write(q);
    }
}