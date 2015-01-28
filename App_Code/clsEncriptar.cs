using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de clsEncriptar
/// </summary>
public class clsEncriptar
{
    public clsEncriptar()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //

    }

    public static string Desencriptar(string stringToDecrypt)
    {

        try
        {
            return int.Parse(stringToDecrypt, System.Globalization.NumberStyles.HexNumber).ToString();
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public static string Encriptar(string stringToEncrypt)
    {
        try
        {
            int i = int.Parse(stringToEncrypt);
            return i.ToString("X");
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }


}