using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class mens : System.Web.UI.Page
{
    string q_gl;
    public  DataTable dt_glimg = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }
        
        q_gl = "select Thumb,*,CONCAT('gl',Gla_ProdNo) as ID from Glass_Products order by Gla_ProdNo DESC";
        dbo.get(q_gl, ref dt_glimg);
    }
}