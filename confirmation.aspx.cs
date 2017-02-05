using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class confirmation : System.Web.UI.Page
{
    DataTable dt_confirmorder = new DataTable();
    DataTable dt_confirmregister = new DataTable();
    DataTable dt_confirmmedical = new DataTable();
    DataTable dt_confirmcl = new DataTable();
    DataTable dt_confirmglass = new DataTable();
    DataTable dt_confirmjob = new DataTable();
    DataTable dt_confirmuserid = new DataTable();
    DataTable dt_confirmoldpres = new DataTable();

    public int sum = 0;
    string q_cus, q_order, q_get_orderid, q_orderinvoice, q_ins_md, q_fe_md, q_ins_gla, q_ins_cl, q_fe_gla, q_fe_cl, q_job, q_fe_job;
    string q_ins_fr_order, q_ins_gl_order, q_ins_cl_order, q_ins_sl_order, q_fe_sl, q_ins_gl_update, q_fe_gl_quant,q_fe_jobprint;
    string fr_presno, gl_presno, cl_presno;

    DataTable dt_id = new DataTable();
    DataTable dt_orderid = new DataTable();
    DataTable dt_medicalid = new DataTable();
    DataTable dt_presid = new DataTable();
    DataTable dt_jobid = new DataTable();
    DataTable dt_fe_sl = new DataTable();
    DataTable dt_fe_gl = new DataTable();
    DataTable dt_jobprintglass = new DataTable();
    DataTable dt_jobprintcl = new DataTable();
    DataTable dt_billproduct = new DataTable();
    DataTable dt_newuser = new DataTable();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        

        dt_jobprintglass.Columns.Add("jobid");
        

        dt_jobprintcl.Columns.Add("jobid");
        

        if (Session["confirmorder"] != null)
            dt_confirmorder = (DataTable)Session["confirmorder"];
        if (Session["confirmregister"] != null)
            dt_confirmregister = (DataTable)Session["confirmregister"];
        if (Session["confirmmedical"] != null)
        {
            Response.Write("medical");
            dt_confirmmedical = (DataTable)Session["confirmmedical"];
        }
        if (Session["confirmglass"] != null)
        {
            Response.Write("glass");
            dt_confirmglass = (DataTable)Session["confirmglass"];
        }
        if (Session["confirmcontactlens"] != null)
            dt_confirmcl = (DataTable)Session["confirmcontactlens"];
        if (Session["confirmjobprint"] != null)
            dt_confirmjob = (DataTable)Session["confirmjobprint"];
        if (Session["confirmuserid"] != null)
            dt_confirmuserid = (DataTable)Session["confirmuserid"];
        if (Session["confirmoldpres"] != null)
            dt_confirmoldpres = (DataTable)Session["confirmoldpres"];


        for (int i = 0; i < dt_confirmorder.Rows.Count; i++)
        {
            sum += Convert.ToInt32(dt_confirmorder.Rows[i]["amount"].ToString());

        }
        //Response.Write(dt_confirmcl.Rows[0]["OD"]);
        orderproductGridView.DataSource = dt_confirmorder;
        orderproductGridView.DataBind();
        amountLabel.Text = Convert.ToString(sum);
    }
    protected void addButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void cancelorderButton_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();

        Response.Redirect("default.aspx");
    }


    protected void confirmButton_Click(object sender, EventArgs e)
    {
        dt_billproduct.Columns.Add("Model Name");
        dt_billproduct.Columns.Add("Quantity");
        dt_billproduct.Columns.Add("Price");
        dt_billproduct.Columns.Add("Fitting Amount");
        dt_billproduct.Columns.Add("Amount");

        //dt_newuser.Columns.Add("UserId");

        if (Session["confirmuserid"] == null)
        {
            Response.Write("register<br/>");
            dt_confirmregister = (DataTable)Session["confirmregister"];
            q_cus = "insert into Customer_Personal_Details(User_Type_Id,Name,Gender,BirthDate,MaritalStatus,AniversaryDate,Profession,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId) values('" + 1 + "','" + dt_confirmregister.Rows[0]["Name"].ToString() + "','" + dt_confirmregister.Rows[0]["Gender"].ToString() + "','" + dt_confirmregister.Rows[0]["BirthDate"].ToString() + "','" + dt_confirmregister.Rows[0]["MaritalStatus"].ToString() + "','" + dt_confirmregister.Rows[0]["AniversaryDate"].ToString() + "','" + dt_confirmregister.Rows[0]["Profession"].ToString() + "','" + dt_confirmregister.Rows[0]["CurrentAdd"].ToString() + "','" + dt_confirmregister.Rows[0]["PermanentAdd"].ToString() + "','" + dt_confirmregister.Rows[0]["City"].ToString() + "','" + dt_confirmregister.Rows[0]["Pincode"].ToString() + "','" + dt_confirmregister.Rows[0]["ContactNo"].ToString() + "','" + dt_confirmregister.Rows[0]["MobileNo"].ToString() + "','" + dt_confirmregister.Rows[0]["EmailId"].ToString() + "')";
            Response.Write(q_cus);
            dbo.dml(q_cus);
            string q_id = "select User_Id,CurrentAdd,ContactNo,MobileNo from Customer_Personal_Details order by User_Id DESC";
            dbo.get(q_id, ref dt_id);
            //dt_confirmuserid.Rows.Add();
            dt_newuser.Columns.Add("UserId");
            dt_newuser.Rows.Add(dt_id.Rows[0]["User_Id"].ToString());
            Session.Add("newuser", dt_newuser);
        }
        else
        {
            Response.Write("userid<br/>");
            dt_confirmuserid = (DataTable)Session["confirmuserid"];
            string q_id = "select User_Id,CurrentAdd,ContactNo,MobileNo from Customer_Personal_Details where User_Id=" + dt_confirmuserid.Rows[0]["userid"];
            //Response.Write(q_id);
            dbo.get(q_id, ref dt_id);
        }


        ////enter into order products
        q_order = "insert into Order_Products(User_Cus_Id,Date,Address,ContactNo,Amount,DoFitting) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_id.Rows[0]["CurrentAdd"] + "','" + dt_id.Rows[0]["ContactNo"] + "','" + amountLabel.Text + "','" + Session["flag"] + "')";
        dbo.dml(q_order);

        ////////////get last order id
        q_get_orderid = "select * from Order_Products order by Order_Id DESC";
        dbo.get(q_get_orderid, ref dt_orderid);
        Session.Add("orderid", dt_orderid.Rows[0]["Order_Id"].ToString());

        ////////////enter into orderinvoice table
        q_orderinvoice = "insert into Order_Invoices(User_Cus_Id,Date,Invoice_Order_Id,Amount,ContactNo) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_orderid.Rows[0]["Order_Id"] + "','" + amountLabel.Text + "','" + dt_id.Rows[0]["ContactNo"] + "')";
        dbo.dml(q_orderinvoice);



        for (int i = 0; i < dt_confirmorder.Rows.Count; i++)
        {
            string id = dt_confirmorder.Rows[i]["proid"].ToString().Substring(0, 2);
            int idno = Convert.ToInt32(dt_confirmorder.Rows[i]["proid"].ToString().Substring(2));
            string statuspro = dt_confirmorder.Rows[i]["status"].ToString();

            if (id == "fr")
            {
                q_fe_sl = "select Quant from Frame_Products where Fr_ProdNo=" + idno;
                dbo.get(q_fe_sl, ref dt_fe_sl);
                int remain = Convert.ToInt32(dt_fe_sl.Rows[0]["Quant"].ToString()) - Convert.ToInt32(dt_confirmorder.Rows[i]["proquant"].ToString());

                if (dt_confirmorder.Rows[i]["fitreqd"] == "yes")
                {
                    Response.Write("fitting<br/>");
                    if (dt_confirmorder.Rows[i]["presreqd"] == "yes")
                    {
                        Response.Write("prescription<br/>");
                        if (dt_confirmorder.Rows[i]["prestype"] == "new")
                        {
                            Response.Write("pres new<br/>");
                            dt_confirmmedical = (DataTable)Session["confirmmedical"];


                            for (int j = 0; j < dt_confirmmedical.Rows.Count; j++)
                            {
                                if (dt_confirmorder.Rows[i]["proid"] == dt_confirmmedical.Rows[j]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmmedical.Rows[j]["status"])
                                {
                                    Response.Write("confirmmedical<br/>");
                                    q_ins_md = "insert into Medical_Details(User_Cus_Id,DocName,DocConNo,HosName,HosConNo) values('" + dt_id.Rows[0]["User_Id"] + "','" + dt_confirmmedical.Rows[j]["DocName"] + "','" + dt_confirmmedical.Rows[j]["DocConNo"] + "','" + dt_confirmmedical.Rows[j]["HosName"] + "','" + dt_confirmmedical.Rows[j]["HosConNo"] + "')";
                                    dbo.dml(q_ins_md);
                                    q_fe_md = "select Medical_Id from Medical_Details order by Medical_Id DESC";
                                    dbo.get(q_fe_md, ref dt_medicalid);

                                    dt_confirmglass = (DataTable)Session["confirmglass"];
                                    Response.Write(dt_confirmglass.Rows.Count);
                                    for (int k = 0; k < dt_confirmglass.Rows.Count; k++)
                                    {
                                        if (dt_confirmorder.Rows[i]["proid"].ToString() == dt_confirmglass.Rows[k]["proid"].ToString() && dt_confirmorder.Rows[i]["status"].ToString() == dt_confirmglass.Rows[k]["status"].ToString())
                                        {
                                            Response.Write("confirmglass<br/>");
                                            q_ins_gla = "insert into Pres_Glass(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd,VA_Left_ProgressiveAdd) values('" + dt_id.Rows[0]["User_Id"] + "','" + 1 + "','" + dt_medicalid.Rows[0]["Medical_Id"] + "','" + dt_confirmglass.Rows[k]["PrescritpionDate"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["PD_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["VA_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["PD_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["VA_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_ProgressiveAdd"] + "')";
                                            dbo.dml(q_ins_gla);
                                            q_fe_gla = "select Gla_PresNo from Pres_Glass order by Gla_PresNo DESC";
                                            dbo.get(q_fe_gla, ref dt_presid);

                                            for (int l = 0; l < dt_confirmjob.Rows.Count; l++)
                                            {
                                                if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[l]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmjob.Rows[l]["status"])
                                                {
                                                    Response.Write("confirmjob<br/>");
                                                    q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[l]["Products"] + "','Glass','" + dt_presid.Rows[0]["Gla_PresNo"] + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[l]["Extra_Note"] + "','" + dt_confirmjob.Rows[l]["PreparedBy"] + "')";
                                                    dbo.dml(q_job);
                                                    //dt_jobid.Clear();
                                                    q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                                    dbo.get(q_fe_job, ref dt_jobid);
                                                    q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                                    dbo.dml(q_job);
                                                    dt_jobprintglass.Rows.Add(dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                                    dt_confirmjob.Rows.RemoveAt(l);
                                                }
                                            }
                                            dt_confirmglass.Rows.RemoveAt(k);
                                        }
                                    }
                                    dt_confirmmedical.Rows.RemoveAt(j);
                                }
                            }
                        }

                        else if (dt_confirmorder.Rows[i]["prestype"] == "previous")
                        {
                            Response.Write("previous<br/>");
                            for (int j = 0; j < dt_confirmoldpres.Rows.Count; j++)
                            {
                                if (dt_confirmorder.Rows[i]["proid"] == dt_confirmoldpres.Rows[j]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmoldpres.Rows[j]["status"])
                                {
                                    Response.Write("oldpres<br/>");
                                    fr_presno = dt_confirmoldpres.Rows[j]["presno"].ToString();

                                    for (int k = 0; k < dt_confirmjob.Rows.Count; k++)
                                    {
                                        if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[k]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmjob.Rows[k]["status"])
                                        {
                                            Response.Write("oldpresjob<br/>");
                                            q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[k]["Products"] + "','Glass','" + fr_presno + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[k]["Extra_Note"] + "','" + dt_confirmjob.Rows[k]["PreparedBy"] + "')";
                                            dbo.dml(q_job);
                                            q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                            dbo.get(q_fe_job, ref dt_jobid);
                                            q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                            dbo.dml(q_job);
                                            dt_jobprintglass.Rows.Add( dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                            dt_confirmjob.Rows.RemoveAt(j);
                                        }
                                    }
                                    dt_confirmoldpres.Rows.RemoveAt(j);
                                }
                            }
                        }

                    }
                }

                q_ins_fr_order = "insert into Frame_Order_Products(OrderId,Fr_ProdNo,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                dbo.dml(q_ins_fr_order);
                q_ins_gl_order = "update Frame_Products set Quant=" + remain + "where Fr_ProdNo=" + idno;
                dbo.dml(q_ins_gl_order);
                Response.Write("<br/><br/>");
            }



            else if (id == "gl")
            {
                Response.Write("glass<br/>");
                q_fe_gl_quant = "select Quant from Glass_Products where Gla_ProdNo=" + idno;
                dt_fe_gl.Clear();
                dbo.get(q_fe_gl_quant, ref dt_fe_gl);
                int remain = Convert.ToInt32(dt_fe_gl.Rows[0]["Quant"].ToString()) - Convert.ToInt32(dt_confirmorder.Rows[i]["proquant"].ToString());
                Response.Write(remain + "<br/>" + i);

                if (dt_confirmorder.Rows[i]["presreqd"] == "yes")
                {
                    Response.Write("glass prescription<br/>");
                    if (dt_confirmorder.Rows[i]["prestype"] == "new")
                    {
                        Response.Write("glass pres new<br/>");
                        dt_confirmmedical = (DataTable)Session["confirmmedical"];
                        dt_confirmglass = (DataTable)Session["confirmglass"];

                        for (int j = 0; j < dt_confirmmedical.Rows.Count; j++)
                        {
                            if (dt_confirmorder.Rows[i]["proid"] == dt_confirmmedical.Rows[j]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmmedical.Rows[j]["status"])
                            {
                                Response.Write("glass medical<br/>");
                                q_ins_md = "insert into Medical_Details(User_Cus_Id,DocName,DocConNo,HosName,HosConNo) values('" + dt_id.Rows[0]["User_Id"] + "','" + dt_confirmmedical.Rows[j]["DocName"] + "','" + dt_confirmmedical.Rows[j]["DocConNo"] + "','" + dt_confirmmedical.Rows[j]["HosName"] + "','" + dt_confirmmedical.Rows[j]["HosConNo"] + "')";
                                dbo.dml(q_ins_md);
                                q_fe_md = "select Medical_Id from Medical_Details order by Medical_Id DESC";
                                dbo.get(q_fe_md, ref dt_medicalid);

                                for (int k = 0; k < dt_confirmglass.Rows.Count; k++)
                                {
                                    dt_confirmglass = (DataTable)Session["confirmglass"];
                                    if (dt_confirmorder.Rows[i]["proid"].ToString() == dt_confirmglass.Rows[k]["proid"].ToString() && dt_confirmorder.Rows[i]["status"].ToString() == dt_confirmglass.Rows[k]["status"].ToString())
                                    {
                                        Response.Write("glass confirmglass<br/>");
                                        q_ins_gla = "insert into Pres_Glass(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd,VA_Left_ProgressiveAdd) values('" + dt_id.Rows[0]["User_Id"] + "','" + 1 + "','" + dt_medicalid.Rows[0]["Medical_Id"] + "','" + dt_confirmglass.Rows[k]["PrescritpionDate"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["PD_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_Distance"] + "','" + dt_confirmglass.Rows[k]["VA_Right_NearAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Right_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["SPH_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["CYL_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["Axis_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["PD_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["PD_Left_ProgressiveAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_Distance"] + "','" + dt_confirmglass.Rows[k]["VA_Left_NearAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_IntermediateAdd"] + "','" + dt_confirmglass.Rows[k]["VA_Left_ProgressiveAdd"] + "')";
                                        dbo.dml(q_ins_gla);
                                        q_fe_gla = "select Gla_PresNo from Pres_Glass order by Gla_PresNo DESC";
                                        dbo.get(q_fe_gla, ref dt_presid);

                                        if (dt_confirmorder.Rows[i]["fitreqd"] == "yes")
                                        {
                                            for (int l = 0; l < dt_confirmjob.Rows.Count; l++)
                                            {
                                                if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[l]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmjob.Rows[l]["status"])
                                                {
                                                    Response.Write("glass confirm job<br/>");
                                                    q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[l]["Products"] + "','Glass','" + dt_presid.Rows[0]["Gla_PresNo"] + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[l]["Extra_Note"] + "','" + dt_confirmjob.Rows[l]["PreparedBy"] + "')";
                                                    dbo.dml(q_job);
                                                    q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                                    dbo.get(q_fe_job, ref dt_jobid);
                                                    q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                                    dbo.dml(q_job);
                                                    dt_jobprintglass.Rows.Add( dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                                    dt_confirmjob.Rows.RemoveAt(l);
                                                }
                                            }
                                        }
                                        dt_confirmglass.Rows.RemoveAt(k);
                                    }
                                }
                                dt_confirmmedical.Rows.RemoveAt(j);
                            }
                        }
                        q_fe_gla = "select Gla_PresNo from Pres_Glass order by Gla_PresNo DESC";
                        Response.Write(q_fe_gla);
                        dbo.get(q_fe_gla, ref dt_presid);
                        q_ins_gl_order = "insert into Glass_Order_Products(OrderId,Gla_Pres_No,Gla_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_presid.Rows[0]["Gla_PresNo"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                        Response.Write(q_ins_gl_order);
                        dbo.dml(q_ins_gl_order);
                    }
                    else if (dt_confirmorder.Rows[i]["prestype"] == "previous")
                    {
                        Response.Write("glass previous<br/>");

                        for (int j = 0; j < dt_confirmoldpres.Rows.Count; j++)
                        {
                            if (dt_confirmorder.Rows[i]["proid"] == dt_confirmoldpres.Rows[j]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmoldpres.Rows[j]["status"])
                            {
                                Response.Write("glass previous oldpres<br/>");
                                gl_presno = dt_confirmoldpres.Rows[j]["presno"].ToString();
                                Response.Write(gl_presno);
                                if (dt_confirmorder.Rows[i]["fitreqd"] == "yes")
                                {
                                    Response.Write("glass fitreqd<br/>");
                                    for (int k = 0; k < dt_confirmjob.Rows.Count; k++)
                                    {
                                        if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[k]["proid"] && dt_confirmorder.Rows[i]["proid"] == dt_confirmoldpres.Rows[k]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmoldpres.Rows[j]["status"])
                                        {
                                            Response.Write("glass previous oldpresjob<br/>");
                                            q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[k]["Products"] + "','Glass','" + gl_presno + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[k]["Extra_Note"] + "','" + dt_confirmjob.Rows[k]["PreparedBy"] + "')";
                                            dbo.dml(q_job);
                                            q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                            dbo.get(q_fe_job, ref dt_jobid);
                                            q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                            dbo.dml(q_job);
                                            dt_jobprintglass.Rows.Add( dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                            dt_confirmjob.Rows.RemoveAt(k);
                                        }
                                    }
                                }
                            }
                            dt_confirmoldpres.Rows.RemoveAt(j);
                        }
                        q_ins_gl_order = "insert into Glass_Order_Products(OrderId,Gla_Pres_No,Gla_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + gl_presno + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                        Response.Write(q_ins_gl_order);
                        dbo.dml(q_ins_gl_order);
                        //q_ins_gl_order = "update Glass_Products set Quant='" + remain + "' where Gla_ProdNo='" + idno + "'";
                        //Response.Write(q_ins_gl_order);
                        //dbo.dml(q_ins_gl_order);

                    }
                }
                else
                {
                    Response.Write("glass without pres<br/>");
                    q_ins_gl_order = "insert into Glass_Order_Products(OrderId,Gla_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                    Response.Write(q_ins_gl_order);
                    dbo.dml(q_ins_gl_order);
                    //q_ins_gl_order = "update Glass_Products set Quant='" + remain + "'where Gla_ProdNo='" + idno+"'";
                    //Response.Write(q_ins_gl_order);
                    //dbo.dml(q_ins_gl_order);
                }
                q_ins_gl_update = "update Glass_Products set Quant='" + remain + "' where Gla_ProdNo='" + idno + "'";
                Response.Write(q_ins_gl_update);
                dbo.dml(q_ins_gl_update);
            }



            else if (id == "cl")
            {
                Response.Write("contact lens<br/>");
                q_fe_gl_quant = "select Quant from Contact_Lens_Products where CL_ProdNo=" + idno;
                dt_fe_gl.Clear();
                dbo.get(q_fe_gl_quant, ref dt_fe_gl);
                int remain = Convert.ToInt32(dt_fe_gl.Rows[0]["Quant"].ToString()) - Convert.ToInt32(dt_confirmorder.Rows[i]["proquant"].ToString());
                Response.Write(remain + "<br/>" + i);

                if (dt_confirmorder.Rows[i]["presreqd"] == "yes")
                {
                    Response.Write("contact lens prescription<br/>");
                    if (dt_confirmorder.Rows[i]["prestype"] == "new")
                    {
                        Response.Write("contact lens pres new<br/>");
                        dt_confirmmedical = (DataTable)Session["confirmmedical"];
                        dt_confirmcl = (DataTable)Session["confirmcontactlens"];

                        for (int j = 0; j < dt_confirmmedical.Rows.Count; j++)
                        {
                            if (dt_confirmorder.Rows[i]["proid"].ToString() == dt_confirmmedical.Rows[j]["proid"].ToString() && dt_confirmorder.Rows[i]["status"].ToString() == dt_confirmmedical.Rows[j]["status"].ToString())
                            {
                                Response.Write("contact lens medical<br/>");
                                q_ins_md = "insert into Medical_Details(User_Cus_Id,DocName,DocConNo,HosName,HosConNo) values('" + dt_id.Rows[0]["User_Id"] + "','" + dt_confirmmedical.Rows[j]["DocName"] + "','" + dt_confirmmedical.Rows[j]["DocConNo"] + "','" + dt_confirmmedical.Rows[j]["HosName"] + "','" + dt_confirmmedical.Rows[j]["HosConNo"] + "')";
                                dbo.dml(q_ins_md);
                                q_fe_md = "select Medical_Id from Medical_Details order by Medical_Id DESC";
                                dbo.get(q_fe_md, ref dt_medicalid);

                                for (int k = 0; k < dt_confirmcl.Rows.Count; k++)
                                {
                                    dt_confirmcl = (DataTable)Session["confirmcontactlens"];
                                    if (dt_confirmorder.Rows[i]["proid"].ToString() == dt_confirmcl.Rows[k]["proid"].ToString() && dt_confirmorder.Rows[i]["status"].ToString() == dt_confirmcl.Rows[k]["status"].ToString())
                                    {
                                        Response.Write("contact lens confirmcontact lens<br/>");
                                        q_ins_cl = "insert into Pres_Contact_Lens(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescribedDate,OD,OH)values('" + dt_id.Rows[0]["User_Id"] + "','" + 2 + "','" + dt_medicalid.Rows[0]["Medical_Id"] + "','" + dt_confirmcl.Rows[k]["PrescribedDate"] + "','" + dt_confirmcl.Rows[k]["OD"] + "','" + dt_confirmcl.Rows[k]["OH"] + "')";
                                        Response.Write(q_ins_cl);
                                        dbo.dml(q_ins_cl);
                                        q_fe_cl = "select CL_PresNo from Pres_Contact_Lens order by CL_PresNo DESC";
                                        Response.Write(q_fe_cl);
                                        dbo.get(q_fe_cl, ref dt_presid);

                                        if (dt_confirmorder.Rows[i]["fitreqd"] == "yes")
                                        {
                                            for (int l = 0; l < dt_confirmjob.Rows.Count; l++)
                                            {
                                                if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[l]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmjob.Rows[l]["status"])
                                                {
                                                    Response.Write("contact lens confirm job<br/>");
                                                    q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[l]["Products"] + "','Contact Lens','" + dt_presid.Rows[0]["CL_PresNo"] + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[l]["Extra_Note"] + "','" + dt_confirmjob.Rows[l]["PreparedBy"] + "')";
                                                    dbo.dml(q_job);
                                                    q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                                    dbo.get(q_fe_job, ref dt_jobid);
                                                    q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                                    dbo.dml(q_job);
                                                    dt_jobprintcl.Rows.Add(dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                                    dt_confirmjob.Rows.RemoveAt(l);
                                                }
                                            }
                                        }
                                        dt_confirmcl.Rows.RemoveAt(k);
                                    }
                                }
                                dt_confirmmedical.Rows.RemoveAt(j);
                            }
                        }
                        q_fe_cl = "select CL_PresNo from Pres_Contact_Lens order by CL_PresNo DESC";
                        dbo.get(q_fe_cl, ref dt_presid);
                        q_ins_cl_order = "insert into Contact_Lens_Order_Products(OrderId,CL_Pres_No,CL_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_presid.Rows[0]["CL_PresNo"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                        dbo.dml(q_ins_cl_order);
                    }
                    else if (dt_confirmorder.Rows[i]["prestype"] == "previous")
                    {
                        Response.Write("contact lens previous<br/>");

                        for (int j = 0; j < dt_confirmoldpres.Rows.Count; j++)
                        {
                            if (dt_confirmorder.Rows[i]["proid"] == dt_confirmoldpres.Rows[j]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmoldpres.Rows[j]["status"])
                            {
                                Response.Write("contact lens previous oldpres<br/>");
                                cl_presno = dt_confirmoldpres.Rows[j]["presno"].ToString();
                                Response.Write(cl_presno);
                                if (dt_confirmorder.Rows[i]["fitreqd"] == "yes")
                                {
                                    Response.Write("contact lens fitreqd<br/>");
                                    for (int k = 0; k < dt_confirmjob.Rows.Count; k++)
                                    {
                                        if (dt_confirmorder.Rows[i]["proid"] == dt_confirmjob.Rows[k]["proid"] && dt_confirmorder.Rows[i]["proid"] == dt_confirmoldpres.Rows[k]["proid"] && dt_confirmorder.Rows[i]["status"] == dt_confirmoldpres.Rows[j]["status"])
                                        {
                                            Response.Write("contact lens previous oldpresjob<br/>");
                                            q_job = "insert into Job_Prints(User_Id,Date,Products,PresType,Pres_No,Contact_No,Extra_Note,Prepared_By) values('" + dt_id.Rows[0]["User_Id"] + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "','" + dt_confirmjob.Rows[k]["Products"] + "','Contact Lens','" + cl_presno + "','" + dt_id.Rows[0]["ContactNo"] + "','" + dt_confirmjob.Rows[k]["Extra_Note"] + "','" + dt_confirmjob.Rows[k]["PreparedBy"] + "')";
                                            dbo.dml(q_job);
                                            q_fe_job = "select max(Job_Id) as Job_Id from Job_Prints";
                                            dbo.get(q_fe_job, ref dt_jobid);
                                            q_job = "insert into Job_Order_Products(OrderId,JobId,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"] + "','" + dt_confirmorder.Rows[i]["fitamt"] + "');";
                                            dbo.dml(q_job);
                                            dt_jobprintcl.Rows.Add(dt_jobid.Rows[dt_jobid.Rows.Count - 1]["Job_Id"]);
                                            dt_confirmjob.Rows.RemoveAt(k);
                                        }
                                    }
                                }
                            }
                            dt_confirmoldpres.Rows.RemoveAt(j);
                        }
                        q_ins_cl_order = "insert into Contact_Lens_Order_Products(OrderId,CL_Pres_No,CL_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + cl_presno + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                        dbo.dml(q_ins_cl_order);
                    }
                }
                else
                {
                    Response.Write("contact lens without pres<br/>");
                    q_ins_cl_order = "insert into Contact_Lens_Order_Products(OrderId,CL_Prod_No,Quant,Price,Amount) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "')";
                    dbo.dml(q_ins_cl_order);
                    Response.Write(q_ins_gl_order);
                }
                q_ins_gl_update = "update Contact_Lens_Products set Quant='" + remain + "' where CL_ProdNo='" + idno + "'";
                Response.Write(q_ins_gl_update);
                dbo.dml(q_ins_gl_update);
            }

            else if (id == "sl")
            {
                Response.Write("SL");
                q_fe_sl = "select * from Solution_Products where Sol_ProdNo=" + idno;
                dbo.get(q_fe_sl, ref dt_fe_sl);

                string dat = dt_fe_sl.Rows[0]["ExpiryDate"].ToString();
                q_ins_sl_order = "insert into Solution_Order_Products(OrderId,Sol_Prod_No,Quant,Price,Amount,ExpiryDate) values('" + dt_orderid.Rows[0]["Order_Id"] + "','" + idno + "','" + dt_confirmorder.Rows[i]["proquant"] + "','" + dt_confirmorder.Rows[i]["proprice"] + "','" + dt_confirmorder.Rows[i]["amount"] + "','" + dat + "')";
                dbo.dml(q_ins_sl_order);


                int remain = Convert.ToInt32(dt_fe_sl.Rows[0]["Quant"].ToString()) - Convert.ToInt32(dt_confirmorder.Rows[i]["proquant"].ToString());
                q_ins_sl_order = "update Solution_Products set Quant=" + remain + "where Sol_ProdNo=" + idno;
                dbo.dml(q_ins_sl_order);
            }

            dt_billproduct.Rows.Add(dt_confirmorder.Rows[i]["promodel"], dt_confirmorder.Rows[i]["proquant"], dt_confirmorder.Rows[i]["proprice"], dt_confirmorder.Rows[i]["fitamt"], dt_confirmorder.Rows[i]["amount"]);
        }
        //q_fe_jobprint = "select * from Job_Order_Products where OrderId='"+dt_orderid.Rows[0]["Order_Id"].ToString()+"'";
        //dbo.get(q_fe_jobprint, ref dt_jobprint);
        Session.Add("jobprintpageglass",dt_jobprintglass);
        Session.Add("jobprintpagecl", dt_jobprintcl);
        Session.Add("billproduct", dt_billproduct);

        if (dt_jobprintglass.Rows.Count != 0 || dt_jobprintcl.Rows.Count != 0)
        {
            Response.Redirect("multiplejobprintpages.aspx?id="+Request.QueryString["id"]);
        }
        
        else
        {
            Response.Redirect("billprintpage.aspx?id=" + Request.QueryString["id"]);
        }
    }
}