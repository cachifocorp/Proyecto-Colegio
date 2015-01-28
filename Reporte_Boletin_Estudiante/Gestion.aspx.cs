using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Boletin_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.openxmlformatsofficedocument.wordprocessingml.documet";
        Response.AddHeader("content-disposition", "attachment; filename=Boletin.doc");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:word\">");
        Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.Charset = "";
        EnableViewState = false;
        /*System.IO.StringWriter writer = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter html = new System.Web.UI.HtmlTextWriter(writer);
        content.RenderControl(html);*/
        Response.Write(clsFunciones.boletin);
        Response.End();
    }

    public void excel()
    {

        Response.ContentType = "application/force-download";
        Response.AddHeader("content-disposition", "attachment; filename=Print.xls");
        Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
        Response.Write("<head>");
        Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        Response.Write("<!--[if gte mso 9]><xml>");
        Response.Write("<x:ExcelWorkbook>");
        Response.Write("<x:ExcelWorksheets>");
        Response.Write("<x:ExcelWorksheet>");
        Response.Write("<x:Name>Report Data</x:Name>");
        Response.Write("<x:WorksheetOptions>");
        Response.Write("<x:Print>");
        Response.Write("<x:ValidPrinterInfo/>");
        Response.Write("</x:Print>");
        Response.Write("</x:WorksheetOptions>");
        Response.Write("</x:ExcelWorksheet>");
        Response.Write("</x:ExcelWorksheets>");
        Response.Write("</x:ExcelWorkbook>");
        Response.Write("</xml>");
        Response.Write("<![endif]--> ");
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        content.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.Write("</head>");
        Response.Flush();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        MemoryStream ms = new MemoryStream();
        Document document = new Document(PageSize.A4, 25, 25, 30, 30);
        PdfWriter writer = PdfWriter.GetInstance(document, ms);
        document.Open();
        /*for (int i = 0; i < 15; i++)
        {
            tabla.AddCell(""+i);
        }*/
        PdfPTable tabla = new PdfPTable(3);
        // the cell object
        PdfPCell cell;
        // we add a cell with colspan 3
        cell = new PdfPCell(new Phrase("Cell with colspan 3"));
        cell.Colspan = 3;
        tabla.AddCell(cell);
        // now we add a cell with rowspan 2
        cell = new PdfPCell(new Phrase("Cell with rowspan 2"));
        cell.Rowspan = 2;
        cell.Colspan = 2;
        tabla.AddCell(cell);
        // we add the four remaining cells with addCell()
        
        tabla.AddCell("row 2; cell 1");
        tabla.AddCell("row 2; cell 2");

        document.Add(new Paragraph("Hello World"));
        document.Add(tabla);
        document.Close();
        writer.Close();
        ms.Close();
        Response.ContentType = "pdf/application";
        Response.AddHeader("content-disposition", "attachment;filename=First_PDF_document.pdf");
        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

    }
}