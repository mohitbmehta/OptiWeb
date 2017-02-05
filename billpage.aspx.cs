using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class billpage : System.Web.UI.Page
{
    string q_fetch_user, q_fetch_product;
    string q_update_stock, q_order_product, q_order_invoice, q_fetch_pres, q_fetch_job, q_repair_invoice;
    string id;
    int id_no, prod_quant, amount;
    int fit_amount;
    DataTable dt_fetch_user = new DataTable();
    DataTable dt_fetch_product = new DataTable();
    DataTable dt_fetch_pres = new DataTable();
    DataTable dt_job = new DataTable();
    DataTable dt_fetch_repair_invoice = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) { Response.Redirect("login.aspx"); }
        q_fetch_user = "select Name,CurrentAdd,ContactNo from Customer_Personal_Details where User_Id='" + Request.QueryString["cu_id"] + "'";
        dbo.get(q_fetch_user, ref dt_fetch_user);
        customernameTextBox.Text = dt_fetch_user.Rows[0]["Name"].ToString();
        addressTextBox.Text = dt_fetch_user.Rows[0]["CurrentAdd"].ToString();
        conNoTextBox.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();
    }



    protected void printButton_Click(object sender, EventArgs e)
    {
        string add = addressTextBox.Text;
        long conno = Convert.ToInt64(conNoTextBox.Text);
        fit_amount = Convert.ToInt32(fittingTextBox.Text);

        amount = fit_amount;
        Response.Write("hello");
        q_fetch_job = "select Job_Id from Job_Prints where User_Id='" + Request.QueryString["cu_id"] + "' order by Job_Id DESC";
        dbo.get(q_fetch_job, ref dt_job);

        q_repair_invoice = "insert into Repair_Invoices(User_Cus_Id,Date,Job_Pri_Id,Amount,ContactNo) values('" + Request.QueryString["cu_id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_job.Rows[0]["Job_Id"] + "','" + amount + "','" + conno + "')";
        dbo.dml(q_repair_invoice);
        Response.Redirect("billpage.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + "&pres=" + Request.QueryString["pres"]);

        
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        q_repair_invoice = "select Amount from Repair_Invoices where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by RepairInvoice_No DESC";
        dbo.get(q_repair_invoice, ref dt_fetch_repair_invoice);
        
        Response.Redirect("billprintpagerepair.aspx?name=" + customernameTextBox.Text + "&add=" + addressTextBox.Text + "&conno=" + conNoTextBox.Text + "&fitam=" + dt_fetch_repair_invoice.Rows[0]["Amount"] + "&amount=" + dt_fetch_repair_invoice.Rows[0]["Amount"]);
    }
}