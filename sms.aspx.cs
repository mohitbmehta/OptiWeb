using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_sms : System.Web.UI.Page
{
    
    public DataTable dt_bd = new DataTable();
    public DataTable dt_ad = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ad_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            sms.Visible = true;
            if (!Page.IsPostBack)
            {
                string q_bd = "select User_Id,Name,BirthDate,ContactNo,MobileNo from Customer_Personal_Details where DATEPART(d,BirthDate)=DATEPART(d,GETDATE()) AND DATEPART(M,BirthDate)=DATEPART(M,GETDATE())";
                dbo.get(q_bd, ref dt_bd);
            }
                string q_ad = "select USer_Id,Name,AniversaryDate,ContactNo,MobileNo from Customer_Personal_Details where DATEPART(d,AniversaryDate)=DATEPART(d,GETDATE()) AND DATEPART(M,AniversaryDate)=DATEPART(M,GETDATE())";
                dbo.get(q_ad, ref dt_ad);
            //}
            nothing.Visible = false;
            birthday.Visible = false;
            aniversary.Visible = false;
            //if (dt_ad.Rows.Count != 0 && dt_bd.Rows.Count != 0)
              //  line.Visible = true;
            if (dt_bd.Rows.Count != 0)
            {
                aniversary.Visible = false;
                birthday.Visible = true;
                //line.Visible = false;
            }
            if (dt_bd.Rows.Count == 0)
            {
                aniversary.Visible = true;
                //line.Visible = false;
            }
            if (dt_ad.Rows.Count == 0 && dt_bd.Rows.Count == 0)
            {
                Response.Write("<center> Sorry there is no any customer's birthday or aniversary</center>");
                //sms.Visible = false;
                nothing.Visible = true;
                //line.Visible = false;
            }
        }
        else
        {
            sms.Visible = false;
            Response.Write("<center>Sorry ! Internet Connection is not avialable</center>");
            nothing.Visible = true;
        }
    }

    protected void birthdayButton_Click(object sender, EventArgs e)
    {
        //nothing.Visible = false;   
        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            string q_bd = "select User_Id,Name,BirthDate,ContactNo,MobileNo from Customer_Personal_Details where DATEPART(d,BirthDate)=DATEPART(d,GETDATE()) AND DATEPART(M,BirthDate)=DATEPART(M,GETDATE())";
            dbo.get(q_bd, ref dt_bd);
            //Response.Write("hello");
            if (dt_bd.Rows.Count > 0)
            {
                int bdnum = dt_bd.Rows.Count;
                Response.Write("hello----"+dt_bd.Rows.Count);
                for (int i = 0; i <bdnum; i++)
                {
                    Communicate.send_sms("" + dt_bd.Rows[0]["ContactNo"] + "", "Hello " + dt_bd.Rows[0]["Name"] + ", how are you?!Wish You A Happy Birthday!! ");
                    Response.Write(i);
                    Response.Write("BirthDay--"+dt_bd.Rows[0]["Name"]+"<br/>");
                    dt_bd.Rows.RemoveAt(0);
                    //if (dt_bd.Rows.Count == 0)
                    //{
                    //    birthday.Visible = false;
                    //    break;
                    //}
                }
                if (dt_ad.Rows.Count != 0)
                {
                    //Response.Redirect("default.aspx");
                    aniversary.Visible = true;
                }
                else
                    Response.Redirect("default.aspx"); 
            }
        }
        else
        {
          Response.Write("<center>Sorry ! Internet Connection is not avialable</center>");
          birthday.Visible = false;
          aniversary.Visible = false;
          //line.Visible = false;
          nothing.Visible = true;
        }
    }

    protected void anniversaryButton_Click(object sender, EventArgs e)
    {
        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {

            if (dt_ad.Rows.Count > 0)
            {
                int adnum = dt_ad.Rows.Count;
                for (int i = 0; i <adnum; i++)
                {
                    Communicate.send_sms("" + dt_ad.Rows[i]["ContactNo"] + "", "Hello " + dt_ad.Rows[i]["Name"] + ", how are you?! Wish You A Happy Aniversary!! ");
                    Response.Write("Aniversary--" + dt_ad.Rows[0]["Name"] + "<br/>");
                    dt_ad.Rows.RemoveAt(0);
                    //if (dt_ad.Rows.Count == 0)
                    //{
                    //    aniversary.Visible = false;
                    //    break;
                    //}
                }
                //if (dt_ad.Rows.Count == 0)
                    //Response.Redirect("default.aspx");
            }
        }
        else
        {
            Response.Write("<center>Sorry ! Internet Connection is not avialable</center>");
            birthday.Visible = false;
            aniversary.Visible = false;
            //line.Visible = false;
            nothing.Visible = true;
        }
    }
    protected void nothingButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}