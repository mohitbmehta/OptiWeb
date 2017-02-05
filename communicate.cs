using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;

public class Communicate
{
    public static void send_sms(String To, String Msg)
    {
        string strUrl = "http://www.aisomex.net/smsapi/send_sms.php?user=trainee&password=999&to=" + HttpContext.Current.Server.UrlEncode(To) + "&msg=" + HttpContext.Current.Server.UrlEncode(Msg);
        WebRequest request = HttpWebRequest.Create(strUrl);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream s = (Stream)response.GetResponseStream();
        StreamReader readStream = new StreamReader(s);
        string dataString = readStream.ReadToEnd();

        //HttpContext.Current.Response.Write(dataString);

        response.Close();
        s.Close();
        readStream.Close();
    }

    //public static void send_email(String To, String Sub, String Msg)
    //{
    //    String From_Email = "aisomex.trainee@gmail.com";
    //    String From_Email_Password = "Aisomex@123";

    //    SmtpClient client = new SmtpClient();
    //    client.Host = "smtp.gmail.com";
    //    client.Port = 587;
    //    client.UseDefaultCredentials = false;
    //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //    client.EnableSsl = true;
    //    client.Credentials = new NetworkCredential(From_Email, From_Email_Password);

    //    MailMessage mm = new MailMessage(From_Email, To, Sub, Msg);
    //    mm.BodyEncoding = UTF8Encoding.UTF8;
    //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

    //    client.Send(mm);
    //}
}