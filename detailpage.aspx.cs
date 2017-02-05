using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class detailpage : System.Web.UI.Page
{
    public static int status = 0;
    DataTable dt_pro = new DataTable();
    public DataTable dt_model = new DataTable();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }
        string id_pro = Request.QueryString["id"];
        id_pro = id_pro.Substring(0, 2);
        string q_model;
        int id_no = Convert.ToInt32(Request.QueryString["id"].Substring(2));

        if (Session["status"] != null)
            status = Convert.ToInt32(Session["status"]);
        else
            status = 0;

        if (id_pro == "fr")
        {
            q_model = "select ModelName,Price,Quant from Frame_Products where Fr_ProdNo='" + id_no + "'";
            dbo.get(q_model, ref dt_model);
        }
        else if (id_pro == "gl")
        {
            q_model = "select ModelName,Price,Quant from Glass_Products where Gla_ProdNo='" + id_no + "'";
            dbo.get(q_model, ref dt_model);
        }
        else if (id_pro == "cl")
        {
            ddlfitting.SelectedIndex = ddlfitting.Items.IndexOf(ddlfitting.Items.FindByValue("yes"));
            ddlfitting.Enabled = false;
            fitamtTextBox.Text = Convert.ToString(0);
            fitamtTextBox.Enabled = false;
            ddlrequired.SelectedIndex = ddlrequired.Items.IndexOf(ddlrequired.Items.FindByValue("yes"));
            ddlrequired.Enabled = false;
            q_model = "select ModelName,Price,Quant from Contact_Lens_Products where CL_ProdNo='" + id_no + "'";
            dbo.get(q_model, ref dt_model);
        }
        else if (id_pro == "sl")
        {
            ddlfitting.SelectedIndex = ddlfitting.Items.IndexOf(ddlfitting.Items.FindByValue("no"));
            ddlfitting.Enabled = false;
            ddlrequired.SelectedIndex = ddlrequired.Items.IndexOf(ddlrequired.Items.FindByValue("no"));
            ddlrequired.Enabled = false;
            q_model = "select ModelName,Price,Quant from Solution_Products where Sol_ProdNo='" + id_no + "'";
            dbo.get(q_model, ref dt_model);
        }

            dt_pro.Columns.Add("proid");
            dt_pro.Columns.Add("promodel");
            dt_pro.Columns.Add("proquant");
            dt_pro.Columns.Add("proprice");
            dt_pro.Columns.Add("presreqd");
            dt_pro.Columns.Add("fitreqd");
            dt_pro.Columns.Add("fitamt");
            dt_pro.Columns.Add("amount");
            dt_pro.Columns.Add("prestype");
            dt_pro.Columns.Add("status");
     }

    protected void buyButton_Click(object sender, EventArgs e)
    {
        int quant;
        quant = Convert.ToInt32(Math.Floor(Convert.ToDouble(quantyTextBox.Text)));
        string id_pro = Request.QueryString["id"];
        id_pro = id_pro.Substring(0, 2);
        int id_no = Convert.ToInt32(Request.QueryString["id"].Substring(2));
        string q_quant;
        DataTable dt_quant = new DataTable();

        if (id_pro == "fr")
        {
            q_quant = "select Quant from Frame_Products where Fr_ProdNo='" + id_no + "'";
            dbo.get(q_quant, ref dt_quant);
        }
        else if (id_pro == "gl")
        {
            q_quant = "select Quant from Glass_Products where Gla_ProdNo='" + id_no + "'";
            dbo.get(q_quant, ref dt_quant);
        }
        else if (id_pro == "cl")
        {
            q_quant = "select Quant from Contact_Lens_Products where CL_ProdNo='" + id_no + "'";
            dbo.get(q_quant, ref dt_quant);
        }
        else if (id_pro == "sl")
        {
            q_quant = "select Quant from Solution_Products where Sol_ProdNo='" + id_no + "'";
            dbo.get(q_quant, ref dt_quant);
        }

        if (quant > Convert.ToInt32(dt_quant.Rows[0]["Quant"]) || quant <= 0 || quantyTextBox.Text == null)
        {
            Response.Redirect("detailpage.aspx?id=" + Request.QueryString["id"] + "&price=" + Request.QueryString["price"]);
            quantyTextBox.Text = "";
        }
        else
        {
            if (ddlfitting.SelectedItem.Value == "no")
                fitamtTextBox.Text = Convert.ToString(0);
            else if (ddlfitting.SelectedItem.Value == "yes")
            {
                if (fitamtTextBox.Text == null || Convert.ToInt32(fitamtTextBox.Text) < 0)
                {
                    Response.Redirect("detailpage.aspx?id=" + Request.QueryString["id"] + "&price=" + Request.QueryString["price"]);
                    fitamtTextBox.Text = "";
                }
            }
            int amt = (Convert.ToInt32(Request.QueryString["price"]) * quant);
            amt = amt + Convert.ToInt32(fitamtTextBox.Text);
            
            dt_pro.Rows.Add(Request.QueryString["id"], dt_model.Rows[0]["ModelName"].ToString(), quant, Request.QueryString["price"], ddlrequired.SelectedItem.Value, ddlfitting.SelectedItem.Value, fitamtTextBox.Text, amt,"null",status);
            Session["status"]=++status;
            
            Session["order"] = dt_pro;
            if (Session["flag"] == null)
            {
                if (dt_pro.Rows[0]["fitreqd"] == "yes")
                {
                    Session.Add("flag", "yes");
                }
                else
                {
                    Session.Add("flag", "no");
                }
            }
            //Session.Add("proquant",quant);
            //Response.Redirect("register.aspx?id=" + Request.QueryString["id"] + "&qty=" + quant + "&price=" + Request.QueryString["price"]);

            if (Session["userid"] == null && Session["register"] == null)
            {
                    Response.Redirect("register.aspx?id=" + 1);
            }
            
            else
            {
                string id = Request.QueryString["id"].Substring(0, 2);
                if (id == "fr")
                {
                    if (ddlfitting.SelectedItem.Value == "yes" && ddlrequired.SelectedItem.Value == "yes")
                    {
                        Response.Redirect("prescription.aspx?id=" + 1);
                    }
                    else if (ddlfitting.SelectedItem.Value == "yes" || ddlrequired.SelectedItem.Value == "no")
                    {
                        Response.Redirect("productconfirmation.aspx?id=" + 1);
                    }
                }
                else if (id == "gl" || id == "cl")
                {
                    if (ddlrequired.SelectedItem.Value == "yes")
                    {
                        Response.Redirect("prescription.aspx?id=" + 1);
                    }
                    else
                    {
                        Response.Redirect("productconfirmation.aspx?id=" + 1);
                    }
                }
                else if (id == "sl")
                {
                    Response.Redirect("productconfirmation.aspx?id=" + 1);
                }
            }
        }
        //protected void nextButton_Click(object sender, EventArgs e)
        //{
        //    int quant;
        //    quant = Convert.ToInt32(Math.Floor(Convert.ToDouble(quantyTextBox.Text)));
        //    string id_pro = Request.QueryString["id"];
        //    id_pro = id_pro.Substring(0, 2);
        //    int id_no = Convert.ToInt32(Request.QueryString["id"].Substring(2));
        //    string q_quant;
        //    DataTable dt_quant = new DataTable();

        //    if (id_pro == "fr")
        //    {
        //        q_quant = "select Quant from Frame_Products where Fr_ProdNo='" + id_no + "'";
        //        dbo.get(q_quant, ref dt_quant);
        //    }
        //    else if (id_pro == "gl")
        //    {
        //        q_quant = "select Quant from Glass_Products where Gla_ProdNo='" + id_no + "'";
        //        dbo.get(q_quant, ref dt_quant);
        //    }
        //    else if (id_pro == "cl")
        //    {
        //        q_quant = "select Quant from Contact_Lens_Products where CL_ProdNo='" + id_no + "'";
        //        dbo.get(q_quant, ref dt_quant);
        //    }
        //    else if (id_pro == "sl")
        //    {
        //        q_quant = "select Quant from Solution_Products where Sol_ProdNo='" + id_no + "'";
        //        dbo.get(q_quant, ref dt_quant);
        //    }

        //    if (quant > Convert.ToInt32(dt_quant.Rows[0]["Quant"]) || quant <= 0 || quantyTextBox.Text == null)
        //    {
        //        Response.Redirect("detailpage.aspx?id=" + Request.QueryString["id"] + "&price=" + Request.QueryString["price"]);
        //        quantyTextBox.Text = "";
        //    }
        //    else
        //    {
        //        int amt = Convert.ToInt32(Request.QueryString["price"]) * quant + Convert.ToInt32(fitamtTextBox.Text);
        //        dt_pro.Rows.Add(Request.QueryString["id"], dt_model.Rows[0]["ModelName"].ToString(), quant, Request.QueryString["price"], ddlrequired.SelectedItem.Value, ddlfitting.SelectedItem.Value,fitamtTextBox.Text, amt);
        //        Session["order"] = dt_pro;
        //        //Session.Add("proquant",quant);
        //        //Response.Redirect("register.aspx?id=" + Request.QueryString["id"] + "&qty=" + quant + "&price=" + Request.QueryString["price"]);
        //        Response.Redirect("register.aspx?id=" + 1);
        //    }
        //}
    }
}