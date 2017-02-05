using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class jobprint : System.Web.UI.Page
{
    string name, extranote, prepare, product;
    DateTime tod;
    string q_fetch_user, q_fetch_pres_glass, q_fetch_pres_cl, q_job;
    DataTable dt_fetch_user = new DataTable();
    DataTable dt_fetch_pres = new DataTable();

    DataTable dt_presno = new DataTable();
    //DataTable dt_cl = new DataTable();
    //DataTable dt_glass = new DataTable();
    DataTable dt_register = new DataTable();
    DataTable dt_userid = new DataTable();
    DataTable dt_order = new DataTable();
    DataTable dt_job = new DataTable();
    DataTable dt_oldpres = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }

        todaydateTextBox.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        todaydateTextBox.Enabled = false;

        preparedTextBox.Text = Session["Name"].ToString();
        preparedTextBox.Enabled = false;

        custnameTextBox.Enabled = false;
        conNoTextBox.Enabled = false;

        if (Request.QueryString["id"] != null)
        {
            jobprinttypeTextBox.Text = "fitting";
            jobprinttypeTextBox.Enabled = false;

            //if (!Page.IsPostBack)
            //{
                dt_order = (DataTable)Session["order"];
            //}
            productdescTextBox.Text = dt_order.Rows[0]["promodel"].ToString();
            productdescTextBox.Enabled = false;
            if (Session["oldpress"] != null)
            {

                dt_oldpres = (DataTable)Session["oldpres"];
                //Response.Write(dt_oldpres);
            }
            dt_oldpres = (DataTable)Session["oldpres"];
            

            if (Session["userid"] != null)
            {
                Response.Write("userid");
                dt_userid = (DataTable)Session["userid"];
                q_fetch_user = "select User_Id,Name,ContactNo from Customer_Personal_Details where User_Id='" + dt_userid.Rows[0]["userid"].ToString() + "'";
                dbo.get(q_fetch_user, ref dt_fetch_user);
                custnameTextBox.Text = dt_fetch_user.Rows[0]["Name"].ToString();
                conNoTextBox.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();
            }
            else
            {
                //Response.Write("register");
                dt_register = (DataTable)Session["register"];
                custnameTextBox.Text = dt_register.Rows[0]["Name"].ToString();
                conNoTextBox.Text = dt_register.Rows[0]["ContactNo"].ToString();
            }


            //if (Session["contactlens"]!=null || dt_oldpres.Rows[0]["prestype"].ToString()=="Contact Lens")
            //{
            //if (!Page.IsPostBack)
            //{
            dt_order = (DataTable)Session["order"];
            if (dt_order.Rows[0]["proid"].ToString().Substring(0, 2) == "cl")
            {
                Response.Write("CL");

                presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Contact Lens"));
                presTypeDropDownList.Enabled = false;
                glasspresPanel.Visible = false;
                contactlenspresPanel.Enabled = false;
                if (Session["contactlens"] != null)
                {
                    dt_fetch_pres = (DataTable)Session["contactlens"];
                    ODTextBox.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
                    OHTextBox.Text = dt_fetch_pres.Rows[0]["OH"].ToString();
                }
                else
                {
                    dt_userid = (DataTable)Session["userid"];
                    q_fetch_pres_cl = "select CL_PresNo,OD,OH from Pres_Contact_Lens where User_Cus_Id='" + dt_userid.Rows[0]["userid"].ToString() + "' order by CL_PresNo DESC";
                    dbo.get(q_fetch_pres_cl, ref dt_fetch_pres);
                    ODTextBox.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
                    OHTextBox.Text = dt_fetch_pres.Rows[0]["OH"].ToString();
                }

            }
            else if (dt_order.Rows[0]["proid"].ToString().Substring(0, 2) == "gl" || dt_order.Rows[0]["proid"].ToString().Substring(0, 2) == "fr")
            {
                dt_order = (DataTable)Session["order"];
                presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Glass"));
                presTypeDropDownList.Enabled = false;
                contactlenspresPanel.Visible = false;
                glasspresPanel.Enabled = false;
                if (Session["glass"] != null)
                {
                    dt_fetch_pres = (DataTable)Session["glass"];
                }
                else
                {
                    dt_oldpres = (DataTable)Session["oldpres"];
                    q_fetch_pres_glass = "select Gla_PresNo,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where Gla_PresNo='" + dt_oldpres.Rows[0]["presno"] + "' order by Gla_PresNo DESC";
                    dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);
                }
                SPHRightDisTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_Distance"].ToString();
                SPHRightNearTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_NearAdd"].ToString();
                SPHRightIntTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_IntermediateAdd"].ToString();
                SPHRightProTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_ProgressiveAdd"].ToString();
                CYLRightDisTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_Distance"].ToString();
                CYLRightNearTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_NearAdd"].ToString();
                CYLRightIntTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_IntermediateAdd"].ToString();
                CYLRightProTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_ProgressiveAdd"].ToString();
                AxisRightDisTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_Distance"].ToString();
                AxisRightNearTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_NearAdd"].ToString();
                AxisRightIntTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_IntermediateAdd"].ToString();
                AxisRightProTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_ProgressiveAdd"].ToString();
                PDRightDisTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_Distance"].ToString();
                PDRightNearTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_NearAdd"].ToString();
                PDRightIntTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_IntermediateAdd"].ToString();
                PDRightProTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_ProgressiveAdd"].ToString();
                VARightDisTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_Distance"].ToString();
                VARightNearTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_NearAdd"].ToString();
                VARightIntTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_IntermediateAdd"].ToString();
                VARightProTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_ProgressiveAdd"].ToString();
                SPHLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_Distance"].ToString();
                SPHLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_NearAdd"].ToString();
                SPHLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_IntermediateAdd"].ToString();
                SPHLeftProTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_ProgressiveAdd"].ToString();
                CYLLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_Distance"].ToString();
                CYLLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_NearAdd"].ToString();
                CYLLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_IntermediateAdd"].ToString();
                CYLLeftProTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_ProgressiveAdd"].ToString();
                AxisLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_Distance"].ToString();
                AxisLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_NearAdd"].ToString();
                AxisLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_IntermediateAdd"].ToString();
                AxisLeftProTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_ProgressiveAdd"].ToString();
                PDLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_Distance"].ToString();
                PDLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_NearAdd"].ToString();
                PDLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_IntermediateAdd"].ToString();
                PDLeftProTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_ProgressiveAdd"].ToString();
                VALeftDisTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_Distance"].ToString();
                VALeftNearTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_NearAdd"].ToString();
                VALeftIntTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_IntermediateAdd"].ToString();
                VALeftProTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_ProgressiveAdd"].ToString();
            }
        }

        else if (Request.QueryString["re"] != null)
        {
            jobprinttypeTextBox.Text = "Repairing";
            jobprinttypeTextBox.Enabled = false;
            contactlenspresPanel.Visible = false;
            glasspresPanel.Enabled = false;
            q_fetch_user = "select User_Id,Name,ContactNo from Customer_Personal_Details where User_Id='" + Request.QueryString["cu_id"].ToString() + "'";
            dbo.get(q_fetch_user, ref dt_fetch_user);
            custnameTextBox.Text = dt_fetch_user.Rows[0]["Name"].ToString();
            conNoTextBox.Text = dt_fetch_user.Rows[0]["ContactNo"].ToString();

            //if (Request.QueryString["pres"] == "Contact Lens")
            //{
            //    presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByText("Contact Lens"));
            //    presTypeDropDownList.Enabled = false;
            //    q_fetch_pres_cl = "select CL_PresNo,OD,OH from Pres_Contact_Lens where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by CL_PresNo DESC";
            //    dbo.get(q_fetch_pres_cl, ref dt_fetch_pres);
            //    ODTextBox.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
            //    OHTextBox.Text = dt_fetch_pres.Rows[0]["OH"].ToString();
            //}

            if (Request.QueryString["pres"] == "Glass")
            {
                presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByText("Glass"));
                presTypeDropDownList.Enabled = false;
                q_fetch_pres_glass = "select Gla_PresNo,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by Gla_PresNo DESC";
                dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);

                SPHRightDisTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_Distance"].ToString();
                SPHRightNearTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_NearAdd"].ToString();
                SPHRightIntTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_IntermediateAdd"].ToString();
                SPHRightProTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Right_ProgressiveAdd"].ToString();
                CYLRightDisTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_Distance"].ToString();
                CYLRightNearTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_NearAdd"].ToString();
                CYLRightIntTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_IntermediateAdd"].ToString();
                CYLRightProTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Right_ProgressiveAdd"].ToString();
                AxisRightDisTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_Distance"].ToString();
                AxisRightNearTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_NearAdd"].ToString();
                AxisRightIntTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_IntermediateAdd"].ToString();
                AxisRightProTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Right_ProgressiveAdd"].ToString();
                PDRightDisTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_Distance"].ToString();
                PDRightNearTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_NearAdd"].ToString();
                PDRightIntTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_IntermediateAdd"].ToString();
                PDRightProTextBox.Text = dt_fetch_pres.Rows[0]["PD_Right_ProgressiveAdd"].ToString();
                VARightDisTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_Distance"].ToString();
                VARightNearTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_NearAdd"].ToString();
                VARightIntTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_IntermediateAdd"].ToString();
                VARightProTextBox.Text = dt_fetch_pres.Rows[0]["VA_Right_ProgressiveAdd"].ToString();
                SPHLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_Distance"].ToString();
                SPHLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_NearAdd"].ToString();
                SPHLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_IntermediateAdd"].ToString();
                SPHLeftProTextBox.Text = dt_fetch_pres.Rows[0]["SPH_Left_ProgressiveAdd"].ToString();
                CYLLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_Distance"].ToString();
                CYLLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_NearAdd"].ToString();
                CYLLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_IntermediateAdd"].ToString();
                CYLLeftProTextBox.Text = dt_fetch_pres.Rows[0]["CYL_Left_ProgressiveAdd"].ToString();
                AxisLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_Distance"].ToString();
                AxisLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_NearAdd"].ToString();
                AxisLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_IntermediateAdd"].ToString();
                AxisLeftProTextBox.Text = dt_fetch_pres.Rows[0]["Axis_Left_ProgressiveAdd"].ToString();
                PDLeftDisTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_Distance"].ToString();
                PDLeftNearTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_NearAdd"].ToString();
                PDLeftIntTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_IntermediateAdd"].ToString();
                PDLeftProTextBox.Text = dt_fetch_pres.Rows[0]["PD_Left_ProgressiveAdd"].ToString();
                VALeftDisTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_Distance"].ToString();
                VALeftNearTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_NearAdd"].ToString();
                VALeftIntTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_IntermediateAdd"].ToString();
                VALeftProTextBox.Text = dt_fetch_pres.Rows[0]["VA_Left_ProgressiveAdd"].ToString();
            }
        }
    }
    protected void printButton_Click(object sender, EventArgs e)
    {
        dt_order = (DataTable)Session["order"];
        //Response.Write(dt_order.Rows[0]["proid"].ToString());
        prepare = preparedTextBox.Text;
        product = productdescTextBox.Text;
        extranote = extranoteTextBox.Text;
        name = presTypeDropDownList.SelectedItem.Value;
        if (Request.QueryString["re"] != null)
        {
            //q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + Request.QueryString["cu_id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + product + "','" + name + "','" + dt_fetch_pres.Rows[0]["CL_PresNo"] + "','" + dt_fetch_user.Rows[0]["ContactNo"] + "','" + extranote + "','" + prepare + "')";
            //dbo.dml(q_job);
            if (name == "Contact Lens")
            {
                q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + Request.QueryString["cu_id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + product + "','" + name + "','" + dt_fetch_pres.Rows[0]["CL_PresNo"] + "','" + dt_fetch_user.Rows[0]["ContactNo"] + "','" + extranote + "','" + prepare + "')";
                dbo.dml(q_job);
                Response.Redirect("jobprintcontactlens.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + "&pres=" + Request.QueryString["pres"]);
            }
            else
            {
                q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + Request.QueryString["cu_id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + product + "','" + name + "','" + dt_fetch_pres.Rows[0]["Gla_PresNo"] + "','" + dt_fetch_user.Rows[0]["ContactNo"] + "','" + extranote + "','" + prepare + "')";
                dbo.dml(q_job);
                Response.Redirect("jobprintglass.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + "&pres=" + Request.QueryString["pres"]);
            }
        }
        else
        {
            //q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + Request.QueryString["cu_id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + product + "','" + name + "','" + dt_fetch_pres.Rows[0]["CL_PresNo"] + "','" + dt_fetch_user.Rows[0]["ContactNo"] + "','" + extranote + "','" + prepare + "')";
            dt_job.Columns.Add("proid");
            dt_job.Columns.Add("Date");
            dt_job.Columns.Add("Products");
            dt_job.Columns.Add("PresType");
            dt_job.Columns.Add("Extra_Note");
            dt_job.Columns.Add("PreparedBy");
            dt_job.Columns.Add("status");
            dt_job.Rows.Add(dt_order.Rows[0]["proid"].ToString(), DateTime.Now.Date.ToString("MM/dd/yyyy"), product, name, extranote, prepare, dt_order.Rows[0]["status"].ToString());
            Session.Add("jobprint", dt_job);
            Response.Redirect("productconfirmation.aspx?id=" + Request.QueryString["id"]);
        }
    }
}