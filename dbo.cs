using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class dbo
{
    private static string con_str = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|db_OptiWeb.mdf;Integrated Security=True";

    public static void dml(string q)
    {
        SqlConnection cn = new SqlConnection(con_str);
        cn.Open();

        SqlCommand cmd = new SqlCommand(q, cn);
        cmd.ExecuteNonQuery();

        cn.Close();
    }

    public static void get(string q, ref DataTable dt)
    {
        SqlDataAdapter da = new SqlDataAdapter(q, con_str);
        da.Fill(dt);
    }
}