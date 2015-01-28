using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de clsDb
/// </summary>
public class clsDb
{
    public clsDb()
    {
    }


    private int ci(string dato)
    {
        return Convert.ToInt32(dato);
    }

    public SqlConnection conexion()
    {
        SqlConnection cone = new SqlConnection(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        return cone;
    }

    public String ContorlasFilas(String sql)
    {
        String dato = "";
        SqlConnection cone = conexion();
        cone.Open();
        SqlCommand objComand = new SqlCommand(sql, cone);
        SqlDataReader objReader = objComand.ExecuteReader();

        if (objReader.Read())
        {
            dato = objReader.GetValue(0).ToString();
        }
        cone.Close();
        return dato;
    }

    public String ContorlasColumnas(String sql)
    {
        String dato = "";
        SqlConnection cone = conexion();
        cone.Open();
        SqlCommand objComand = new SqlCommand(sql, cone);
        SqlDataReader objReader = objComand.ExecuteReader();

        if (objReader.Read())
        {
            dato = "" + Convert.ToInt32(objReader.HasRows); ;
        }
        cone.Close();
        return dato;
    }

    public String[,] RetornarMatriz(String sql)
    {

        try
        {
            SqlConnection cone = conexion();
            cone.Open();
            int c = 0;
            SqlCommand objComand = new SqlCommand(sql, cone);
            SqlDataReader objReader = objComand.ExecuteReader();
            DataTable da = new DataTable();
            da.Load(objReader);
            String[,] datos = new String[da.Rows.Count, da.Columns.Count];

            for (int j = 0; j < da.Rows.Count; j++)
            {
                DataRow fil = da.Rows[c];
                for (int i = 0; i < da.Columns.Count; i++)
                {

                    datos[c, i] = fil[i].ToString();
                }
                c++;
            }
            cone.Close();
            return datos;
        }
        catch
        {
            String[,] datos = new String[1, 1];
            datos[0, 0] = "";
            return datos;
        }
    }

    public String[] returnVector(String sql)
    {
        try
        {
            SqlConnection cone = conexion();
            cone.Open();
            int c = 0;
            SqlCommand objComand = new SqlCommand(sql, cone);
            SqlDataReader objReader = objComand.ExecuteReader();


            DataTable da = new DataTable();
            da.Load(objReader);
            int fila = Convert.ToInt32(da.Columns.Count);
            String[] datos = new String[fila];
            for (int j = 0; j < da.Columns.Count; j++)
            {
                DataRow fil = da.Rows[c];
                datos[j] = fil[j].ToString();

            }
            cone.Close();
            return datos;
        }
        catch
        {
            String[] datos = { "" };
            return datos;
        }
    }

    public bool ejecutar(String sql)
    {
        try
        {
            SqlConnection cone = conexion();
            cone.Open();
            SqlCommand objComand = new SqlCommand(sql, cone);
            objComand.ExecuteNonQuery();
            cone.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
}