using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_login : System.Web.UI.Page
{
    string path = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = btnLogin.UniqueID;
        Page.Form.DefaultFocus = txtAdminEmail.UniqueID;

        path = (String)Session["path"];
       // if (Session["Ad_Id"] == null) { Response.Redirect("login.aspx"); }
        //if (Session["Emp_Id"] == null)
        //{
        //    Response.Redirect("login.aspx");
        //}
        //else if (Session["Emp_Id"] != null &&  Session["User_Type_Id"] !="2")
        //{
        //    Response.Redirect("../login.aspx");
        //}
        //else
        //{
        //    if (Session["User_Type_Id"] != "2")
        //    {
        //        Response.Redirect("login.aspx");
        //    }
        //}
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblLoginError.Text = "";

        DataTable dt = new DataTable();
        string q_login = "select * from Staff_Personal_Details where User_Type_Id='2' and Email = '" + txtAdminEmail.Text + "' and Password = '" + txtpwd.Text + "'";
        dbo.get(q_login, ref dt);

        if (dt.Rows.Count > 0)
        {
            Session.Add("Ad_Id", dt.Rows[0]["Emp_Id"]);
            Session.Add("AdName", dt.Rows[0]["Name"]);
            Session.Add("User_Type_Id", dt.Rows[0]["User_Type_Id"]);
            Session.Timeout = 5;

            if (path != null)
                Response.Redirect(path);
            else
                Response.Redirect("default.aspx");
        }
        else
        {
            lblLoginError.Text = "Wrong combination. Try again <br /><br />";
        }
    }
}