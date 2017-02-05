using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class other : System.Web.UI.Page
{
    public DataTable dt_details = new DataTable();
    BoundField bf_gd;
    //public DataColumn col=new DataColumn();
    //public DataRow row=new DataRow();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = showButton.UniqueID;
        if (Session["Ad_Id"] == null) 
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx"); 
        }
    }

    protected void showButton_Click1(object sender, EventArgs e)
    {
        //fetch details of customer
        if (ddlselect.SelectedItem.Text.ToString() == "Customer")
        {
            string q_details = "select cpd.User_Id,ut.UserType,cpd.Name,cpd.BirthDate,cpd.MaritalStatus,cpd.AniversaryDate,cpd.Profession from Customer_Personal_Details cpd,User_Types ut where cpd.User_Type_Id=ut.User_TypeId order by User_Id";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Staff")
        {
            string q_details = "select spd.Emp_Id,ut.UserType,spd.Name,spd.BirthDate,spd.MaritalStatus,spd.AniversaryDate,spd.Profession,spd.Position,spd.Salary from Staff_Personal_Details spd,User_Types ut where spd.User_Type_Id=ut.User_TypeId order by Emp_Id";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Seller Vendor")
        {
            string q_details = "select SVendor_Id,SellerType as Type,PersonName as Name,CurrentAdd as Current_Address,PermanentAdd as Permanent_Address,City as City,ContactNo as Contact_No,MobileNo as Mobile_No,EmailId as Email from Seller_Vendors order by SVendor_Id";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Seller Company")
        {
            string q_details = "select SCompany_Id,SellerType as Type,CompanyName as Company,PersonName as Name,CurrentAdd as Current_Address,PermanentAdd as Permanent_Address,City as City,ContactNo as Contact_No,MobileNo as Mobile_No,EmailId as Email from Seller_Companies order by SCompany_Id";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Frame")
        {
            string q_details = "select Fr_ProdNo,pt.ProdType as Product,SellerType as Seller,PurchaseDate as Purchase,ModelName as Model,Category,Quant,Price,Type,Colour,Shape,Material from Frame_Products fp,Product_Types pt where fp.Prod_Type_Id=pt.Prod_TypeId order by Fr_ProdNo";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Glass")
        {
            string q_details = "select Gla_ProdNo,pt.ProdType as Product,SellerType as Seller,PurchaseDate as Purchase,ModelName as Model,Type,Quant,Price,Material from Glass_Products gp,Product_Types pt where gp.Prod_Type_Id=pt.Prod_TypeId order by Gla_ProdNo";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Contact Lens")
        {
            string q_details = "select Cl_ProdNo,pt.ProdType as Product,SellerType as Seller,PurchaseDate as Purchase,ModelName as Model,Type,Power,Quant,Price,Material from Contact_Lens_Products clp,Product_Types pt where clp.Prod_Type_Id=pt.Prod_TypeId order by Cl_ProdNo";
            dbo.get(q_details, ref dt_details);
        }
        else if (ddlselect.SelectedItem.Text.ToString() == "Solution")
        {
            string q_details = "select Sol_ProdNo,pt.ProdType as Product,SellerType as Seller,PurchaseDate as Purchase,ModelName as Model,QuantLitre as Quantity_Litre,Quant,Price,Material from Solution_Products sp,Product_Types pt where sp.Prod_Type_Id=pt.Prod_TypeId order by Sol_ProdNo";
            dbo.get(q_details, ref dt_details);
        }
        //displayGridView.DataSource = dt_details;
        //displayGridView.DataBind();
    }
    
}