using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class feedback : System.Web.UI.Page
{
    String name, sub, msg, q_fb;
    DateTime date;
    long conno;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        name = Convert.ToString(this.nameTextBox.Text);
        sub = Convert.ToString(this.subjectTextBox.Text);
        conno = Convert.ToInt64(this.contactnoTextBox.Text);
        date = Convert.ToDateTime(this.todaydate.Text).Date;
        msg = Convert.ToString(this.messageTextBox.Text);
        q_fb = "INSERT INTO Feedback VALUES('" + name + "','" + date + "','" + sub + "','" + msg + "','" + conno + "')";
        dbo.dml(q_fb);
        Response.Redirect("default.aspx");
    }
}