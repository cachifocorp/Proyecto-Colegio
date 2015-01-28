using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Carnet_Busqueda : System.Web.UI.Page
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

            throw;
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Estudiante();
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
       string word = "";
        word += "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml'= xmlns='http://www.w3.org/TR/REC-html40'>";
        word += "<head><title>Boletin</title>";
        word += "<!--[if gte mso 9]>";
        word += "<xml>";
        word += "<w:WordDocument>";
        word += "<w:View>Print</w:View>";
        word += "<w:Zoom>100</w:Zoom>";
        word += "<w:DoNotOptimizeForBrowser/>";
        word += "</w:WordDocument>";
        word += "</xml>";
        word += "<![endif]-->";
        word += "<style>";
        word += "@page";
        word += "{";
        word += "size:21.59cm 35.56cm;  /* Oficio */";
        word += "margin:1cm 1cm 1cm 1cm; /* Margins: 2.5 cm on each side */";
        word += "mso-page-orientation: portrait;  ";
        /*word += "mso-header:h1;";
        word += "mso-footer:f1;";*/
        word += "}";
        for (int i = 0; i < tbl_Estudiante.Rows.Count; i++)
        {
            word += "@page Section" + (i + 1) + " {  }";
            word += "div.Section" + (i + 1) + " { page:Section" + (i + 1) + ";mso-header:h1; }";

        }

        //word += "#hrdftrtbl {margin:0in 0in 0in 9in}";
        //word += "p.MsoFooter, li.MsoFooter, div.MsoFooter{margin:0in;margin-bottom:.0001pt;mso-pagination:widow-orphan;tab-stops:center 3.0in right 6.0in;font-size:12.0pt;}";
        /*word += "p.MsoFooter, li.MsoFooter, div.MsoFooter{margin:0in; margin-bottom:.0001pt; mso-pagination:widow-orphan; tab-stops:center 3.0in right 6.0in; font-size:12.0pt; font-family:'Arial';}" +
        "p.MsoHeader, li.MsoHeader, div.MsoHeader {margin:0in; margin-bottom:.0001pt; mso-pagination:widow-orphan; tab-stops:center 3.0in right 6.0in; font-size:12.0pt; font-family:'Arial';}";*/
        word += "</style>";
        word += "</head>";
        word += "<body>";

        clsFunciones.carnet = "";
        int fila = 0;
        foreach (GridViewRow row in tbl_Estudiante.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkEstudiante") as CheckBox);
                if (chkRow.Checked)
                {
                    //word += "<div class=Section"+(fila+1)+">";
                    word += generarCarnet(int.Parse(row.Cells[1].Text));                   
                    word += "<br><br>";
                   // word += "</div>";
                    fila++;
                }
            }
        }
        word += "</body>";
        word += "</html>";
        clsFunciones.carnet = word;
        Response.RedirectToRoute("General", new { Modulo = "Reporte", Entidad = "Carnet", Pagina = "Gestion" });
    }
    public String generarCarnet(Int64 documento)
    {
        configurationFiles f = new configurationFiles();

        Estudiante objEstudiante = new Estudiante();
        OperacionEstudiante objOperEstudiante = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objEstudiante.documento_numero = documento;
        DataTable dt_Estudiante = objOperEstudiante.ConsultarEstudiante(objEstudiante);
        Matricula objMatricula = new Matricula();
        OperacionMatricula objOperMatricula = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMatricula.id_estudiante = documento;
        DataTable dt_Matricula = objOperMatricula.ConsultarMatricula(objMatricula);
        String cad = "<div id=\"contenedor\">" +
                       " <div id=\"infocarnet\" style=\" background:url(http://localhost/itipuente/Reporte_Carnet/images/" + f.obtener_valor(Server.MapPath("~") + "/Reporte_Carnet/configuration.config", "") + "); @media print {body:before {content: url(http://localhost/itipuente/Reporte_Carnet/images/" + f.obtener_valor(Server.MapPath("~") + "/Reporte_Carnet/configuration.config", "")+") !important;}\">" +
                          "<h2><strong>INSTITUTO TÉCNICO FRANCISCO DE PAULA SANTANDER</storng></h2>" +
                         " <img src = '" + dt_Estudiante.Rows[0].ItemArray[20].ToString().Replace("~", "../..") + "' class=\"foto\">   " +
                          "<div id=\"information\">" +
                            "<p> <strong>"+dt_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[5].ToString() +" "+dt_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[7].ToString()+"</strong></p>" +
                            "<p>D.I.: " + dt_Estudiante.Rows[0].ItemArray[2].ToString() + "</p>" +
                            "<p> SALON: " + dt_Matricula.Rows[0].ItemArray[9].ToString() + "</p>" +
                          "</div>" +
                        "</div>" +
                        "<div id=\"infocolegio\">" +
                          "<p>" +
                            "Este carnet es personal e intransferible,acrédita al portador como estudiante del " +
                            "<br><strong>INSTITUTO TÉCNICO FRANCISCO DE PAULA SANTANDER</strong><br> en caso de pérdida favor comunicarse al sitio web http://wwww.itipuentenacional.edu.co " +
                          "</p> " +
                            "<img src=\"http://localhost/itipuente/Reporte_Carnet/images/firma.jpg\" id=\"firma\"> " +
                        "</div>" +
                    "</div>";

        /*String cad = "<table width='415.748031496px' heigth='321.2598425197px' style = 'margin-top:50px;border: 1px solid #000; font-family:Calibri ;border-collapse:collapse;font-size:16px'><tr>";
        cad += "<td width='50%'><table width='207.874015748px'><tr>";
        cad += "<td colspan='2'><img src='http://academico.itipuentenacional.edu.co/img/header_carnet.jpg'></td></tr><tr>";
        cad += "<td rowspan='4' width='30%' style = 'padding: 5px ;text-align:center;'><img alt='logo' style='border: 1px solid #000' src = '" +dt_Estudiante.Rows[0].ItemArray[20].ToString().Replace("~", "../..") + "'  width='70' height='70' ></td>";
        cad += "<td style='text-align:center ; '>" + dt_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[5].ToString() + "</td></tr><tr>";
        cad += "<td style='text-align:center'>" + dt_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[7].ToString() + "</td></tr><tr>";
        cad += "<td style='text-align:center'> D.I. "+dt_Estudiante.Rows[0].ItemArray[2].ToString()+"</td></tr><tr>";
        cad += "<td style='text-align:right'></td></tr><tr>";
        cad += "<td colspan='2' style='text-align:center'>SALÓN : "+dt_Matricula.Rows[0].ItemArray[9].ToString()+"</td></tr>";
        cad += "<td colspan='2'><img src='http://academico.itipuentenacional.edu.co/img/footer_carnet.jpg'></td>";
        cad += "</table></td>";
        cad += "<td>Este carnet es personal e intransferible,acrédita al portador como estudiante del INSTITUTO TÉCNICO FRANCISCO DE ";
        cad += " PAULA SANTANDER, en caso de pérdida favor comunicarse al sitio web http://wwww.itipuentenacional.edu.co </td></tr></table>";
        cad += "<br>";
        cad += "</table>";*/


        return cad;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        configurationFiles f = new configurationFiles();
         
       // f.modificar_valor( Server.MapPath("~")+"/Reporte_Carnet/configuration.config", "bg1", "imagen.png");
       // mess.InnerHtml = "<p>" + f.obtener_valor(Server.MapPath("~") + "/Reporte_Carnet/configuration.config", "") + "</p>";

        Boolean fileOK = false;
        String path = Server.MapPath("~/Reporte_Carnet/images/");
        if (filebg.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(filebg.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }

        if (fileOK)
        {
            try
            {
                filebg.PostedFile.SaveAs(path+ filebg.FileName);
                Label2.Text = "Imagen Subida Con Exito";
                f.modificar_valor(Server.MapPath("~") + "/Reporte_Carnet/configuration.config", "bg1", filebg.FileName);
            }
            catch (Exception ex)
            {
                Label2.Text = "File could not be uploaded.";
            }
        }
        else
        {
            Label2.Text = "Cannot accept files of this type.";
        }
    }
}