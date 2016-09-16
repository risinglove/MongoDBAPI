using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MongoDB_WEB
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BarCode.GenQRCode("http://www.BAIDU.com");
            using (var webClient = new System.Net.WebClient())
            {
                string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Code.ashx";
                webClient.DownloadFile("http://www.hao123.com", "F:/2.png");
            }
        }
    }
}