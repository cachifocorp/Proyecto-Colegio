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

public partial class Proceso_Cambio_Calificacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.cargar();
            btnCambiar.Visible = false;
        }
        ver_tbl();
    }

    public void cargar()
    {
        try
        {
            Anio_Escolar objAnio_Escolar = (Anio_Escolar)Session["anioEscolar"];

            Anio_Escolar_Periodo objPeriodo = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperPeriodo = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objPeriodo.id_anio_escolar = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperPeriodo.ConsultarAnio_Escolar_Periodo(objPeriodo), ddlPeriodo);

        }
        catch (Exception)
        {
        }
    }

    protected void txtEstudiante_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Estudiante objEstudiante = new Estudiante();
            OperacionEstudiante objOperEstudiante = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objEstudiante.documento_numero = Convert.ToInt64(txtEstudiante.Text);
            DataTable dta_Estudiante = objOperEstudiante.ConsultarEstudiante(objEstudiante);
            txtNombres.Text = dta_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dta_Estudiante.Rows[0].ItemArray[5].ToString();
            txtApellidos.Text = dta_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dta_Estudiante.Rows[0].ItemArray[7].ToString();
        }
        catch (Exception)
        {
        }
    }

    public void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ver_tbl();
    }

    public void ver_tbl()
    {
        if (txtEstudiante.Text != "" && txtNombres.Text != "" && txtApellidos.Text != "")
        {
            tbl_Calificacion.Columns.Clear();
            Matricula objMatricula = new Matricula();
            OperacionMatricula objOperMatricula = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMatricula.id_estudiante = Convert.ToInt64(txtEstudiante.Text);
            DataTable dts_Matricula = objOperMatricula.ConsultarMatricula(objMatricula);

            Asignacion objAsignacion = new Asignacion();
            OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

            objAsignacion.id_salon = int.Parse(dts_Matricula.Rows[0].ItemArray[2].ToString());
            DataTable asignacion_Normal = objOperAsignacion.ConsultarAsignacion(objAsignacion);

            objAsignacion.id_salon = int.Parse(dts_Matricula.Rows[0].ItemArray[8].ToString());
            DataTable asignacion_Tecnica = objOperAsignacion.ConsultarAsignacion(objAsignacion);

            asignacion_Normal.Merge(asignacion_Tecnica);

            DataView dv_Materia = asignacion_Normal.DefaultView;
            dv_Materia.Sort = "orden_impresion ASC";

            GridView tbl_Materia = new GridView();
            tbl_Materia.DataSource = dv_Materia;
            tbl_Materia.DataBind();

            Calificacion_Configuracion objConfiguracionCalificacion = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOperCalificacion_Configuracion = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objConfiguracionCalificacion.id_periodo = int.Parse(ddlPeriodo.SelectedValue);
            GridView tbl_configuracion = new GridView();
            objConfiguracionCalificacion.id_periodo = int.Parse(ddlPeriodo.SelectedValue.ToString());
            tbl_configuracion.DataSource = objOperCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objConfiguracionCalificacion);
            tbl_configuracion.DataBind();


            DataTable tbl_Calificaciones = new DataTable();
            tbl_Calificaciones.Clear();
            tbl_Calificaciones.Columns.Add("ID");
            tbl_Calificaciones.Columns.Add("MATERIA");
            agregarItemTemplate("ID", "ID", "Label", "0", "span2");
            agregarItemTemplate("MATERIA", "MATERIA", "Label", "0", "span2");

            for (int i = 0; i < tbl_configuracion.Rows.Count; i++)
            {
                string header = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text) + " <br> (" + tbl_configuracion.Rows[i].Cells[3].Text + ")%");
                string eval = HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text);
                agregarItemTemplate(header, eval, "Textbox", tbl_configuracion.Rows[i].Cells[3].Text + " " + tbl_configuracion.Rows[i].Cells[0].Text, "span1");
                tbl_Calificaciones.Columns.Add(HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text));
            }

            tbl_Calificaciones.Columns.Add("PROMEDIO PERIODO");
            agregarItemTemplate("PROMEDIO PERIODO", "PROMEDIO PERIODO", "Label", "Periodo", "span1");

            double promedio_periodo = 0;

            Calificacion objCalificacion = new Calificacion();
            OperacionCalificacion objOperCalificacion = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objCalificacion.id_estudiante = Convert.ToInt64(txtEstudiante.Text);
            DataView dtv_Notas = objOperCalificacion.ConsultarCalificacion(objCalificacion).DefaultView;

            foreach (GridViewRow dr in tbl_Materia.Rows)
            {

                DataRow dtw = tbl_Calificaciones.NewRow();
                dtw["ID"] = dr.Cells[0].Text;
                dtw["MATERIA"] = dr.Cells[7].Text;

                for (int i = 0; i < tbl_configuracion.Rows.Count; i++)
                {

                    /*Filtramos las notas por el tipo de calificación*/
                    dtv_Notas.RowFilter = "id_asignacion = " + int.Parse(dr.Cells[0].Text) + " AND id_calificacion_configuracion = " + int.Parse(tbl_configuracion.Rows[i].Cells[0].Text);

                    /*Llenado del datarow con las notas del estudiante 
                     * observacion: el indice del dataview es el nombre de la nota
                     y calculamos el promedio apartir de las notas almacenadas*/
                    GridView tbl_Notas_Filtradas = new GridView();
                    tbl_Notas_Filtradas.DataSource = dtv_Notas;
                    tbl_Notas_Filtradas.DataBind();
                    if (tbl_Notas_Filtradas.Rows.Count > 0)
                    {
                        dtw[HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text)] = Decimal.Parse(tbl_Notas_Filtradas.Rows[0].Cells[5].Text);
                        promedio_periodo += double.Parse(tbl_Notas_Filtradas.Rows[0].Cells[5].Text) * (double.Parse(tbl_configuracion.Rows[i].Cells[3].Text) / 100);
                    }
                    else
                    {
                        dtw[HttpUtility.HtmlDecode(tbl_configuracion.Rows[i].Cells[1].Text)] = "";
                    }
                }

                dtw["PROMEDIO PERIODO"] = String.Format("{0:f2}", promedio_periodo);
                promedio_periodo = 0;
                tbl_Calificaciones.Rows.Add(dtw);
            }
            tbl_Calificacion.DataSource = tbl_Calificaciones.DefaultView;
            tbl_Calificacion.DataBind();
            tbl_Calificaciones.Clear();
        }
        btnCambiar.Visible = true;
        //  ClientScript.RegisterStartupScript(this.GetType(), "forzar", "<script>javascript:Forzar();</script>");
    }

    public void agregarItemTemplate(string header, string eval, string control, string cssclass, string spanheader)
    {
        var textbox = new TemplateField();
        textbox.HeaderText = header;
        textbox.HeaderStyle.CssClass = spanheader;
        textbox.ItemTemplate = new TextColumn(eval, control, cssclass);
        tbl_Calificacion.Columns.Add(textbox);
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
            DataField = datafield;
            Control = control;
            CssClass = cssclass;
        }

        public void InstantiateIn(Control container)
        {
            if (Control.Equals("Textbox"))
            {
                var textbox = new TextBox();
                textbox.ID = DataField;
                textbox.AutoPostBack = false;
                textbox.DataBinding += label_DataBinding;
                container.Controls.Add(textbox);
            }
            else if (Control.Equals("Label"))
            {
                var label = new Label();
                label.ID = DataField;
                label.CssClass = "span12 " + CssClass;
                label.DataBinding += label_DataBinding;
                container.Controls.Add(label);
            }
        }

        void label_DataBinding(object sender, EventArgs e)
        {
            if (Control.Equals("Textbox"))
            {
                var textbox = (TextBox)sender;
                var context = DataBinder.GetDataItem(textbox.NamingContainer);
                textbox.Text = DataBinder.Eval(context, DataField).ToString();
                /*True y false es para saber si el dato se encuentra en la bd para 
                 luego realizar validaciones*/
                if (DataBinder.Eval(context, DataField).ToString() != "")
                {
                    textbox.CssClass = "span12 " + CssClass + " true";
                }
                else
                {
                    textbox.CssClass = "span12 " + CssClass + " false";
                }
                textbox.Style.Value = "text-align:center";
            }
            else if (Control.Equals("Label"))
            {
                var label = (Label)sender;
                var context = DataBinder.GetDataItem(label.NamingContainer);
                label.Text = DataBinder.Eval(context, DataField).ToString();
            }
        }
    }

    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        // ClientScript.RegisterStartupScript(this.GetType(), "forzar", "<script>javascript:Forzar();</script>");
        Calificacion objCalificacion = new Calificacion();
        OperacionCalificacion objOperCalificacion = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objCalificacion.id_estudiante = Convert.ToInt64(txtEstudiante.Text);
        Int64 cedula = Convert.ToInt64(txtEstudiante.Text);

        DataView dtv_Notas = objOperCalificacion.ConsultarCalificacion(objCalificacion).DefaultView;
        foreach (GridViewRow dr in tbl_Calificacion.Rows)
        {
            Label id_asignacion = (Label)dr.Cells[0].Controls[0];
            for (int j = 2; j < tbl_Calificacion.Columns.Count - 1; j++)
            {
                var vlr = dr.Cells[j].Controls[0];
                if (dr.Cells[j].Controls[0] is TextBox)
                {
                    TextBox txt = (TextBox)dr.Cells[j].Controls[0];
                    if (txt.Text != "")
                    {
                        /*El css tiene información del control para determinar el porcentaje
                         que tiene ese control y el id la configuración de la nota*/
                        string[] css = txt.CssClass.ToString().Split();

                        /*Filtramos el dataview dependicendo del tipo de calificacion 
                         * para validar que no se encuentre almacenada*/
                        dtv_Notas.RowFilter = "id_asignacion = " + int.Parse(id_asignacion.Text) + " AND id_calificacion_configuracion = " + int.Parse(css[2].Trim());
                        string id = "";

                        /*Cargo la notas que se han filtrado dependiendo del tipo y el estudiante*/
                        GridView tbl_Notas = new GridView();
                        tbl_Notas.DataSource = dtv_Notas;
                        tbl_Notas.DataBind();

                        /*Validamos que la nota no se haya cambiado el valor para 
                         realizar una validación */
                        int cambio = 0;
                        if (tbl_Notas.Rows.Count > 0)
                        {
                            id = tbl_Notas.Rows[0].Cells[0].Text;
                            if (tbl_Notas.Rows[0].Cells[5].Text != txt.Text.Replace(".", ","))
                            {
                                cambio = 1;
                                objCalificacion.id = int.Parse(id);
                            }
                            else
                            {
                                cambio = 0;
                            }
                        }

                        objCalificacion.valor = Decimal.Parse(txt.Text.Replace(".", ","));
                        objCalificacion.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objCalificacion.id_estudiante = cedula;
                        objCalificacion.id_calificacion_configuracion = int.Parse(css[2].Trim());
                        objCalificacion.id_asignacion = int.Parse(id_asignacion.Text);
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
            txtEstudiante.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            ddlPeriodo.SelectedValue = "0";
            tbl_Calificacion.DataSource = null;
            tbl_Calificacion.DataBind();
        }
    }
}