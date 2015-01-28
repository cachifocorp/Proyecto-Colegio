using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Proceso_Votacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        DataSetTableAdapters.votacionTableAdapter objVotacion = new DataSetTableAdapters.votacionTableAdapter();
        objVotacion.DeleteQuery();
        GridView1.DataBind();
    }
    protected void btnIniciar_Click(object sender, EventArgs e)
    {
        DataSetTableAdapters.votacionTableAdapter objVotacion = new DataSetTableAdapters.votacionTableAdapter();
        objVotacion.Iniciar_Votación();
        ShowNotification("Inicio Votación","La votación ha iniciado","info");
    }

    public void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }


    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        DataSetTableAdapters.votacionTableAdapter objVotacion = new DataSetTableAdapters.votacionTableAdapter();
        objVotacion.Finalizar_Votacion();
        ShowNotification("Inicio Votación", "La votación ha finalizado", "info");
    }
}