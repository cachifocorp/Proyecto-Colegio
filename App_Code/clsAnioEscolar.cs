using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de clsAnioEscolar
/// </summary>
public class clsAnioEscolar
{
	public clsAnioEscolar()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    private int id;
    private string descripcion;
    private DateTime fecha_inicio;
    private DateTime fecha_fin;
    private decimal nota_minima;
    private int id_colegio;
    private int numero_periodos;
    private decimal rendimiento_superior;
    private decimal rendimiento_alto; 
    private decimal rendimiento_basico;
    private decimal rendimiento_bajo;
    private decimal nota_maxima;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }
    

    public DateTime Fecha_inicio
    {
        get { return fecha_inicio; }
        set { fecha_inicio = value; }
    }
   

    public DateTime Fecha_fin
    {
        get { return fecha_fin; }
        set { fecha_fin = value; }
    }
   

    public decimal Nota_minima
    {
        get { return nota_minima; }
        set { nota_minima = value; }
    }
    

    public decimal Nota_maxima
    {
        get { return nota_maxima; }
        set { nota_maxima = value; }
    }
    

    public decimal Rendimiento_bajo
    {
        get { return rendimiento_bajo; }
        set { rendimiento_bajo = value; }
    }
    

    public decimal Rendimiento_basico
    {
        get { return rendimiento_basico; }
        set { rendimiento_basico = value; }
    }
    

    public decimal Rendimiento_alto
    {
        get { return rendimiento_alto; }
        set { rendimiento_alto = value; }
    }
    

    public decimal Rendimiento_superior
    {
        get { return rendimiento_superior; }
        set { rendimiento_superior = value; }
    }
    

    public int Numero_periodos
    {
        get { return numero_periodos; }
        set { numero_periodos = value; }
    }
    

    public int Id_colegio
    {
        get { return id_colegio; }
        set { id_colegio = value; }
    }
}