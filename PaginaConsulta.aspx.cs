using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaginaConsulta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataSource datasource = new SqlDataSource();
        datasource.SelectCommand = TextBox1.Text;
        datasource.ConnectionString = ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString;
        GridView1.DataSource = datasource;
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clsDb db = new clsDb();
        String SQL = TextBox1.Text;
		Response.Write(SQL);
        db.ejecutar(SQL);
    }
}