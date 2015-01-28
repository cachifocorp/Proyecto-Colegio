using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AccesoInfo
{
    /// <summary>
    /// Summary description for transacciones
    /// </summary>
    public class transacciones
    {
         
        clsDb db = new clsDb();
         
        public transacciones(){} 
            public String getTecnica(int id_salon, Int64 cedula){
                String SQL = "exec sp_maestro_asignacion @p_opcion_maestro=N'T',@p_id=NULL,@p_id_salon="+id_salon+",@p_id_materia=NULL,@p_id_docente=NULL,@p_intensidad=NULL,@p_tecnica=NULL,@p_id_usuario="+cedula;
                String resultado = "";
                try{
                    SqlConnection con = db.conexion();
                    con.Open();
                    SqlCommand com = new SqlCommand(SQL, con);
                    SqlDataReader reader = com.ExecuteReader(); 
                    while (reader.Read()){
                        resultado = reader["descripcion"].ToString();
                    }
                    con.Close();
                    return resultado;
                }catch(Exception ex){
                    return resultado;
                }
                    
           }
        
    }
}