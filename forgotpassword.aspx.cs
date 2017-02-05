using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnStep1_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != "")
        {
            string q = "select * from Staff_Personal_Details where User_Type_Id='2' and Email = '" + txtEmail.Text + "'";
            DataTable dt = new DataTable();
            dbo.get(q, ref dt);
            if (dt.Rows.Count > 0)
            {

                Session.Add("fp_Ad_Id", dt.Rows[0]["Emp_Id"]);
                Session.Add("fp_SAns", dt.Rows[0]["SAns"]);

                lblSecQue.Text = dt.Rows[0]["SQue"].ToString();

                pnl1.Visible = false;
                pnl2.Visible = true;

            }
            else
                lblStep1Error.Text = "Wrong Email";
        }
        else
            lblStep1Error.Text = "Provide Email";
    }
    protected void btnStep2_Click(object sender, EventArgs e)
    {
        if (txtSecAns.Text != "")
        {
            if (txtSecAns.Text == Session["fp_SAns"].ToString())
            {
                pnl2.Visible = false;
                pnl3.Visible = true;
            }
            else
                lblStep2Error.Text = "Wrong Answer";
        }
        else
            lblStep2Error.Text = "Provide Answer";
    }
    protected void btnStep3_Click(object sender, EventArgs e)
    {
        if (txtPwd.Text == "" || txtCPwd.Text == "" || txtPwd.Text != txtCPwd.Text || txtPwd.Text.Length < 4)
        {
            lblStep3Error.Text = "Provide appropriate matching passwords having 4+ chars.";
        }
        else
        {
            string q = "update Staff_Personal_Details set Password = '" + txtPwd.Text + "' where User_Type_Id='2' and Emp_Id = '" + Session["fp_Ad_Id"] + "'";
            dbo.dml(q);

            Session.Remove("fp_Ad_Id");
            Session.Remove("fp_SAns");

            Response.Redirect("login.aspx");
        }
    }
}