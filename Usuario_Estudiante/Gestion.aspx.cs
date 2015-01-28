using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;
using System.Drawing;
using System.IO;

public partial class Usuario_Estudiante_Gestion : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardarPersonal.UniqueID;
        if (!IsPostBack)
        {
            this.cargar();
        }
    }

    protected void btnGuardarPersonal_Click(object sender, EventArgs e)
    {
        try {



            /*Estudiante*/
            Estudiante objEstudiante                                = new Estudiante();
            String NombreImagen                                     = "";
            if (fluFoto.HasFile)
            {
                clsFunciones objFunciones                           = new clsFunciones();
                Object[] rimagen                                    = objFunciones.redimencionar(fluFoto, 200, 200,txtDocumento_Numero.Text);
                Bitmap newImagen                                    = (Bitmap)rimagen[0];
                NombreImagen                                        = (String)rimagen[1];
                newImagen.Save(Server.MapPath("~/img/fotos/" + NombreImagen));
            }
            OperacionEstudiante objOperEstudiante                   = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (int.Parse(ddlDocumento_Id_Tipo.SelectedValue.ToString()) == 0)
            {
                objEstudiante.documento_id_tipo                     = 4;
            }
            else {
                objEstudiante.documento_id_tipo                     = int.Parse(ddlDocumento_Id_Tipo.SelectedValue.ToString());
            }
            objEstudiante.documento_numero = Convert.ToInt64(txtDocumento_Numero.Text);
            if (int.Parse(ddlDocumento_Municipio.SelectedValue.ToString()) == 0)
            {
                objEstudiante.documento_id_municipio_expedicion     = 2;
            }
            else {
                objEstudiante.documento_id_municipio_expedicion     = int.Parse(ddlDocumento_Municipio.SelectedValue.ToString());
            }
            objEstudiante.nombre_1                              = txtNombre_1.Text;
            objEstudiante.nombre_2                              = txtNombre_2.Text;
            objEstudiante.apellido_1                            = txtApellido_1.Text;
            objEstudiante.apellido_2                            = txtApellido_2.Text;
            if (int.Parse(ddlGenero.SelectedValue.ToString()) == 0)
            {
                objEstudiante.id_genero                         = 6;

            }
            else {
                objEstudiante.id_genero                         = int.Parse(ddlGenero.SelectedValue.ToString());
            
            }
            if (txtEmail.Text == "")
            {
                objEstudiante.email                             = "@";

            }
            else {
                objEstudiante.email                             = txtEmail.Text;
            
            }
            if (Request.Form[txtNacimiento_Fecha.UniqueID].ToString() == "")
            {
                objEstudiante.nacimiento_fecha                  = DateTime.Parse(DateTime.Now.ToShortDateString());
            }
            else {
                objEstudiante.nacimiento_fecha                  = DateTime.Parse(Request.Form[txtNacimiento_Fecha.UniqueID].ToString());
            }
            if (int.Parse(ddlNacimiento_Municipio.SelectedValue.ToString()) == 0)
            {
                objEstudiante.nacimiento_id_municipio           = 912;

            }
            else {
                objEstudiante.nacimiento_id_municipio           = int.Parse(ddlNacimiento_Municipio.SelectedValue.ToString());
            
            }
            if (txtDireccion_Numero.Text == "")
            {
                objEstudiante.direccion_numero                  = "CRA";

            }
            else {
                objEstudiante.direccion_numero                  = txtDireccion_Numero.Text;
            
            }
            if (txtDireccion_Barrio.Text == "")
            {
                objEstudiante.direccion_barrio                  = "BAR";
            }
            else
            {
                objEstudiante.direccion_barrio                  = txtDireccion_Barrio.Text;
            }
            if (int.Parse(ddlDireccion_Municipio.SelectedValue.ToString()) == 0)
            {
                objEstudiante.direccion_id_municipio            = 912;

            }
            else
            {
                objEstudiante.direccion_id_municipio            = int.Parse(ddlDireccion_Municipio.SelectedValue.ToString());
            }

            if (int.Parse(ddlZona.SelectedValue.ToString()) == 0)
            {
                objEstudiante.zona                              = 7;
            }
            else
            {
                objEstudiante.zona                              = int.Parse(ddlZona.SelectedValue.ToString());
            }

            if (txtTelefono_Fijo.Text == "")
            {
                objEstudiante.telefono_fijo                     = 9999;

            }
            else
            {
                objEstudiante.telefono_fijo                     = int.Parse(txtTelefono_Fijo.Text);
            }

            if (txtTelefono_Celular.Text == "")
            {
                objEstudiante.telefono_celular                  = 9999;

            }
            else
            {
                objEstudiante.telefono_celular                  = int.Parse(txtTelefono_Celular.Text);
            }
            
            objEstudiante.id_usuario                            = int.Parse(Session["id_usuario"].ToString());

            if (txtNumero_Documento_Acudiente_1.Text == "")
            {
                objEstudiante.id_acudiente_1                    = 1;
                txtNumero_Documento_Acudiente_1.Text            = 1.ToString();
            }
            else
            {
                objEstudiante.id_acudiente_1                    = int.Parse(txtNumero_Documento_Acudiente_1.Text);
            }

            if (txtNumero_Documento_Acudiente_2.Text == "")
            {
                objEstudiante.id_acudiente_2                    = 1;
                txtNumero_Documento_Acudiente_2.Text = 1.ToString();
            }
            else
            {
                objEstudiante.id_acudiente_2                    = int.Parse(txtNumero_Documento_Acudiente_2.Text);
            }

            if (int.Parse(ddlEps.SelectedValue.ToString()) == 0)
            {
                objEstudiante.id_eps                            = 972;

            }
            else
            {
                objEstudiante.id_eps                            = int.Parse(ddlEps.SelectedValue.ToString());
            }

            if (int.Parse(ddlGrupo_Sanguineo.SelectedValue.ToString()) == 0)
            {
                objEstudiante.id_grupo_sanguineo                = 9;

            }
            else {
                objEstudiante.id_grupo_sanguineo                = int.Parse(ddlGrupo_Sanguineo.SelectedValue.ToString());
            
            }

            if (txtSisben_Numero.Text == "")
            {
                objEstudiante.sisben_numero                     = 1;

            }
            else {
                objEstudiante.sisben_numero                     = int.Parse(txtSisben_Numero.Text);
            
            }

            if (int.Parse(ddlSisben_Nivel.SelectedValue.ToString()) == 0)
            {
                objEstudiante.sisben_nivel                      = 1;

            }
            else {
                objEstudiante.sisben_nivel                      = int.Parse(ddlSisben_Nivel.SelectedValue.ToString());
            }

            if (int.Parse(ddlEstrato.SelectedValue.ToString()) == 0)
            {
                objEstudiante.estrato                           = 1;

            }
            else
            {
                objEstudiante.estrato                           = int.Parse(ddlEstrato.SelectedValue.ToString());
            }
            string accion                                       = Page.RouteData.Values["Accion"].ToString();

            /*Acudiente 1*/
            Acudiente objAcudiente_1                                = new Acudiente();
            OperacionAcudiente objOperAcudiente                     = new OperacionAcudiente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);


            if (txtNumero_Documento_Acudiente_1.Text != "")
            {
                objAcudiente_1.documento_id_tipo                    = int.Parse(ddlTipo_Documento_Acudiente_1.SelectedValue.ToString());
                objAcudiente_1.documento_numero                     = Convert.ToInt64(txtNumero_Documento_Acudiente_1.Text);
                objAcudiente_1.nombres                              = HttpUtility.HtmlDecode(txtNombres_Acudiente_1.Text);
                objAcudiente_1.apellidos                            = HttpUtility.HtmlDecode(txtApellidos_Acudiente_1.Text);
                objAcudiente_1.id_parentesco                        = int.Parse(ddlParentesco_Acudiente_1.SelectedValue.ToString());
                objAcudiente_1.email                                = txtEmail_Acudiente_1.Text;
                objAcudiente_1.direccion_numero                     = txtDireccion_Numero_Acudiente_1.Text;
                objAcudiente_1.direccion_barrio                     = txtDireccion_Barrio_Acudiente_1.Text;
                objAcudiente_1.direccion_id_municipio               = int.Parse(ddlDireccion_Municipio_Acudiente_1.SelectedValue.ToString());
                objAcudiente_1.telefonos                            = txtTelefonos_Acudiente_1.Text;
                objAcudiente_1.id_usuario                           = int.Parse(Session["id_usuario"].ToString());
            }
            /*Acudiente 2*/

            Acudiente objAcudiente_2                                = new Acudiente();

            if (txtNumero_Documento_Acudiente_2.Text != "")
            {
                objAcudiente_2.documento_id_tipo                    = int.Parse(ddlTipo_Documento_Acudiente_2.SelectedValue.ToString());
                objAcudiente_2.documento_numero                     = Convert.ToInt64(txtNumero_Documento_Acudiente_2.Text);
                objAcudiente_2.nombres                              = HttpUtility.HtmlDecode(txtNombres_Acudiente_2.Text);
                objAcudiente_2.apellidos                            = HttpUtility.HtmlDecode(txtApellidos_Acudiente_2.Text);
                objAcudiente_2.id_parentesco                        = int.Parse(ddlParentesco_Acudiente_2.SelectedValue.ToString());
                objAcudiente_2.email                                = txtEmail_Acudiente_2.Text;
                objAcudiente_2.direccion_numero                     = txtDireccion_Numero_Acudiente_2.Text;
                objAcudiente_2.direccion_barrio                     = txtDireccion_Barrio_Acudiente_2.Text;
                objAcudiente_2.direccion_id_municipio               = int.Parse(ddlDireccion_Municipio_Acudiente_2.SelectedValue.ToString());
                objAcudiente_2.telefonos                            = txtTelefonos_Acudiente_2.Text;
                objAcudiente_2.id_usuario                           = int.Parse(Session["id_usuario"].ToString());
            }

            if (accion.Equals("Agregar"))
            {
                if (fluFoto.FileName.ToString() == "")
                {
                    objEstudiante.foto = "~/img/fotos/usuario.jpg";
                }
                else {
                    objEstudiante.foto = "~/img/fotos/" + NombreImagen;
                }
                if (txtNumero_Documento_Acudiente_1.Text != "" && txtNumero_Documento_Acudiente_2.Text != "")
                {
                    if (validarAcudiente(int.Parse(txtNumero_Documento_Acudiente_1.Text)) == 0)
                    {
                        objOperAcudiente.InsertarAcudiente(objAcudiente_1);
                    }
                    if (validarAcudiente(int.Parse(txtNumero_Documento_Acudiente_2.Text)) == 0)
                    {
                        objOperAcudiente.InsertarAcudiente(objAcudiente_2);
                    }
                    objOperEstudiante.InsertarEstudiante(objEstudiante);
                    Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Busqueda", Accion = "Agrego" });
                }
            }
            else
            {
                if (imgEstudiante.DescriptionUrl == "/Academico/img/fotos/usuario.jpg")
                {
                    if (fluFoto.FileName.ToString() == "")
                    {
                        objEstudiante.foto = "~/img/fotos/usuario.jpg";
                    }
                    else
                    {
                        objEstudiante.foto = "~/img/fotos/" + NombreImagen;
                    }
                }
                else
                {
                    
                    if (fluFoto.FileName.ToString() == "")
                    {
                        objEstudiante.foto = imgEstudiante.DescriptionUrl.Replace("/Academico","~");
                    }
                    else
                    {
                        objEstudiante.foto = "~/img/fotos/" + NombreImagen;
                    }
                }
                objEstudiante.id = int.Parse(Page.RouteData.Values["id"].ToString());
                if (txtNumero_Documento_Acudiente_1.Text != "" && txtNumero_Documento_Acudiente_2.Text != "")
                {
                    if (validarAcudiente(int.Parse(txtNumero_Documento_Acudiente_1.Text)) == 0)
                    {
                        objOperAcudiente.InsertarAcudiente(objAcudiente_1);
                    }
                    else
                    {
                        objAcudiente_1.id = clsFunciones.documento_acudiente_1;
                        objOperAcudiente.ActualizarAcudiente(objAcudiente_1);
                    }
                    if (validarAcudiente(int.Parse(txtNumero_Documento_Acudiente_2.Text)) == 0)
                    {
                        objOperAcudiente.InsertarAcudiente(objAcudiente_2);
                    }
                    else
                    {
                        objAcudiente_2.id = clsFunciones.documento_acudiente_2;
                        objOperAcudiente.ActualizarAcudiente(objAcudiente_2);
                    }
                    objOperEstudiante.ActualizarEstudiante(objEstudiante);
                    Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Busqueda", Accion = "Edito" });
                }
            }
        }
        catch (Exception){}
    }
    protected void btnCancelarPersonal_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoutePermanent("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar()
    {
        try
        {
            /*Estudiante*/
            DataView dtv_Listado                                = ((DataTable)Session["listado"]).DefaultView;
            dtv_Listado.RowFilter                               = "id_tipo_listado=2";
            this.enlazarCombo(dtv_Listado, ddlGenero);
            dtv_Listado.RowFilter                               = "id_tipo_listado=1";
            this.enlazarCombo(dtv_Listado, ddlDocumento_Id_Tipo);
            this.enlazarCombo(dtv_Listado, ddlTipo_Documento_Acudiente_1);
            this.enlazarCombo(dtv_Listado, ddlTipo_Documento_Acudiente_2);
            dtv_Listado.RowFilter                               = "id_tipo_listado=3";
            this.enlazarCombo(dtv_Listado, ddlZona);
            dtv_Listado.RowFilter                               = "id_tipo_listado=5";
            this.enlazarCombo(dtv_Listado, ddlParentesco_Acudiente_1);
            this.enlazarCombo(dtv_Listado, ddlParentesco_Acudiente_2);
            dtv_Listado.RowFilter                               = "id_tipo_listado=14";
            this.enlazarCombo(dtv_Listado, ddlEps);
            dtv_Listado.RowFilter                               = "id_tipo_listado=4";
            this.enlazarCombo(dtv_Listado, ddlGrupo_Sanguineo);
            Departamento objDepartamento                        = new Departamento();
            OperacionDepartamento objOperDepartamento           = new OperacionDepartamento(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable tbl_departamento                          = new DataTable();
            tbl_departamento                                    = objOperDepartamento.ConsultarDepartamento(objDepartamento);
            Municipio objMunicipio                              = new Municipio();
            OperacionMunicipio objOperMunicipio                 = new OperacionMunicipio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.municipio                              = objOperMunicipio.ConsultarMunicipio(objMunicipio);
            string accion                                       = Page.RouteData.Values["accion"].ToString();
            this.enlazarCombo(tbl_departamento, ddlDocumento_Departamento);
            this.enlazarCombo(tbl_departamento, ddlNacimiento_Departamento);
            this.enlazarCombo(tbl_departamento, ddlDireccion_Departamento);

            /*Acudiente*/
            this.enlazarCombo(tbl_departamento,ddlDireccion_Departamento_Acudiente_1);
            this.enlazarCombo(tbl_departamento, ddlDireccion_Departamento_Acudiente_2);

            if (accion.Equals("Edita"))
            {
                /*Estudiante*/
                this.enlazarCombo(clsFunciones.municipio, ddlDocumento_Municipio);
                this.enlazarCombo(clsFunciones.municipio, ddlNacimiento_Municipio);
                this.enlazarCombo(clsFunciones.municipio, ddlDireccion_Municipio);
                string id                                       = Page.RouteData.Values["id"].ToString();
                Estudiante objEstudiante                        = new Estudiante();
                OperacionEstudiante objOperEstudiante           = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Estudiante                         = new GridView();
                objEstudiante.id                                = int.Parse(id);
                tbl_Estudiante.DataSource                       = objOperEstudiante.ConsultarEstudiante(objEstudiante);
                tbl_Estudiante.DataBind();
                ddlDocumento_Id_Tipo.SelectedValue              = tbl_Estudiante.Rows[0].Cells[1].Text;
                txtDocumento_Numero.Text                        = tbl_Estudiante.Rows[0].Cells[2].Text;
                ddlDocumento_Municipio.Text                     = tbl_Estudiante.Rows[0].Cells[3].Text;
                txtNombre_1.Text                                = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[4].Text);
                if (tbl_Estudiante.Rows[0].Cells[5].Text == "&nbsp;")
                {
                    txtNombre_2.Text                            = "";
                }
                else
                {
                    txtNombre_2.Text                            = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[5].Text);
                }
                txtApellido_1.Text                              = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[6].Text);
                txtApellido_2.Text                              = HttpUtility.HtmlDecode(tbl_Estudiante.Rows[0].Cells[7].Text);
                ddlGenero.Text                                  = tbl_Estudiante.Rows[0].Cells[8].Text;
                txtEmail.Text                                   = tbl_Estudiante.Rows[0].Cells[9].Text;
                txtNacimiento_Fecha.Text                        = DateTime.Parse(tbl_Estudiante.Rows[0].Cells[10].Text).ToShortDateString();
                ddlNacimiento_Municipio.SelectedValue           = tbl_Estudiante.Rows[0].Cells[11].Text;
                txtDireccion_Numero.Text                        = tbl_Estudiante.Rows[0].Cells[12].Text;
                txtDireccion_Barrio.Text                        = tbl_Estudiante.Rows[0].Cells[13].Text;
                ddlDireccion_Municipio.SelectedValue            = tbl_Estudiante.Rows[0].Cells[14].Text;
                ddlZona.SelectedValue                           = tbl_Estudiante.Rows[0].Cells[15].Text;
                txtTelefono_Fijo.Text                           = tbl_Estudiante.Rows[0].Cells[16].Text;
                txtTelefono_Celular.Text                        = tbl_Estudiante.Rows[0].Cells[17].Text;
                clsFunciones.documento_acudiente_1              = int.Parse(tbl_Estudiante.Rows[0].Cells[18].Text);
                clsFunciones.documento_acudiente_2              = int.Parse(tbl_Estudiante.Rows[0].Cells[19].Text);
                imgEstudiante.ImageUrl                          = ResolveUrl(tbl_Estudiante.Rows[0].Cells[20].Text);
                imgEstudiante.DescriptionUrl                    = ResolveUrl(tbl_Estudiante.Rows[0].Cells[20].Text);
                ddlEps.SelectedValue                            = tbl_Estudiante.Rows[0].Cells[21].Text;
                ddlGrupo_Sanguineo.SelectedValue                = tbl_Estudiante.Rows[0].Cells[22].Text;
                txtSisben_Numero.Text                           = tbl_Estudiante.Rows[0].Cells[23].Text;
                ddlSisben_Nivel.Text                            = tbl_Estudiante.Rows[0].Cells[24].Text;
                ddlEstrato.SelectedValue                        = tbl_Estudiante.Rows[0].Cells[25].Text;
                this.seleccionar_Departamento(ddlDocumento_Departamento, ddlDocumento_Municipio);
                this.seleccionar_Departamento(ddlNacimiento_Departamento, ddlNacimiento_Municipio);
                this.seleccionar_Departamento(ddlDireccion_Departamento, ddlDireccion_Municipio);

                /*Acudiente 1*/
                this.enlazarCombo(clsFunciones.municipio, ddlDireccion_Municipio_Acudiente_1);
                this.enlazarCombo(clsFunciones.municipio, ddlDireccion_Municipio_Acudiente_2);
                Acudiente objAcudiente_1                            = new Acudiente();
                OperacionAcudiente objOperAcudiente                 = new OperacionAcudiente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Acudiente_1                            = new GridView();
                objAcudiente_1.id                                   = int.Parse(tbl_Estudiante.Rows[0].Cells[18].Text);
                tbl_Acudiente_1.DataSource                          = objOperAcudiente.ConsultarAcudiente(objAcudiente_1);
                tbl_Acudiente_1.DataBind();
                ddlTipo_Documento_Acudiente_1.SelectedValue         = tbl_Acudiente_1.Rows[0].Cells[1].Text;
                txtNumero_Documento_Acudiente_1.Text                = tbl_Acudiente_1.Rows[0].Cells[2].Text;
                txtNombres_Acudiente_1.Text                         = HttpUtility.HtmlDecode(tbl_Acudiente_1.Rows[0].Cells[3].Text);
                txtApellidos_Acudiente_1.Text                       = HttpUtility.HtmlDecode(tbl_Acudiente_1.Rows[0].Cells[4].Text);
                ddlParentesco_Acudiente_1.SelectedValue             = tbl_Acudiente_1.Rows[0].Cells[5].Text;
                txtEmail_Acudiente_1.Text                           = tbl_Acudiente_1.Rows[0].Cells[6].Text;
                txtDireccion_Numero_Acudiente_1.Text                = tbl_Acudiente_1.Rows[0].Cells[7].Text;
                txtDireccion_Barrio_Acudiente_1.Text                = tbl_Acudiente_1.Rows[0].Cells[8].Text;
                ddlDireccion_Municipio_Acudiente_1.SelectedValue    = tbl_Acudiente_1.Rows[0].Cells[9].Text;
                txtTelefonos_Acudiente_1.Text                       = tbl_Acudiente_1.Rows[0].Cells[10].Text;

                /*Acudiente 2*/
                Acudiente objAcudiente_2                            = new Acudiente();
                GridView tbl_Acudiente_2                            = new GridView();
                objAcudiente_2.id                                   = int.Parse(tbl_Estudiante.Rows[0].Cells[19].Text);
                tbl_Acudiente_2.DataSource                          = objOperAcudiente.ConsultarAcudiente(objAcudiente_2);
                tbl_Acudiente_2.DataBind();
                ddlTipo_Documento_Acudiente_2.SelectedValue         = tbl_Acudiente_2.Rows[0].Cells[1].Text;
                txtNumero_Documento_Acudiente_2.Text                = tbl_Acudiente_2.Rows[0].Cells[2].Text;
                txtNombres_Acudiente_2.Text                         = HttpUtility.HtmlDecode(tbl_Acudiente_2.Rows[0].Cells[3].Text);
                txtApellidos_Acudiente_2.Text                       = HttpUtility.HtmlDecode(tbl_Acudiente_2.Rows[0].Cells[4].Text);
                ddlParentesco_Acudiente_2.SelectedValue             = tbl_Acudiente_2.Rows[0].Cells[5].Text;
                txtEmail_Acudiente_2.Text                           = tbl_Acudiente_2.Rows[0].Cells[6].Text;
                txtDireccion_Numero_Acudiente_2.Text                = tbl_Acudiente_2.Rows[0].Cells[7].Text;
                txtDireccion_Barrio_Acudiente_2.Text                = tbl_Acudiente_2.Rows[0].Cells[8].Text;
                ddlDireccion_Municipio_Acudiente_2.SelectedValue    = tbl_Acudiente_2.Rows[0].Cells[9].Text;
                txtTelefonos_Acudiente_2.Text                       = tbl_Acudiente_2.Rows[0].Cells[10].Text;
                //fluFoto.FileName = tbl_Acudiente_2.Rows[0].Cells[20].Text;
                this.seleccionar_Departamento(ddlDireccion_Departamento_Acudiente_1, ddlDireccion_Municipio_Acudiente_1);
                this.seleccionar_Departamento(ddlDireccion_Departamento_Acudiente_2, ddlDireccion_Municipio_Acudiente_2);

                /*Salud*/
            }
        }
        catch (Exception) { }
    }

    protected void ddlDocumento_Departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlDocumento_Departamento,ddlDocumento_Municipio);
    }

    public void seleccionar_Municipio(DropDownList ddlDepartamento, DropDownList ddlMunicipio)
    {
        if (ddlDepartamento.SelectedValue != null)
        {
            ddlMunicipio.Items.Clear();
            ListItem item                   = new ListItem();
            item.Value                      = "0";
            item.Text                       = "--- SELECCIONE UNO ---";
            ddlMunicipio.Items.Add(item);
            DataView dtv_Municipio          = clsFunciones.municipio.DefaultView;
            dtv_Municipio.RowFilter         = "id_departamento=" + ddlDepartamento.SelectedValue;
            enlazarCombo(dtv_Municipio,ddlMunicipio);
        }
    }



    public int validarAcudiente (int documento) {
        int val = 0;
        Acudiente objAcudiente                  = new Acudiente();
        OperacionAcudiente objOperAcudiente     = new OperacionAcudiente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Acudiente                  = new GridView();
        objAcudiente.documento_numero           = Convert.ToInt64(documento);
        tbl_Acudiente.DataSource                = objOperAcudiente.ConsultarAcudiente(objAcudiente);
        tbl_Acudiente.DataBind();
        if (tbl_Acudiente.Rows.Count > 0 ) {
            val = 1;
        }else {
            val = 0;
        }
        return val;
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo) {
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

    public void seleccionar_Departamento(DropDownList ddlDepartamento, DropDownList ddlMunicipio)
    {
        if (ddlMunicipio.SelectedValue != null && int.Parse(ddlMunicipio.SelectedValue) > 0)
        {
            ddlDepartamento.SelectedValue = clsFunciones.municipio.Select("id="+ddlMunicipio.SelectedValue)[0][2].ToString();
        }
    }

    
    protected void ddlNacimiento_Departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlNacimiento_Departamento,ddlNacimiento_Municipio);
    }
    protected void ddlDireccion_Departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlDireccion_Departamento,ddlDireccion_Municipio);
    }
    protected void ddlDireccion_Departamento_Acudiente_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlDireccion_Departamento_Acudiente_1, ddlDireccion_Municipio_Acudiente_1);
    }
    protected void ddlDireccion_Departamento_Acudiente_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.seleccionar_Municipio(ddlDireccion_Departamento_Acudiente_2, ddlDireccion_Municipio_Acudiente_2);
    }
    protected void txtNumero_Documento_Acudiente_1_TextChanged(object sender, EventArgs e)
    {
        if (txtNumero_Documento_Acudiente_1.Text != "")
        {
            OperacionAcudiente objOperAcudiente                             = new OperacionAcudiente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            OperacionMunicipio objOperMunicipio                             = new OperacionMunicipio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Municipio objMunicipio                                          = new Municipio();
            Acudiente objAcudiente_1                                        = new Acudiente();
            GridView tbl_Acudiente_1                                        = new GridView();
            objAcudiente_1.documento_numero                                 = Convert.ToInt64(txtNumero_Documento_Acudiente_1.Text);
            tbl_Acudiente_1.DataSource                                      = objOperAcudiente.ConsultarAcudiente(objAcudiente_1);
            tbl_Acudiente_1.DataBind();
            if (tbl_Acudiente_1.Rows.Count > 0)
            {

                this.enlazarCombo(objOperMunicipio.ConsultarMunicipio(objMunicipio), ddlDireccion_Municipio_Acudiente_1);
                ddlTipo_Documento_Acudiente_1.SelectedValue                 = tbl_Acudiente_1.Rows[0].Cells[1].Text;
                txtNumero_Documento_Acudiente_1.Text                        = tbl_Acudiente_1.Rows[0].Cells[2].Text;
                txtNombres_Acudiente_1.Text                                 = HttpUtility.HtmlEncode(tbl_Acudiente_1.Rows[0].Cells[3].Text);
                txtApellidos_Acudiente_1.Text                               = HttpUtility.HtmlEncode(tbl_Acudiente_1.Rows[0].Cells[4].Text);
                ddlParentesco_Acudiente_1.SelectedValue                     = tbl_Acudiente_1.Rows[0].Cells[5].Text;
                txtEmail_Acudiente_1.Text                                   = tbl_Acudiente_1.Rows[0].Cells[6].Text;
                txtDireccion_Numero_Acudiente_1.Text                        = tbl_Acudiente_1.Rows[0].Cells[7].Text;
                txtDireccion_Barrio_Acudiente_1.Text                        = tbl_Acudiente_1.Rows[0].Cells[8].Text;
                ddlDireccion_Municipio_Acudiente_1.SelectedValue            = tbl_Acudiente_1.Rows[0].Cells[9].Text;
                txtTelefonos_Acudiente_1.Text                               = tbl_Acudiente_1.Rows[0].Cells[10].Text;
                this.seleccionar_Departamento(ddlDireccion_Departamento_Acudiente_1, ddlDireccion_Municipio_Acudiente_1);
            }else {
                txtNombres_Acudiente_1.Text                                 = "";;
                txtApellidos_Acudiente_1.Text                               = "";
                ddlParentesco_Acudiente_1.SelectedValue                     = "0";
                txtEmail_Acudiente_1.Text                                   = "";
                txtDireccion_Numero_Acudiente_1.Text                        = "";
                txtDireccion_Barrio_Acudiente_1.Text                        = "";
                ddlDireccion_Departamento_Acudiente_1.SelectedValue         = "0";
                ddlDireccion_Municipio_Acudiente_1.SelectedValue            = "0" ;
                txtTelefonos_Acudiente_1.Text                               = "" ;
            }
        }else {
                txtNombres_Acudiente_1.Text                                 = ""; ;
                txtApellidos_Acudiente_1.Text                               = "";
                ddlParentesco_Acudiente_1.SelectedValue                     = "0";
                txtEmail_Acudiente_1.Text                                   = "";
                txtDireccion_Numero_Acudiente_1.Text                        = "";
                txtDireccion_Barrio_Acudiente_1.Text                        = "";
                ddlDireccion_Departamento_Acudiente_1.SelectedValue         = "0";
                ddlDireccion_Municipio_Acudiente_1.SelectedValue            = "0";
                txtTelefonos_Acudiente_1.Text                               = "";
        }
    }
    protected void txtNumero_Documento_Acudiente_2_TextChanged(object sender, EventArgs e)
    {
        if (txtNumero_Documento_Acudiente_2.Text != "")
        {
            OperacionAcudiente objOperAcudiente                             = new OperacionAcudiente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            OperacionMunicipio objOperMunicipio                             = new OperacionMunicipio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Municipio objMunicipio                                          = new Municipio();

            Acudiente objAcudiente_2                                        = new Acudiente();
            GridView tbl_Acudiente_2                                        = new GridView();
            objAcudiente_2.documento_numero                                 = Convert.ToInt64(txtNumero_Documento_Acudiente_2.Text);
            tbl_Acudiente_2.DataSource                                      = objOperAcudiente.ConsultarAcudiente(objAcudiente_2);
            tbl_Acudiente_2.DataBind();
            if (tbl_Acudiente_2.Rows.Count > 0)
            {
                this.enlazarCombo(objOperMunicipio.ConsultarMunicipio(objMunicipio), ddlDireccion_Municipio_Acudiente_2);
                ddlTipo_Documento_Acudiente_2.SelectedValue                 = tbl_Acudiente_2.Rows[0].Cells[1].Text;
                txtNumero_Documento_Acudiente_2.Text                        = tbl_Acudiente_2.Rows[0].Cells[2].Text;
                txtNombres_Acudiente_2.Text                                 = HttpUtility.HtmlEncode(tbl_Acudiente_2.Rows[0].Cells[3].Text);
                txtApellidos_Acudiente_2.Text                               = HttpUtility.HtmlEncode(tbl_Acudiente_2.Rows[0].Cells[4].Text);
                ddlParentesco_Acudiente_2.SelectedValue                     = tbl_Acudiente_2.Rows[0].Cells[5].Text;
                txtEmail_Acudiente_2.Text                                   = tbl_Acudiente_2.Rows[0].Cells[6].Text;
                txtDireccion_Numero_Acudiente_2.Text                        = tbl_Acudiente_2.Rows[0].Cells[7].Text;
                txtDireccion_Barrio_Acudiente_2.Text                        = tbl_Acudiente_2.Rows[0].Cells[8].Text;
                ddlDireccion_Municipio_Acudiente_2.SelectedValue            = tbl_Acudiente_2.Rows[0].Cells[9].Text;
                txtTelefonos_Acudiente_2.Text                               = tbl_Acudiente_2.Rows[0].Cells[10].Text;
                this.seleccionar_Departamento(ddlDireccion_Departamento_Acudiente_2, ddlDireccion_Municipio_Acudiente_2);
            }else {
                txtNombres_Acudiente_2.Text                                 = ""; ;
                txtApellidos_Acudiente_2.Text                               = "";
                ddlParentesco_Acudiente_2.SelectedValue                     = "0";
                txtEmail_Acudiente_2.Text                                   = "";
                txtDireccion_Numero_Acudiente_2.Text                        = "";
                txtDireccion_Barrio_Acudiente_2.Text                        = "";
                ddlDireccion_Departamento_Acudiente_2.SelectedValue         = "0";
                ddlDireccion_Municipio_Acudiente_2.SelectedValue            = "0";
                txtTelefonos_Acudiente_2.Text                               = "";
            }
        }else {
            txtNombres_Acudiente_2.Text                                     = ""; ;
            txtApellidos_Acudiente_2.Text                                   = "";
            ddlParentesco_Acudiente_2.SelectedValue                         = "0";
            txtEmail_Acudiente_2.Text                                       = "";
            txtDireccion_Numero_Acudiente_2.Text                            = "";
            txtDireccion_Barrio_Acudiente_2.Text                            = "";
            ddlDireccion_Departamento_Acudiente_2.SelectedValue             = "0";
            ddlDireccion_Municipio_Acudiente_2.SelectedValue                = "0";
            txtTelefonos_Acudiente_2.Text                                   = "";
        }
    }
}