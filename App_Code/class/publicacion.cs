using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for publicacion
/// </summary>
public class publicacion
{
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private String titulo;

    public String Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }
    private String descripcion;

    public String Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }
    private String imagen;

    public String Imagen
    {
        get { return imagen; }
        set { imagen = value; }
    }
    private int id_usuario;

    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }
    private DateTime timestamp;

    public DateTime Timestamp
    {
        get { return timestamp; }
        set { timestamp = value; }
    }
    private int categoria;

    public int Categoria
    {
        get { return categoria; }
        set { categoria = value; }
    }

    private int usuarioTipo;

    public int UsuarioTipo
    {
        get { return usuarioTipo; }
        set { usuarioTipo = value; }
    }


	public publicacion()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}