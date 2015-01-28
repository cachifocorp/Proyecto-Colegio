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

public partial class Proceso_Indicador_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardar.UniqueID;
        if (!IsPostBack) {
            this.cargar();
        }
    }

    public void cargar () {
        try
        {
            
            Anio_Escolar objAnio_Escolar                                                    = (Anio_Escolar)Session["anioEscolar"];

            /*Periodo*/
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                                    = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo                       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable tbl_Periodo                                                           = new DataTable();
            objAnio_Escolar_Periodo.id_anio_escolar                                         = objAnio_Escolar.id;
            tbl_Periodo                                                                     = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);

            /*Estudiante que pertenecen a un salón*/
            Asignacion objAsignacion                                                        = new Asignacion();
            OperacionAsignacion objOperAsignacion                                           = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id                                                                = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
            tbl_Estudiante.DataSource                                                       = objOperAsignacion.ConsultarEstudiante(objAsignacion);
            tbl_Estudiante.DataBind();
            DataTable tbl_Asignacion                                                        = new DataTable();
            tbl_Asignacion                                                                  = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            
            /*Indicadores*/
            Indicador objIndicador                                                          = new Indicador();
            OperacionIndicador objOperIndicador                                             = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objIndicador.id_materia                                                         = int.Parse(tbl_Asignacion.Rows[0].ItemArray[2].ToString());
            objIndicador.id_grado                                                           = int.Parse(tbl_Asignacion.Rows[0].ItemArray[11].ToString());
            objIndicador.id_anio_escolar_periodo                                            = int.Parse(tbl_Periodo.Rows[0].ItemArray[0].ToString());
            tbl_Indicadores.DataSource                                                      = objOperIndicador.ConsultarIndicador(objIndicador);
            tbl_Indicadores.DataBind();
            
        }
        catch (Exception)
        {
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Boletin objBoletin                      = new Boletin();
        OperacionBoletin objOperBoletin         = new OperacionBoletin(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        foreach (GridViewRow row in tbl_Estudiante.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkEstudiante") as CheckBox);
                if (chkRow.Checked)
                {
                    foreach (GridViewRow rowIndicador in tbl_Indicadores.Rows)
                    {
                        if (rowIndicador.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRowIndicador = (rowIndicador.Cells[0].FindControl("chkIndicador") as CheckBox);
                            if (chkRowIndicador.Checked)
                            {
                                objBoletin.id_asignacion            = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                                objBoletin.id_estudiante            = Convert.ToInt64(row.Cells[1].Text);
                                objBoletin.id_indicador             = int.Parse(rowIndicador.Cells[1].Text);
                                objBoletin.id_usuario               = int.Parse(Session["id_usuario"].ToString());
                                DataTable tbl_Boletin               = objOperBoletin.ConsultarBoletin(objBoletin);
                                if (tbl_Boletin.Rows.Count == 0)
                                {
                                    objOperBoletin.InsertarBoletin(objBoletin);
                                }
                            }
                        }
                    }
                }
            }
        }
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Agrego" });
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Cancelo" });
    }
}