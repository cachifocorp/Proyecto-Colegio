using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de clsFunciones
/// </summary>
public class clsFunciones
{

    public clsFunciones()
    {

        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Envia el nombre del menu padre o modulo
    /// </summary>
    public static string nombre_padre { get; set; }

    /// <summary>
    /// Envia el nombre del menu hijo
    /// </summary>
    public static string nombre_hijo { get; set; }

    public static int guardar_area { get; set; }

    /// <summary>
    /// Función para limpiar los controles de un formulario
    /// </summary>
    /// <param name="controles">Controles</param>
    public static void CleanControl(ControlCollection controles)
    {
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Text = string.Empty;
            else if (control is DropDownList)
                ((DropDownList)control).ClearSelection();
            else if (control is RadioButtonList)
                ((RadioButtonList)control).ClearSelection();
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Checked = false;
            else if (control is CheckBox)
                ((CheckBox)control).Checked = false;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }

    public static void lockControl(ControlCollection controles)
    {
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Enabled = false;
            else if (control is DropDownList)
                ((DropDownList)control).Enabled = false;
            else if (control is RadioButtonList)
                ((RadioButtonList)control).Enabled = false;
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Enabled = false;
            else if (control is CheckBox)
                ((CheckBox)control).Enabled = false;
            else if (control is FileUpload)
                ((FileUpload)control).Enabled = false;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }

    public static void unlockControl(ControlCollection controles)
    {
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Enabled = true;
            else if (control is DropDownList)
                ((DropDownList)control).Enabled = true;
            else if (control is RadioButtonList)
                ((RadioButtonList)control).Enabled = true;
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Enabled = true;
            else if (control is CheckBox)
                ((CheckBox)control).Enabled = true;
            else if (control is FileUpload)
                ((FileUpload)control).Enabled = true;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }

    public static DataTable municipio { get; set; }

    public static DataTable salon { get; set; }
    
    public static int documento_acudiente_1 { get; set; }

    public static int documento_acudiente_2 { get; set; }

    public static void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }

    public static void enlazarCombo(DataView     dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }

    public static string nombre_nota { get; set;}

    public static string boletin { get; set; }

    public static string consolidado { get; set; }

    public static string planilla { get; set; }

    public static string carnet { get; set; }

    public Object[] redimencionar(FileUpload img, int newWidth, int newHeight,string nombre)
    {

        Bitmap originalBMP = new Bitmap(img.FileContent);
        Object[] datos = new Object[2];
        // Calculate the new image dimensions
        int origWidth = originalBMP.Width;
        int origHeight = originalBMP.Height;
        int sngRatio = origWidth / origHeight;
        // Create a new bitmap which will hold the previous resized bitmap
        Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
        // Create a graphic based on the new bitmap
        Graphics oGraphics = Graphics.FromImage(newBMP);

        // Set the properties for the new graphic file
        oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw the new graphic based on the resized bitmap
        oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

        // Save the new graphic file to the server
        // newBMP.Save(directory + "tn_" + filename);
       
        // Once finished with the bitmap objects, we deallocate them.
        datos[1] = nombre+".jpg";
        datos[0] = newBMP;
        originalBMP.Dispose();
        oGraphics.Dispose();

        return datos;
    }
}