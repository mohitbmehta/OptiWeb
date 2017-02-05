using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class search : System.Web.UI.Page
{
    public DataTable dt_videos = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string q_videos = "select * from Contact_Lens_Products,Frame_Products,Glass_Products,Solution_Products ";

        if (Request.QueryString["s"] != null)
        {
            string[] words = Request.QueryString["s"].Split(' ');

            q_videos += " and ( ";

            foreach (string w in words)
            {
                q_videos += "ModelName like '%" + w + "%' OR ";
            }
            //Response.Write(q_videos);


            q_videos = q_videos.Substring(0, q_videos.Length - 3) + ")";

        }
        //Response.Write(q_videos);

        dbo.get(q_videos, ref dt_videos);
        
    }
}