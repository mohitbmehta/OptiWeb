using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_report : System.Web.UI.Page
{
    string q_report,q_user,q_user_order_frame,q_user_order_glass,q_user_order_cl,q_user_order_sl;
    public DataTable dt_report = new DataTable();
    public DataTable dt_user = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ad_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        if (ddlproductreport.SelectedItem.Value == "Frame")
        {
            q_report = "select cpd.Name,op.Date,fp.ModelName,fop.Quant,fop.Price,op.Amount from Customer_Personal_Details cpd,Frame_Order_Products fop,Frame_Products fp,Order_Products op where op.User_Cus_Id=cpd.User_Id and op.Order_Id=fop.OrderId and fop.Fr_ProdNo=fp.Fr_ProdNo";
            dbo.get(q_report, ref dt_report);
        }
        else if (ddlproductreport.SelectedItem.Value == "Glass")
        {
            q_report = "select cpd.Name,op.Date,gp.ModelName,gop.Quant,gop.Price,op.Amount from Customer_Personal_Details cpd,Glass_Order_Products gop,Glass_Products gp,Order_Products op where op.User_Cus_Id=cpd.User_Id and op.Order_Id=gop.OrderId and gop.Gla_Prod_No=gp.Gla_ProdNo";
            dbo.get(q_report, ref dt_report);
        }
        else if (ddlproductreport.SelectedItem.Value == "Contact Lens")
        {
            q_report = "select cpd.Name,op.Date,clp.ModelName,clop.Quant,clop.Price,op.Amount from Customer_Personal_Details cpd,Contact_Lens_Order_Products clop,Contact_Lens_Products clp,Order_Products op where op.User_Cus_Id=cpd.User_Id and op.Order_Id=clop.OrderId and clop.CL_Prod_No=clp.CL_ProdNo";
            dbo.get(q_report, ref dt_report);
        }
        else if (ddlproductreport.SelectedItem.Value == "Solution")
        {
            q_report = "select cpd.Name,op.Date,sp.ModelName,sop.Quant,sop.Price,op.Amount from Customer_Personal_Details cpd,Solution_Order_Products sop,Solution_Products sp,Order_Products op where op.User_Cus_Id=cpd.User_Id and op.Order_Id=sop.OrderId and sop.Sol_Prod_No=sp.Sol_ProdNo";
            dbo.get(q_report, ref dt_report);
        }
        //else if (ddlproductreport.SelectedItem.Value == "Sales")
        //{
        //    ////q_report = "select cpd.Name,op.Date,sp.ModelName,sop.Quant,sop.Price,clp.ModelName,clop.Quant,clop.Price,gp.ModelName,gop.Quant,gop.Price,fp.ModelName,fop.Quant,fop.Price,op.Amount from Customer_Personal_Details cpd,Solution_Order_Products sop,Solution_Products sp,Contact_Lens_Order_Products clop,Contact_Lens_Products clp,Order_Products op,Glass_Order_Products gop,Glass_Products gp,Frame_Order_Products fop,Frame_Products fp where (op.User_Cus_Id=cpd.User_Id) and ((op.Order_Id=sop.OrderId and sop.Sol_Prod_No=sp.Sol_ProdNo) or (op.Order_Id=clop.OrderId and clop.CL_Prod_No=clp.CL_ProdNo) or (op.Order_Id=gop.OrderId and gop.Gla_Prod_No=gp.Gla_ProdNo) or (op.Order_Id=fop.OrderId and fop.Fr_ProdNo=fp.Fr_ProdNo))";
        //    ////q_report = "select cpd.Name,op.Date,sp.ModelName,sop.Quant,sop.Price,clp.ModelName,clop.Quant,clop.Price,gp.ModelName,gop.Quant,gop.Price,fp.ModelName,fop.Quant,fop.Price,op.Amount from Customer_Personal_Details cpd,Solution_Order_Products sop,Solution_Products sp,Contact_Lens_Order_Products clop,Contact_Lens_Products clp,Order_Products op,Glass_Order_Products gop,Glass_Products gp,Frame_Order_Products fop,Frame_Products fp";
        //    //q_report = "select cpd.Name,op.Date,sp.ModelName,sop.Quant,sop.Price,clp.ModelName,clop.Quant,clop.Price,op.Amount from Customer_Personal_Details AS cpd INNER JOIN Order_Products AS op INNER JOIN Solution_Order_Products AS sop INNER JOIN Solution_Products AS sp INNER JOIN Contact_Lens_Order_Products AS clop INNER JOIN Contact_Lens_Products AS clp";
        //    //dbo.get(q_report, ref dt_report);
        //}
    }
}