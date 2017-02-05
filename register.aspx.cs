using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_register : System.Web.UI.Page
{
    String name, ms, pro, uid, pos, pass, gen;
    DateTime bd, ad;
    String curad, perad, city, email, ans;
    int ddlutid;
    long conNo, mobNo, pin, sal;
    public static long cp_counter, sp_counter;
    String q_cus, q_stf, q_cd_cus, q_cd_stf;
    DataTable dt_getstring = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ad_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }
        DataTable dt_cu = new DataTable();
        DataTable dt_up_cu = new DataTable();
        DataTable dt_up_stf = new DataTable();
        
        q_cus = "select User_Id from Customer_Personal_Details order by User_Id DESC";
        dbo.get(q_cus, ref dt_cu);
        String id;

        //if (Request.QueryString["up"] != null && Request.QueryString["tb"] != null)
        //{

        //    ddlregistered.Visible = false;
        //    custidLabel.Visible = false;
        //    custidTextBox.Visible = false;
        //    ddlfitting.Visible = false;
        //    userdetailsPanel.Visible = true;

        //}

        if (!Page.IsPostBack)
        {

            DataTable dt_ut = new DataTable();

            string q_usertype = "select * from User_Types where UserType<>'Customer' ";
            dbo.get(q_usertype, ref dt_ut);

            for (int i = 0; i < dt_ut.Rows.Count; i++)
            {
                ddlUserType.Items.Add(new ListItem(dt_ut.Rows[i]["UserType"].ToString(), dt_ut.Rows[i]["User_TypeId"].ToString()));
            }

            if (Request.QueryString["up"] != null)
            {
                userdetailsPanel.Visible = true;
                 if (Request.QueryString["tb"] == "Staff")
                {
                    String q = "select User_Type_Id,ut.UserType,Name,Gender,BirthDate,MaritalStatus,AniversaryDate,Profession,Position,Salary,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,Email,SQue,SAns from Staff_Personal_Details spd,User_Types ut where spd.User_Type_Id=ut.User_TypeId and Emp_Id='" + Request.QueryString["up"] + "'";
                    dbo.get(q, ref dt_up_stf);

                    ddlUserType.SelectedIndex = ddlUserType.Items.IndexOf(ddlUserType.Items.FindByValue(dt_up_stf.Rows[0]["User_Type_Id"].ToString()));
                    maritalStatusDropDownList.SelectedIndex = maritalStatusDropDownList.Items.IndexOf(maritalStatusDropDownList.Items.FindByValue(dt_up_stf.Rows[0]["MaritalStatus"].ToString()));
                    genderTextBox.Text = dt_up_stf.Rows[0]["Gender"].ToString();
                    genderTextBox.Enabled = false;
                    nameTextBox.Text = dt_up_stf.Rows[0]["Name"].ToString();
                    nameTextBox.Enabled = false;
                    professionTextBox.Text = dt_up_stf.Rows[0]["profession"].ToString();
                    birthDateTextBox.Text = dt_up_stf.Rows[0]["BirthDate"].ToString();
                    birthDateTextBox.Enabled = false;
                    curAddTextBox.Text = dt_up_stf.Rows[0]["CurrentAdd"].ToString();
                    perAddTextBox.Text = dt_up_stf.Rows[0]["PermanentAdd"].ToString();
                    cityTextBox.Text = dt_up_stf.Rows[0]["City"].ToString();
                    pincodeTextBox.Text = dt_up_stf.Rows[0]["Pincode"].ToString();
                    conNoTextBox.Text = dt_up_stf.Rows[0]["ContactNo"].ToString();
                    mobNoTextBox.Text = dt_up_stf.Rows[0]["MobileNo"].ToString();
                    emailTextBox.Text = dt_up_stf.Rows[0]["Email"].ToString();
                    positionTextBox.Text = dt_up_stf.Rows[0]["Position"].ToString();
                    salaryTextBox.Text = dt_up_stf.Rows[0]["Salary"].ToString();
                    ddlSecQue.SelectedIndex = ddlSecQue.Items.IndexOf(ddlSecQue.Items.FindByValue(dt_up_stf.Rows[0]["SQue"].ToString()));
                    SAnsTextBox.Text = dt_up_stf.Rows[0]["SAns"].ToString();
                    ddlUserType.Enabled = false;
                    passwordLabel.Visible = false;
                    passwordTextBox.Visible = false;
                    ConfirmPwdLabel.Visible = false;
                    ConfirmpwdTextBox.Visible = false;
                }
            }
        }

        if (Request.QueryString["del"] != null)
        {
            if (Request.QueryString["tb"] == "Staff")
            {
                String q = "delete from Staff_Personal_Details where Emp_Id='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
        }
    }

    protected void saveNextButton_Click(object sender, EventArgs e)
    {
        DataTable dt_utid = new DataTable();
        DataTable dt_cusfe = new DataTable();
        //String ddlut = ddlUserType.SelectedItem.Text.ToString();
        ddlutid = Convert.ToInt32(ddlUserType.SelectedItem.Value);
        //Response.Write(Convert.ToInt32(ddlUserType.SelectedItem.Value.ToString());
        name = nameTextBox.Text;
        bd = Convert.ToDateTime(this.birthDateTextBox.Text).Date;
        ms = maritalStatusDropDownList.SelectedItem.Text.ToString();
        if (ms != "single")
        {
            ad = Convert.ToDateTime(aniversaryDateTextBox.Text).Date;
        }
        pro = professionTextBox.Text;
        email = emailTextBox.Text;
        curad = curAddTextBox.Text;
        perad = perAddTextBox.Text;
        city = cityTextBox.Text;
        pin = Convert.ToInt32(pincodeTextBox.Text);
        gen = genderTextBox.Text;
        conNo = Convert.ToInt64(conNoTextBox.Text);
        mobNo = Convert.ToInt64(mobNoTextBox.Text);

        pos = positionTextBox.Text;
        sal = Convert.ToInt64(salaryTextBox.Text);
        pass = passwordTextBox.Text;
        ans = SAnsTextBox.Text;

        if (ms != "single")
        {
            q_stf = "insert into Staff_Personal_Details(User_Type_Id,Name,Gender,BirthDate,MaritalStatus,AniversaryDate,Profession,Position,Salary,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,Email,Password,SQue,SAns) values('" + ddlUserType.SelectedItem.Value + "','" + name + "','" + gen + "','" + bd + "','" + ms + "','" + ad + "','" + pro + "','" + pos + "','" + sal + "','" + curad + "','" + perad + "','" + city + "','" + pin + "','" + conNo + "','" + mobNo + "','" + email + "','" + pass + "','" + ddlSecQue.SelectedValue + "','" + ans + "')";
            dbo.dml(q_stf);
        }
        else
        {
            q_stf = "insert into Staff_Personal_Details(User_Type_Id,Name,Gender,BirthDate,MaritalStatus,Profession,Position,Salary,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,Email,Password,SQue,SAns) values('" + ddlUserType.SelectedItem.Value + "','" + name + "','" + gen + "','" + bd + "','" + ms + "','" + pro + "','" + pos + "','" + sal + "','" + curad + "','" + perad + "','" + city + "','" + pin + "','" + conNo + "','" + mobNo + "','" + email + "','" + pass + "','" + ddlSecQue.SelectedValue + "','" + ans + "')";
            dbo.dml(q_stf);
        }
        Response.Redirect("other.aspx");
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        DataTable dt_utid = new DataTable();
        DataTable dt_cusfe = new DataTable();
        name = nameTextBox.Text;
        bd = Convert.ToDateTime(this.birthDateTextBox.Text).Date;
        ms = maritalStatusDropDownList.SelectedItem.Text.ToString();
        if (ms != "single")
        {
            ad = Convert.ToDateTime(aniversaryDateTextBox.Text).Date;
        }
        else
            ad = Convert.ToDateTime("1/1/2001").Date;
        pro = professionTextBox.Text;
        email = emailTextBox.Text;
        curad = curAddTextBox.Text;
        perad = perAddTextBox.Text;
        city = cityTextBox.Text;
        pin = Convert.ToInt32(pincodeTextBox.Text);
        gen = genderTextBox.Text;
        conNo = Convert.ToInt64(conNoTextBox.Text);
        mobNo = Convert.ToInt64(mobNoTextBox.Text);
        pos = positionTextBox.Text;
        sal = Convert.ToInt64(salaryTextBox.Text);
        pass = passwordTextBox.Text;

        q_cd_stf = "update Staff_Personal_Details set Name='" + name + "',Gender='" + gen + "',BirthDate='" + bd + "',MaritalStatus='" + ms + "',AniversaryDate='" + ad + "',Profession='" + pro + "',Position='" + pos + "',Salary='" + sal + "',CurrentAdd='" + curad + "',PermanentAdd='" + perad + "',City='" + city + "',Pincode='" + pin + "',ContactNo='" + conNo + "',MobileNo='" + mobNo + "',Email='" + email + "',SQue='"+ddlSecQue.SelectedItem.Value+"',SAns='"+SAnsTextBox.Text+"' where Emp_Id='" + Request.QueryString["up"] + "'";
        Response.Write(q_cd_stf);
        dbo.dml(q_cd_stf);
        Response.Redirect("other.aspx");
    }
}