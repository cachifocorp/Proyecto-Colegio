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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;
using System.Data.OleDb;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Drawing;

public partial class Proceso_Calificacion_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.vertbl_Calificacion();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportar);
    }

    public void vertbl_Calificacion()
    {
        try
        {
            /*Asignación*/
            
            Anio_Escolar objAnio_Escolar                                                    = (Anio_Escolar)Session["anioEscolar"];

            /*Periodo*/
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                                    = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo                       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Periodo                                                            = new GridView();
            objAnio_Escolar_Periodo.id_anio_escolar                                         = objAnio_Escolar.id;
            tbl_Periodo.DataSource                                                          = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);
            tbl_Periodo.DataBind();
            DateTime inicio                                                                 = DateTime.Parse(tbl_Periodo.Rows[0].Cells[3].Text);
            DateTime fin                                                                    = DateTime.Parse(tbl_Periodo.Rows[0].Cells[4].Text);
            DateTime fecha_fin_calificacion                                                 = DateTime.Parse(tbl_Periodo.Rows[0].Cells[5].Text);
            DateTime actual                                                                 = DateTime.Parse(tbl_Periodo.Rows[0].Cells[9].Text);

            if ((fecha_fin_calificacion >= actual) && (actual > inicio && fin > actual))
            {
                tbl_Calificacion.Columns.Clear();
                //tbl_Calificacion.Caption                                                        = "Calificaciones Periodo " + tbl_Periodo.Rows[0].Cells[1].Text + " Año " + objAnio_Escolar.descripcion;
                
                Asignacion objAsignacion                                                        = new Asignacion();
                OperacionAsignacion objOperAsignacion                                           = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Asignacion                                                         = new GridView();
                objAsignacion.id                                                                = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                tbl_Asignacion.DataSource                                                       = objOperAsignacion.ConsultarAsignacion(objAsignacion);
                tbl_Asignacion.DataBind();

                /*Configuración Calificaciones*/
                DataTable tbl_Estudiante                                                        = new DataTable();
                DataTable tbl_Calificaciones                                                    = new DataTable();
                Calificacion_Configuracion objConfiguracionCalificacion                         = new Calificacion_Configuracion();
                OperacionCalificacion_Configuracion objOperCalificacion_Configuracion           = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_configuracion                                                      = new GridView();
                objConfiguracionCalificacion.id_periodo                                         = int.Parse(tbl_Periodo.Rows[0].Cells[0].Text);
                tbl_configuracion.DataSource                                                    = objOperCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objConfiguracionCalificacion);
                tbl_configuracion.DataBind();

                /*Asistencia*/
                Asistencia objAsistencia                                                        = new Asistencia();
                OperacionAsistencia objOperAsistencia                                           = new OperacionAsistencia(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Asistencia                                                         = new GridView();
                objAsistencia.id_asignacion                                                     = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                objAsistencia.id_periodo                                                        = int.Parse(tbl_Periodo.Rows[0].Cells[0].Text);
               /* tbl_Asistencia.DataSource                                                       = objOperAsistencia.ConsultarAsistencia(objAsistencia);
                tbl_Asistencia.DataBind();*/
                DataView dtv_Asistencia                                                         = objOperAsistencia.ConsultarAsistencia(objAsistencia).DefaultView;

                /*Construcción de Campos de Datatables y Gridview*/
                tbl_Calificaciones.Columns.Add("ESTUDIANTE");
                tbl_Calificaciones.Columns.Add("DOCUMENTO NÚMERO");
                tbl_Calificaciones.Columns.Add("NOMBRE ESTUDIANTE");
                agregarItemTemplate("ESTUDIANTE", "ESTUDIANTE", "Imagen", "0", "span2");
                agregarItemTemplate("DOCUMENTO NÚMERO", "DOCUMENTO NÚMERO", "Label", "0", "span2");
                agregarItemTemplate("NOMBRE ESTUDIANTE", "NOMBRE ESTUDIANTE", "Label", "0", "span6");

                for (int i = 0; i < tbl_configuracion.Rows.Count; i++)
                {
                    string header                                                               = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text) + " <br> (" + tbl_configuracion.Rows[i].Cells[3].Text + ")%");
                    string eval                                                                 = HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text);
                    agregarItemTemplate(header, eval, "Textbox", tbl_configuracion.Rows[i].Cells[3].Text + " " + tbl_configuracion.Rows[i].Cells[0].Text, "span1");
                    tbl_Calificaciones.Columns.Add(HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text));
                }

                tbl_Calificaciones.Columns.Add("ASISTENCIA");
                tbl_Calificaciones.Columns.Add("PROMEDIO PERIODO");
                agregarItemTemplate("ASISTENCIA", "ASISTENCIA", "Textbox", "0", "span1");
                agregarItemTemplate("PROMEDIO PERIODO", "PROMEDIO PERIODO", "Label", "Periodo", "span1");

                /*Llenado de datos del gridview*/
                tbl_Estudiante                                                                  = objOperAsignacion.ConsultarEstudiante(objAsignacion);
                double promedio_periodo                                                         = 0;
                Calificacion objCalificacion                                                    = new Calificacion();
                OperacionCalificacion objOperCalificacion                                       = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objCalificacion.id_asignacion                                                   = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                DataView dtv_Notas                                                              = objOperCalificacion.ConsultarCalificacion(objCalificacion).DefaultView;
                foreach (DataRow dr in tbl_Estudiante.Rows)
                {
                    /*Llenadado del datarow de los estudiantes con el úumero de documento
                     y el nombre del estudiante*/
                    DataRow dtw                                                                 = tbl_Calificaciones.NewRow();
                    dtw["ESTUDIANTE"]                                                           = ResolveUrl(dr.ItemArray[6].ToString());
                    dtw["DOCUMENTO NÚMERO"]                                                     = dr.ItemArray[1];
                    dtw["NOMBRE ESTUDIANTE"]                                                    = dr.ItemArray[4] + " " + dr.ItemArray[5] + " " + dr.ItemArray[2] + " " + dr.ItemArray[3] ;

                    /*Consulta de las calificaciones del estudiante que ya se encuentran almacenadas*/

                    for (int i = 0; i < tbl_configuracion.Rows.Count; i++)
                    {
                        /*Filtramos las notas por el tipo de calificación*/
                        dtv_Notas.RowFilter                                                     = "id_estudiante = " + Convert.ToInt64(dr.ItemArray[0].ToString()) + " AND id_calificacion_configuracion = " + int.Parse(tbl_configuracion.Rows[i].Cells[0].Text);
                    
                        /*Llenado del datarow con las notas del estudiante 
                         * observacion: el indice del dataview es el nombre de la nota
                         y calculamos el promedio apartir de las notas almacenadas*/
                        GridView tbl_Notas_Filtradas                                            = new GridView();
                        tbl_Notas_Filtradas.DataSource                                          = dtv_Notas;
                        tbl_Notas_Filtradas.DataBind();
                        if (tbl_Notas_Filtradas.Rows.Count > 0)
                        {
                            dtw[HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text)] = Decimal.Parse(tbl_Notas_Filtradas.Rows[0].Cells[5].Text);
                            promedio_periodo                                                    += double.Parse(tbl_Notas_Filtradas.Rows[0].Cells[5].Text) * (double.Parse(tbl_configuracion.Rows[i].Cells[3].Text) / 100);
                        }
                        else
                        {
                            dtw[HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text)] = "";
                        }
                    }


                    dtv_Asistencia.RowFilter                                                    = "estudiante = " + dr.ItemArray[1];
                    GridView tbl_Asistencia_Estudiante                                          = new GridView();
                    tbl_Asistencia_Estudiante.DataSource                                        = dtv_Asistencia;
                    tbl_Asistencia_Estudiante.DataBind();
                    if (tbl_Asistencia_Estudiante.Rows.Count > 0){
                        dtw["ASISTENCIA"]                                                       = tbl_Asistencia_Estudiante.Rows[0].Cells[4].Text; 
                    }else {
                        dtw["ASISTENCIA"]                                                       = ""; 
                    }
                    /*Formato del promedio del semestre a dos decimales y agregamos el
                     datarow al datatable*/
                    dtw["PROMEDIO PERIODO"]                                                     = String.Format("{0:f2}", promedio_periodo);
                    promedio_periodo                                                            = 0;
                    tbl_Calificaciones.Rows.Add(dtw);
                }
            
                /*Enlazamos el datatable = tbl_Calificaciones al
                 Gridview = tbl_Calificacion*/
                tbl_Calificacion.DataSource                                                     = tbl_Calificaciones;
                tbl_Calificacion.DataBind();
        }
        else {
            ShowNotification("Calificaciones","El fecha máxima para subir las calificaciones ha caducado","info");
        }
        }
        catch (Exception) { }
    }

    public void agregarItemTemplate(string header, string eval, string control, string cssclass, string spanheader)
    {
        var textbox                         = new TemplateField();
        textbox.HeaderText                  = header;
        textbox.HeaderStyle.CssClass        = spanheader;
        textbox.ItemTemplate                = new TextColumn(eval, control, cssclass);
        tbl_Calificacion.Columns.Add(textbox);
    }

    public string getTitulo () {
        try
        {
            string titulo  = "";
            Anio_Escolar objAnio_Escolar                                    = (Anio_Escolar)Session["anioEscolar"];
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                    = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Periodo                                            = new GridView();
            objAnio_Escolar_Periodo.id_anio_escolar                         = objAnio_Escolar.id;
            tbl_Periodo.DataSource                                          = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);
            tbl_Periodo.DataBind();
            Asignacion objAsignacion                                        = new Asignacion();
            OperacionAsignacion objOperAsignacion                           = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id                                                = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
            DataTable dts_Asignacion                                        = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            titulo                                                          = "CALIFICACIONES PERIODO " + tbl_Periodo.Rows[0].Cells[1].Text + " AÑO " + objAnio_Escolar.descripcion + " (" + dts_Asignacion.Rows[0].ItemArray[7].ToString() + ")";
            return titulo;
        }
        catch (Exception)
        {
            return null;
        }
    }

    

    protected void btnCargar_Click(object sender, EventArgs e)
    {

        OperacionCalificacion objOperCalificacion                           = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Anio_Escolar objAnio_Escolar                                        = (Anio_Escolar)Session["anioEscolar"];
        
        Asistencia objAsistencia                                            = new Asistencia();
        OperacionAsistencia objOperAsistencia                               = new OperacionAsistencia(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        Anio_Escolar_Periodo objAnio_Escolar_Periodo                        = new Anio_Escolar_Periodo();
        OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo           = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Periodo                                                = new GridView();
        objAnio_Escolar_Periodo.id_anio_escolar                             = objAnio_Escolar.id;
        tbl_Periodo.DataSource                                              = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);                                        
        tbl_Periodo.DataBind();

        objAsistencia.id_asignacion                                         = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
        objAsistencia.id_periodo                                            = int.Parse(tbl_Periodo.Rows[0].Cells[0].Text);
        DataView dtv_Asistencia                                             = objOperAsistencia.ConsultarAsistencia(objAsistencia).DefaultView;

        Calificacion objCalificacion2                                       = new Calificacion();
        objCalificacion2.id_asignacion                                      = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));

        DataView dtv_Notas                                                  = objOperCalificacion.ConsultarCalificacion(objCalificacion2).DefaultView;


        Asignacion objAsignacion                                            = new Asignacion();
        OperacionAsignacion objOperAsignacion                               = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objAsignacion.id                                                    = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
        DataView Estudiante                                                 = objOperAsignacion.ConsultarEstudiante(objAsignacion).DefaultView;
        GridView tbl_Estudiantes                                            = new GridView();
        for (int i = 0; i < tbl_Calificacion.Rows.Count; i++)
        {
            /*Recorremos el gridview*/
            Calificacion objCalificacion                                                = new Calificacion();
            Label cod_Estudiante                                                        = (Label)tbl_Calificacion.Rows[i].Cells[1].Controls[0];

            Estudiante.RowFilter                                                        = "documento_numero =" + Convert.ToInt64(cod_Estudiante.Text);
            tbl_Estudiantes.DataSource                                                  = Estudiante;
            tbl_Estudiantes.DataBind();


            for (int j = 3; j < tbl_Calificacion.Rows[i].Cells.Count-2; j++)
            {
                if (tbl_Calificacion.Rows[i].Cells[j].Controls[0] is TextBox)
                {
                    TextBox txt = (TextBox)tbl_Calificacion.Rows[i].Cells[j].Controls[0];
                    if (txt.Text != "")
                    {
                        /*El css tiene información del control para determinar el porcentaje
                         que tiene ese control y el id la configuración de la nota*/
                        string[] css                                        = txt.CssClass.ToString().Split();

                        /*Filtramos el dataview dependicendo del tipo de calificacion 
                         * para validar que no se encuentre almacenada*/
                        dtv_Notas.RowFilter                                 = "id_estudiante = "+ Convert.ToInt64(tbl_Estudiantes.Rows[0].Cells[0].Text)+" AND id_calificacion_configuracion = " + int.Parse(css[2].Trim());
                        string id = "";
                        
                        /*Cargo la notas que se han filtrado dependiendo del tipo y el estudiante*/
                        GridView tbl_Notas                                  = new GridView();
                        tbl_Notas.DataSource                                = dtv_Notas;
                        tbl_Notas.DataBind();
        
                        /*Validamos que la nota no se haya cambiado el valor para 
                         realizar una validación */
                        int cambio = 0;
                        if (tbl_Notas.Rows.Count > 0)
                        {
                            id = tbl_Notas.Rows[0].Cells[0].Text;
                            if (tbl_Notas.Rows[0].Cells[5].Text != txt.Text.Replace(".", ","))
                            {
                                cambio                                      = 1;
                                objCalificacion.id                          = int.Parse(id);
                            }
                            else
                            {
                                cambio = 0;
                            }
                        }

                        objCalificacion.valor                               = Decimal.Parse(txt.Text.Replace(".", ","));
                        objCalificacion.id_usuario                          = int.Parse(Session["id_usuario"].ToString());
                        objCalificacion.id_estudiante                       = Convert.ToInt64(cod_Estudiante.Text);
                        objCalificacion.id_calificacion_configuracion       = int.Parse(css[2].Trim());
                        objCalificacion.id_asignacion                       = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                        /*Por medio del css del elemento validamos si ya estaba
                         en la base de datos ese registro y dependiendo de esto
                         lo editamos*/
                        if (css[3].Trim().Equals("false"))
                        {
                            objOperCalificacion.InsertarCalificacion(objCalificacion);
                        }
                        else
                        {
                            if (cambio == 1)
                            {
                                objOperCalificacion.ActualizarCalificacion(objCalificacion);
                            }
                        }
                    }
                }
            }
                    TextBox asistencia          = (TextBox)tbl_Calificacion.Rows[i].Cells[tbl_Calificacion.Columns.Count - 2].Controls[0];
                    string[] css_asistencia     = asistencia.CssClass.ToString().Split();

                    Asistencia objAsistencia_2 = new Asistencia();
                    if (asistencia.Text != "")
                    {
                        dtv_Asistencia.RowFilter                            = "id_estudiante=" + Convert.ToInt64(tbl_Estudiantes.Rows[0].Cells[0].Text);
                        GridView tbl_Asistencias                            = new GridView();
                        tbl_Asistencias.DataSource                          = dtv_Asistencia;
                        tbl_Asistencias.DataBind();

                        objAsistencia_2.id_estudiante                       = Convert.ToInt64(cod_Estudiante.Text);
                        objAsistencia_2.id_asignacion                       = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                        objAsistencia_2.id_periodo                          = int.Parse(tbl_Periodo.Rows[0].Cells[0].Text);
                        objAsistencia_2.id_usuario                          = int.Parse(Session["id_usuario"].ToString());

                        int cambio = 0;
                        if (tbl_Asistencias.Rows.Count > 0) {
                            string id_asistencia                            = tbl_Asistencias.Rows[0].Cells[0].Text;
                            if (tbl_Asistencias.Rows[0].Cells[4].Text != asistencia.Text.Replace(".", ","))
                            {
                                cambio = 1;
                                objAsistencia_2.id = int.Parse(id_asistencia);
                            }
                            else
                            {
                                cambio = 0;
                            }
                        }

                        objAsistencia_2.cantidad = int.Parse(asistencia.Text);
                        
                        if (css_asistencia[2].Trim().Equals("false")){
                            objOperAsistencia.InsertarAsistencia(objAsistencia_2);
                        }else {
                           if (cambio == 1) {
                               objOperAsistencia.ActualizarAsistencia(objAsistencia_2);
                            }
                        } 
                        
                    }
        }

        this.ShowNotification("Agregar", Resources.Mensaje.msjAgregar, "success");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Calificacion", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataTable dt                                                    = new DataTable();
        dt                                                              = gridviewtoDatatable(tbl_Calificacion);

        /*Consultamos el periodo*/
        Anio_Escolar_Periodo objAnio_Escolar_Periodo                    = new Anio_Escolar_Periodo();
        Anio_Escolar objAnio_Escolar                                    = (Anio_Escolar)Session["anioEscolar"];
        OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Periodo                                            = new GridView();
        objAnio_Escolar_Periodo.id_anio_escolar                         = objAnio_Escolar.id;
        tbl_Periodo.DataSource                                          = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);
        tbl_Periodo.DataBind();

        /*Asignacion*/
        Asignacion objAsignacion                                        = new Asignacion();
        OperacionAsignacion objOperAsignacion                           = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Asignacion                                         = new GridView();
        objAsignacion.id                                                = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
        tbl_Asignacion.DataSource                                       = objOperAsignacion.ConsultarAsignacion(objAsignacion);
        tbl_Asignacion.DataBind();

        using (ExcelPackage pck = new ExcelPackage())
        {

            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Calificacion");

            /*Cargar los datos del datatable creado apartir del gridview*/
            ws.Cells["A7"].LoadFromDataTable(dt, true);
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment      = ExcelVerticalAlignment.Center;
            ws.Cells[ws.Dimension.Address].Style.HorizontalAlignment    = ExcelHorizontalAlignment.Center;
            ws.Workbook.CalcMode                                        = ExcelCalcMode.Automatic;

            /* System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~/img/logo_colegio.png"));
             ExcelPicture pic = ws.Drawings.AddPicture("My Image", img);*/

            /*Cabecera de las primeras 6 filas*/
            ws.Cells["A1:" + ws.Cells[1, dt.Columns.Count].Address].Merge                       = true;
            ws.Cells["A1:" + ws.Cells[1, dt.Columns.Count].Address].Style.HorizontalAlignment   = ExcelHorizontalAlignment.Center;
            ws.Cells["A1"].Value = Resources.Mensaje.tituloExcel;
            ws.Cells["A2"].Value                                        = "ASIGNACIÓN";
            ws.Cells["A3"].Value                                        = "DOCENTE";
            ws.Cells["A4"].Value                                        = "MATERIA";
            ws.Cells["A5"].Value                                        = "SALÓN";
            ws.Cells["A6"].Value                                        = "PERIODO";

            ws.Cells["B2:" + ws.Cells[2, dt.Columns.Count].Address].Value   = tbl_Asignacion.Rows[0].Cells[0].Text;
            ws.Cells["B3:" + ws.Cells[3, dt.Columns.Count].Address].Value   = HttpUtility.HtmlDecode(tbl_Asignacion.Rows[0].Cells[10].Text);
            ws.Cells["B4:" + ws.Cells[4, dt.Columns.Count].Address].Value   = HttpUtility.HtmlDecode(tbl_Asignacion.Rows[0].Cells[7].Text);
            ws.Cells["B5:" + ws.Cells[5, dt.Columns.Count].Address].Value   = HttpUtility.HtmlDecode(tbl_Asignacion.Rows[0].Cells[8].Text);
            ws.Cells["B6:" + ws.Cells[6, dt.Columns.Count].Address].Value   = HttpUtility.HtmlDecode(tbl_Periodo.Rows[0].Cells[1].Text) + " " + objAnio_Escolar.descripcion;

            /*Formateando la cabecera. Uniendo las celdas*/
            for (int i = 2; i < 7; i++)
            {
                ws.Cells["B" + i + ":" + ws.Cells[i, dt.Columns.Count].Address].Style.HorizontalAlignment   = ExcelHorizontalAlignment.Left;
                ws.Cells["B" + i + ":" + ws.Cells[i, dt.Columns.Count].Address].Style.VerticalAlignment     = ExcelVerticalAlignment.Center;
                ws.Cells["B" + i + ":" + ws.Cells[i, dt.Columns.Count].Address].Merge                       = true;

            }
            
            /*Estilo de las celdas*/
            string rango_trabajo                                            = ws.Cells[8, (ws.Dimension.Start.Column + 2)].Address + ":" + ws.Cells[ws.Dimension.End.Row, (ws.Dimension.End.Column - 1)].Address;
            ws.Cells[ws.Dimension.Address].Style.Fill.PatternType           = ExcelFillStyle.Solid;
            ws.Cells[ws.Dimension.Address].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            ws.Cells[ws.Dimension.Address].Style.Border.Top.Style           = ExcelBorderStyle.Double;
            ws.Cells[ws.Dimension.Address].Style.Border.Left.Style          = ExcelBorderStyle.Double;
            ws.Cells[ws.Dimension.Address].Style.Border.Right.Style         = ExcelBorderStyle.Double;
            ws.Cells[ws.Dimension.Address].Style.Border.Bottom.Style        = ExcelBorderStyle.Double;
            string formula                                                  = "";
            string porcentaje                                               = "";
            
            /*Obtención del promedio apartir de una formula*/
            for (int i = 8; i <= ws.Dimension.End.Row; i++)
            {

                for (int j = 3; j < ws.Dimension.End.Column-1; j++)
                {
                    porcentaje                                              = (decimal.Parse(ws.Cells[7, j].Value.ToString().Substring(ws.Cells[7, j].Value.ToString().Length - 4, 2)) / 100).ToString();
                    formula                                                 += "" + ws.Cells[i, j].Address + "*" + porcentaje.Replace(",", ".");
                    if (j < (ws.Dimension.End.Column - 2))
                    {
                        formula += "+";
                    }
                }
                ws.Cells[i, ws.Dimension.End.Column].Formula                = formula;

                formula = "";
            }

            /*Validación del excel*/
            var rango = ws.Cells[8, 3].Address + ":" + ws.Cells[ws.Dimension.End.Row, ws.Dimension.End.Column-2].Address;
            var validation = ws.DataValidations.AddDecimalValidation(rango);
            // Alternatively:
            //var validation = sheet.Cells["A1:A2"].DataValidation.AddIntegerDataValidation();
            validation.ErrorStyle                                           = ExcelDataValidationWarningStyle.stop;
            validation.PromptTitle                                          = "Ingrese un valor decimal";
            validation.Prompt                                               = "El valor debe estar entre "+objAnio_Escolar.nota_minima+" y "+objAnio_Escolar.nota_maxima;
            validation.ShowInputMessage                                     = true;
            validation.ErrorTitle                                           = "El valor introducido es inválido";
            validation.Error                                                = "El valor debe estar entre " + objAnio_Escolar.nota_minima + " y " + objAnio_Escolar.nota_maxima;
            validation.ShowErrorMessage                                     = true;
            validation.Operator                                             = ExcelDataValidationOperator.between;
            validation.Formula.Value                                        = (double)objAnio_Escolar.nota_minima;
            validation.Formula2.Value                                       = (double)objAnio_Escolar.nota_maxima;

            /*Proteccion del documento para no permitir modificaciones*/
            ws.Workbook.Protection.LockWindows                              = true;
            ws.Workbook.Protection.LockStructure                            = true;

            ws.Workbook.View.SetWindowSize(150, 525, 14500, 6000);
            ws.Workbook.View.ShowHorizontalScrollBar                        = false;
            ws.Workbook.View.ShowVerticalScrollBar                          = false;
            ws.Workbook.View.ShowSheetTabs                                  = false;

            ws.Cells[rango].Style.Numberformat.Format = "#,##0.00";
            

            /*Diseño de Encabezado*/
            /*using (var rng = ws.Cells [ws.Cells[1,ws.Dimension.Start.Column].Address+":"+ws.Cells[1,ws.Dimension.End.Column].Address])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Font.Color.SetColor(Color.White);
                rng.Style.WrapText = true;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
            }*/

            ws.Cells[rango_trabajo].Style.Locked                            = false;
            ws.Cells[rango_trabajo].Style.Fill.PatternType                  = ExcelFillStyle.Solid;

            ws.Cells[rango_trabajo].Style.Fill.BackgroundColor.SetColor(Color.White);
            ws.Cells[ws.Cells[8, ws.Dimension.End.Column].Address + ":" + ws.Cells[ws.Dimension.End.Row, ws.Dimension.End.Column].Address].Style.Hidden = true;    //Hide the formula

            ws.Protection.IsProtected                                       = true;
            ws.Protection.AllowDeleteColumns                                = false;
            ws.Protection.AllowDeleteRows                                   = false;
            ws.Protection.AllowFormatCells                                  = false;
            ws.Protection.AllowFormatColumns                                = false;
            ws.Protection.AllowFormatRows                                   = false;
            ws.Protection.SetPassword(Resources.Mensaje.passwordExcel);

            /*pck.Encryption.Algorithm = EncryptionAlgorithm.AES256;
            pck.Encryption.Password = Resources.Mensaje.passwordExcel;*/
            pck.Encryption.IsEncrypted                                      = true;
            Byte[] fileBytes                                                = pck.GetAsByteArray();
            Response.Clear();
            Response.Charset                                                = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding                                        = System.Text.UTF8Encoding.UTF8;
            Response.ContentType                                            = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=Planilla " + ws.Cells["B2"].Value + " " + System.DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ".xlsx");
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
        }
    }

    /*
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    */
    protected void btnImportar_Click(object sender, EventArgs e)
    {
        if (fluImportar.HasFile)
        {
            string fileName = Path.GetFileName(fluImportar.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fluImportar.PostedFile.FileName);
            string fileLocation = Server.MapPath("~/documentos/" + fileName);
            fluImportar.SaveAs(fileLocation);
            getDataTableFromExcel(fileLocation);
            ShowNotification("Exitoso", "Se ha importado correctamente el excel", "info");
        }else {
            ShowNotification("Error Archivo","Por favor seleccione un archivo","info");
        }

    }

    public DataTable gridviewtoDatatable(GridView dtg)
    {
        DataTable dt = new DataTable();
        if (dtg.HeaderRow != null)
        {
            for (int i = 1; i < dtg.HeaderRow.Cells.Count; i++)
            {
                if (i > 2)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text.Replace("<br>", ""), typeof(decimal));
                }
                else
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text, typeof(string));
                }
            }
        }
        for (int i = 0; i < dtg.Rows.Count; i++)
        {
            DataRow dr;
            dr = dt.NewRow();
            for (int j = 1; j < dtg.Rows[i].Cells.Count; j++)
            {
                if (tbl_Calificacion.Rows[i].Cells[j].Controls[0] is Label)
                {
                    Label lbl = (Label)tbl_Calificacion.Rows[i].Cells[j].Controls[0];

                    if (j < 3)
                    {
                        dr[j-1] = lbl.Text.Trim();
                    }
                }

                else if (tbl_Calificacion.Rows[i].Cells[j].Controls[0] is TextBox)
                {
                    TextBox txt = (TextBox)tbl_Calificacion.Rows[i].Cells[j].Controls[0];
                    if (txt.Text != "")
                    {
                        dr[j-1] = decimal.Parse(txt.Text.Trim());
                        string[] css = txt.CssClass.ToString().Split();
                    }
                    else
                    {
                        dr[j-1] = 0;
                    }
                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public void getDataTableFromExcel(string path)
    {
        using (var pck = new OfficeOpenXml.ExcelPackage())
        {
            using (var stream = File.OpenRead(path))
            {
                pck.Load(stream);
            }
            var ws                                                                      = pck.Workbook.Worksheets.First();

            Calificacion_Configuracion objConfiguracionCalificacion                     = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOperCalificacion_Configuracion       = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_configuracion                                                  = new GridView();
            tbl_configuracion.DataSource                                                = objOperCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objConfiguracionCalificacion);
            tbl_configuracion.DataBind();

            Asignacion objAsignacion                                                    = new Asignacion();
            OperacionAsignacion objOperAsignacion                                       = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Asignacion                                                     = new GridView();

            Docente objDocente                                                          = new Docente();
            OperacionDocente objOperDocente                                             = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Docente                                                        = new GridView();

            Usuario objUsuario                                                          = new Usuario();
            OperacionUsuario objOperUsuario                                             = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Usuario                                                        = new GridView();

            Anio_Escolar_Periodo objAnio_Escolar_Periodo                                = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo                   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Anio_Escolar_Periodo                                           = new GridView();

            if (int.Parse(ws.Cells["B2"].Value.ToString()) == int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString())))
            {
                objAsignacion.id                                                        = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                tbl_Asignacion.DataSource                                               = objOperAsignacion.ConsultarAsignacion(objAsignacion);
                tbl_Asignacion.DataBind();
                objDocente.documento_numero                                             = int.Parse(tbl_Asignacion.Rows[0].Cells[9].Text);
                tbl_Docente.DataSource                                                  = objOperDocente.ConsultarDocente(objDocente);
                tbl_Docente.DataBind();

                objUsuario.documento                                                    = int.Parse(tbl_Asignacion.Rows[0].Cells[9].Text);
                tbl_Usuario.DataSource                                                  = objOperUsuario.ConsultarUsuario(objUsuario);
                tbl_Usuario.DataBind();

                if (int.Parse(Session["id_usuario"].ToString()) == int.Parse(tbl_Usuario.Rows[0].Cells[0].Text) || int.Parse(Session["id_usuario_tipo"].ToString()) != 2)
                {
                    string[] cs                                                         = ws.Cells["B6"].Value.ToString().Split();
                    Anio_Escolar objAnio_Escolar                                        = (Anio_Escolar)Session["anioEscolar"];
                    objAnio_Escolar_Periodo.id_anio_escolar                             = objAnio_Escolar.id;
                    tbl_Anio_Escolar_Periodo.DataSource                                 = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);
                    tbl_Anio_Escolar_Periodo.DataBind();
                    if (cs[0].ToString().Equals(HttpUtility.HtmlDecode(tbl_Anio_Escolar_Periodo.Rows[0].Cells[1].Text)) && (int.Parse(cs[1].Trim()) == objAnio_Escolar.descripcion))
                    {
                        int fila = 0;
                        int columna = 0;
                        for (int i = 8; i < ws.Dimension.End.Row + 1; i++)
                        {
                            columna = 3;
                            for (int j = 3; j < ws.Dimension.End.Column - 1 ; j++)
                            {
                                if (tbl_Calificacion.Rows[fila].Cells[columna].Controls[0] is TextBox)
                                {
                                    TextBox txt                                         = (TextBox)tbl_Calificacion.Rows[fila].Cells[columna].Controls[0];
                                    decimal valor                                       = decimal.Parse(ws.Cells[i, j].Value.ToString());
                                    if (valor != 0)
                                    {
                                        txt.Text                                        = String.Format("{0:f2}", Math.Round(valor, 2));
                                    }
                                    else
                                    {
                                        txt.Text                                        = "";
                                    }
                                }
                                columna++;
                            }

                            if (tbl_Calificacion.Rows[fila].Cells[tbl_Calificacion.Columns.Count - 2].Controls[0] is TextBox)
                            {
                                TextBox txt                                             = (TextBox)tbl_Calificacion.Rows[fila].Cells[tbl_Calificacion.Columns.Count-2].Controls[0];
                                int valor                                               = int.Parse(ws.Cells[i, ws.Dimension.End.Column-1].Value.ToString());
                                if (valor != 0)
                                {
                                    txt.Text                                            = valor.ToString();
                                }
                                else
                                {
                                    txt.Text                                            = "";
                                }
                            }

                            if (tbl_Calificacion.Rows[fila].Cells[tbl_Calificacion.Columns.Count - 1].Controls[0] is Label)
                            {
                                Label lbl                                               = (Label)tbl_Calificacion.Rows[fila].Cells[tbl_Calificacion.Columns.Count - 1].Controls[0];
                                decimal valor                                           = decimal.Parse(ws.Cells[i, ws.Dimension.End.Column].Value.ToString());
                                if (valor != 0)
                                {
                                    lbl.Text                                            = String.Format("{0:f2}", valor);
                                }
                                else
                                {
                                    lbl.Text                                            = "";
                                }
                            }
                            fila++;
                        }
                    }
                    else
                    {
                        ShowNotification("Periodo", "El periodo no corresponde con el que se va a calificar", "info");
                    }
                }
                else
                {
                    ShowNotification("Usuario Incorrecto", "Esta intentando subir un archivo que no corresponde con el docente", "info");
                }

            }
            else
            {
                ShowNotification("Mensaje", "El archivo no corresponde con la materia que va a calificar", "info");
            }
        }

    }

    /// <summary>
    /// Clase para construir los textbox y label del gridview
    /// </summary>
    class TextColumn : ITemplate
    {

        public String DataField { get; set; }
        public String Control { get; set; }
        public String CssClass { get; set; }

        public TextColumn(string datafield, string control, string cssclass)
        {
            DataField           = datafield;
            Control             = control;
            CssClass            = cssclass;
        }

        public void InstantiateIn(Control container)
        {
            if (Control.Equals("Textbox"))
            {
                var textbox                 = new TextBox();
                textbox.ID                  = DataField;
                textbox.AutoPostBack        = false;
                textbox.DataBinding         += label_DataBinding;
                container.Controls.Add(textbox);
            }
            else if (Control.Equals("Label"))
            {
                var label                   = new Label();
                label.ID                    = DataField;
                label.CssClass              = "span12 " + CssClass;
                label.DataBinding           += label_DataBinding;
                container.Controls.Add(label);
            }
            else if (Control.Equals("Imagen"))
            {
                var label                   = new System.Web.UI.WebControls.Image();
                label.ID                    = DataField;
                label.DataBinding           += label_DataBinding;
                container.Controls.Add(label);
            }
        }

        void label_DataBinding(object sender, EventArgs e)
        {
            if (Control.Equals("Textbox"))
            {
                var textbox                 = (TextBox)sender;
                var context                 = DataBinder.GetDataItem(textbox.NamingContainer);
                textbox.Text                = DataBinder.Eval(context, DataField).ToString();
                /*True y false es para saber si el dato se encuentra en la bd para 
                 luego realizar validaciones*/
                if (DataBinder.Eval(context, DataField).ToString() != "")
                {
                    textbox.CssClass        = "span12 " + CssClass + " true";
                }
                else
                {
                    textbox.CssClass        = "span12 " + CssClass + " false";
                }
                textbox.Style.Value         = "text-align:center";
            }
            else if (Control.Equals("Label"))
            {
                var label                   = (Label)sender;
                var context                 = DataBinder.GetDataItem(label.NamingContainer);
                label.Text                  = DataBinder.Eval(context, DataField).ToString();
            }
            else if (Control.Equals("Imagen"))
            {
                var imagen                  = (System.Web.UI.WebControls.Image)sender;
                var context                 = DataBinder.GetDataItem(imagen.NamingContainer);
                imagen.ImageUrl             = DataBinder.Eval(context, DataField).ToString();
                imagen.Height               = 50;
                imagen.Width                = 50;
            }
        }
    }

    public void ShowNotification(string title, string msg, string nt)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder("");
        sbScript.Append("alert('Notas Cargadas Exitosamente');");
        ScriptManager.RegisterStartupScript(this, GetType(), "ClientScript", sbScript.ToString(), true);
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Calificacion", Pagina = "Busqueda" });
    }
}
