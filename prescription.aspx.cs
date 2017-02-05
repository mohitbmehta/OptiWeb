using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class prescription : System.Web.UI.Page
{
    string prestype;
    string docname, hosname;
    long docno, hosno;
    string presdate;
    int presty;
    public string q_fetch_pres_glass, presno,medicalid;
    public string oldpresno = null;
    DataTable dt_fetch_pres = new DataTable();

    string sphld, cylld, axisld, pdld, vald;
    string sphln, cylln, axisln, pdln, valn;
    string sphli, cylli, axisli, pdli, vali;
    string sphlp, cyllp, axislp, pdlp, valp;

    string sphrd, cylrd, axisrd, pdrd, vard;
    string sphrn, cylrn, axisrn, pdrn, varn;
    string sphri, cylri, axisri, pdri, vari;
    string sphrp, cylrp, axisrp, pdrp, varp;

    string od, oh;

    string q_ins_cl, q_ins_gl, q_ins_md, q_fe_md;

    DataTable dt_md = new DataTable();
    DataTable dt_pro = new DataTable();
   
    DataTable dt_medical = new DataTable();
    DataTable dt_glass = new DataTable();
    DataTable dt_cl = new DataTable();
    DataTable dt_userid = new DataTable();
    DataTable dt_oldpres = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                dt_pro = (DataTable)Session["order"];

                Response.Write(dt_pro.Rows[0]["status"].ToString());
            }
            

            if (Request.QueryString["re"] != null || dt_pro.Rows[0]["presreqd"].ToString() == "yes")
            {
                ddlrequired.SelectedIndex = ddlrequired.Items.IndexOf(ddlrequired.Items.FindByValue("yes"));
                ddlrequired.Enabled = false;
                /*if (Session["userid"] == null)
                {
                    ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByText("New"));
                    ddlselectpres.Enabled = false;

                }
                if (dt_pro.Rows[0]["proid"].ToString().Substring(0, 2) == "gl")
                {
                    presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByText("Glass"));
                }
                else if (dt_pro.Rows[0]["proid"].ToString().Substring(0, 2) == "cl")
                {
                    presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByText("Contact Lens"));
                }
                presTypeDropDownList.Enabled = false;
                */
            }
            
        }
    }
    protected void saveNextButton_Click(object sender, EventArgs e)
    {
        dt_pro = (DataTable)Session["order"];
        docname = docNameTextBox.Text;
        docno = Convert.ToInt64(docConNoTextBox.Text);
        hosname = hosNameTextBox.Text;
        hosno = Convert.ToInt64(hosConNoTextBox.Text);
        presdate = Convert.ToDateTime(presDateTextBox.Text).Date.ToString();
        prestype = presTypeDropDownList.SelectedItem.Value;

        if (Request.QueryString["re"] != null)
        {
            q_ins_md = "insert into Medical_Details(User_Cus_Id,DocName,DocConNo,HosName,HosConNo) values('" + Request.QueryString["cu_id"] + "','" + docname + "','" + docno + "','" + hosname + "','" + hosno + "')";
            dbo.dml(q_ins_md);
            q_fe_md = "select Medical_Id from Medical_Details order by Medical_Id DESC";
            dbo.get(q_fe_md, ref dt_md);
        }
        else
        {
            dt_medical.Columns.Add("proid");
            dt_medical.Columns.Add("DocName");
            dt_medical.Columns.Add("DocConNo");
            dt_medical.Columns.Add("HosName");
            dt_medical.Columns.Add("HosConNo");
            dt_medical.Columns.Add("status");

            dt_medical.Rows.Add(dt_pro.Rows[0]["proid"].ToString(), docname, docno, hosname, hosno, dt_pro.Rows[0]["status"]);
            Session.Add("medical", dt_medical);
        }

        if (prestype == "Contact Lens")
        {
           
            presty = Convert.ToInt32("2");
            od = ODTextBox.Text;
            oh = OHTextBox.Text;
            if (Request.QueryString["re"] != null)
            {
                q_ins_cl = "insert into Pres_Contact_Lens(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescribedDate,OD,OH) values('" + Request.QueryString["cu_id"] + "','" + presty + "','" + dt_md.Rows[0]["Medical_Id"] + "','" + Convert.ToDateTime(presdate).ToString("MM/dd/yyyy") + "','" + od + "','" + oh + "')";
                dbo.dml(q_ins_cl);
                Response.Redirect("jobprint.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + " & prestaken = yes&pres=" + prestype);
            }
            else
            {
                dt_cl.Columns.Add("proid");
                dt_cl.Columns.Add("PrescribedDate");
                dt_cl.Columns.Add("prestypeid");
                dt_cl.Columns.Add("OD");
                dt_cl.Columns.Add("OH");
                dt_cl.Columns.Add("status");

                dt_cl.Rows.Add(dt_pro.Rows[0]["proid"].ToString(), Convert.ToDateTime(presdate).ToString("MM/dd/yyyy"), presty, od, oh, dt_pro.Rows[0]["status"]);
                Session.Add("contactlens", dt_cl);
                dt_pro.Rows[0]["prestype"] = "new";
                Session["order"] = dt_pro;

                if (dt_pro.Rows[0]["fitreqd"].ToString() == "yes")
                {
                    //Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + " & prestaken = yes&pres=" + prestype);
                    Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"]);
                }
                else
                {

                    //Response.Redirect("billpage.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"]+" & prestaken = yes");
                    Response.Redirect("productconfirmation.aspx?id=" + Request.QueryString["id"]);
                }
            }
        }
        else if (prestype == "Glass")
        {
            //cl.Visible = false;
            presty = Convert.ToInt32("1");
            sphrd = SPHRightDisTextBox.Text;
            sphrn = SPHRightNearTextBox.Text;
            sphri = SPHRightIntTextBox.Text;
            sphrp = SPHRightProTextBox.Text;

            cylrd = CYLRightDisTextBox.Text;
            cylrn = CYLRightNearTextBox.Text;
            cylri = CYLRightIntTextBox.Text;
            cylrp = CYLRightProTextBox.Text;

            axisrd = AxisRightDisTextBox.Text;
            axisrn = AxisRightNearTextBox.Text;
            axisri = AxisRightIntTextBox.Text;
            axisrp = AxisRightProTextBox.Text;

            pdrd = PDRightDisTextBox.Text;
            pdrn = PDRightNearTextBox.Text;
            pdri = PDRightIntTextBox.Text;
            pdrp = PDRightProTextBox.Text;

            vard = VARightDisTextBox.Text;
            varn = VARightNearTextBox.Text;
            vari = VARightIntTextBox.Text;
            varp = VARightProTextBox.Text;




            sphld = SPHLeftDisTextBox.Text;
            sphln = SPHLeftNearTextBox.Text;
            sphli = SPHLeftIntTextBox.Text;
            sphlp = SPHLeftProTextBox.Text;

            cylld = CYLLeftDisTextBox.Text;
            cylln = CYLLeftNearTextBox.Text;
            cylli = CYLLeftIntTextBox.Text;
            cyllp = CYLLeftProTextBox.Text;

            axisld = AxisLeftDisTextBox.Text;
            axisln = AxisLeftNearTextBox.Text;
            axisli = AxisLeftIntTextBox.Text;
            axislp = AxisLeftProTextBox.Text;

            pdld = PDLeftDisTextBox.Text;
            pdln = PDLeftNearTextBox.Text;
            pdli = PDLeftIntTextBox.Text;
            pdlp = PDLeftProTextBox.Text;

            vald = VALeftDisTextBox.Text;
            valn = VALeftNearTextBox.Text;
            vali = VALeftIntTextBox.Text;
            valp = VALeftProTextBox.Text;

           

            //q_ins_cl = "insert into Pres_Glass(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd) values('" + Request.QueryString["cu_id"] + "','" + presty + "','" + dt_md.Rows[0]["Medical_Id"] + "','" + Convert.ToDateTime(presdate).ToString("MM/dd/yyyy") + "','" + sphrd + "','" + sphrn + "','" + sphri + "','" + sphrp + "','" + cylrd + "','" + cylrn + "','" + cylri + "','" + cylrp + "','" + axisrd + "','" + axisrn + "','" + axisri + "','" + axisrp + "','" + pdrd + "','" + pdrn + "','" + pdri + "','" + pdrp + "','" + vard + "','" + varn + "','" + vari + "','" + varp + "','" + sphld + "','" + sphln + "','" + sphli + "','" + sphlp + "','" + cylld + "','" + cylln + "','" + cylli + "','" + cyllp + "','" + axisld + "','" + axisln + "','" + axisli + "','" + axislp + "','" + pdld + "','" + pdln + "','" + pdli + "','" + pdlp + "','" + vald + "','" + valn + "','" + vali + "','" + valp + "')";
            //dbo.dml(q_ins_cl);

            if (Request.QueryString["id"] != null)
            {

                dt_glass.Columns.Add("proid");
                dt_glass.Columns.Add("prestypeid");
                dt_glass.Columns.Add("PrescritpionDate");
                dt_glass.Columns.Add("SPH_Right_Distance");
                dt_glass.Columns.Add("SPH_Right_NearAdd");
                dt_glass.Columns.Add("SPH_Right_IntermediateAdd");
                dt_glass.Columns.Add("SPH_Right_ProgressiveAdd");
                dt_glass.Columns.Add("CYL_Right_Distance");
                dt_glass.Columns.Add("CYL_Right_NearAdd");
                dt_glass.Columns.Add("CYL_Right_IntermediateAdd");
                dt_glass.Columns.Add("CYL_Right_ProgressiveAdd");
                dt_glass.Columns.Add("Axis_Right_Distance");
                dt_glass.Columns.Add("Axis_Right_NearAdd");
                dt_glass.Columns.Add("Axis_Right_IntermediateAdd");
                dt_glass.Columns.Add("Axis_Right_ProgressiveAdd");
                dt_glass.Columns.Add("PD_Right_Distance");
                dt_glass.Columns.Add("PD_Right_NearAdd");
                dt_glass.Columns.Add("PD_Right_IntermediateAdd");
                dt_glass.Columns.Add("PD_Right_ProgressiveAdd");
                dt_glass.Columns.Add("VA_Right_Distance");
                dt_glass.Columns.Add("VA_Right_NearAdd");
                dt_glass.Columns.Add("VA_Right_IntermediateAdd");
                dt_glass.Columns.Add("VA_Right_ProgressiveAdd");
                dt_glass.Columns.Add("SPH_Left_Distance");
                dt_glass.Columns.Add("SPH_Left_NearAdd");
                dt_glass.Columns.Add("SPH_Left_IntermediateAdd");
                dt_glass.Columns.Add("SPH_Left_ProgressiveAdd");
                dt_glass.Columns.Add("CYL_Left_Distance");
                dt_glass.Columns.Add("CYL_Left_NearAdd");
                dt_glass.Columns.Add("CYL_Left_IntermediateAdd");
                dt_glass.Columns.Add("CYL_Left_ProgressiveAdd");
                dt_glass.Columns.Add("Axis_Left_Distance");
                dt_glass.Columns.Add("Axis_Left_NearAdd");
                dt_glass.Columns.Add("Axis_Left_IntermediateAdd");
                dt_glass.Columns.Add("Axis_Left_ProgressiveAdd");
                dt_glass.Columns.Add("PD_Left_Distance");
                dt_glass.Columns.Add("PD_Left_NearAdd");
                dt_glass.Columns.Add("PD_Left_IntermediateAdd");
                dt_glass.Columns.Add("PD_Left_ProgressiveAdd");
                dt_glass.Columns.Add("VA_Left_Distance");
                dt_glass.Columns.Add("VA_Left_NearAdd");
                dt_glass.Columns.Add("VA_Left_IntermediateAdd");
                dt_glass.Columns.Add("VA_Left_ProgressiveAdd");
                dt_glass.Columns.Add("status");


                dt_glass.Rows.Add(dt_pro.Rows[0]["proid"].ToString(), presty, Convert.ToDateTime(presdate).ToString("MM/dd/yyyy"), sphrd, sphrn, sphri, sphrp, cylrd, cylrn, cylri, cylrp, axisrd, axisrn, axisri, axisrp, pdrd, pdrn, pdri, pdrp, vard, varn, vari, varp, sphld, sphln, sphli, sphlp, cylld, cylln, cylli, cyllp, axisld, axisln, axisli, axislp, pdld, pdln, pdli, pdlp, vald, valn, vali, valp, dt_pro.Rows[0]["status"]);
                Session.Add("glass", dt_glass);
                dt_pro.Rows[0]["prestype"] = "new";
                Session["order"] = dt_pro;

                //if (Request.QueryString["fit"] == "yes")
                if(dt_pro.Rows[0]["fitreqd"].ToString()=="yes")
                {
                    //Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + " & prestaken = yes&pres=" + prestype);
                    Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"]);
                }
                else
                {
                    
                    //Response.Redirect("billpage.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"]+" & prestaken = yes");
                    Response.Redirect("productconfirmation.aspx?id=" + Request.QueryString["id"]);
                }
            }

            else if (Request.QueryString["re"] != null)
            {
                q_ins_gl = "insert into Pres_Glass(User_Cus_Id,Pres_Type_Id,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd) values('" + Request.QueryString["cu_id"] + "','" + presty + "','" + dt_md.Rows[0]["Medical_Id"] + "','" + Convert.ToDateTime(presdate).ToString("MM/dd/yyyy") + "','" + sphrd + "','" + sphrn + "','" + sphri + "','" + sphrp + "','" + cylrd + "','" + cylrn + "','" + cylri + "','" + cylrp + "','" + axisrd + "','" + axisrn + "','" + axisri + "','" + axisrp + "','" + pdrd + "','" + pdrn + "','" + pdri + "','" + pdrp + "','" + vard + "','" + varn + "','" + vari + "','" + varp + "','" + sphld + "','" + sphln + "','" + sphli + "','" + sphlp + "','" + cylld + "','" + cylln + "','" + cylli + "','" + cyllp + "','" + axisld + "','" + axisln + "','" + axisli + "','" + axislp + "','" + pdld + "','" + pdln + "','" + pdli + "','" + pdlp + "','" + vald + "','" + valn + "','" + vali + "','" + valp + "')";
                dbo.dml(q_ins_gl);
                Response.Redirect("jobprint.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + " & prestaken = yes&pres=" + prestype);
            }

        }
    }
    protected void displayButton_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null && Session["userid"]!=null)
        {
            dt_oldpres.Columns.Add("proid");
            dt_oldpres.Columns.Add("Medical_Id");
            dt_oldpres.Columns.Add("presno");
            dt_oldpres.Columns.Add("prestype");
            dt_oldpres.Columns.Add("status");

            
            dt_pro = (DataTable)Session["order"];
            nextoldpresButton.Visible = true;
            medicalPanel.Visible = true;
            dt_userid = (DataTable)Session["userid"];

            q_fe_md = "select Medical_Id,DocName,DocConNo,HosName,HosConNo from Medical_Details where User_Cus_Id='" + dt_userid.Rows[0]["userid"] + "'";
            dbo.get(q_fe_md, ref dt_md);
            if (dt_md.Rows.Count > 0)
            {
                //medicalPanel.Enabled = false;
                docNameTextBox.Text = dt_md.Rows[0]["DocName"].ToString();
                docConNoTextBox.Text = dt_md.Rows[0]["DocConNo"].ToString();
                hosNameTextBox.Text = dt_md.Rows[0]["HosName"].ToString();
                hosConNoTextBox.Text = dt_md.Rows[0]["HosConNo"].ToString();

                if (ddlselectprestype.SelectedItem.Value == "Glass")
                {
                    //presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Glass"));
                    //q_fetch_pres_glass = "select Gla_PresNo,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where User_Cus_Id='" + dt_userid.Rows[0]["userid"].ToString() + "' order by Gla_PresNo DESC";
                    q_fetch_pres_glass = "select * from Pres_Glass where User_Cus_Id='" + dt_userid.Rows[0]["userid"] + "' order by Gla_PresNo DESC";
                    dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);

                    if (dt_fetch_pres.Rows.Count > 0)
                    {
                        //Response.Write(dt_userid.Rows[0]["userid"].ToString());
                        //Response.Write(dt_fetch_pres.Rows[0]["Gla_PresNo"].ToString());
                        presDateTextBox.Text = dt_fetch_pres.Rows[0]["PrescriptionDate"].ToString();
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

                        presno = dt_fetch_pres.Rows[0]["Gla_PresNo"].ToString();
                        prestype = ddlselectprestype.SelectedItem.Value;
                        medicalid = dt_fetch_pres.Rows[0]["Medical_Cus_Id"].ToString();
                        dt_oldpres.Rows.Add(dt_pro.Rows[0]["proid"], medicalid, presno, prestype, dt_pro.Rows[0]["status"]);
                        Session.Add("oldpres", dt_oldpres);
                        dt_pro.Rows[0]["prestype"] = "previous";
                        Session["order"] = dt_pro;
                    }
                    else
                    {
                        ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByText("New"));
                    }
                }
                else if (ddlselectprestype.SelectedItem.Value == "Contact Lens")
                {
                    //presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Contact Lens"));
                    q_fetch_pres_glass = "select CL_PresNo,Medical_Cus_Id,PrescribedDate,OD,OH from Pres_Contact_Lens where User_Cus_Id='" + dt_userid.Rows[0]["userid"] + "' order by CL_PresNo DESC";
                    dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);
                    if (dt_fetch_pres.Rows.Count > 0)
                    {
                        presDateTextBox.Text = dt_fetch_pres.Rows[0]["PrescribedDate"].ToString();
                        ODTextBox.Text = dt_fetch_pres.Rows[0]["OD"].ToString();
                        OHTextBox.Text = dt_fetch_pres.Rows[0]["OH"].ToString();
                        presno = dt_fetch_pres.Rows[0]["CL_PresNo"].ToString();
                        prestype = ddlselectprestype.SelectedItem.Value;
                        medicalid = dt_fetch_pres.Rows[0]["Medical_Cus_Id"].ToString();
                        //dt_oldpres.Rows[0]["presno"] = presno;
                        //dt_oldpres.Rows[0]["Medical_Id"] = medicalid;
                        //dt_oldpres.Rows[0]["prestype"] = prestype;
                        dt_oldpres.Rows.Add(dt_pro.Rows[0]["proid"].ToString(), medicalid, presno, prestype, dt_pro.Rows[0]["status"].ToString());
                        Session.Add("oldpres", dt_oldpres);
                        dt_pro.Rows[0]["prestype"] = "previous";
                        Session["order"] = dt_pro;
                    }
                    else
                    {
                        ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByText("New"));
                    }
                }
            }
            else
            {
                ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByValue("New"));
                medicalPanel.Enabled = true;

            }
        }
        else
            {
                ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByValue("New"));
                medicalPanel.Enabled = true;

            }

        if (Request.QueryString["re"] != null)
        {
            if (ddlselectprestype.SelectedItem.Value == "Glass")
            {
                //presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Glass"));
                //q_fetch_pres_glass = "select Gla_PresNo,Medical_Cus_Id,PrescriptionDate,SPH_Right_Distance,SPH_Right_NearAdd,SPH_Right_IntermediateAdd,SPH_Right_ProgressiveAdd,CYL_Right_Distance,CYL_Right_NearAdd,CYL_Right_IntermediateAdd,CYL_Right_ProgressiveAdd,Axis_Right_Distance,Axis_Right_NearAdd,Axis_Right_IntermediateAdd,Axis_Right_ProgressiveAdd,PD_Right_Distance,PD_Right_NearAdd,PD_Right_IntermediateAdd,PD_Right_ProgressiveAdd,VA_Right_Distance,VA_Right_NearAdd,VA_Right_IntermediateAdd,VA_Right_ProgressiveAdd,SPH_Left_Distance,SPH_Left_NearAdd,SPH_Left_IntermediateAdd,SPH_Left_ProgressiveAdd,CYL_Left_Distance,CYL_Left_NearAdd,CYL_Left_IntermediateAdd,CYL_Left_ProgressiveAdd,Axis_Left_Distance,Axis_Left_NearAdd,Axis_Left_IntermediateAdd,Axis_Left_ProgressiveAdd,PD_Left_Distance,PD_Left_NearAdd,PD_Left_IntermediateAdd,PD_Left_ProgressiveAdd,VA_Left_Distance,VA_Left_NearAdd,VA_Left_IntermediateAdd, VA_Left_ProgressiveAdd from Pres_Glass where User_Cus_Id='" + dt_userid.Rows[0]["userid"].ToString() + "' order by Gla_PresNo DESC";
                q_fetch_pres_glass = "select * from Pres_Glass where User_Cus_Id='" + Request.QueryString["cu_id"] + "' order by Gla_PresNo DESC";
                dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);
                if (dt_fetch_pres.Rows.Count > 0)
                {
                    nextoldpresButton.Visible = true;
                    oldpresno =dt_fetch_pres.Rows[0]["Gla_PresNo"].ToString();
                    Session.Add("reoldpres", oldpresno);
                }
                else
                {
                    ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByText("New"));
                }
            }
            else if (ddlselectprestype.SelectedItem.Value == "Contact Lens")
            {
                //presTypeDropDownList.SelectedIndex = presTypeDropDownList.Items.IndexOf(presTypeDropDownList.Items.FindByValue("Contact Lens"));
                q_fetch_pres_glass = "select CL_PresNo,Medical_Cus_Id,PrescribedDate,OD,OH from Pres_Contact_Lens where User_Cus_Id='" + dt_userid.Rows[0]["userid"] + "' order by CL_PresNo DESC";
                dbo.get(q_fetch_pres_glass, ref dt_fetch_pres);
                if (dt_fetch_pres.Rows.Count > 0)
                {
                    nextoldpresButton.Visible = true;
                    oldpresno = dt_fetch_pres.Rows[0]["CL_PresNo"].ToString();
                    Session.Add("reoldpres", oldpresno);
                }
                else
                {
                    ddlselectpres.SelectedIndex = ddlselectpres.Items.IndexOf(ddlselectpres.Items.FindByText("New"));
                }
            }
        } 
    }


    protected void nextButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("productconfirmation.aspx?id=" + Request.QueryString["id"]);
    }
    protected void nextoldpresButton_Click(object sender, EventArgs e)
    {
        dt_pro = (DataTable)Session["order"];
        if (Request.QueryString["id"] != null)
        {
            
            /*Response.Write(dt_pro.Rows[0]["proid"].ToString());
            Response.Write("<br/>");
            Response.Write("hello" + presno);
            //dt_oldpres.Rows.Add(dt_pro.Rows[0]["proid"].ToString(),medicalid,presno, prestype);
            //Response.Write(dt_oldpres);
            Session.Add("oldpres",dt_oldpres);*/
           
            
            
            if (dt_pro.Rows[0]["fitreqd"].ToString() == "yes")
            {
                //Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + "&prestaken=yes&pres=" + prestype + "&presno=" + presno);
                Response.Redirect("jobprint.aspx?id=" + Request.QueryString["id"]);
            }
            else
            {
                //Response.Redirect("billpage.aspx?id=" + Request.QueryString["id"] + "&qty=" + Request.QueryString["qty"] + "&price=" + Request.QueryString["price"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_Id"] + "&prestaken=yes&presno=" + presno);
                Response.Redirect("productconfirmation.aspx?id=" + Request.QueryString["id"]);
            }
        }

        else if (Request.QueryString["re"] != null)
        {
            Response.Redirect("jobprint.aspx?re=" + Request.QueryString["re"] + "&fit=" + Request.QueryString["fit"] + "&cu_id=" + Request.QueryString["cu_id"] + "&prestaken=yes&pres=" + ddlselectprestype.SelectedItem.Value + "&presno=" + Session["reoldpres"]);
        }
    }
}