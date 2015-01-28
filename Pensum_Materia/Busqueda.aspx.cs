using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Pensum_Materia_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDescripcion.Focus();
        Form.DefaultButton = btnBuscar.UniqueID;
        if (!IsPostBack) {
            this.cargar();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Materia objMateria = new Materia();
            OperacionMateria objOpeMateria = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Materia.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objMateria.id = int.Parse(row.Cells[1].Text);
                        objMateria.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOpeMateria.EliminarMateria(objMateria);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Materia();
    }

    private void vertbl_Materia()
    {
        try
        {
            Materia objMateria = new Materia();
            OperacionMateria objOperMateria     = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMateria.id_grado                 = int.Parse(ddlGrado.SelectedValue.ToString());
            objMateria.id_area                  = int.Parse(ddlArea.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objMateria.descripcion          = txtDescripcion.Text.Trim();
            }
            else
            {
                objMateria.descripcion = null;
            }
            tbl_Materia.DataSource = objOperMateria.ConsultarMateria(objMateria);
            tbl_Materia.DataBind();
            if (tbl_Materia.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception)
        {
        }
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    public void cargar (){
        try {
            Grado objGrado                  = new Grado();
            Anio_Escolar objAnio_Escolar    = (Anio_Escolar)Session["anioEscolar"];
            OperacionGrado objOperGrado     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar        = objAnio_Escolar.id;
            Area objArea = new Area();
            OperacionArea objOperArea       = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objArea.id_anio_escolar         = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado), ddlGrado);
            clsFunciones.enlazarCombo(objOperArea.ConsultarArea(objArea),ddlArea);
        }
        catch (Exception) {}
    }

    protected void tbl_Materia_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Materia.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Materia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Materia.PageIndex = e.NewPageIndex;
        vertbl_Materia();
    }
}