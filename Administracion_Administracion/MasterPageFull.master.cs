using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Administracion_MasterPageFull : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.HeaderEncoding = System.Text.Encoding.Default;
        //String pagina = Request.RawUrl;
        if (Session["id_usuario"].ToString() == "" && Session["menu"] is DataTable && Session["usuario"].ToString() == "" &&
            Session["anioEscolar"].ToString() == "" && Session["permisosmenu"].ToString() == ""
            && Session["listado"].ToString() == "" && Session["id_usuario_tipo"].ToString() == "")
        {
            Response.RedirectToRoutePermanent("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Login", Accion = "Logout" });
        }
        Response.AppendHeader("Cache-Control", "no-store");
    }
}
