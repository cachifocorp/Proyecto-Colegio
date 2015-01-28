using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Administracion_Noticia : System.Web.UI.Page
{
    maestro_publicacion m = new maestro_publicacion();
    protected void Page_Load(object sender, EventArgs e)
    {
       news.InnerHtml= m.getPublicacion(Convert.ToInt32(Page.RouteData.Values["id"]));
    }
}