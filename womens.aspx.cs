using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class womens : System.Web.UI.Page
{
    string q_fr;
    public DataTable dt_frimg = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        
        q_fr = "select Thumb,*,CONCAT('fr',Fr_ProdNo) as ID from Frame_Products order by Fr_ProdNo DESC";
        dbo.get(q_fr, ref dt_frimg);
        //q="select * from Frame_Products";
        //Response.Write(q);
    }
}