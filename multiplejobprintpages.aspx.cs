using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class multiplejobprintpages : System.Web.UI.Page
{
    public DataTable dt_jobprintglass = new DataTable();
    public DataTable dt_jobprintcl = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        
        dt_jobprintglass = (DataTable)Session["jobprintpageglass"];
        dt_jobprintcl = (DataTable)Session["jobprintpagecl"];
        Response.Write(dt_jobprintglass.Rows.Count);

        if (dt_jobprintglass.Rows.Count == 0)
            glassjob.Visible = false;
        else if (dt_jobprintcl.Rows.Count == 0)
            cljob.Visible = false;
    }
}