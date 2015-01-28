using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Default : System.Web.UI.Page
{
    public maestro_publicacion ma = new maestro_publicacion();
    protected void Page_Load(object sender, EventArgs e)
    {

        clsFunciones.nombre_padre = null;
        clsFunciones.nombre_padre = null;
        getpubs();

    }


    private void getpubs() {
        if (Session["id_usuario_tipo"].ToString() == "5")
        {
           pubs.InnerHtml = ma.getPublicaciones(5, 1); 
        }
        if (Session["id_usuario_tipo"].ToString() == "2")
        {
            pubsdoc.InnerHtml = ma.getPublicaciones(2, 1);
        }
        
        
    }
}