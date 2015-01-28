using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.fonts;
using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Boletin_Busqueda : System.Web.UI.Page
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

            Anio_Escolar objAnio_Escolar = (Anio_Escolar)Session["anioEscolar"];

            /*Periodo*/
            Anio_Escolar_Periodo objAnio_Escolar_Periodo = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAnio_Escolar_Periodo.id_anio_escolar = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo), ddlPeriodo);

            /*Salones*/

            Asignacion objAsignacion = new Asignacion();
            OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.tecnica = 1;

            ddlSalon.DataValueField = "id_salon";
            ddlSalon.DataTextField = "salon";
            ddlSalon.DataSource = objOperAsignacion.ConsultarAsignacion(objAsignacion).DefaultView.ToTable(true, "id_salon", "salon");
            ddlSalon.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void vertbl_Estudiante()
    {
        try
        {
            Asignacion objAsignacion = new Asignacion();
            OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_salon = int.Parse(ddlSalon.SelectedValue.ToString());
            DataTable dt = objOperAsignacion.ConsultarEstudiante(objAsignacion);
            dt.Columns.Add("nombre_completo", typeof(string), "apellido_1 + ' ' + apellido_2 + ' ' + nombre_1 + ' ' + nombre_2");
            tbl_Estudiante.DataSource = dt;
            tbl_Estudiante.DataBind();
        }
        catch (Exception)
        {
        }
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Estudiante();
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        generarBoletin();
    }

    public void generarBoletin()
    {

        /*Consultamos la matricula*/
        Matricula objMatricula = new Matricula();
        OperacionMatricula objOperMatricula = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Asignacion objAsignacion = new Asignacion();
        OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Asistencia objAsistencia = new Asistencia();
        OperacionAsistencia objOperAsistencia = new OperacionAsistencia(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Indicador objIndicador = new Indicador();
        OperacionIndicador objOperIndicador = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Calificacion objCalificacion = new Calificacion();
        OperacionCalificacion objOperCalificacion = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Calificacion_Configuracion objConfiguracion_Calificacion = new Calificacion_Configuracion();
        OperacionCalificacion_Configuracion objOperConfiguracion_Calificacion = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Salon objSalon = new Salon();
        OperacionSalon objOperSalon = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);



        MemoryStream ms = new MemoryStream();
        Document document = new Document(PageSize.LEGAL, 10f, 10f, 10f, 10f);
        PdfWriter writer = PdfWriter.GetInstance(document, ms);

        document.Open();


        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        Font fontEncabezado = FontFactory.GetFont("Arial", 12);


        float[] porcentajes = { 15f, 45f, 10f, 10f, 10f, 10f };



        foreach (GridViewRow row in tbl_Estudiante.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkEstudiante") as CheckBox);
                if (chkRow.Checked)
                {

                    objMatricula.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    DataTable dts_Matricula = objOperMatricula.ConsultarMatricula(objMatricula);

                    PdfPTable tabla = new PdfPTable(porcentajes);
                    tabla.WidthPercentage = 100f;
                    PdfPCell celda;
                    Phrase texto;

                    celda = new PdfPCell();
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Uri("http://academico.itipuentenacional.edu.co/img/logo.png"));
                    image.ScaleAbsolute(40f, 40f);
                    image.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    celda.Image = image;
                    celda.Border = Rectangle.NO_BORDER;
                    tabla.AddCell(celda);

                    celda = new PdfPCell();
                    celda.Colspan = 4;
                    texto = new Phrase();
                    texto.Font = fontEncabezado;
                    texto.Add("INSTITUTO TÉCNICO INDUSTRIAL FRANCISCO DE PAULA SANTANDER \n\nwww.itipuentenacional.edu.co \n\nINFORME ACADÉMICO 2014");
                    celda.Phrase = texto;
                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = Rectangle.NO_BORDER;
                    tabla.AddCell(celda);

                    celda = new PdfPCell();
                    iTextSharp.text.Image image_estudiante = iTextSharp.text.Image.GetInstance(new Uri("http://academico.itipuentenacional.edu.co/" + dts_Matricula.Rows[0].ItemArray[10].ToString().Replace("~/", "")));
                    image.ScaleAbsolute(10f, 10f);
                    //image.ScalePercent(10f);
                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Image = image_estudiante;
                    celda.AddElement(texto);
                    celda.Border = Rectangle.NO_BORDER;
                    tabla.AddCell(celda);

                    String[] celdasEncabezado = { "CURSO", "ESTUDIANTE", "DOCUMENTO", "PROMEDIO GENERAL", "ESCALA NACIONAL", "PERIODO" };

                    for (int i = 0; i < celdasEncabezado.Length; i++)
                    {
                        agregarCelda(celdasEncabezado[i], tabla);
                    }

                    /*CURSO*/
                    agregarCelda(ddlSalon.SelectedItem.Text, tabla);

                    /*ESTUDIANTE*/
                    var nombre_completo = dts_Matricula.Rows[0].ItemArray[7].ToString() + " "
                        + dts_Matricula.Rows[0].ItemArray[6].ToString();
                    agregarCelda(nombre_completo, tabla);

                    /*DOCUMENTO*/
                    agregarCelda(Int64.Parse(row.Cells[1].Text).ToString(), tabla);

                    /*PROMEDIO GENERAL*/
                    Calificacion objPromedioGeneral = new Calificacion();
                    objPromedioGeneral.id_asignacion = int.Parse(dts_Matricula.Rows[0].ItemArray[2].ToString());
                    objPromedioGeneral.id_calificacion_configuracion = int.Parse(ddlPeriodo.SelectedValue.ToString());
                    DataView promedio_general = objOperCalificacion.ConsultarPromedio_General(objPromedioGeneral).DefaultView;
                    promedio_general.RowFilter = "id_estudiante="+ row.Cells[1].Text;
                    GridView tbl_promedio_general = new GridView();
                    tbl_promedio_general.DataSource = promedio_general;
                    tbl_promedio_general.DataBind();
                    decimal promedio_g = 0;
                    if (tbl_promedio_general.Rows.Count > 0)
                    {
                        promedio_g = decimal.Parse(tbl_promedio_general.Rows[0].Cells[2].Text);
                        agregarCelda(String.Format("{0:f2}", promedio_g), tabla);
                    }
                    else {
                        agregarCelda("0,00", tabla);
                    }

                    


                    /*ESCALA NACIONAL*/
                    agregarCelda(getDesempeño(promedio_g), tabla);

                    /*PERIODO*/
                    agregarCelda(ddlPeriodo.SelectedItem.Text, tabla);

                    tabla.SpacingBefore = 10;
                    document.Add(tabla);

                    tabla.Rows.Clear();


                    objAsignacion.id_salon = int.Parse(ddlSalon.SelectedValue.ToString());
                    DataTable dts = objOperAsignacion.ConsultarAsignacion(objAsignacion);

                    objAsignacion.id_salon = int.Parse(dts_Matricula.Rows[0].ItemArray[8].ToString());
                    DataTable tecnica = objOperAsignacion.ConsultarAsignacion(objAsignacion);
                    dts.Merge(tecnica);

                    DataView dtv_Asignacion = dts.DefaultView;
                    dtv_Asignacion.Sort = "orden_impresion ASC";

                    GridView tbl_Asignacion = new GridView();
                    tbl_Asignacion.DataSource = dtv_Asignacion;
                    tbl_Asignacion.DataBind();


                    objAsistencia.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    objAsistencia.id_periodo = int.Parse(ddlPeriodo.SelectedValue.ToString());
                    DataView dtvAsistencia = objOperAsistencia.ConsultarAsistencia(objAsistencia).DefaultView;

                    objConfiguracion_Calificacion.id_periodo = int.Parse(ddlPeriodo.SelectedValue.ToString());
                    DataTable dta_Configuracion_Calificacion = objOperConfiguracion_Calificacion.ConsultarCalificacion_Configuracion(objConfiguracion_Calificacion);

                    objIndicador.id_grado = int.Parse(dts.Rows[0].ItemArray[11].ToString());
                    objIndicador.id_anio_escolar_periodo = int.Parse(ddlPeriodo.SelectedValue.ToString());
                    DataView dtvIndicador = objOperIndicador.ConsultarIndicador(objIndicador).DefaultView;
                    GridView tbl_Indicador = new GridView();

                    objCalificacion.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    DataView dtvCalificacion = objOperCalificacion.ConsultarCalificacion(objCalificacion).DefaultView;
                    GridView tbl_Calificacion = new GridView();
                    Calificacion objCalificacion_Promedio = new Calificacion();
                    objCalificacion_Promedio.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    objCalificacion_Promedio.id_asignacion = int.Parse(ddlPeriodo.SelectedValue.ToString());
                    DataView dtvPromedio = objOperCalificacion.ConsultarPromedio_Periodo(objCalificacion_Promedio).DefaultView;
                    DataView dtvPromedio_Acumulado = objOperCalificacion.ConsultarPromedio_PeriodoAcumulado(objCalificacion_Promedio).DefaultView;
                    double promedio_acumulado = 0;
                    GridView tbl_Promedio = new GridView();
                    GridView tbl_Promedio_Acumulado = new GridView();
                    Asistencia objAsistencia_2 = new Asistencia();
                    objAsistencia_2.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    DataView dtvAsistencia_Total = objOperAsistencia.ConsultarAsistencia(objAsistencia_2).DefaultView;
                    GridView tbl_Asistencia_Total = new GridView();
                    int asistencia_total = 0;
                    /*Tenemos que recorrer las notas solo esta el sabeer*/
                    foreach (GridViewRow dr in tbl_Asignacion.Rows)
                    {
                        dtvAsistencia.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                        dtvAsistencia_Total.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                        tbl_Asistencia_Total.DataSource = dtvAsistencia_Total;
                        tbl_Asistencia_Total.DataBind();

                        GridView tbl_Asistencia = new GridView();
                        tbl_Asistencia.DataSource = dtvAsistencia;
                        tbl_Asistencia.DataBind();
                        agregarCelda(HttpUtility.HtmlDecode(dr.Cells[7].Text), tabla, 14083004);
                        agregarCelda("DOCENTE: " + HttpUtility.HtmlDecode(dr.Cells[10].Text), tabla, 14083004);
                        agregarCelda("VALORIZACIÓN", tabla, 14083004);
                        agregarCelda("DEFINITIVA PERIODO", tabla, 14083004);
                        agregarCelda("DEFINITIVA ACUMULADO", tabla, 14083004);
                        agregarCelda("FALLAS PERIODO", tabla, 14083004);

                        String saber = "";
                        String indicador = "";
                        String calificacion = "";
                        String promedio = "";
                        String Spromedio_acumulado = "";
                        String asistencia = "";
                        for (int i = 0; i < dta_Configuracion_Calificacion.Rows.Count; i++)
                        {

                            dtvIndicador.RowFilter = "id_materia =" + dr.Cells[2].Text
                                                                                                    + " AND id_saber = " + int.Parse(dta_Configuracion_Calificacion.Rows[i].ItemArray[0].ToString());
                            tbl_Indicador.DataSource = dtvIndicador;
                            tbl_Indicador.DataBind();

                            dtvCalificacion.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text) +
                                                                                                      " AND id_calificacion_configuracion = " + dta_Configuracion_Calificacion.Rows[i].ItemArray[0].ToString();
                            saber = dta_Configuracion_Calificacion.Rows[i].ItemArray[1].ToString();

                            /*Agregamos indicadores*/
                            if (tbl_Indicador.Rows.Count == 1)
                            {
                                indicador = HttpUtility.HtmlDecode(tbl_Indicador.Rows[0].Cells[1].Text);
                            }
                            else
                            {
                                indicador = "";
                            }

                            /*Agregamos calificaciones*/
                            tbl_Calificacion.DataSource = dtvCalificacion;
                            tbl_Calificacion.DataBind();
                            if (tbl_Calificacion.Rows.Count == 1)
                            {
                                calificacion = tbl_Calificacion.Rows[0].Cells[5].Text;
                            }
                            else
                            {
                                calificacion = "0,00";
                            }

                            dtvPromedio.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                            tbl_Promedio.DataSource = dtvPromedio;
                            tbl_Promedio.DataBind();
                            if (tbl_Promedio.Rows.Count == 1)
                            {
                                promedio = tbl_Promedio.Rows[0].Cells[2].Text;
                            }
                            else
                            {
                                promedio = "0,00";
                            }

                            dtvPromedio_Acumulado.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                            tbl_Promedio_Acumulado.DataSource = dtvPromedio_Acumulado;
                            tbl_Promedio_Acumulado.DataBind();

                            for (int j = 0; j < tbl_Promedio_Acumulado.Rows.Count; j++)
                            {
                                promedio_acumulado += double.Parse(tbl_Promedio_Acumulado.Rows[j].Cells[2].Text);
                                
                            }
                            double valor = promedio_acumulado;
                            if (valor > 0)
                            {
                                Spromedio_acumulado = String.Format("{0:f2}", valor);

                            }
                            else
                            {
                                Spromedio_acumulado = "0,00";
                                valor = 0;
                            }
                            promedio_acumulado = 0;
                            if (tbl_Asistencia.Rows.Count > 0)
                            {
                                asistencia = tbl_Asistencia.Rows[0].Cells[4].Text;
                            }
                            else
                            {
                                asistencia = "0";
                            }

                            if (i != (dta_Configuracion_Calificacion.Rows.Count - 1))
                            {

                                agregarCelda(saber, tabla);
                                agregarCeldaJustificado(indicador, tabla);
                                agregarCelda(calificacion, tabla);

                            }
                            else
                            {
                                agregarCeldaRow(saber, tabla, 2);
                                agregarCeldaRowJustificado(indicador, tabla, 2);
                                agregarCeldaRow(calificacion, tabla, 2);
                                for (int k = 0; k < 2; k++)
                                {
                                    if (k == 0)
                                    {
                                        agregarCelda("DESEMPEÑO PERIODO", tabla, 14083004);
                                        agregarCelda("DESEMPEÑO ACUMULADO", tabla, 14083004);
                                        agregarCelda("FALLAS ACUMULADAS", tabla, 14083004);
                                    }
                                    else
                                    {
                                        agregarCelda(getDesempeño(Convert.ToDecimal(promedio)), tabla);
                                        agregarCelda(getDesempeño(Convert.ToDecimal(valor)), tabla);
                                        if (tbl_Asistencia_Total.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < tbl_Asistencia_Total.Rows.Count; j++)
                                            {
                                                asistencia_total += int.Parse(tbl_Asistencia_Total.Rows[j].Cells[4].Text);
                                            }

                                        }
                                        else
                                        {
                                            asistencia_total = 0;
                                        }
                                        agregarCelda("" + asistencia_total, tabla);

                                    }

                                }
                            }

                            if (i == 0)
                            {
                                agregarCeldaRow(promedio, tabla, 2);
                                agregarCeldaRow(Spromedio_acumulado, tabla, 2);
                                agregarCeldaRow(asistencia, tabla, 2);
                            }
                        }
                    }

                    document.Add(tabla);
                    if (tbl_promedio_general.Rows.Count > 0)
                    {
                        document.Add(new Paragraph(new Phrase("EL ESTUDIANTE HA OCUPADO EL PUESTO " + tbl_promedio_general.Rows[0].Cells[0].Text, fontTinyItalic)) { Alignment = Element.ALIGN_CENTER });
                    }
                    tabla = new PdfPTable(1);
                    tabla.SpacingBefore = 40f;
                    objSalon.id = int.Parse(ddlSalon.SelectedValue.ToString());
                    Phrase director = new Phrase(objOperSalon.ConsultarSalon(objSalon).Rows[0].ItemArray[8].ToString(), fontTinyItalic);
                    celda.Phrase = director;
                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.TOP_BORDER;
                    tabla.AddCell(celda);
                    document.Add(tabla);
                    document.NewPage();
                }
            }
        }
        document.Close();
        writer.Close();
        ms.Close();
        Response.ContentType = "pdf/application";
        Response.AddHeader("content-disposition", "attachment;filename=Boletin.pdf");
        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
    }

    public void agregarCelda(String valor, PdfPTable tabla)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public void agregarCeldaJustificado(String valor, PdfPTable tabla)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public void agregarCelda(String valor, PdfPTable tabla, int color)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.BackgroundColor = new BaseColor(color);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public void agregarCeldaCol(String valor, PdfPTable tabla, int colspan)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        celda.Colspan = colspan;
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public void agregarCeldaRow(String valor, PdfPTable tabla, int rowspan)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        celda.Rowspan = rowspan;
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public void agregarCeldaRowJustificado(String valor, PdfPTable tabla, int rowspan)
    {
        Font fontTinyItalic = FontFactory.GetFont("Arial", 7);
        PdfPCell celda = new PdfPCell();
        celda.Rowspan = rowspan;
        Phrase texto = new Phrase(valor, fontTinyItalic);
        celda.Phrase = texto;
        celda.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
        celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        tabla.AddCell(celda);
    }

    public String getDesempeño(decimal valor)
    {
        String desempeño = "";
        Anio_Escolar objAnio_Escolar = (Anio_Escolar)Session["anioEscolar"];
        if (valor <= objAnio_Escolar.rendimiento_bajo)
        {
            desempeño = "BAJO";
        }
        else if (valor <= objAnio_Escolar.rendimiento_basico)
        {
            desempeño = "BÁSICO";
        }
        else if (valor <= objAnio_Escolar.rendimiento_alto)
        {
            desempeño = "ALTO";
        }
        else
        {
            desempeño = "SUPERIOR";
        }
        return desempeño;
    }

}