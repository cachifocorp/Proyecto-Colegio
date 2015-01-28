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

public partial class Tecnica_Asignacion_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cargar();
        }
    }

    public void cargar()
    {
        try { 
            string id                               = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
            Matricula objMatricula                  = new Matricula();
            OperacionMatricula objOperMatricula     = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMatricula.id_salon                   = int.Parse(id);
            tbl_Estudiante.DataSource               = objOperMatricula.ConsultarMatricula(objMatricula);
            tbl_Estudiante.DataBind();
            
            Salon objSalon                          = new Salon();
            OperacionSalon objOperSalon             = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Salones                    = new GridView();
            objSalon.id                             = int.Parse(id);
            tbl_Salones.DataSource                  = objOperSalon.ConsultarSalon(objSalon);
            tbl_Salones.DataBind();
            Salon objSalon_2                        = new Salon();
            objSalon_2.id_grado                     = int.Parse(tbl_Salones.Rows[0].Cells[5].Text);
            objSalon_2.cantidad                     = 2;
            tbl_Tecnicas.DataSource                 = objOperSalon.ConsultarSalon_Tecnica(objSalon_2);
            tbl_Tecnicas.DataBind();

        }
        catch (Exception ex) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Tecnica", Entidad = "Asignacion", Pagina = "Busqueda", Accion = "Cancelo" });
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Matricula objMatricula                  = new Matricula();
            OperacionMatricula objOperMatricula     = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Estudiante.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkEstudiante") as CheckBox);
                    if (chkRow.Checked)
                    {
                        foreach (GridViewRow rowTecnica in tbl_Tecnicas.Rows)
                        {
                            if (rowTecnica.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRowTecnica = (rowTecnica.Cells[0].FindControl("chkTecnica") as CheckBox);
                               
                                if (chkRowTecnica.Checked)
                                {
                                   objMatricula.id_salon_tecnica        = int.Parse(rowTecnica.Cells[1].Text); 
                                   objMatricula.id                      = int.Parse(row.Cells[1].Text);
                                   objMatricula.id_usuario              = int.Parse(Session["id_usuario"].ToString());
                                   objOperMatricula.ActualizarMatriculaTecnica(objMatricula); 
                                }
                            }
                        }
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Tecnica", Entidad = "Asignacion", Pagina = "Busqueda", Accion = "Agrego" });
        }
        catch (Exception ex)
        {
        }
    }
}