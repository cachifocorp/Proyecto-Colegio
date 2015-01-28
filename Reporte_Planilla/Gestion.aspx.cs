using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Planilla_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string style = "<style>body {margin-top: 1px; margin-right: 1px; margin-bottom: 1px; margin-left: 1px;}</style>";
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.openxmlformatsofficedocument.wordprocessingml.documet";
        Response.AddHeader("content-disposition", "attachment; filename=Planilla.doc");
        Response.AddHeader("Expires", "0");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");

        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.Charset = "";
        EnableViewState = false;
        System.IO.StringWriter writer = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter html = new System.Web.UI.HtmlTextWriter(writer);
        Response.Write(style);
        content.RenderControl(html);
        Response.Write(writer);
        Response.End();
    }
}