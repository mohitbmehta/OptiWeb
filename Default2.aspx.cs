using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Default2 : System.Web.UI.Page
{

    public DataTable dt_c = new DataTable(); 
    public DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //string q = "select * from Seller_Companies";
        //dbo.get(q, ref dt);
        //Response.Write(dt.Rows[0]["PersonName"]);
       
            string q_c = "select * from Seller_Companies";
            dbo.get(q_c, ref dt_c);
            if (Page.IsPostBack)
            {    
                for (int i = 0; i < dt_c.Rows.Count; i++)
            {
                ddlpersonname.Items.Add(new ListItem(dt_c.Rows[i]["PersonName"].ToString(), dt_c.Rows[i]["SCompany_Id"].ToString()));
            }
        }

    }
    protected void ddlprosellertype_SelectedIndexChanged(object sender, EventArgs e)
    {


        //if (ddlprosellertype.SelectedValue != "0")
        //{
        //    string q = "select * from Seller_Companies";
        //    dbo.get(q, ref dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ddlpersonname.Items.Add(new ListItem(dt.Rows[i]["PersonName"].ToString(), dt.Rows[i]["SCompany_Id"].ToString()));
        //    }
        //}
        //else
       // {
            string q = "select * from Seller_Vendors";
            dbo.get(q, ref dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlpersonname.Items.Add(new ListItem(dt.Rows[i]["PersonName"].ToString(), dt.Rows[i]["SVendor_Id"].ToString()));
            }

       // }
        //else
        //{
          
        //}

        //if (Page.IsPostBack)
        //{
            //if (ddlprosellertype.SelectedValue == "Company")
            //{
                //string q = "select * from Seller_Companies";
                //dbo.get(q, ref dt);
                //Response.Write(dt.Rows[0]["PersonName"]);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                    //ddlpersonname.Items.Add(new ListItem(dt.Rows[0]["PersonName"].ToString(), dt.Rows[0]["SCompany_Id"].ToString()));
               // }
            //}
            //else
            //{

            //}
        //}
    }
    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Response.Write("hi");
    //    if (RadioButtonList1.SelectedValue!="1")
    //    {
    //        string q = "select * from Seller_Companies";
    //        dbo.get(q, ref dt);
    //        Response.Write(dt.Rows[0]["PersonName"]);
    //    }
    //}
}