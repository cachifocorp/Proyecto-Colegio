using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;
using System.Data;

public partial class Administracion_Administracion_MasterPage : System.Web.UI.MasterPage
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
        this.mostrar_Mensajes(); 
        this.permisos();
        Response.AppendHeader("Cache-Control", "no-store");
    }

    public void permisos()
    {
        try
        {
            DataView dtv_Permiso = new DataView();
            dtv_Permiso = ((DataTable)Session["menu"]).DefaultView;
            dtv_Permiso.RowFilter = "descripcion = '" + Page.RouteData.Values["Entidad"].ToString().Replace("_", " ").Replace("ion", "ión") + "'";
            GridView tbl_Permisos = new GridView();
            tbl_Permisos.DataSource = dtv_Permiso;
            tbl_Permisos.DataBind();
            if (tbl_Permisos.Rows.Count == 0)
            {
               if (Page.RouteData.Values["Entidad"].ToString() != "Administracion" && Page.RouteData.Values["Pagina"] != "Default") {
                   if (!IsPostBack)
                   {
                       Response.RedirectToRoutePermanent("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default" });
                   }
                }
            }


        }
        catch (Exception) { }

    }

    public string menu()
    {
        string menus = "";
        if (Session["id_usuario"] != null && Session["menu"] is DataTable)
        {
            DataView dtv_Menu               = ((DataTable)Session["menu"]).DefaultView;
            GridView tbl_Padres             = new GridView();
            dtv_Menu.RowFilter              = "id_padre=1";
            tbl_Padres.DataSource           = dtv_Menu;
            tbl_Padres.DataBind();
            foreach (GridViewRow padre in tbl_Padres.Rows)
            {
                GridView tbl_Hijos          = new GridView();
                dtv_Menu.RowFilter          = "id_padre=" + padre.Cells[0].Text;
                tbl_Hijos.DataSource        = dtv_Menu;
                tbl_Hijos.DataBind();
                dtv_Menu.RowFilter = null;
                string nombre_padre = "";
                string nombre_hijo = "";
                try
                {
                    nombre_padre = Page.RouteData.Values["Modulo"].ToString().Replace("_", " ").Replace("cion", "ción");
                    nombre_hijo = Page.RouteData.Values["Entidad"].ToString().Replace("_", " ").Replace("cion", "ción");
                }
                catch { }
                if (tbl_Hijos.Rows.Count > 0)
                {
                    if (HttpUtility.HtmlDecode(padre.Cells[1].Text).Equals(nombre_padre))
                    {
                        menus += "<li class='dropdown-list active'>";
                    }
                    else
                    {
                        menus += "<li class='dropdown-list'>";
                    }
                    menus += "<a href='#' class='dropdown-toggle' data-toggle='dropdown-list'>" + padre.Cells[1].Text + "</a>";

                }
                else
                {
                    menus += "<li><a href=../" + padre.Cells[2].Text + "/Busqueda/Busco>" + padre.Cells[1].Text + "</a></li>";
                }

                foreach (GridViewRow hijo in tbl_Hijos.Rows)
                {
                    menus += "<ul class='dropdown-menu'>";

                    if (HttpUtility.HtmlDecode(hijo.Cells[1].Text).Equals(nombre_hijo))
                    {
                        menus += "<li class='active'><a title=" + hijo.Cells[1].Text + " href=" + ResolveUrl(".." + hijo.Cells[2].Text) + "/Busqueda/Busco>" + hijo.Cells[1].Text + "</a></li>";
                    }
                    else
                    {
                        menus += "<li><a title=" + hijo.Cells[1].Text + " href=" + ResolveUrl(".." + hijo.Cells[2].Text) + "/Busqueda/Busco>" + hijo.Cells[1].Text + "</a></li>";
                    }

                    menus += "</ul>";
                }

                if (int.Parse(padre.Cells[3].Text) == 1)
                {
                    menus += "</li>";
                }
            }

        }
        else
        {
            Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Login", Accion = "Logout" });
        }

        return menus;
    }

    public void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    public void mostrar_Mensajes()
    {
        try
        {
            string accion = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agrego"))
            {
                this.ShowNotification("Agregar", Resources.Mensaje.msjAgregar, "success");
            }
            else if (accion.Equals("Edito"))
            {
                this.ShowNotification("Editar", Resources.Mensaje.msjEditar, "info");
            }
            else if (accion.Equals("Cancelo"))
            {
                this.ShowNotification("Cancelar", Resources.Mensaje.msjCancelar, "error");
            }
            else if (accion.Equals("Elimino"))
            {
                this.ShowNotification("Eliminar", Resources.Mensaje.msjEliminar, "info");
            }
        }
        catch { }
    }
}
