using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Consolidado_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargar();
        }
    }

    public void cargar()
    {
        try
        {

            Anio_Escolar objAnio_Escolar                                    = (Anio_Escolar)Session["anioEscolar"];

            /*Periodo*/
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                    = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAnio_Escolar_Periodo.id_anio_escolar                         = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo), ddlPeriodo);

            /*Salones*/

            Salon objSalon                                                  = new Salon();
            OperacionSalon objOperSalon                                     = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperSalon.ConsultarSalon(objSalon), ddlSalon);
        }
        catch (Exception)
        {

        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsFunciones.consolidado = "";
        Calificacion objCalificacion_Promedio                           = new Calificacion();
        OperacionCalificacion objOperCalificacion                       = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objCalificacion_Promedio.id_calificacion_configuracion          = int.Parse(ddlSalon.SelectedValue.ToString());
        objCalificacion_Promedio.id_asignacion                          = int.Parse(ddlPeriodo.SelectedValue.ToString());
        DataView dtvPromedio = null;
        if (!chkAcumulado.Checked)
        {
            dtvPromedio = objOperCalificacion.ConsultarPromedio_Periodo(objCalificacion_Promedio).DefaultView;
        }
        else {
            dtvPromedio = objOperCalificacion.ConsultarPromedio_PeriodoAcumulado(objCalificacion_Promedio).DefaultView;
        }
        Asignacion objAsignacion                                        = new Asignacion();
        OperacionAsignacion objOperAsignacion                           = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objAsignacion.id_salon                                          = int.Parse(ddlSalon.SelectedValue.ToString());
        DataTable dtEstudiante                                          = objOperAsignacion.ConsultarEstudiante(objAsignacion);
        DataTable dtAsignacion                                          = objOperAsignacion.ConsultarAsignacion(objAsignacion);
        Matricula objMatricula                                          = new Matricula();
        OperacionMatricula objOperMatricula                             = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        DataTable dta_Matricula                                         = new DataTable();
        Asignacion objAsignacion_Tecnica                                = new Asignacion();
        DataTable dtAsignacion_Tecnica                                  = new DataTable();
        Salon objSalon                                                  = new Salon();
        OperacionSalon objOperSalon                                     = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objSalon.id                                                     = int.Parse(ddlSalon.SelectedValue.ToString());
        DataTable dtvDirector                                           = objOperSalon.ConsultarSalon(objSalon);
        Docente objDocente                                              = new Docente();
        OperacionDocente objOperDocente                                 = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objDocente.id                                                   = int.Parse(dtvDirector.Rows[0].ItemArray[4].ToString());
        DataTable dtaDocente                                            = objOperDocente.ConsultarDocente(objDocente);
        Anio_Escolar objAnio_Escolar                                    = (Anio_Escolar)Session["anioEscolar"];
        Colegio objColegio                                              = new Colegio();
        OperacionColegio objOperColegio                                 = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        DataTable dt                                                    = objOperColegio.ConsultarColegio(objColegio);
        string estilo                                                   = "style = 'text-align:center; border: 1px solid #000'";
        string htmlencabezado                                           = "<table width='100%' height='100%' style='font-size:10px; font-family:Calibri ;border-collapse:collapse;'>";
        htmlencabezado                                                  += " <tr>";
        htmlencabezado                                                  += "<td style = 'text-align:center'><img alt='logo' src = 'http://academico.itipuentenacional.edu.co/img/logo.png'  width='80' height='80' ></td>";
        htmlencabezado                                                  += "<td colspan='3' style = 'text-align:center'><strong><h3>" + dt.Rows[0].ItemArray[1] + "</h3></strong><h4 style='font-weight:bold'>CONSOLIDADO DE CALIFICACIONES</h4></td>";
        htmlencabezado                                                  += "</tr>";
        htmlencabezado                                                  += "<tr>";
        htmlencabezado                                                  += "<td " + estilo + "><strong>DIRECTOR</strong></td>";
        htmlencabezado                                                  += " <td " + estilo + "><strong>SALÓN</strong></td>";
        htmlencabezado                                                  += " <td " + estilo + "><strong>PERIODO</strong></td>";
        htmlencabezado                                                  += " <td " + estilo + "><strong>AÑO</strong></td>";
        htmlencabezado                                                  += "</tr>";
        htmlencabezado                                                  += " <tr>";
        htmlencabezado                                                  += "<td " + estilo + ">" + dtaDocente.Rows[0].ItemArray[3].ToString() + " " + dtaDocente.Rows[0].ItemArray[4].ToString() + "</td>";
        htmlencabezado                                                  += " <td " + estilo + ">" + ddlSalon.SelectedItem.Text.ToString() + "</td>";
        htmlencabezado                                                  += " <td " + estilo + ">" + ddlPeriodo.SelectedItem.Text.ToString() + "</td>";
        htmlencabezado                                                  += "  <td " + estilo + ">" + objAnio_Escolar.descripcion + "</td>";
        htmlencabezado                                                  += "  </tr>";

        htmlencabezado                                                  += "</table>";
        string htmlmateria                                              = "<table width='100%' height='100%' style='font-size:10px; font-family:Calibri ;border-collapse:collapse; border: 1px solid #000'>";
        htmlmateria                                                     +="<tr><td colspan = '4' style= 'text-align:center'><strong>ASIGNATURAS</strong></td></tr>";
        string html                                                     = "<table width='100%' height='100%' style='font-size:10px; font-family:Calibri ;border-collapse:collapse; border: 1px solid #000'>";
        string encabezado                                               = "<tr><td bgcolor='#d6e3bc' " + estilo + ">#</td><td bgcolor='#d6e3bc' " + estilo + ">DOCUMENTO</td><td bgcolor='#d6e3bc' " + estilo + ">ESTUDIANTE</td>";

        string cuerpo                                                   = "";
        GridView tbl_Promedio                                           = new GridView();
        DataView dtvPromedio_Tecnica                                    = new DataView();
        for (int i = 0; i < dtEstudiante.Rows.Count; i++)
        {
            objMatricula.id_estudiante = Convert.ToInt64(dtEstudiante.Rows[i].ItemArray[1].ToString());
            dta_Matricula                                               = objOperMatricula.ConsultarMatricula(objMatricula);
            if (int.Parse(dta_Matricula.Rows[0].ItemArray[8].ToString()) != 39)
            {
                objAsignacion_Tecnica.id_salon                          = int.Parse(dta_Matricula.Rows[0].ItemArray[8].ToString());
                dtAsignacion_Tecnica                                    = objOperAsignacion.ConsultarAsignacion(objAsignacion_Tecnica);
                objCalificacion_Promedio.id_calificacion_configuracion  = int.Parse(dta_Matricula.Rows[0].ItemArray[8].ToString());
                objCalificacion_Promedio.id_asignacion                  = int.Parse(ddlPeriodo.SelectedValue.ToString());
                if (!chkAcumulado.Checked)
                {
                    dtvPromedio_Tecnica = objOperCalificacion.ConsultarPromedio_Periodo(objCalificacion_Promedio).DefaultView;
                }
                else {
                    dtvPromedio_Tecnica = objOperCalificacion.ConsultarPromedio_PeriodoAcumulado(objCalificacion_Promedio).DefaultView;
                }
                
            }

            int materias_perdidas                                       = getMateriasPerdidas(dtvPromedio,dtEstudiante.Rows[i].ItemArray[0].ToString(),objAnio_Escolar.rendimiento_bajo);
            int tecnica_perdida                                         = getMateriasPerdidas(dtvPromedio_Tecnica,dtEstudiante.Rows[i].ItemArray[0].ToString(),objAnio_Escolar.rendimiento_bajo);
            String color = "";
            if (chkAcumulado.Checked)
            {
                color = "red";
            }
            else {
                color = "red";
            }
            if (materias_perdidas > 0 || tecnica_perdida > 0)
            {
                cuerpo                                                  += "<tr><td style = 'text-align:center; border: 1px solid #000;color:"+color+"'>" + (i + 1) + "</td>";
                cuerpo                                                  += "<td style = 'text-align:center; border: 1px solid #000;color:"+color+"'>" + dtEstudiante.Rows[i].ItemArray[1] + "</td>";
                cuerpo                                                  += "<td style = 'text-align:center; border: 1px solid #000;color:"+color+"'>" + dtEstudiante.Rows[i].ItemArray[4] + " " + dtEstudiante.Rows[i].ItemArray[5] +
                                                                        " " + dtEstudiante.Rows[i].ItemArray[2] + " " + dtEstudiante.Rows[i].ItemArray[3] + "</td>";
            }else{
                cuerpo                                                  += "<tr><td " + estilo + ">" + (i + 1) + "</td>";
                cuerpo                                                  += "<td " + estilo + ">" + dtEstudiante.Rows[i].ItemArray[1] + "</td>";
                cuerpo                                                  += "<td " + estilo + ">" + dtEstudiante.Rows[i].ItemArray[4] + " " + dtEstudiante.Rows[i].ItemArray[5] +
                                                                        " " + dtEstudiante.Rows[i].ItemArray[2] + " " + dtEstudiante.Rows[i].ItemArray[3] + "</td>";
            }
            int cont = 0;
            for (int j = 0; j < dtAsignacion.Rows.Count; j++)
            {
                cont++;
                if (i == 0)
                {
                    encabezado                                          += "<td bgcolor='#d6e3bc' " + estilo + ">" + dtAsignacion.Rows[j].ItemArray[7].ToString().Substring(0, 3) + "</td>";
                    if (cont == 1)
                    {
                        htmlmateria                                     += "<tr>";
                    }
                    htmlmateria                                         += "<td style = 'text-align:center'>" + dtAsignacion.Rows[j].ItemArray[7].ToString().Substring(0, 3) + " : " + dtAsignacion.Rows[j].ItemArray[7].ToString() + "</td>";
                    if (cont == 4)
                    {
                        htmlmateria                                     += "</tr>";
                        cont = 0;
                    }
                }

                dtvPromedio.RowFilter = "id_estudiante = " + Convert.ToInt64(dtEstudiante.Rows[i].ItemArray[0].ToString()) + " AND id_asignacion = " + int.Parse(dtAsignacion.Rows[j].ItemArray[0].ToString());
                tbl_Promedio.DataSource                                 = dtvPromedio;
                tbl_Promedio.DataBind();
                if (tbl_Promedio.Rows.Count == 1)
                {
                    if (decimal.Parse(tbl_Promedio.Rows[0].Cells[2].Text) <= objAnio_Escolar.rendimiento_bajo)
                    {
                        cuerpo                                              += "<td style = 'text-align:center; border: 1px solid #000;color:"+color+"'>" + tbl_Promedio.Rows[0].Cells[2].Text + "</td>";
                    }else{
                        cuerpo                                              += "<td " + estilo + ">" + tbl_Promedio.Rows[0].Cells[2].Text + "</td>";
                    }
                }
                else
                {
                    cuerpo                                              += "<td " + estilo + ">0,00</td>";
                }

            }



            for (int j = 0; j < dtAsignacion_Tecnica.Rows.Count; j++)
            {
                if (i == 0)
                {
                    encabezado                                          += "<td bgcolor='#d6e3bc' " + estilo + ">TE" + (j + 1) + "</td>";


                    if (cont != 0)
                    {
                        htmlmateria                                     += "<td style = 'text-align:center'>TE" + (j + 1) + ": " + dtAsignacion_Tecnica.Rows[j].ItemArray[7].ToString() + " </td>";
                        if (cont == 4)
                        {
                            htmlmateria                                 += "</tr>";
                            cont = 0;
                        }
                    }
                    else
                    {
                        if (cont == 1)
                        {
                            htmlmateria                                 = "<tr>";
                        }
                        htmlmateria                                     += "<td style = 'text-align:center'>TE" + (j + 1) + ": " + dtAsignacion_Tecnica.Rows[j].ItemArray[7].ToString() + " </td>";
                        if (cont == 4)
                        {
                            htmlmateria                                 += "</tr>";
                            cont = 0;
                        }
                    }
                    cont++;
                }


                dtvPromedio_Tecnica.RowFilter = "id_estudiante = " + Convert.ToInt64(dtEstudiante.Rows[i].ItemArray[0].ToString()) + " AND id_asignacion = " + int.Parse(dtAsignacion_Tecnica.Rows[j].ItemArray[0].ToString());
                tbl_Promedio.DataSource = dtvPromedio_Tecnica;
                tbl_Promedio.DataBind();
                if (tbl_Promedio.Rows.Count == 1)
                {
                    if (decimal.Parse(tbl_Promedio.Rows[0].Cells[2].Text) <= objAnio_Escolar.rendimiento_bajo)
                    {
                        cuerpo                                              += "<td style = 'text-align:center; border: 1px solid #000;color:"+color+"'>" + tbl_Promedio.Rows[0].Cells[2].Text + "</td>";
                    }else {
                        cuerpo                                              += "<td " + estilo + ">" + tbl_Promedio.Rows[0].Cells[2].Text + "</td>";
                    }
                }
                else
                {
                    cuerpo                                              += "<td " + estilo + ">0,00</td>";
                }
            }

            if (cont != 0)
            {
                int falta = 4 - cont;
                for (int k = 0; k < falta; k++)
                {
                    htmlmateria                                         += "<td></td>";
                }
                htmlmateria                                             += "</tr>";
            }

            cuerpo += "</tr>";
            if (i == 0)
            {
                encabezado                                              += "</tr>";

            }
        }
        htmlmateria                                                     += "</table>";
        html                                                            += encabezado + cuerpo;
        html                                                            += "</table>";
        clsFunciones.consolidado                                        = htmlencabezado + "<br>" +  htmlmateria + html;
        Response.RedirectToRoute("General", new { Modulo = "Reporte", Entidad = "Consolidado", Pagina = "Gestion" });
    
    }

    public int getMateriasPerdidas (DataView dtv_promedio,string id_estudiante,decimal nota_baja) {

        dtv_promedio.RowFilter                                          = "id_estudiante = " + id_estudiante;
        GridView tbl_Promedio                                           = new GridView();
        tbl_Promedio.DataSource                                         = dtv_promedio;
        tbl_Promedio.DataBind();
        int count                                                       = 0;
        for (int i = 0; i < tbl_Promedio.Rows.Count; i++)
        {
            if (decimal.Parse(tbl_Promedio.Rows[i].Cells[2].Text) <= nota_baja)
            {
                count ++;
            }
        }
      return count;
    }
}