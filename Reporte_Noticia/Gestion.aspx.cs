using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Carnet_Gestion : System.Web.UI.Page
{
   public  configurationFiles f = new configurationFiles();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.openxmlformatsofficedocument.wordprocessingml.documet";
        Response.AddHeader("content-disposition", "attachment; filename=Carnet.doc");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:word\">");
        Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.Charset = "";
        EnableViewState = false;
        /*System.IO.StringWriter writer = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter html = new System.Web.UI.HtmlTextWriter(writer);
        content.RenderControl(html);*/
        Response.Write(clsFunciones.carnet);
        Response.End();
    }
}