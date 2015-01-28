<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse la aplicación
        RegistroRoutes();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación
        

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error sin procesar

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse una nueva sesión
        Session["id_usuario"] = "";
        Session["usuario"] = "";
        Session["permisosmenu"] = "";
        Session["anioEscolar"] = "";
        Session["listado"] = "";
        Session["id_usuario_tipo"] = "";
        Session["menu"] = "";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.
        Session["id_usuario"] = "";
        Session["usuario"] = "";
        Session["permisosmenu"] = "";
        Session["anioEscolar"] = "";
        Session["listado"] = "";
        Session["id_usuario_tipo"] = "";
        Session["menu"] = "";
    }

    public static void RegistroRoutes()
    {
        RouteTable.Routes.MapPageRoute("General", "{Modulo}/{Entidad}/{Pagina}/{Accion}/{*Id}", "~/{Modulo}_{Entidad}/{Pagina}.aspx", true,
            new RouteValueDictionary { { "Entidad", string.Empty }, { "Accion", string.Empty } });
    }
     
</script>
