using LogicaNegocio;
using ObjetosNegocio;
using AccesoInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        int cc = 0;
        int grado = Convert.ToInt32(ddlSalon.SelectedValue);
        foreach (GridViewRow row in tbl_Estudiante.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkEstudiante") as CheckBox);

                if (chkRow.Checked)
                {
                    cc++;
                    word += generarCarnet(Convert.ToInt64(row.Cells[1].Text));

                    if (cc == 3 && grado >= 20 && grado <= 38)
                    {
                        word += "<div style=\"height:20px;\"></div>";
                        cc = 0;
                    }
                    else if (cc == 3)
                    {
                        word += "<div style=\"height:90px;\"></div>";
                        cc = 0;
                    }
                    fila++;
                }
            }
        }
        word += "</body>";
        word += "</html>";
        clsFunciones.carnet = word;
        Response.RedirectToRoute("General", new { Modulo = "Reporte", Entidad = "Paz_salvo", Pagina = "Gestion" });
    }
    public String generarCarnet(Int64 documento)
    {
        Estudiante objEstudiante = new Estudiante();
        OperacionEstudiante objOperEstudiante = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objEstudiante.documento_numero = documento;
        DataTable dt_Estudiante = objOperEstudiante.ConsultarEstudiante(objEstudiante);
        Matricula objMatricula = new Matricula();
        OperacionMatricula objOperMatricula = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMatricula.id_estudiante = documento;
        DataTable dt_Matricula = objOperMatricula.ConsultarMatricula(objMatricula);
        String tecnica = new transacciones().getTecnica(Convert.ToInt32(ddlSalon.SelectedValue), documento);

        DateTime date = DateTime.Now;
        String cad = "<table style=\"border-collapse: collapse;  height:200px; border: 2px solid black; margin-top:10px;\">" +
                        "<tbody>" +
                            "<tr>" +
                                "<td style=\"text-align: center; border-color: rgb(0, 0, 0);\">&nbsp;<img src=\"http://academico.itipuentenacional.edu.co/img/logo.png\"   style=\"width:80px; height:60px;\" /></td>" +
                                "<td style=\"border-color: rgb(0, 0, 0);\">" +
                                    "<div style=\"font-size: 13px; text-align: center;\"><span style=\"font-weight: bold; font-size: 13px;\">&nbsp;<span style=\"font-family: Calibri; font-size: 13px;\">INSTITUTO T&Eacute;CNICO FRANCISCO DE PAULA SANTANDE</span><span style=\"font-family: Calibri; line-height: 1.42857143; font-size: 13px; background-color: transparent;\">R</span></span></div>" +
                                    "<div style=\"text-align: center;\"><span style=\"font-size: 10px;\">PUENTE NACIONAL SANTANDER</span></div></td>" +
                                "<td style=\"text-align: center; border-color: rgb(0, 0, 0);\">&nbsp;<span style=\"font-weight: bold; font-family: 'Trebuchet MS', Helvetica, sans-serif; font-size: 26px;\">PAZ Y SALVO</span></td>" +
                            "</tr>" +
                            "<tr  >";

                            int grado = Convert.ToInt32(ddlSalon.SelectedValue);
                            if (grado >= 20 && grado <= 38)
                            {
                                cad += "<td style=\"text-align: center; \" colspan='2' ><strong> " + dt_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[5].ToString() + "  " + dt_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[7].ToString() + "</strong>    | AREA T.: " + tecnica + "</td>" +
                                 "<td style=\"\"> |  SALÓN : " + dt_Matricula.Rows[0].ItemArray[9].ToString() + " | &nbsp;AÑO:  <strong>" + date.ToString("yyyy") + "</strong></td>";
                            }
                            else
                            {
                                cad += "<td style=\"text-align: center; \" colspan='2' >ALUMNO/A: <strong> " + dt_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[5].ToString() + "  " + dt_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[7].ToString() + "</strong>    |    SALÓN : " + dt_Matricula.Rows[0].ItemArray[9].ToString() + "</td>" +
                                      "<td style=\"\"> |  &nbsp;AÑO:  <strong>" + date.ToString("yyyy") + "</strong></td>";

                            }

                            cad += "</tr>" +
                                "<tr>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\"><span style=\"text-align: center; line-height: 1.42857143; background-color: transparent;\">&nbsp;</span>" +
                                      "<div style=\"text-align: center; \">" +
                                        "<div style=\"text-align: center; \">_____________________________</div>" +
                                        "<div style=\"text-align: center; \"><span style=\"line-height: 1.42857143; font-weight: bold; background-color: transparent;\">QUIMICA</span></div></td>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\"><span style=\"text-align: center; line-height: 1.42857143; background-color: transparent;\">&nbsp;</span>" +

                                        "<div style=\"text-align: center; \">" +
                                        "<div style=\"text-align: center; \">_____________________________</div>" +
                                        "<div style=\"text-align: center; \"><span style=\"font-weight: bold;\">&nbsp;FISICA</span></div></td>" +
                                    "<td style=\"text-align: center; border-color: rgb(0, 0, 0);\"><span style=\"text-align: center; line-height: 1.42857143; background-color: transparent;\">&nbsp;</span>" +

                                         "<div style=\"text-align: center; \">" +
                                        "<div style=\"text-align: center; \">_____________________________</div>" +
                                        "<div style=\"text-align: center; \"><span style=\"font-weight: bold;\">CIEN.NAT.BIO&nbsp;</span></div></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;<span style=\"text-align: center; line-height: 1.42857143; background-color: transparent;\">&nbsp;</span>" +
                                    "<div>" +
                                     "<div style=\"text-align: center; \">" +
                                        "<div style=\"text-align: center;\">____________________________</div>" +
                                        "<div style=\"text-align: center;\"><span style=\"line-height: 1.42857143; font-weight: bold; background-color: transparent;\">ÁREA T&Eacute;CNICA</span></div></td>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">" +
                                     "<div>" +
                                      "<div style=\"text-align: center;\">&nbsp;</div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold ;\">DIR. GRUPO</span></div></div></td>" +
                                   "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;" +
                                        "<div>" +
                                            "<div style=\"text-align: center;\"> </div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">ED. FíSICA</span></div></div></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;<span style=\"text-align: center; line-height: 1.42857143; background-color: transparent;\">&nbsp;</span>" +
                                     "<div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">SECRETARíA</span></div></div></td>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;" +
                                        "<div>" +
                                            "<div style=\"text-align: center;\">&nbsp;</div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">PAGADURIA</span></div></div></td>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;" +
                                        "<div>" +
                                            "<div style=\"text-align: center;\">&nbsp;</div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">BIBLIOTECA</span></div></div></td>" +
                               " </tr>" +
                                "<tr>" +
                                    "<td style=\"border-color: rgb(0, 0, 0);\">&nbsp;" +
                                         "<div>" +

                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">INFORMÁTICA</span></div></div></td>" +
                                   " <td style=\"border-color: rgb(0, 0, 0);\">&nbsp;</td>" +
                                   " <td style=\"border-color: rgb(0, 0, 0);\">&nbsp;" +
                                                "</div>" +
                                            "<div style=\"text-align: center;\">____________________________</div>" +
                                            "<div style=\"text-align: center;\"><span style=\"font-weight: bold;\">COORDINADOR</span></div></div></td>" +
                                "</tr>" +
                           " </tbody>" +
                        "</table>";
        
        
        
        
        
        
        
        
        //cad+="<tr>";
        //cad += "<td width='50%'><table width='100%'><tr>";
        //cad += "<td colspan='2'><img src='http://academico.itipuentenacional.edu.co/img/header_carnet.jpg'></td></tr><tr>";
        //cad += "<td rowspan='4' width='30%' style = 'padding: 5px ;text-align:center;'><img alt='logo' style='border: 1px solid #000' src = '" +dt_Estudiante.Rows[0].ItemArray[20].ToString().Replace("~", "../..") + "'  width='70' height='70' ></td>";
        //cad += "<td style='text-align:center ; '>" + dt_Estudiante.Rows[0].ItemArray[4].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[5].ToString() + "</td></tr><tr>";
        //cad += "<td style='text-align:center'>" + dt_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dt_Estudiante.Rows[0].ItemArray[7].ToString() + "</td></tr><tr>";
        //cad += "<td style='text-align:center'> D.I. "+dt_Estudiante.Rows[0].ItemArray[2].ToString()+"</td></tr><tr>";
        //cad += "<td style='text-align:right'></td></tr><tr>";
        //cad += "<td colspan='2' style='text-align:center'>SALÓN : "+dt_Matricula.Rows[0].ItemArray[9].ToString()+"</td></tr>";
        //cad += "<td colspan='2'><img src='http://academico.itipuentenacional.edu.co/img/footer_carnet.jpg'></td>";
        //cad += "</table></td>";
        //cad += "<td>Este carnet es personal e intransferible,acrédita al portador como estudiante del INSTITUTO TÉCNICO FRANCISCO DE ";
        //cad += " PAULA SANTANDER, en caso de pérdida favor comunicarse al sitio web http://wwww.itipuentenacional.edu.co </td></tr></table>";
        //cad += "<br>";
        //cad += "</table>";
        return cad;
    }

}