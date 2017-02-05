using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class jobprintcontactlens : System.Web.UI.Page
{
    string name, extranote, prepare, product;
    string q_fetch_user, q_fetch_pres_glass, q_fetch_pres_cl, q_fetch_job, q_se_fe_job, q_se_fe_user, q_se_fe_pres_cl;
    DataTable dt_fetch_user = new DataTable();
    DataTable dt_fetch_pres = new DataTable();
    DataTable dt_fetch_job = new DataTable();
    DataTable dt_jobprintglass = new DataTable();
    DataTable dt_jobprintcl = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }

        if (Request.QueryString["id"] != null)
        {
            jobprinttypetextLabel.Text = "fitting";
            q_se_fe_job = "select * from Job_Prints where Job_Id='" + Request.QueryString["jobid"] + "'";
            dbo.get(q_se_fe_job, ref dt_fetch_job);

            todaydatetextLabel.Text = dt_fetch_job.Rows[0]["Date"].ToString();
            productdesctextLabel.Text = dt_fetch_job.Rows[0]["Products"].ToString();
            prestypetextLabel.Text = dt_fetch_job.Rows[0]["PresType"].ToString();
            conNotextLabel.Text = dt_fetch_job.Rows[0]["Contact_No"].ToString();
            extranotetextLabel.Text = dt_fetch_job.Rows[0]["Extra_Note"].ToString();
            preparedtextLabel.Text = dt_fetch_job.Rows[0]["Prepared_By"].ToString();

            q_se_fe_user = "select Name,ContactNo from Customer_Personal_Details where User_Id='" + dt_fetch_job.Rows[0]["User_Id"] + "'";
            dbo.get(q_se_fe_user, ref dt_fetch_user);

            custnametextLabel.Text = dt_fetch_user.Rows[0]["Name"].ToString();
            conNotextLabel.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();

            q_se_fe_pres_cl = "select CL_PresNo,OD,OH from Pres_Contact_Lens where CL_PresNo='" + dt_fetch_job.Rows[0]["Pres_No"] + "' order by CL_PresNo DESC";
            dbo.get(q_se_fe_pres_cl, ref dt_fetch_pres);
            ODtextLabel.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
            OHtextLabel.Text = dt_fetch_pres.Rows[0]["OH"].ToString();
        }

        else if (Request.QueryString["re"] != null)
        {
            jobprinttypetextLabel.Text = "repairing";
            q_fetch_user = "select Name,ContactNo from Customer_Personal_Details where User_Id='" + Request.QueryString["cu_id"] + "'";
            dbo.get(q_fetch_user, ref dt_fetch_user);
            custnametextLabel.Text = dt_fetch_user.Rows[0]["Name"].ToString();
            conNotextLabel.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();

            q_fetch_pres_cl = "select CL_PresNo,OD,OH from Pres_Contact_Lens where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by CL_PresNo DESC";
            dbo.get(q_fetch_pres_cl, ref dt_fetch_pres);
            ODtextLabel.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
            OHtextLabel.Text = dt_fetch_pres.Rows[0]["OH"].ToString();

            q_fetch_job = "select Date,Products,PresType,Contact_No,Extra_Note,Prepared_By from Job_Prints order by Job_Id DESC";
            dbo.get(q_fetch_job, ref dt_fetch_job);
            todaydatetextLabel.Text = dt_fetch_job.Rows[0]["Date"].ToString();
            productdesctextLabel.Text = dt_fetch_job.Rows[0]["Products"].ToString();
            prestypetextLabel.Text = dt_fetch_job.Rows[0]["PresType"].ToString();
            conNotextLabel.Text = dt_fetch_job.Rows[0]["Contact_No"].ToString();
            extranotetextLabel.Text = dt_fetch_job.Rows[0]["Extra_Note"].ToString();
            preparedtextLabel.Text = dt_fetch_job.Rows[0]["Prepared_By"].ToString();
        }
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["re"] == null)
        {
            //if (Session["jobprintpagecl"] != null)
            //{
            dt_jobprintglass = (DataTable)Session["jobprintpageglass"];
                dt_jobprintcl = (DataTable)Session["jobprintpagecl"];
                for (int i = 0; i < dt_jobprintcl.Rows.Count; i++)
                {
                    Response.Write(i + "<br/>");
                    if (dt_jobprintcl.Rows[i]["jobid"].ToString() == Request.QueryString["jobid"])
                    {
                        Response.Write(dt_jobprintcl.Rows.Count + "<br/>");
                        dt_jobprintcl.Rows.RemoveAt(i);
                        Session["jobprintpagecl"] = dt_jobprintcl;
                        Response.Write(dt_jobprintcl.Rows.Count + "<br/>");
                    }
                }
            //}
            if (dt_jobprintglass.Rows.Count == 0 && dt_jobprintcl.Rows.Count == 0)
            {
               Response.Redirect("billprintpage.aspx?id=" + Request.QueryString["id"]);
            }
            else
            {
                Response.Redirect("multiplejobprintpages.aspx?id=" + Request.QueryString["id"]);
            }
            //Response.Redirect("billpage.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + "&pres=" + Request.QueryString["pres"]);
        }
        else
        {
            Response.Redirect("billpage.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + "&pres=" + Request.QueryString["pres"]);
        }
    }
}