using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class productconfirmation : System.Web.UI.Page
{
    DataTable dt_order = new DataTable();
    DataTable dt_register = new DataTable();
    DataTable dt_medical = new DataTable();
    DataTable dt_cl = new DataTable();
    DataTable dt_glass = new DataTable();
    DataTable dt_job = new DataTable();
    DataTable dt_userid = new DataTable();
    DataTable dt_oldpres = new DataTable();

    DataRow dr;

    DataTable dt_confirmorder = new DataTable();
    DataTable dt_confirmregister = new DataTable();
    DataTable dt_confirmmedical = new DataTable();
    DataTable dt_confirmcl = new DataTable();
    DataTable dt_confirmglass = new DataTable();
    DataTable dt_confirmjob = new DataTable();
    DataTable dt_confirmuserid = new DataTable();
    DataTable dt_confirmoldpres = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }



        dt_order = (DataTable)Session["order"];
        dt_register = (DataTable)Session["register"];
        dt_medical = (DataTable)Session["medical"];
        dt_glass = (DataTable)Session["glass"];
        dt_cl = (DataTable)Session["contactlens"];
        dt_job = (DataTable)Session["jobprint"];
        dt_userid = (DataTable)Session["userid"];
        dt_oldpres = (DataTable)Session["oldpres"];

        //if (!Page.IsPostBack)
        //{
            modelnameTextLabel.Text = dt_order.Rows[0]["promodel"].ToString();
            quantTextBox.Text = dt_order.Rows[0]["proquant"].ToString();
            priceTextLabel.Text = dt_order.Rows[0]["proprice"].ToString();
            fitTextLabel.Text = dt_order.Rows[0]["fitamt"].ToString();
            amountTextLabel.Text = dt_order.Rows[0]["amount"].ToString();
        //}
        if (Session["order"] != null)
        {
            dt_order = (DataTable)Session["order"];
            
           
            if (Session["confirmorder"] != null)
            {
                
                dt_confirmorder = (DataTable)Session["confirmorder"];
            }
            else
            {
                dt_confirmorder = dt_order.Clone();
                Session.Add("confirmorder", dt_confirmorder);
            }

        }

        if (Session["register"] != null)
        {
            if (Session["confirmregister"] != null)
                dt_confirmregister = (DataTable)Session["confirmregister"];
            else
            {
                dt_confirmregister = dt_register.Clone();
                Session.Add("confirmregister", dt_confirmregister);
            }

        }

        if (Session["userid"] != null)
        {
            if (Session["confirmuserid"] != null)
                dt_confirmuserid = (DataTable)Session["confirmuserid"];
            else
            {
                dt_confirmuserid = dt_userid.Clone();
                Session.Add("confirmuserid", dt_confirmuserid);
            }

        }

        if (Session["medical"] != null)
        {
            if (Session["confirmmedical"] != null)
                dt_confirmmedical = (DataTable)Session["confirmmedical"];
            else
            {
                dt_confirmmedical = dt_medical.Clone();
                Session.Add("confirmmedical", dt_confirmmedical);
            }

        }

        if (Session["glass"] != null)
        {
            if (Session["confirmglass"] != null)
                dt_confirmglass = (DataTable)Session["confirmglass"];
            else
            {
                dt_confirmglass = dt_glass.Clone();
                Session.Add("confirmglass", dt_confirmglass);
            }
        }

        if (Session["contactlens"] != null)
        {
            if (Session["confrimcontactlens"] != null)
                dt_confirmcl = (DataTable)Session["confirmcontactlens"];
            else
            {
                dt_confirmcl = dt_cl.Clone();
                Session.Add("confirmcontactlens", dt_confirmcl);
            }
        }
        if (Session["jobprint"] != null)
        {
            if (Session["confirmjobprint"] != null)
                dt_confirmjob = (DataTable)Session["confirmjobprint"];
            else
            {
                dt_confirmjob = dt_job.Clone();
                Session.Add("confirmjobprint", dt_confirmjob);
            }
            
        }

        if (Session["oldpres"] != null)
        {
            if (Session["confirmoldpres"] != null)
                dt_confirmoldpres = (DataTable)Session["confirmoldpres"];
            else
            {
                dt_confirmoldpres = dt_oldpres.Clone();
                Session.Add("confirmoldpres", dt_confirmoldpres);
            }
        }
        
    }
    protected void cancelproductButton_Click(object sender, EventArgs e)
    {
        dt_order.Clear();
        Session["order"] = dt_order;
        if (Session["medical"] != null)
        {
            dt_medical.Clear();
            if (Session["glass"] != null)
            {
                dt_glass.Clear();
                Session["glass"] = dt_glass;
            }
            if (Session["contactlens"] != null)
            {
                dt_cl.Clear();
                Session["contactlens"] = dt_cl;
            }
            Session["medical"] = dt_medical;
        }
        if (Session["jobprint"] != null)
        {
            dt_job.Clear();
            Session["jobprint"] = dt_job;
        }
        if (Session["oldpres"] != null)
        {
            dt_oldpres.Clear();
            Session["oldpres"] = dt_oldpres;
        }
        Response.Redirect("confirmation.aspx?id="+Request.QueryString["id"]);
    }
    protected void addcartButton_Click(object sender, EventArgs e)
    {
        

            if (Session["userid"] != null)
            {
                dt_userid = (DataTable)Session["userid"];
                //dt_confirmuserid = dt_userid.Clone();
                dr = dt_confirmuserid.NewRow();
                dr.ItemArray = dt_userid.Rows[0].ItemArray;
                dt_confirmuserid.Rows.Add(dr);
                Session["confirmuserid"] = dt_confirmuserid;
            }
            else
            {
                dt_register = (DataTable)Session["register"];
                //dt_confirmregister = dt_register.Clone();
                dr = dt_confirmregister.NewRow();
                dr.ItemArray = dt_register.Rows[0].ItemArray;
                dt_confirmregister.Rows.Add(dr);
                Session["confirmregister"] = dt_confirmregister;
            }
            if (dt_order.Rows[0]["presreqd"] == "yes")
            {
                if (dt_order.Rows[0]["prestype"] == "new")
                {
                    dt_medical = (DataTable)Session["medical"];
                    for (int i = 0; i < dt_confirmmedical.Rows.Count; i++)
                    {
                        if (dt_confirmmedical.Rows[i]["proid"] == dt_medical.Rows[0]["proid"] && dt_confirmmedical.Rows[i]["status"] == dt_medical.Rows[0]["status"])
                        {
                            dt_confirmmedical.Rows.RemoveAt(i);
                        }
                    }
                    //dt_confirmmedical = dt_medical.Clone();
                    dr = dt_confirmmedical.NewRow();
                    dr.ItemArray = dt_medical.Rows[0].ItemArray;
                    dt_confirmmedical.Rows.Add(dr);
                    Session["confirmmedical"] = dt_confirmmedical;

                    if (Session["glass"] != null)
                    {
                        dt_glass = (DataTable)Session["glass"];
                        for (int i = 0; i < dt_confirmglass.Rows.Count; i++)
                        {
                            if (dt_confirmglass.Rows[i]["proid"] == dt_glass.Rows[0]["proid"] && dt_confirmglass.Rows[i]["status"] == dt_glass.Rows[0]["status"])
                            {
                                dt_confirmglass.Rows.RemoveAt(i);
                            }
                        }
                        //dt_confirmglass = dt_glass.Clone();
                        dr = dt_confirmglass.NewRow();
                        dr.ItemArray = dt_glass.Rows[0].ItemArray;
                        dt_confirmglass.Rows.Add(dr);
                        Session["confirmglass"] = dt_confirmglass;
                    }
                    else if (Session["contactlens"] != null)
                    {
                        dt_cl = (DataTable)Session["contactlens"];
                        for (int i = 0; i < dt_confirmcl.Rows.Count; i++)
                        {
                            if (dt_confirmcl.Rows[i]["proid"] == dt_cl.Rows[0]["proid"] && dt_confirmcl.Rows[i]["status"] == dt_cl.Rows[0]["status"])
                            {
                                dt_confirmcl.Rows.RemoveAt(i);
                            }
                        }
                        //dt_confirmcl = dt_cl.Clone();
                        dr = dt_confirmcl.NewRow();
                        dr.ItemArray = dt_cl.Rows[0].ItemArray;
                        dt_confirmcl.Rows.Add(dr);
                        Session["confirmcontactlens"] = dt_confirmcl;
                    }
                }
                else if (dt_order.Rows[0]["prestype"] == "previous")
                {
                    dt_oldpres = (DataTable)Session["oldpres"];
                    for (int i = 0; i < dt_confirmoldpres.Rows.Count; i++)
                    {
                        if (dt_confirmoldpres.Rows[i]["proid"] == dt_oldpres.Rows[0]["proid"] && dt_confirmoldpres.Rows[i]["status"] == dt_oldpres.Rows[0]["status"])
                        {
                            dt_confirmoldpres.Rows.RemoveAt(i);
                        }
                    }
                    //dt_confirmoldpres = dt_oldpres.Clone();
                    dr = dt_confirmoldpres.NewRow();
                    dr.ItemArray = dt_oldpres.Rows[0].ItemArray;
                    dt_confirmoldpres.Rows.Add(dr);
                    Session["confirmoldpres"] = dt_confirmoldpres;
                }
                if (dt_order.Rows[0]["fitreqd"] == "yes")
                {
                    dt_job = (DataTable)Session["jobprint"];
                    for (int i = 0; i < dt_confirmjob.Rows.Count; i++)
                    {
                        if (dt_confirmjob.Rows[i]["proid"] == dt_job.Rows[0]["proid"] && dt_confirmjob.Rows[i]["status"] == dt_job.Rows[0]["status"])
                        {
                            dt_confirmjob.Rows.RemoveAt(i);
                        }
                    }
                    //dt_confirmjob = dt_job.Clone();
                    dr = dt_confirmjob.NewRow();
                    dr.ItemArray = dt_job.Rows[0].ItemArray;
                    dt_confirmjob.Rows.Add(dr);
                    Session["confirmjobprint"] = dt_confirmjob;
                }
            }
        
            dt_order = (DataTable)Session["order"];

            for (int i = 0; i < dt_confirmorder.Rows.Count; i++)
            {
                if (dt_confirmorder.Rows[i]["proid"] == dt_order.Rows[0]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_order.Rows[0]["status"])
                {
                    dt_confirmorder.Rows.RemoveAt(i);
                }
            }
                //dt_confirmorder = dt_order.Clone();
            dr = dt_confirmorder.NewRow();
            dr.ItemArray = dt_order.Rows[0].ItemArray;
            dt_confirmorder.Rows.Add(dr);
            Session["confirmorder"] = dt_confirmorder;

        Response.Redirect("confirmation.aspx?id=" + Request.QueryString["id"]);
    }

    protected void calButton_Click(object sender, EventArgs e)
    {
        //Response.Write(addquantTextBox.Text);
        int toquant = Convert.ToInt32(addquantTextBox.Text) + Convert.ToInt32(quantTextBox.Text);
        Response.Write(toquant);
        int amount = toquant * Convert.ToInt32(priceTextLabel.Text);
        amount = amount + Convert.ToInt32(fitTextLabel.Text);
        //Response.Write(amount);
        amountTextLabel.Text = Convert.ToString(amount);
        dt_order.Rows[0]["proquant"] = toquant;
        dt_order.Rows[0]["amount"] = amount;
        Session["order"] = dt_order;
        //int amt = Convert.ToInt32(quantTextBox.Text) * Convert.ToInt32(dt_order.Rows[0]["proprice"]);
        //Response.Write(amt);
    }
}