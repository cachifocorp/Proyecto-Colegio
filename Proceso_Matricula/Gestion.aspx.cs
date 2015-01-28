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

public partial class Usuario_Usuario_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardar.UniqueID;
        if(!IsPostBack) {
            txtEstudiante.Focus();
            this.cargar();
        }else {
            var ctrlName        = Request.Params[Page.postEventSourceID];
            var args            = Request.Params[Page.postEventArgumentID];
            HandleCustomPostbackEvent(ctrlName, args);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtEstudiante, "OnBlur");
        txtEstudiante.Attributes.Add("onblur", onBlurScript);
    }

    private void HandleCustomPostbackEvent(string ctrlName, string args)
    {
        if (ctrlName == txtEstudiante.UniqueID && args == "OnBlur")
        {
            if (txtEstudiante.Text != "") {
                Estudiante objEstudiante                    = new Estudiante();
                OperacionEstudiante objOperEstudiante       = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objEstudiante.documento_numero              = Convert.ToInt64(txtEstudiante.Text);
                GridView tbl_Estudiante                     = new GridView();
                tbl_Estudiante.DataSource                   = objOperEstudiante.ConsultarEstudiante(objEstudiante);
                tbl_Estudiante.DataBind();
                if (tbl_Estudiante.Rows.Count > 0)
                {
                    txtNombres.Text                         = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[4].Text) + " " +  HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[5].Text);
                    txtApellidos.Text                       = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[6].Text) + " " + HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[7].Text);
                }
            }
        }
    }

    public void cargar (){
        try {
            Grado objGrado                              = new Grado();
            OperacionGrado objOperGrado                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado),ddlGrado);
            Salon objSalon                              = new Salon();
            OperacionSalon objOperSalon                 = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.salon                          = objOperSalon.ConsultarSalon(objSalon);
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita")) {
                Matricula objMatricula                  = new Matricula();
                OperacionMatricula objOperMatricula     = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                clsFunciones.enlazarCombo (clsFunciones.salon,ddlSalon);
                string id                               = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objMatricula.id                         = int.Parse(id);
                GridView tbl_Matricula                  = new GridView();
                tbl_Matricula.DataSource                = objOperMatricula.ConsultarMatricula(objMatricula);
                tbl_Matricula.DataBind();
                ddlSalon.SelectedValue                  = tbl_Matricula.Rows[0].Cells[2].Text;
                txtEstudiante.Text                      = tbl_Matricula.Rows[0].Cells[5].Text;
                txtNombres.Text                         = HttpUtility.HtmlDecode(tbl_Matricula.Rows[0].Cells[6].Text);
                txtApellidos.Text                         = HttpUtility.HtmlDecode(tbl_Matricula.Rows[0].Cells[7].Text);
                this.seleccionar_Grado(ddlGrado,ddlSalon);
            }
        }catch (Exception) {}
    }

    public void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
        
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Matricula objMatricula                          = new Matricula();
        OperacionMatricula objOperMatricula             = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMatricula.id_estudiante                      = Convert.ToInt64(txtEstudiante.Text);
        objMatricula.id_salon                           = int.Parse(ddlSalon.SelectedValue.ToString());
        int validar                                     = objOperMatricula.ConsultarMatricula(objMatricula).Rows.Count;
        objMatricula.id_usuario                         = int.Parse(Session["id_usuario"].ToString());
        string accion                                   = Page.RouteData.Values["Accion"].ToString();
        if (accion.Equals("Agregar")){
            if (validar == 0)
            {
                objOperMatricula.InsertarMatricula(objMatricula);
                Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Busqueda", Accion = "Agrego" });    
            }else {
                ShowNotification("Estudiante Matriculado","El estudiante ya se encuentra matricula en este curso","info");
            }
        }else {
            string id                                   = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
            objMatricula.id                             = int.Parse(id);
            objOperMatricula.ActualizarMatricula(objMatricula);
            Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Busqueda", Accion = "Edito" });
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void seleccionar_Salon(DropDownList ddlGrado, DropDownList ddlCurso)
    {
        if (ddlGrado.SelectedValue != null)
        {
            ddlCurso.Items.Clear();
            ListItem item           = new ListItem();
            item.Value              = "0";
            item.Text               = "--- SELECCIONE UNO ---";
            ddlCurso.Items.Add(item);
            DataView dtv_Grado      = clsFunciones.salon.DefaultView;
            dtv_Grado.RowFilter     = "id_grado=" + ddlGrado.SelectedValue;
            ddlCurso.DataSource     = dtv_Grado;
            ddlCurso.DataValueField = "id";
            ddlCurso.DataTextField  = "descripcion";
            ddlCurso.DataBind();
        }
    }

    public void seleccionar_Grado(DropDownList ddlGrado, DropDownList ddlSalon)
    {
        if (ddlSalon.SelectedValue != null)
        {
            ddlGrado.SelectedValue = clsFunciones.salon.Select("id=" + ddlSalon.SelectedValue)[0][5].ToString();
        }
    }
    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        seleccionar_Salon(ddlGrado,ddlSalon);
    }
    
}