using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class billprintpage : System.Web.UI.Page
{
    string q_fetch_user, q_fetch_product;
    string q_update_stock, q_order_product, q_order_invoice,q_user;
    string id;
    int id_no, prod_quant, amount;
    double fit_amount;
    public DataTable dt_fetch_product = new DataTable();
    public DataTable dt_order = new DataTable();
    public DataTable dt_fetch_user = new DataTable();
    public DataTable dt_invoice = new DataTable();
    public DataTable dt_userid = new DataTable();
    public DataTable dt_billorder = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        datetextLabel2.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");

        dt_fetch_product = (DataTable)Session["confirmorder"];

        //Response.Write(dt_fetch_product.Rows[0]["promodel"].ToString());

        dt_billorder = (DataTable)Session["billproduct"];

        
        if (Session["confirmuserid"] != null)
        {
            //Response.Write("olduser");
            dt_userid = (DataTable)Session["confirmuserid"];
            q_user = "select * from Customer_Personal_Details where User_Id='" + dt_userid.Rows[0]["userid"] + "'";
            dbo.get(q_user, ref dt_fetch_user);
        }
        else if(Session["newuser"]!=null)
        {
            //Response.Write("new user");
            dt_userid = (DataTable)Session["newuser"];
            q_user = "select * from Customer_Personal_Details where User_Id='" + dt_userid.Rows[0]["userid"] + "'";
            dbo.get(q_user, ref dt_fetch_user);
        }
        //Response.Write(dt_fetch_user.Rows.Count+"<br/>");

        nametextLabel.Text = dt_fetch_user.Rows[0]["Name"].ToString();
        contacttextLabel.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();
        addresstextLabel.Text = dt_fetch_user.Rows[0]["CurrentAdd"].ToString();

        //Response.Write(Session["orderid"]);
        q_order_product = "select * from Order_Products where Order_Id='" + Session["orderid"] + "'";
        dbo.get(q_order_product, ref dt_order);
        amounttextLabel.Text = dt_order.Rows[0]["Amount"].ToString();
        //Response.Write(dt_order.Rows.Count + "<br/>");
       


        q_order_invoice = "select OrderInvoice_No from Order_Invoices where Invoice_Order_Id='" + dt_order.Rows[0]["Order_Id"] + "'";
        dbo.get(q_order_invoice, ref dt_invoice);
        invoicenotextLabel.Text = dt_invoice.Rows[0]["OrderInvoice_No"].ToString();

        //Response.Write(dt_invoice.Rows.Count + "<br/>");
    }
    protected void printButton_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("default.aspx");
    }
}