using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class kids : System.Web.UI.Page
{
    string q_cl;
    public DataTable dt_climg = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }

        q_cl = "select top 3 Thumb,*,CONCAT('cl',CL_ProdNo) as ID from Contact_Lens_Products order by CL_ProdNo DESC";
        dbo.get(q_cl, ref dt_climg);
    }
}