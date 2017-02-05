using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_stockmaintain : System.Web.UI.Page
{
    string protype, model, colour, shape, material, cat, type, rim, seller,gltype;
    string q_proent;
    int quant, quan_lit, seller_id, protype_id;
    double price, power;
    DateTime pd;
    //Upload
    string fnm1 = "";
    string q_up_selven, q_up_selcom, q_up_fr, q_up_gl, q_up_cl, q_up_sol;
    string q_sel;
    DataTable dt_seller = new DataTable();
    DataTable dt_update = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ad_Id"] == null)
        {
            Session.Add("path", Request.Url.ToString());
            Response.Redirect("login.aspx");
        }
        secomupdateButton.Visible = false;
        proupdateButton.Visible = false;
        string q_pro;
        DataTable dt_pro = new DataTable();
        if (!ddlformtype.AutoPostBack)
        {
            if (ddlformtype.SelectedValue.ToString() == "Seller")
            {
                q_pro = "select * from Product_Types";
                dbo.get(q_pro, ref dt_pro);
                for (int i = 0; i < dt_pro.Rows.Count; i++)
                {
                    ddlproducttype.Items.Add(new ListItem(dt_pro.Rows[i]["ProdType"].ToString(), dt_pro.Rows[i]["Prod_TypeId"].ToString()));
                }
                //materialTextBox.Text = dt_pro.Rows[0]["ProdType"].ToString();
            }
        }

        if (Request.QueryString["del"] != null)
        {
            if (Request.QueryString["tb"] == "Frame")
            {
                String q = "delete from Frame_Products where Fr_ProdNo='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
            else if (Request.QueryString["tb"] == "Glass")
            {
                String q = "delete from Glass_Products where Gl_ProdNo='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
            else if (Request.QueryString["tb"] == "Contact Lens")
            {
                String q = "delete from Contact_Lens_Products where Cl_ProdNo='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
            else if (Request.QueryString["tb"] == "Solution")
            {
                String q = "delete from Solution_Products where Sl_ProdNo='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
            else if (Request.QueryString["tb"] == "Seller Vendor")
            {
                String q = "delete from Seller_Vendors where SVendor_Id='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
            else if (Request.QueryString["tb"] == "Seller Vendor")
            {
                String q = "delete from Seller_Companies where SCompany_Id='" + Request.QueryString["del"] + "'";
                dbo.dml(q);
                Response.Redirect("other.aspx");
            }
        }

        if (!Page.IsPostBack)
        {

            if (Request.QueryString["up"] != null)
            {
                if ((Request.QueryString["tb"] == "Seller Vendor") || (Request.QueryString["tb"] == "Seller Company"))
                {
                    secomsaveButton.Visible = false;
                    secomupdateButton.Visible = true;
                    ddlformtype.SelectedIndex = ddlformtype.Items.IndexOf(ddlformtype.Items.FindByText("Seller"));
                    ddlformtype.Enabled = false;
                    if (Request.QueryString["tb"] == "Seller Vendor")
                    {
                        companynameLabel.Visible = false;
                        companyTextBox.Visible = false;
                        q_up_selven = "select SellerType,PersonName,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId from Seller_Vendors where SVendor_Id='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_selven, ref dt_update);
                        ddlsellertype.SelectedIndex = ddlsellertype.Items.IndexOf(ddlsellertype.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        ddlsellertype.Enabled = false;
                        personnameTextBox.Text = dt_update.Rows[0]["PersonName"].ToString();
                        curAddTextBox.Text = dt_update.Rows[0]["CurrentAdd"].ToString();
                        perAddTextBox.Text = dt_update.Rows[0]["PermanentAdd"].ToString();
                        cityTextBox.Text = dt_update.Rows[0]["City"].ToString();
                        pincodeTextBox.Text = dt_update.Rows[0]["Pincode"].ToString();
                        conNoTextBox.Text = dt_update.Rows[0]["ContactNo"].ToString();
                        mobNoTextBox.Text = dt_update.Rows[0]["MobileNo"].ToString();
                        emailTextBox.Text = dt_update.Rows[0]["EmailId"].ToString();
                    }
                    else if (Request.QueryString["tb"] == "Seller Company")
                    {
                        q_up_selcom = "select SellerType,PersonName,CompanyName,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId from Seller_Companies where SCompany_Id='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_selcom, ref dt_update);
                        ddlsellertype.SelectedIndex = ddlsellertype.Items.IndexOf(ddlsellertype.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        ddlsellertype.Enabled = false;
                        personnameTextBox.Text = dt_update.Rows[0]["PersonName"].ToString();
                        companyTextBox.Text = dt_update.Rows[0]["CompanyName"].ToString();
                        companyTextBox.Enabled = false;
                        curAddTextBox.Text = dt_update.Rows[0]["CurrentAdd"].ToString();
                        perAddTextBox.Text = dt_update.Rows[0]["PermanentAdd"].ToString();
                        cityTextBox.Text = dt_update.Rows[0]["City"].ToString();
                        pincodeTextBox.Text = dt_update.Rows[0]["Pincode"].ToString();
                        conNoTextBox.Text = dt_update.Rows[0]["ContactNo"].ToString();
                        mobNoTextBox.Text = dt_update.Rows[0]["MobileNo"].ToString();
                        emailTextBox.Text = dt_update.Rows[0]["EmailId"].ToString();
                    }
                }
                if ((Request.QueryString["tb"] == "Frame") || (Request.QueryString["tb"] == "Glass") || (Request.QueryString["tb"] == "Contact Lens") || (Request.QueryString["tb"] == "Solution"))
                {
                    ddlformtype.SelectedIndex = ddlformtype.Items.IndexOf(ddlformtype.Items.FindByText("Product"));
                    ddlformtype.Enabled = false;
                    fuThumb.Visible = false;
                    fuThumbError.Visible = false;
                    imageuploadLabel.Visible = false;
                    prosaveButton.Visible = false;
                    proupdateButton.Visible = true;
                    if (Request.QueryString["tb"] == "Frame")
                    {
                        contactlens.Visible = false;
                        solution.Visible = false;
                        glass.Visible = false;

                        q_up_fr = "select Prod_Type_Id,SellerType,SVen_Id,SCom_Id,PurchaseDate,ModelName,Category,Quant,Price,Type,Colour,Shape,Material from Frame_Products where Fr_ProdNo='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_fr, ref dt_update);
                        ddlproducttype.SelectedIndex = ddlproducttype.Items.IndexOf(ddlproducttype.Items.FindByValue(dt_update.Rows[0]["Prod_Type_Id"].ToString()));
                        ddlproducttype.Enabled = false;
                        modelnameTextBox.Text = dt_update.Rows[0]["ModelName"].ToString();
                        purchasedateTextBox.Text = dt_update.Rows[0]["PurchaseDate"].ToString();
                        purchasedateTextBox.Enabled = false;

                        // RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        // RadioButtonList1.Enabled = false;
                        if (prosellertypeDropDownList.SelectedItem.Value == "Company")
                        {

                            //ddlpersonname.Items.Clear();
                            q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlcompanyperson.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
                            }

                            ddlcompanyperson.SelectedIndex = ddlcompanyperson.Items.IndexOf(ddlcompanyperson.Items.FindByValue(dt_update.Rows[0]["SVen_Id"].ToString()));
                            ddlcompanyperson.Enabled = false;
                        }
                        else if (prosellertypeDropDownList.SelectedItem.Value == "Vendor")
                        {
                            //ddlpersonname.Items.Clear();
                            q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
                            }
                            ddlpersonname.SelectedIndex = ddlpersonname.Items.IndexOf(ddlpersonname.Items.FindByValue(dt_update.Rows[0]["SCom_Id"].ToString()));
                            ddlpersonname.Enabled = false;
                        }
                        //ddlpersonname.Enabled = false;
                        quantTextBox.Text = dt_update.Rows[0]["Quant"].ToString();
                        priceTextBox.Text = dt_update.Rows[0]["Price"].ToString();
                        colourTextBox.Text = dt_update.Rows[0]["Colour"].ToString();
                        shapeTextBox.Text = dt_update.Rows[0]["Shape"].ToString();
                        materialTextBox.Text = dt_update.Rows[0]["Material"].ToString();
                        categoryTextBox.Text = dt_update.Rows[0]["Category"].ToString();
                        ddlrimtype.SelectedIndex =ddlrimtype.Items.IndexOf(ddlrimtype.Items.FindByValue( dt_update.Rows[0]["Type"].ToString()));
                        ddlrimtype.Enabled = false;
                    }
                    else if (Request.QueryString["tb"] == "Glass")
                    {
                        frame.Visible = false;
                        contactlens.Visible = false;
                        solution.Visible = false;

                        q_up_gl = "select Prod_Type_Id,SellerType,Sven_Id,SCom_Id,PurchaseDate,ModelName,Quant,Price,Type,Material from Glass_Products where Gla_ProdNo='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_gl, ref dt_update);

                        ddlproducttype.SelectedIndex = ddlproducttype.Items.IndexOf(ddlproducttype.Items.FindByValue(dt_update.Rows[0]["Prod_Type_Id"].ToString()));
                        ddlproducttype.Enabled = false;
                        modelnameTextBox.Text = dt_update.Rows[0]["ModelName"].ToString();
                        purchasedateTextBox.Text = dt_update.Rows[0]["PurchaseDate"].ToString();
                        purchasedateTextBox.Enabled = false;
                        prosellertypeDropDownList.SelectedIndex = prosellertypeDropDownList.Items.IndexOf(prosellertypeDropDownList.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        prosellertypeDropDownList.Enabled = false;
                        if (prosellertypeDropDownList.SelectedItem.Value == "Company")
                        {

                            //ddlpersonname.Items.Clear();
                            q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlcompanyperson.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
                            }

                            ddlcompanyperson.SelectedIndex = ddlcompanyperson.Items.IndexOf(ddlcompanyperson.Items.FindByValue(dt_update.Rows[0]["SVen_Id"].ToString()));
                            ddlcompanyperson.Enabled = false;
                        }
                        else if (prosellertypeDropDownList.SelectedItem.Value == "Vendor")
                        {
                            //ddlpersonname.Items.Clear();
                            q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
                            }
                            ddlpersonname.SelectedIndex = ddlpersonname.Items.IndexOf(ddlpersonname.Items.FindByValue(dt_update.Rows[0]["SCom_Id"].ToString()));
                            ddlpersonname.Enabled = false;
                        }
                        //ddlpersonname.Enabled = false;
                        quantTextBox.Text = dt_update.Rows[0]["Quant"].ToString();
                        priceTextBox.Text = dt_update.Rows[0]["Price"].ToString();
                        materialTextBox.Text = dt_update.Rows[0]["Material"].ToString();
                        glasstypeTextBox.Text = dt_update.Rows[0]["Type"].ToString();
                        glasstypeTextBox.Enabled = false;
                    }
                    else if (Request.QueryString["tb"] == "Contact Lens")
                    {
                        frame.Visible = false;
                        solution.Visible = false;
                        glass.Visible = false;

                        q_up_cl = "select Prod_Type_Id,SellerType,SVen_Id,SCom_Id,PurchaseDate,ModelName,Quant,Price,Type,Power,Material from Contact_Lens_Products where Cl_ProdNo='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_cl, ref dt_update);

                        ddlproducttype.SelectedIndex = ddlproducttype.Items.IndexOf(ddlproducttype.Items.FindByValue(dt_update.Rows[0]["Prod_Type_Id"].ToString()));
                        ddlproducttype.Enabled = false;
                        modelnameTextBox.Text = dt_update.Rows[0]["ModelName"].ToString();
                        purchasedateTextBox.Text = dt_update.Rows[0]["PurchaseDate"].ToString();
                        purchasedateTextBox.Enabled = false;
                        prosellertypeDropDownList.SelectedIndex = prosellertypeDropDownList.Items.IndexOf(prosellertypeDropDownList.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        prosellertypeDropDownList.Enabled = false;
                        if (prosellertypeDropDownList.SelectedItem.Value == "Company")
                        {

                            //ddlpersonname.Items.Clear();
                            q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlcompanyperson.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
                            }

                            ddlcompanyperson.SelectedIndex = ddlcompanyperson.Items.IndexOf(ddlcompanyperson.Items.FindByValue(dt_update.Rows[0]["SVen_Id"].ToString()));
                            ddlcompanyperson.Enabled = false;
                        }
                        else if (prosellertypeDropDownList.SelectedItem.Value == "Vendor")
                        {
                            //ddlpersonname.Items.Clear();
                            q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
                            }
                            ddlpersonname.SelectedIndex = ddlpersonname.Items.IndexOf(ddlpersonname.Items.FindByValue(dt_update.Rows[0]["SCom_Id"].ToString()));
                            ddlpersonname.Enabled = false;
                        }
                        //ddlpersonname.Enabled = false;
                        quantTextBox.Text = dt_update.Rows[0]["Quant"].ToString();
                        priceTextBox.Text = dt_update.Rows[0]["Price"].ToString();
                        materialTextBox.Text = dt_update.Rows[0]["Material"].ToString();
                        typeTextBox.Text = dt_update.Rows[0]["Type"].ToString();
                        powerTextBox.Text = dt_update.Rows[0]["Power"].ToString();
                        powerTextBox.Enabled = false;
                    }
                    else if (Request.QueryString["tb"] == "Solution")
                    {
                        frame.Visible = false;
                        contactlens.Visible = false;
                        glass.Visible = false;

                        q_up_sol = "select Prod_Type_Id,SellerType,SVen_Id,SCom_Id,PurchaseDate,ModelName,QuantLitre,Quant,Price,Material,ExpiryDate,Thumb from Solution_Products where Sol_ProdNo='" + Request.QueryString["up"] + "'";
                        dbo.get(q_up_sol, ref dt_update);
                        ddlproducttype.SelectedIndex = ddlproducttype.Items.IndexOf(ddlproducttype.Items.FindByValue(dt_update.Rows[0]["Prod_Type_Id"].ToString()));
                        ddlproducttype.Enabled = false;
                        modelnameTextBox.Text = dt_update.Rows[0]["ModelName"].ToString();
                        purchasedateTextBox.Text = dt_update.Rows[0]["PurchaseDate"].ToString();
                        purchasedateTextBox.Enabled = false;
                        prosellertypeDropDownList.SelectedIndex = prosellertypeDropDownList.Items.IndexOf(prosellertypeDropDownList.Items.FindByValue(dt_update.Rows[0]["SellerType"].ToString()));
                        prosellertypeDropDownList.Enabled = false;
                        if (prosellertypeDropDownList.SelectedItem.Value == "Company")
                        {

                            //ddlpersonname.Items.Clear();
                            q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlcompanyperson.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
                            }

                            ddlcompanyperson.SelectedIndex = ddlcompanyperson.Items.IndexOf(ddlcompanyperson.Items.FindByValue(dt_update.Rows[0]["SVen_Id"].ToString()));
                            ddlcompanyperson.Enabled = false;
                        }
                        else if (prosellertypeDropDownList.SelectedItem.Value == "Vendor")
                        {
                            //ddlpersonname.Items.Clear();
                            q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
                            dbo.get(q_sel, ref dt_seller);
                            for (int i = 0; i < dt_seller.Rows.Count; i++)
                            {
                                ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
                            }
                            ddlpersonname.SelectedIndex = ddlpersonname.Items.IndexOf(ddlpersonname.Items.FindByValue(dt_update.Rows[0]["SCom_Id"].ToString()));
                            ddlpersonname.Enabled = false;
                        }
                        //ddlpersonname.Enabled = false;
                        quantTextBox.Text = dt_update.Rows[0]["Quant"].ToString();
                        priceTextBox.Text = dt_update.Rows[0]["Price"].ToString();
                        materialTextBox.Text = dt_update.Rows[0]["Material"].ToString();
                        expiryTextBox.Text = dt_update.Rows[0]["ExpiryDate"].ToString();
                        expiryTextBox.Enabled = false;
                        quantlitreTextBox.Text = dt_update.Rows[0]["QuantLitre"].ToString();
                        quantlitreTextBox.Enabled = false;
                        
                    }
                }
            }
        }
    }

    protected void ddlformtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void secomsaveButton_Click(object sender, EventArgs e)
    {
        string q_secom;
        string com_nm, nm, cu_ad, pr_ad, city, email, setype, stype;
        long c_no, m_no;
        int pin;

        stype = ddlsellertype.SelectedValue.ToString();
        //companyTextBox.Text = stype;
        com_nm = companyTextBox.Text.ToString();
        nm = personnameTextBox.Text.ToString();
        cu_ad = curAddTextBox.Text.ToString();
        pr_ad = perAddTextBox.Text.ToString();
        city = cityTextBox.Text.ToString();
        email = emailTextBox.Text.ToString();
        pin = Convert.ToInt32(pincodeTextBox.Text.ToString());
        c_no = Convert.ToInt64(conNoTextBox.Text.ToString());
        m_no = Convert.ToInt64(mobNoTextBox.Text.ToString());
        if (stype == "Company")
        {
            setype = "Company";
            q_secom = "insert into Seller_Companies(SellerType,PersonName,CompanyName,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId) values('" + setype + "','" + nm + "','" + com_nm + "','" + cu_ad + "','" + pr_ad + "','" + city + "','" + pin + "','" + c_no + "','" + m_no + "','" + email + "')";
            dbo.dml(q_secom);
        }
        else if (stype == "Vendor")
        {
            setype = "Vendor";
            q_secom = "insert into Seller_Vendors(SellerType,PersonName,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId) values('" + setype + "','" + nm + "','" + cu_ad + "','" + pr_ad + "','" + city + "','" + pin + "','" + c_no + "','" + m_no + "','" + email + "')";
            dbo.dml(q_secom);
        }
        Response.Redirect("other.aspx");
    }
    protected void prosaveButton_Click(object sender, EventArgs e)
    {
        protype_id = Convert.ToInt32(ddlproducttype.SelectedValue.ToString());
        protype = ddlproducttype.SelectedItem.Text;
        model = modelnameTextBox.Text;
        //person = personnameTextBox.Text;
        material = materialTextBox.Text;
        quant = Convert.ToInt32(quantTextBox.Text);
        price = Convert.ToDouble(priceTextBox.Text);
        pd = Convert.ToDateTime(purchasedateTextBox.Text).Date;
        type = typeTextBox.Text;
        seller = prosellertypeDropDownList.SelectedValue.ToString();

        cat = categoryTextBox.Text;



        //Upload Thumb
        //fuThumbError.Text = "";
        if (fuThumb.FileName != "")
        {
            if (fuThumb.FileName.ToLower().EndsWith(".jpg") || fuThumb.FileName.ToLower().EndsWith(".png") || fuThumb.FileName.ToLower().EndsWith(".jpeg"))
            {
                fnm1 = DateTime.Now.Ticks + "_" + fuThumb.FileName;
                fuThumb.SaveAs(Server.MapPath("uploads") + @"\" + fnm1);
            }
            //else
            //{
            //    fuThumbError.Text = "Provide jpg file for thumb.";
            //}
        }

        if (protype == "Frame")
        {
            colour = colourTextBox.Text;
            shape = shapeTextBox.Text;
            rim = ddlrimtype.SelectedValue.ToString();

            //if (fuThumbError.Text=="")
            //{

            if (seller == "Vendor")
            {
                seller_id = Convert.ToInt32(ddlpersonname.SelectedItem.Value);
                q_proent = "insert into Frame_Products (Prod_Type_Id,SellerType,SVen_Id,PurchaseDate,ModelName,Category,Quant,Price,Type,Colour,Shape,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + cat + "','" + quant + "','" + price + "','" + rim + "','" + colour + "','" + shape + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
            else if (seller == "Company")
            {
                seller_id = Convert.ToInt32(ddlcompanyperson.SelectedItem.Value);
                q_proent = "insert into Frame_Products (Prod_Type_Id,SellerType,SCom_Id,PurchaseDate,ModelName,Category,Quant,Price,Type,Colour,Shape,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + cat + "','" + quant + "','" + price + "','" + rim + "','" + colour + "','" + shape + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
            //}

        }
        else if (protype == "Contact Lens")
        {
            power = Convert.ToDouble(powerTextBox.Text);
            if (seller == "Vendor")
            {
                seller_id = Convert.ToInt32(ddlpersonname.SelectedItem.Value);
                q_proent = "insert into Contact_Lens_Products (Prod_Type_Id,SellerType,SVen_Id,PurchaseDate,ModelName,Type,Power,Quant,Price,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + type + "','" + power + "','" + quant + "','" + price + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
            else if (seller == "Company")
            {
                seller_id = Convert.ToInt32(ddlcompanyperson.SelectedItem.Value);
                q_proent = "insert into Contact_Lens_Products (Prod_Type_Id,SellerType,SCom_Id,PurchaseDate,ModelName,Type,Power,Quant,Price,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + type + "','" + power + "','" + quant + "','" + price + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
        }
        else if (protype == "Solution")
        {
            DateTime ed;
            ed = Convert.ToDateTime(expiryTextBox.Text).Date;
            quan_lit = Convert.ToInt32(quantlitreTextBox.Text);
            if (seller == "Vendor")
            {
                seller_id = Convert.ToInt32(ddlpersonname.SelectedItem.Value);
                q_proent = "insert into Solution_Products (Prod_Type_Id,SellerType,SVen_Id,PurchaseDate,ModelName,QuantLitre,Quant,Price,Material,ExpiryDate,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + quan_lit + "','" + quant + "','" + price + "','" + material + "','" + ed + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
            else if (seller == "Company")
            {
                seller_id = Convert.ToInt32(ddlcompanyperson.SelectedItem.Value);
                q_proent = "insert into Solution_Products (Prod_Type_Id,SellerType,SCom_Id,PurchaseDate,ModelName,QuantLitre,Quant,Price,Material,ExpiryDate,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + quan_lit + "','" + quant + "','" + price + "','" + material + "','" + ed + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
        }
        else if (protype == "Glass")
        {
            gltype = glasstypeTextBox.Text;
            if (seller == "Vendor")
            {
                seller_id = Convert.ToInt32(ddlpersonname.SelectedItem.Value);
                q_proent = "insert into Glass_Products (Prod_Type_Id,SellerType,SVen_Id,PurchaseDate,ModelName,Type,Quant,Price,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + gltype + "','" + quant + "','" + price + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
            else if (seller == "Company")
            {
                seller_id = Convert.ToInt32(ddlcompanyperson.SelectedItem.Value);
                q_proent = "insert into Glass_Products (Prod_Type_Id,SellerType,SCom_Id,PurchaseDate,ModelName,Type,Quant,Price,Material,Thumb) values('" + protype_id + "','" + seller + "','" + seller_id + "','" + pd + "','" + model + "','" + gltype + "','" + quant + "','" + price + "','" + material + "','" + fnm1 + "')";
                dbo.dml(q_proent);
                Response.Redirect("other.aspx");
            }
        }

        //typeTextBox.Text = protype;
    }
    protected void secomupdateButton_Click(object sender, EventArgs e)
    {
        string q_secom;
        string com_nm, nm, cu_ad, pr_ad, city, email, setype, stype;
        long c_no, m_no;
        int pin;

        //stype = ddlsellertype.SelectedValue.ToString();
        //companyTextBox.Text = stype;
        com_nm = companyTextBox.Text.ToString();
        nm = personnameTextBox.Text.ToString();
        cu_ad = curAddTextBox.Text.ToString();
        pr_ad = perAddTextBox.Text.ToString();
        city = cityTextBox.Text.ToString();
        email = emailTextBox.Text.ToString();
        pin = Convert.ToInt32(pincodeTextBox.Text.ToString());
        c_no = Convert.ToInt64(conNoTextBox.Text.ToString());
        m_no = Convert.ToInt64(mobNoTextBox.Text.ToString());

        if (Request.QueryString["tb"] == "Seller Company")
        {
            setype = "Company";
            q_secom = "update Seller_Companies set PersonName='" + nm + "',CurrentAdd='" + cu_ad + "',PermanentAdd='" + pr_ad + "',City='" + city + "',Pincode='" + pin + "',ContactNo='" + c_no + "',MobileNo='" + m_no + "',EmailId='" + email + "' where SCompany_Id='" + Request.QueryString["up"] + "'";
            //q_secom = "insert into Seller_Companies(SellerType,PersonName,CompanyName,CurrentAdd,PermanentAdd,City,Pincode,ContactNo,MobileNo,EmailId) values('" + setype + "','" + nm + "','" + com_nm + "','" + cu_ad + "','" + pr_ad + "','" + city + "','" + pin + "','" + c_no + "','" + m_no + "','" + email + "')";
            Response.Write(q_secom);
            dbo.dml(q_secom);
        }
        else if (Request.QueryString["tb"] == "Seller Vendor")
        {
            setype = "Vendor";
            q_secom = "update Seller_Vendors set PersonName='" + nm + "',CurrentAdd='" + cu_ad + "',PermanentAdd='" + pr_ad + "',City='" + city + "',Pincode='" + pin + "',ContactNo='" + c_no + "',MobileNo='" + m_no + "',EmailId='" + email + "' where SVendor_Id='" + Request.QueryString["up"] + "'";
            Response.Write(q_secom);
            dbo.dml(q_secom);
        }


    }
    protected void proupdateButton_Click(object sender, EventArgs e)
    {
        protype_id = Convert.ToInt32(ddlproducttype.SelectedValue.ToString());
        protype = ddlproducttype.SelectedItem.Text;
        model = modelnameTextBox.Text;
        //person = personnameTextBox.Text;
        material = materialTextBox.Text;
        quant = Convert.ToInt32(quantTextBox.Text);
        price = Convert.ToDouble(priceTextBox.Text);
        pd = Convert.ToDateTime(purchasedateTextBox.Text).Date;
        type = typeTextBox.Text;
        seller = prosellertypeDropDownList.SelectedValue.ToString();
        if (seller == "Vendor")
            seller_id = Convert.ToInt32(ddlpersonname.SelectedItem.Value);
        else if (seller == "Company")
            seller_id = Convert.ToInt32(ddlcompanyperson.SelectedItem.Value);
        cat = categoryTextBox.Text;
        gltype = glasstypeTextBox.Text;
        rim = ddlrimtype.SelectedItem.Value;



        if (Request.QueryString["tb"] == "Frame")
        {
            colour = colourTextBox.Text;
            shape = shapeTextBox.Text;
            rim = ddlrimtype.SelectedValue.ToString();

            q_proent = "update Frame_Products set Type='"+rim+"', ModelName='" + model + "',Category='" + cat + "',Quant='" + quant + "',Price='" + price + "',Colour='" + colour + "',Shape='" + shape + "',Material='" + material + "' where Fr_ProdNo='" + Request.QueryString["up"] + "'";
            Response.Write(q_proent);
            dbo.dml(q_proent);
            Response.Redirect("other.aspx");

        }
        else if (Request.QueryString["tb"] == "Contact Lens")
        {
            //power = Convert.ToDouble(powerTextBox.Text);
            q_proent = "update Contact_Lens_Products set ModelName='" + model + "',type='" + type + "',Quant='" + quant + "',Price='" + price + "',Material='" + material + "' where Cl_ProdNo='" + Request.QueryString["up"] + "'";
            Response.Write(q_proent);
            dbo.dml(q_proent);
            Response.Redirect("other.aspx");
        }
        else if (Request.QueryString["tb"] == "Solution")
        {
            //DateTime ed;
            quan_lit = Convert.ToInt32(quantlitreTextBox.Text);
            q_proent = "update Solution_Products set ModelName='" + model + "',QuantLitre='" + quan_lit + "',Quant='" + quant + "',Price='" + price + "',Material='" + material + "' where Sol_ProdNo='" + Request.QueryString["up"] + "'";
            Response.Write(q_proent);
            dbo.dml(q_proent);
            Response.Redirect("other.aspx");
        }
        else if (Request.QueryString["tb"] == "Glass")
        {
            q_proent = "update Glass_Products set Type='"+gltype+"', ModelName='" + model + "',Quant='" + quant + "',Price='" + price + "',Material='" + material + "' where Gla_ProdNo='" + Request.QueryString["up"] + "'";
            Response.Write(q_proent);
            dbo.dml(q_proent);
            Response.Redirect("other.aspx");
        }
    }

    //protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    Response.Write("hello");
    //    DataTable dt_seller = new DataTable();
    //    string prosel = RadioButtonList1.SelectedValue.ToString();
    //    string q_sel;
    //    if (Page.IsPostBack)
    //    {
    //        if (RadioButtonList1.AutoPostBack)
    //        {
    //            if (prosel == "Company")
    //            {
    //                // ddlpersonname.Items.Clear();
    //                q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
    //                dbo.get(q_sel, ref dt_seller);
    //                for (int i = 0; i < dt_seller.Rows.Count; i++)
    //                {
    //                    ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["CompanyName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
    //                }
    //            }
    //            else if (prosel == "Vendor")
    //            {
    //                //ddlpersonname.Items.Clear();
    //                q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
    //                dbo.get(q_sel, ref dt_seller);
    //                for (int i = 0; i < dt_seller.Rows.Count; i++)
    //                {
    //                    ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
    //                }
    //            }

    //        }

    //    }
    //}

    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Response.Write("hello");
    //    DataTable dt_seller = new DataTable();
    //    string prosel =RadioButtonList1.SelectedValue;
    //    string q_sel;
    //    if (Page.IsPostBack)
    //    {
    //        if (RadioButtonList1.AutoPostBack)
    //        {
    //            if (prosel == "0")
    //            {
    //                // ddlpersonname.Items.Clear();
    //                q_sel = "select SCompany_Id,CompanyName from Seller_Companies";
    //                dbo.get(q_sel, ref dt_seller);
    //                for (int i = 0; i < dt_seller.Rows.Count; i++)
    //                {
    //                    ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["CompanyName"].ToString(), dt_seller.Rows[i]["SCompany_Id"].ToString()));
    //                }
    //            }
    //            else if (prosel == "1")
    //            {
    //                //ddlpersonname.Items.Clear();
    //                q_sel = "select SVendor_Id,PersonName from Seller_Vendors";
    //                dbo.get(q_sel, ref dt_seller);
    //                for (int i = 0; i < dt_seller.Rows.Count; i++)
    //                {
    //                    ddlpersonname.Items.Add(new ListItem(dt_seller.Rows[i]["PersonName"].ToString(), dt_seller.Rows[i]["SVendor_Id"].ToString()));
    //                }
    //            }

    //        }

    //    }
    //}
}
