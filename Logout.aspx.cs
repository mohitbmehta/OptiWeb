﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ad_Id"] == null) { Response.Redirect("login.aspx"); }
        Session.Clear();
        //Session.RemoveAll();
        //Session.Abandon();
        Response.Redirect("login.aspx");
    }
}