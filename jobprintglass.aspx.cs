using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class jobprintglass : System.Web.UI.Page
{
    string name, extranote, prepare, product;
    string q_fetch_user, q_fetch_pres_glass, q_fetch_job, q_se_fe_job, q_se_fe_user, q_se_fe_pres_glass;
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
            jobprinttypetextLabel.Text = "Fitting";
            //if (Session["jobprintpageglass"] != null)
            //{
                //Response.Write("hello");
                //dt_jobprintglass = (DataTable)Session["jobprintpageglass"];
                //for (int i = 0; i < dt_jobprintglass.Rows.Count; i++)
                //{
                    //Response.Write(i+"<br/>");
                    //Response.Write("<br/> jobid-"+dt_jobprintglass.Rows[i]["jobid"]+"<br/>");
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

                    q_se_fe_pres_glass = "select Gla_PresNo,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where Gla_PresNo='" + dt_fetch_job.Rows[0]["Pres_No"] + "' order by Gla_PresNo DESC";
                    dbo.get(q_se_fe_pres_glass, ref dt_fetch_pres);

                    SPHRightDistextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_Distance"].ToString();
                    SPHRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_NearAdd"].ToString();
                    SPHRightInttextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_IntermediateAdd"].ToString();
                    SPHRightProtextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_ProgressiveAdd"].ToString();
                    CYLRightDistextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_Distance"].ToString();
                    CYLRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_NearAdd"].ToString();
                    CYLRightInttextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_IntermediateAdd"].ToString();
                    CYLRightProtextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_ProgressiveAdd"].ToString();
                    AxisRightDistextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_Distance"].ToString();
                    AxisRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_NearAdd"].ToString();
                    AxisRightInttextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_IntermediateAdd"].ToString();
                    AxisRightProtextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_ProgressiveAdd"].ToString();
                    PDRightDistextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_Distance"].ToString();
                    PDRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_NearAdd"].ToString();
                    PDRightInttextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_IntermediateAdd"].ToString();
                    PDRightProtextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_ProgressiveAdd"].ToString();
                    VARightDistextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_Distance"].ToString();
                    VARightNeartextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_NearAdd"].ToString();
                    VARightInttextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_IntermediateAdd"].ToString();
                    VARightProtextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_ProgressiveAdd"].ToString();
                    SPHLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_Distance"].ToString();
                    SPHLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_NearAdd"].ToString();
                    SPHLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_IntermediateAdd"].ToString();
                    SPHLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_ProgressiveAdd"].ToString();
                    CYLLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_Distance"].ToString();
                    CYLLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_NearAdd"].ToString();
                    CYLLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_IntermediateAdd"].ToString();
                    CYLLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_ProgressiveAdd"].ToString();
                    AxisLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_Distance"].ToString();
                    AxisLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_NearAdd"].ToString();
                    AxisLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_IntermediateAdd"].ToString();
                    AxisLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_ProgressiveAdd"].ToString();
                    PDLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_Distance"].ToString();
                    PDLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_NearAdd"].ToString();
                    PDLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_IntermediateAdd"].ToString();
                    PDLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_ProgressiveAdd"].ToString();
                    VALeftDistextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_Distance"].ToString();
                    VALeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_NearAdd"].ToString();
                    VALeftInttextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_IntermediateAdd"].ToString();
                    VALeftProtextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_ProgressiveAdd"].ToString();

                    Response.Write("<hr size='1' color='black' />");
                //}
            //}
        }

        else if (Request.QueryString["re"] != null)
        {
            jobprinttypetextLabel.Text = "Repairing";
            q_fetch_user = "select Name,ContactNo from Customer_Personal_Details where User_Id='" + Request.QueryString["cu_id"] + "'";
            dbo.get(q_fetch_user, ref dt_fetch_user);
            custnametextLabel.Text = dt_fetch_user.Rows[0]["Name"].ToString();
            conNotextLabel.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();


            q_fetch_job = "select Date,Products,PresType,Contact_No,Extra_Note,Prepared_By from Job_Prints order by Job_Id DESC";
            dbo.get(q_fetch_job, ref dt_fetch_job);
            todaydatetextLabel.Text = dt_fetch_job.Rows[0]["Date"].ToString();
            productdesctextLabel.Text = dt_fetch_job.Rows[0]["Products"].ToString();
            prestypetextLabel.Text = dt_fetch_job.Rows[0]["PresType"].ToString();
            conNotextLabel.Text = dt_fetch_job.Rows[0]["Contact_No"].ToString();
            extranotetextLabel.Text = dt_fetch_job.Rows[0]["Extra_Note"].ToString();
            preparedtextLabel.Text = dt_fetch_job.Rows[0]["Prepared_By"].ToString();


            q_fetch_pres_glass = "select Gla_PresNo,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by Gla_PresNo DESC";
            dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);

            SPHRightDistextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_Distance"].ToString();
            SPHRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_NearAdd"].ToString();
            SPHRightInttextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_IntermediateAdd"].ToString();
            SPHRightProtextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Right_ProgressiveAdd"].ToString();
            CYLRightDistextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_Distance"].ToString();
            CYLRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_NearAdd"].ToString();
            CYLRightInttextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_IntermediateAdd"].ToString();
            CYLRightProtextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Right_ProgressiveAdd"].ToString();
            AxisRightDistextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_Distance"].ToString();
            AxisRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_NearAdd"].ToString();
            AxisRightInttextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_IntermediateAdd"].ToString();
            AxisRightProtextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Right_ProgressiveAdd"].ToString();
            PDRightDistextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_Distance"].ToString();
            PDRightNeartextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_NearAdd"].ToString();
            PDRightInttextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_IntermediateAdd"].ToString();
            PDRightProtextLabel.Text = dt_fetch_pres.Rows[0]["PD_Right_ProgressiveAdd"].ToString();
            VARightDistextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_Distance"].ToString();
            VARightNeartextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_NearAdd"].ToString();
            VARightInttextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_IntermediateAdd"].ToString();
            VARightProtextLabel.Text = dt_fetch_pres.Rows[0]["VA_Right_ProgressiveAdd"].ToString();
            SPHLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_Distance"].ToString();
            SPHLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_NearAdd"].ToString();
            SPHLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_IntermediateAdd"].ToString();
            SPHLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["SPH_Left_ProgressiveAdd"].ToString();
            CYLLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_Distance"].ToString();
            CYLLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_NearAdd"].ToString();
            CYLLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_IntermediateAdd"].ToString();
            CYLLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["CYL_Left_ProgressiveAdd"].ToString();
            AxisLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_Distance"].ToString();
            AxisLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_NearAdd"].ToString();
            AxisLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_IntermediateAdd"].ToString();
            AxisLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["Axis_Left_ProgressiveAdd"].ToString();
            PDLeftDistextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_Distance"].ToString();
            PDLeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_NearAdd"].ToString();
            PDLeftInttextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_IntermediateAdd"].ToString();
            PDLeftProtextLabel.Text = dt_fetch_pres.Rows[0]["PD_Left_ProgressiveAdd"].ToString();
            VALeftDistextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_Distance"].ToString();
            VALeftNeartextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_NearAdd"].ToString();
            VALeftInttextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_IntermediateAdd"].ToString();
            VALeftProtextLabel.Text = dt_fetch_pres.Rows[0]["VA_Left_ProgressiveAdd"].ToString();
        }
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            Response.Write("hello");
            if (Session["jobprintpageglass"] != null)
            {
                dt_jobprintglass = (DataTable)Session["jobprintpageglass"];
                dt_jobprintcl = (DataTable)Session["jobprintpagecl"];

                Response.Write(dt_jobprintglass.Rows.Count);
                for (int i = 0; i < dt_jobprintglass.Rows.Count; i++)
                {
                    Response.Write(i + "<br/>");
                    if (dt_jobprintglass.Rows[i]["jobid"].ToString() == Request.QueryString["jobid"])
                    {
                        Response.Write(dt_jobprintglass.Rows.Count + "<br/>");
                        dt_jobprintglass.Rows.RemoveAt(i);
                        Session["jobprintpageglass"] = dt_jobprintglass;
                        Response.Write(dt_jobprintglass.Rows.Count + "<br/>");
                    }
                }
            }
            if (dt_jobprintglass.Rows.Count == 0 && dt_jobprintcl.Rows.Count == 0)
            {
                Response.Redirect("billprintpage.aspx?id=" + Request.QueryString["id"]);
            }
            else
            {
                Response.Redirect("multiplejobprintpages.aspx?id=" + Request.QueryString["id"]);
            }
        }
        else
        {
            Response.Redirect("billpage.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + "&pres=" + Request.QueryString["pres"]);
        }
    }
}