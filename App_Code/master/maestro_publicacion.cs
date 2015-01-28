using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for maestro_publicacion
/// </summary>
public class maestro_publicacion
{
    clsDb db = new clsDb();
	public maestro_publicacion()
	{		
	}

    public String getPublicaciones( int id_tipousuario, int id_categoria) {

        String SQL = " select  id, titulo, descripcion,categoria,id_usuariotipo, timestamp, image " +
            "from  publicacion where id_usuariotipo = " + id_tipousuario + " and categoria= " + id_categoria;
           // " order by timestamp OFFSET 0 ROWS FETCH NEXT 4 ROWS ONLY";
        String pub = "";
        try
        {

            SqlConnection con = db.conexion();
            con.Open();
            SqlCommand com = new SqlCommand(SQL, con);
            SqlDataReader reader = com.ExecuteReader();
            //<ul class="thumbnails">
            int count = 0;
            while (reader.Read()){
                if (count==0)
                {
                    pub+="<ul class=\"thumbnails\">";
                }
                if (count <4){
                    pub += "<li class=\"span4\">" +
                    "<div class=\"thumbnail\">" +
                      "<img src=\"http://academico.itipuentenacional.edu.co/Reporte_Noticia/images/" + reader["image"] + "\"  style=\"width: 300px; height: 200px;\">" +
                      "<div class=\"caption\">" +
                        "<h3>" + reader["titulo"] + "</h3> <hr>" +
                        "<p>" + reader["descripcion"].ToString().Substring(0, 30) + "...</p>" +
                        "<p><a href=\"noticia/id/"+reader["id"]+"\" class=\"btn btn-primary\">Leer más</a></p>" +
                      "</div>" +
                    "</div>" +
                  "</li>";
                    count++;
                }
                else {
                    count = 0;
                    pub += "</ul>";
                }
            }
            con.Close();
            return pub;
        }
        catch (Exception ex)
        {
            return pub;
        }
        
    }


        public String getPublicacion(int id){
            String data = "";
            try
            {
                String SQL = "select * from publicacion where id = "+id;
                SqlConnection con = db.conexion();
                con.Open();
                SqlCommand com = new SqlCommand(SQL, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read()) {
                data+="<div class=\"page-header text-center\">"+
                        "<h1>"+reader["titulo"]+"<small></small></h1>"+
                    "</div>"+
                       " <p class=\"lead \" style=\"text-align : justify;\">" + reader["descripcion"] + "</p>" +
                    "<img src=\"http://academico.itipuentenacional.edu.co/Reporte_Noticia/images/" + reader["image"] + "\"  />";

                }
                con.Close();
                return data;
            }
            catch (Exception ex) {
                return data;
            }
       }
    
}