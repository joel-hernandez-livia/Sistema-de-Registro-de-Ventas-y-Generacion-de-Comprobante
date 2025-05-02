using iTextSharp.text.pdf;
using iTextSharp.text;
using Sistema_de_ventas_y_comprobante.Capa_Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_ventas_y_comprobante.Capa_Negocio
{
    public class GeneradorPDF
    {
        public void GenerarComprobante(Ventas venta, List<DetalleVenta> detalles, string rutaArchivo)
        {
            Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

            using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Encabezado
                Paragraph titulo = new Paragraph("Comprobante de Venta", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph("Fecha: " + venta.Fecha));
                doc.Add(new Paragraph("Cliente: " + venta.NombreCliente));
                doc.Add(new Paragraph("Documento: " + venta.DocumentoCliente));
                doc.Add(new Paragraph(" ")); // espacio

                // Tabla de productos
                PdfPTable tabla = new PdfPTable(4); // 4 columnas
                tabla.WidthPercentage = 100;
                tabla.AddCell("Producto");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Precio Unitario");
                tabla.AddCell("Importe");

                foreach (var d in detalles)
                {
                    tabla.AddCell(d.NombreProducto); // Asegúrate de tener este campo
                    tabla.AddCell(d.Cantidad.ToString());
                    tabla.AddCell("S/. " + d.PrecioUnitario.ToString("0.00"));
                    tabla.AddCell("S/. " + (d.Cantidad * d.PrecioUnitario).ToString("0.00"));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Total: S/. " + venta.Total.ToString("0.00")));
                doc.Close();
            }
        }
    }
}
