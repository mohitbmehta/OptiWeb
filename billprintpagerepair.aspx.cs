using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class billprintpagerepair : System.Web.UI.Page
{
    string q_fetch_user, q_fetch_product;
    string q_update_stock, q_order_product, q_repair_invoice;
    string id;
    int id_no, prod_quant, amount;
    double fit_amount;
    DataTable dt_fetch_user = new DataTable();
    DataTable dt_fetch_product = new DataTable();
    DataTable dt_repair_invoice = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        customernametextLabel.Text = Request.QueryString["name"];
        addresstextLabel.Text = Request.QueryString["add"];
        conNotextLabel.Text = Request.QueryString["conno"];
        fittingtextLabel.Text = Request.QueryString["fitam"];
        amounttextLabel.Text = Request.QueryString["amount"];
    }
    protected void printButton_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("default.aspx");
    }
}