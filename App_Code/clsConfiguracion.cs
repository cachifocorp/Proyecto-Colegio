using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de clsConfiguracion
/// </summary>
public class clsConfiguracion
{

    private static string color_sistema = "green";


	public clsConfiguracion()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public static string Color_Sistema
    {
        get { return clsConfiguracion.color_sistema; }
        set { clsConfiguracion.color_sistema = value; }
    }


}