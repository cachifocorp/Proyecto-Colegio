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
using System.Drawing;

public partial class Usuario_Docente_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnGuardar.UniqueID;
            ddlDocumento_Id_Tipo.Focus();
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Docente objDocente                                  = new Docente();
            String NombreImagen                                 = "";

            if (fluFoto.HasFile)
            {
                clsFunciones objFunciones = new clsFunciones();
                Object[] rimagen = objFunciones.redimencionar(fluFoto, 200, 200, txtDocumento_Numero.Text);
                Bitmap newImagen = (Bitmap)rimagen[0];
                NombreImagen = (String)rimagen[1];
                newImagen.Save(Server.MapPath("~/img/fotos/" + NombreImagen));
            }
            OperacionDocente objOperDocente                     = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objDocente.documento_id_tipo                        = int.Parse(ddlDocumento_Id_Tipo.SelectedValue.ToString());
            objDocente.documento_numero                         = int.Parse(txtDocumento_Numero.Text);
            objDocente.nombres                                  = txtDescripcion.Text;
            objDocente.apellidos                                = txtApellidos.Text;
            objDocente.fecha_nacimiento                         = DateTime.Parse(Request.Form[txtFecha_Nacimiento.UniqueID]);
            objDocente.id_grupo_sanguineo                       = int.Parse(ddlGrupo_Sanguineo.SelectedValue.ToString());
            objDocente.id_genero                                = int.Parse(ddlGenero.SelectedValue.ToString());
            objDocente.id_grupo_sanguineo                       = int.Parse(ddlGrupo_Sanguineo.SelectedValue.ToString());
            objDocente.email                                    = txtEmail.Text;
            objDocente.direccion_numero                         = txtDireccion_Numero.Text;
            objDocente.direccion_id_municipio                   = int.Parse(ddlMunicipio.SelectedValue.ToString());
            objDocente.telefonos                                = txtTelefonos.Text;
            
            objDocente.id_usuario                               = int.Parse(Session["id_usuario"].ToString());
            string accion                                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                if (fluFoto.FileName.ToString() == "")
                {
                    objDocente.foto = "~/img/fotos/usuario.jpg";
                }
                else
                {
                    objDocente.foto = "~/img/fotos/" + NombreImagen;
                }
                objOperDocente.InsertarDocente(objDocente);
                Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                if (imgDocente.DescriptionUrl == "/Academico/img/fotos/usuario.jpg")
                {
                    if (fluFoto.FileName.ToString() == "")
                    {
                        objDocente.foto = "~/img/fotos/usuario.jpg";
                    }
                    else
                    {
                        objDocente.foto = "~/img/fotos/" + NombreImagen;
                    }
                }
                else
                { 
                    if (fluFoto.FileName.ToString() == "")
                    {
                        objDocente.foto = imgDocente.DescriptionUrl.Replace("/Academico","~");
                    }
                    else
                    {
                        objDocente.foto = "~/img/fotos/" + NombreImagen;
                    }
                }
                objDocente.id                                   = int.Parse(Page.RouteData.Values["id"].ToString());
                objOperDocente.ActualizarDocente(objDocente);
                Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Busqueda", Accion = "Edito" });
            }
            //clsFunciones.CleanControl(frmDocente.Controls);
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Busqueda", Accion = "Cancelo" });
    }
    public void cargar()
    {
        try
        {
            DataView dtv_Municipio                              = ((DataTable)Session["listado"]).DefaultView;
            dtv_Municipio.RowFilter                             = "id_tipo_listado=1";
            this.enlazarCombo(dtv_Municipio, ddlDocumento_Id_Tipo);
            dtv_Municipio.RowFilter                             = "id_tipo_listado=2";
            this.enlazarCombo(dtv_Municipio, ddlGenero);
            dtv_Municipio.RowFilter                             = "id_tipo_listado=4";
            this.enlazarCombo(dtv_Municipio, ddlGrupo_Sanguineo);
            Departamento objDepartamento                        = new Departamento();
            OperacionDepartamento objOperDepartamento           = new OperacionDepartamento(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            this.enlazarCombo(objOperDepartamento.ConsultarDepartamento(objDepartamento), ddlDepartamento);
            Municipio objMunicipio                              = new Municipio();
            OperacionMunicipio objOperMunicipio                 = new OperacionMunicipio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.municipio                              = objOperMunicipio.ConsultarMunicipio(objMunicipio);
            Docente objDocente                                  = new Docente();
            OperacionDocente objOperDocente                     = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Docente                                = new GridView();
            string accion                                       = Page.RouteData.Values["accion"].ToString();
            if (accion.Equals("Edita"))
            {
                this.enlazarCombo(clsFunciones.municipio, ddlMunicipio);
                string id                                       = Page.RouteData.Values["id"].ToString();
                objDocente.id                                   = int.Parse(id);
                tbl_Docente.DataSource                          = objOperDocente.ConsultarDocente(objDocente);
                tbl_Docente.DataBind();
                ddlDocumento_Id_Tipo.SelectedValue              = tbl_Docente.Rows[0].Cells[1].Text;
                txtDocumento_Numero.Text                        = tbl_Docente.Rows[0].Cells[2].Text;
                txtDescripcion.Text                             = HttpUtility.HtmlDecode(tbl_Docente.Rows[0].Cells[3].Text);
                txtApellidos.Text                               = HttpUtility.HtmlDecode(tbl_Docente.Rows[0].Cells[4].Text);
                txtFecha_Nacimiento.Text                        = DateTime.Parse(tbl_Docente.Rows[0].Cells[5].Text).ToShortDateString();
                ddlGenero.SelectedValue                         = tbl_Docente.Rows[0].Cells[6].Text;
                ddlGrupo_Sanguineo.SelectedValue                = tbl_Docente.Rows[0].Cells[7].Text;
                txtEmail.Text                                   = tbl_Docente.Rows[0].Cells[8].Text;
                txtDireccion_Numero.Text                        = tbl_Docente.Rows[0].Cells[9].Text;
                ddlMunicipio.SelectedValue                      = tbl_Docente.Rows[0].Cells[10].Text;
                ddlDepartamento.SelectedValue                   = tbl_Docente.Rows[0].Cells[11].Text;
                txtTelefonos.Text                               = tbl_Docente.Rows[0].Cells[12].Text;
                imgDocente.ImageUrl                             = tbl_Docente.Rows[0].Cells[13].Text;
                imgDocente.DescriptionUrl                       = tbl_Docente.Rows[0].Cells[13].Text;
                this.seleccionar_Departamento(ddlDepartamento, ddlMunicipio);
            }
        }
        catch (Exception) { }
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }

    public void enlazarCombo(DataView dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }
        
    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlDepartamento,ddlMunicipio);
    }

    public void seleccionar_Municipio(DropDownList ddlDepartamento, DropDownList ddlMunicipio)
    {
        if (ddlDepartamento.SelectedValue != null)
        {
            ddlMunicipio.Items.Clear();
            ListItem item                       = new ListItem();
            item.Value                          = "0";
            item.Text                           = "--- SELECCIONE UNO ---";
            ddlMunicipio.Items.Add(item);
            DataView dtv_Municipio              = clsFunciones.municipio.DefaultView;
            dtv_Municipio.RowFilter             = "id_departamento=" + ddlDepartamento.SelectedValue;
            ddlMunicipio.DataSource             = dtv_Municipio;
            ddlMunicipio.DataValueField         = "id";
            ddlMunicipio.DataTextField          = "descripcion";
            ddlMunicipio.DataBind();
        }
    }

    public void seleccionar_Departamento(DropDownList ddlDepartamento, DropDownList ddlMunicipio)
    {
        if (ddlMunicipio.SelectedValue != null)
        {
            ddlDepartamento.SelectedValue = clsFunciones.municipio.Select("id=" + ddlMunicipio.SelectedValue)[0][2].ToString();
        }
    }

}