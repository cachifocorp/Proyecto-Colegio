using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.fonts;
using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

            ///*Periodo*/
            //Anio_Escolar_Periodo objAnio_Escolar_Periodo = new Anio_Escolar_Periodo();
            //OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            //objAnio_Escolar_Periodo.id_anio_escolar = objAnio_Escolar.id;
            //clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo), ddlPeriodo);

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


    public String getpromedioGeneral(Int64 id_estudiante) {
        clsDb db = new clsDb();
        String SQL = "select id_estudiante,sum(promedio)/count(*) as promedioFinal from ( "+
                        "select id_estudiante,sum(promedio)/count(distinct d.id_asignacion) as promedio,idarea,descripcion "+
                        "from( SELECT "+
                         " c.id_estudiante, c.id_asignacion,ar.id as idarea,ar.descripcion, "+
                         " CAST ( ((SUM(c.valor * cc.porcentaje / 100.0))*(convert(float,asp.porcentaje)/10) )/(select nota_maxima from dbo.anio_escolar where estado='A') As decimal(18,2))  AS promedio " +
                         " FROM dbo.calificacion c "+
                         " INNER JOIN dbo.calificacion_configuracion cc   ON cc.id = c.id_calificacion_configuracion "+
                         " INNER JOIN dbo.asignacion a ON a.id = c.id_asignacion "+
                         " INNER JOIN dbo.materia m ON m.id = a.id_materia "+
                         " INNER JOIN dbo.estudiante e ON e.id = c.id_estudiante "+
                         " inner join dbo.anio_escolar_periodo asp on asp.id=cc.id_periodo "+
                         " INNER JOIN area ar   ON ar.id =m.id_area "+
                         "         WHERE id_estudiante = "+id_estudiante+" "+
                         "         AND cc.id_periodo    <= 4 "+
                         "         AND cc.estado = 'A' "+
                         "         AND c.estado = 'A' "+
                         "         AND e.estado = 'A'  "+
                         "         GROUP BY c.id_estudiante,c.id_asignacion "+
                         "         ,M.descripcion "+
                         "         ,asp.porcentaje, "+
                         " ar.id "+
                         " ,ar.descripcion "+
                         " ) d "+
                         " group by id_estudiante,idarea,descripcion "+
                         " ) as f "+
                         " group by id_estudiante";
        String calificacion="";
        try
        { 
            SqlConnection con = db.conexion();
            con.Open();
            SqlCommand com = new SqlCommand(SQL, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                calificacion = Convert.ToDecimal(reader["promedioFinal"].ToString()).ToString("#.##");
            }
            con.Close();
            return calificacion;
        }
        catch (Exception ex)
        {
            return calificacion;
        }
    
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

        int periodo = 4;

        MemoryStream ms = new MemoryStream();
        Document document = new Document(PageSize.EXECUTIVE, 10f, 10f, 10f, 10f);
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
                    texto.Add("INSTITUTO TÉCNICO INDUSTRIAL FRANCISCO DE PAULA SANTANDER \n\nwww.itipuentenacional.edu.co \n\nINFORME ACADÉMICO FINAL 2014");
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

                    float[] porce = { 15f, 45f, 15f, 15f, 15f};
                    PdfPTable tbheader = new PdfPTable(porce);
                    tbheader.WidthPercentage = 100f;
                    String[] celdasEncabezado = { "CURSO", "ESTUDIANTE", "DOCUMENTO", "PROMEDIO GENERAL", "ESCALA NACIONAL" };

                    for (int i = 0; i < celdasEncabezado.Length; i++)
                    {
                        agregarCelda(celdasEncabezado[i], tbheader);
                    }

                    /*CURSO*/
                    agregarCelda(ddlSalon.SelectedItem.Text, tbheader);

                    /*ESTUDIANTE*/
                    var nombre_completo = dts_Matricula.Rows[0].ItemArray[7].ToString() + " "
                        + dts_Matricula.Rows[0].ItemArray[6].ToString();
                    agregarCelda(nombre_completo, tbheader);

                    /*DOCUMENTO*/
                    agregarCelda(Int64.Parse(row.Cells[1].Text).ToString(), tbheader);

                    /*PROMEDIO GENERAL*/
                    Calificacion objPromedioGeneral = new Calificacion();
                    objPromedioGeneral.id_asignacion = int.Parse(dts_Matricula.Rows[0].ItemArray[2].ToString());
                    objPromedioGeneral.id_calificacion_configuracion = periodo;
                    DataView promedio_general = objOperCalificacion.ConsultarPromedio_General(objPromedioGeneral).DefaultView;
                    promedio_general.RowFilter = "id_estudiante="+ row.Cells[1].Text;
                    GridView tbl_promedio_general = new GridView();
                    tbl_promedio_general.DataSource = promedio_general;
                    tbl_promedio_general.DataBind();
                    decimal promedio_g = 0;
                    //promedio periodo
                    Calificacion objCalificacion_Promedio = new Calificacion();
                    objCalificacion_Promedio.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    objCalificacion_Promedio.id_asignacion = periodo;
                    DataView dtvPromedio_Acumulado = objOperCalificacion.ConsultarPromedio_PeriodoAcumulado(objCalificacion_Promedio).DefaultView;
                    String cal = "";
                    cal = getpromedioGeneral(Int64.Parse(dts_Matricula.Rows[0].ItemArray[1].ToString()));
                    if (cal.Length>0)
                    {
                        agregarCelda(String.Format("{0:f2}", cal), tbheader);
                    }else {
                        agregarCelda("0,00", tbheader);
                    }  

                    //if (tbl_promedio_general.Rows.Count > 0)
                    //{
                    //    promedio_g = decimal.Parse(tbl_promedio_general.Rows[0].Cells[2].Text);
                    //    agregarCelda(String.Format("{0:f2}", promedio_g), tbheader);
                    //}
                    //else {
                    //    agregarCelda("0,00", tbheader);
                    //}                     

                    /*ESCALA NACIONAL*/
                    agregarCelda(getDesempeño(Convert.ToDecimal(cal)), tbheader);


                    tbheader.SpacingBefore = 10;
                    document.Add(tabla);
                    document.Add(tbheader);


                    tbheader.Rows.Clear();
                    tabla.Rows.Clear();

                    tbheader.SetWidths(new float[] {10,10,10,10,10 });
                    agregarCeldaCol("", tbheader,5);
                    
                    document.Add(tbheader);

                    //Tabla del body
                    tbheader = new PdfPTable(8);
                    float[] medidas = { 25f,5f, 5f, 5f, 5f,10f,10f,10f };
                    tbheader.SetWidths(medidas);
                    tbheader.WidthPercentage = 100f;
                   

                    String[] celdasTabla = { "ÁREA", "VALORACIÓN POR PERIODO",
                                               "DEFINITIVA", "DESEMPEÑO", "FALLAS ACOMULADAS"};

                    agregarCeldaRow(celdasTabla[0],tbheader,2);
                    agregarCeldaCol(celdasTabla[1], tbheader,4);
                    agregarCeldaRow(celdasTabla[2], tbheader, 2);
                    agregarCeldaRow(celdasTabla[3], tbheader, 2);
                    agregarCeldaRow(celdasTabla[4], tbheader, 2);
                    agregarCelda("1", tbheader, 14083004);
                    agregarCelda("2", tbheader, 14083004);
                    agregarCelda("3", tbheader, 14083004);
                    agregarCelda("4", tbheader, 14083004    );

                    document.Add(tbheader);
                    tbheader.Rows.Clear();

                    //Agregamos los campos de datos de la tabla 
                    objAsignacion.id_salon = int.Parse(ddlSalon.SelectedValue.ToString());
                    DataTable dts = objOperAsignacion.ConsultarAsignacion(objAsignacion);

                    objAsignacion.id_salon = int.Parse(dts_Matricula.Rows[0].ItemArray[8].ToString());
                    DataTable tecnica = objOperAsignacion.ConsultarAsignacion(objAsignacion);
                    dts.Merge(tecnica);

                    DataView dtv_Asignacion = dts.DefaultView;
                    dtv_Asignacion.Sort = "areadescripcion ASC";

                    GridView tbl_Asignacion = new GridView();
                    tbl_Asignacion.DataSource = dtv_Asignacion;
                    tbl_Asignacion.DataBind();

                    //Datos    
                   
                    GridView tbl_Promedio_Acumulado = new GridView();
                    double promedio_acumulado = 0;
                    Asistencia objAsistencia_2 = new Asistencia();
                    objAsistencia_2.id_estudiante = Int64.Parse(row.Cells[1].Text);
                    DataView dtvAsistencia_Total = objOperAsistencia.ConsultarAsistencia(objAsistencia_2).DefaultView;
                    GridView tbl_Asistencia_Total = new GridView();
                    int asistencia_total = 0;
                    List<decimal> calificacionArea = new List<decimal>();
                    int cont = 0;
                    foreach (GridViewRow dr in tbl_Asignacion.Rows )
                    {
                        Calificacion objCalificacion_Promedios = new Calificacion();
                        objCalificacion_Promedio.id_estudiante = Int64.Parse(row.Cells[1].Text);
                        String Spromedio_acumulado = "";                     
                        dtvAsistencia_Total.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                        tbl_Asistencia_Total.DataSource = dtvAsistencia_Total;
                        tbl_Asistencia_Total.DataBind();

                        if (int.Parse(dr.Cells[16].Text) == 11 || int.Parse(dr.Cells[16].Text) == 12)
                        {
                            cont++;
                            if (cont <= 1)
                            {
                               
                                agregarCeldaCol(HttpUtility.HtmlDecode(dr.Cells[15].Text) + "\n\n\n", tbheader, 5);

                                DataView promedioarea = dtvPromedio_Acumulado;
                                GridView proma = new GridView();
                                //promedioarea.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                                promedioarea.RowFilter = "idarea = " + dr.Cells[16].Text;
                                proma.DataSource = promedioarea;
                                proma.DataBind();
                                double promca = 0;
                                for (int h = 0; h < proma.Rows.Count; h++)
                                {
                                    promca += double.Parse(proma.Rows[h].Cells[2].Text);
                                }
                                double totalprom = (promca / proma.Rows.Count);
                                agregarCelda(String.Format("{0:f2}", totalprom ), tbheader);
                                agregarCelda(getDesempeño(Convert.ToDecimal(totalprom)), tbheader);
                                agregarCelda("", tbheader);

                                calificacionArea.Add(Convert.ToDecimal(totalprom));

                            }

                            agregarCelda(HttpUtility.HtmlDecode(dr.Cells[7].Text) + "\n\n\n", tbheader, 14085004);

                        }
                        else {
                            if (cont>0)
                            {
                                agregarCeldaCol("\n", tbheader, 8);
                            }
                            cont = 0;
                            agregarCelda(HttpUtility.HtmlDecode(dr.Cells[7].Text) + "\n\n\n", tbheader, 14083004);
                        }
                        
                            
                        
                        
                        
                        for (int i = 1; i <= 4; i++)
                        {
                            objCalificacion_Promedio.id_asignacion = i;
                            DataView dtvPromedio = objOperCalificacion.ConsultarPromedio_Periodo(objCalificacion_Promedio).DefaultView;                        
                            
                            dtvPromedio.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                            GridView tbl_Promedio = new GridView();
                            
                            tbl_Promedio.DataSource = dtvPromedio;
                            tbl_Promedio.DataBind();
                            if (tbl_Promedio.Rows.Count > 0)
                            {
                                agregarCelda(tbl_Promedio.Rows[0].Cells[2].Text, tbheader);
                            }
                            else {
                                agregarCelda("0,0", tbheader);
                            }
                        }

                        dtvPromedio_Acumulado.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text);
                        tbl_Promedio_Acumulado.DataSource = dtvPromedio_Acumulado;
                        tbl_Promedio_Acumulado.DataBind();

                        for (int j = 0; j < tbl_Promedio_Acumulado.Rows.Count; j++)
                        {
                            promedio_acumulado += double.Parse(tbl_Promedio_Acumulado.Rows[j].Cells[2].Text);

                        }
                        double valor =  promedio_acumulado;
                                if (int.Parse(dr.Cells[16].Text) == 11 || int.Parse(dr.Cells[16].Text) == 12)
                                {

                                }
                                else {
                                    calificacionArea.Add(Convert.ToDecimal(valor));
                                }
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
                        
                        agregarCelda(valor+"", tbheader, 14083004);
                        agregarCelda(getDesempeño(Convert.ToDecimal(valor)) + "", tbheader);

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
                        agregarCelda("" + asistencia_total, tbheader);
                       
                    }

                    

                    document.Add(tbheader);
                    tbheader.Rows.Clear();
                    document.Add(new Paragraph(new Phrase(promovido(calificacionArea.ToArray()), fontTinyItalic)) { Alignment = Element.ALIGN_CENTER });
                    calificacionArea.Clear();
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
        Response.AddHeader("content-disposition", "attachment;filename=BoletinFinal.pdf");
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

    public String promovido(decimal[] notas)
    {
        int cont = 0;
        for (int i = 0; i < notas.Length; i++)
        {
            if (notas[i]< Convert.ToDecimal(6.0))
            {
                cont++;
            }
        }

        if (cont <= 2)
        {
            return "PROMOVIDO";
        }
        else {
            return "NO PROMOVIDO";
        }
    }
}