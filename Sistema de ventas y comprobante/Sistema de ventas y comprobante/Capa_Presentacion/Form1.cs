using iTextSharp.text;
using Sistema_de_ventas_y_comprobante.Capa_Acceso_Datos;
using Sistema_de_ventas_y_comprobante.Capa_Modelo;
using Sistema_de_ventas_y_comprobante.Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Sistema_de_ventas_y_comprobante
{
    public partial class Form1 : Form
    {
        private LogicaVenta logicaVenta = new LogicaVenta();
        public Form1()
        {
            InitializeComponent();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.Columns.Add("Codigo", "Codigo");
            dgvProductos.Columns.Add("Descripcion", "Descripcion");
            dgvProductos.Columns.Add("PrecioUnit", "Precio Unitario");
            dgvProductos.Columns.Add("Cantidad", "Cantidad");
            dgvProductos.Columns.Add("Importe", "Importe");

            dgvProductos.Columns["Codigo"].FillWeight = 20;        
            dgvProductos.Columns["Descripcion"].FillWeight = 40; 
            dgvProductos.Columns["PrecioUnit"].FillWeight = 15;
            dgvProductos.Columns["Cantidad"].FillWeight = 10;
            dgvProductos.Columns["Importe"].FillWeight = 15;

            lblTotalOutput.Text = logicaVenta.CalcularTotal(dgvProductos).ToString("0.00");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //int indice_fila = dgvProductos.Rows.Add();
           // DataGridViewRow fila = dgvProductos.Rows[indice_fila];



            string codigo = txtbCodigoProducto.Text.Trim();
            //int cantidad = int.Parse(txtbCantidadProducto.Text.Trim());

            int cantidad;
            if (!int.TryParse(txtbCantidadProducto.Text.Trim(), out cantidad))
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var producto = logicaVenta.BuscarProductoPorCodigo(codigo);
            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            if (logicaVenta.ProductoYaAgregado(dgvProductos, codigo, out int filaExistente))
            {
                int nuevaCantidad = int.Parse(dgvProductos.Rows[filaExistente].Cells["Cantidad"].Value.ToString()) + cantidad;
                dgvProductos.Rows[filaExistente].Cells["Cantidad"].Value = nuevaCantidad;
                dgvProductos.Rows[filaExistente].Cells["Importe"].Value = nuevaCantidad * producto.Precio;
            }
            else
            {
                dgvProductos.Rows.Add(codigo, producto.Nombre, producto.Precio.ToString("0.00"), cantidad, (producto.Precio * cantidad).ToString("0.00"));
            }

            lblTotalOutput.Text = logicaVenta.CalcularTotal(dgvProductos).ToString("0.00");
            txtbCodigoProducto.Clear();
            txtbCantidadProducto.Clear();
            txtbCodigoProducto.Focus();


        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?",
                                                      "Confirmar eliminación",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvProductos.Rows.RemoveAt(dgvProductos.SelectedRows[0].Index);
                    lblTotalOutput.Text = logicaVenta.CalcularTotal(dgvProductos).ToString("0.00");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.",
                                "Información",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }



        private void btnVender_Click(object sender, EventArgs e)
        {
            string nombreCliente = txtbNombreCliente.Text.Trim();
            string documentoCliente = txtbDocumentoCliente.Text.Trim();

            if (string.IsNullOrEmpty(nombreCliente) || string.IsNullOrEmpty(documentoCliente))
            {
                MessageBox.Show("Debe ingresar el nombre y documento del cliente.");
                return;
            }

            bool ventaExitosa = logicaVenta.RegistrarVentaConDetalles(dgvProductos, nombreCliente, documentoCliente, out string error);

            if (ventaExitosa)
            {
                MessageBox.Show("Venta registrada exitosamente.");
                

                // Obtener detalles de la venta para el comprobante
                List<DetalleVenta> detallesVenta = new List<DetalleVenta>();
                foreach (DataGridViewRow fila in dgvProductos.Rows)
                {
                    detallesVenta.Add(new DetalleVenta
                    {
                        NombreProducto = fila.Cells["Descripcion"].Value.ToString(), 
                        Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDecimal(fila.Cells["PrecioUnit"].Value)
                    });
                }

                decimal total = logicaVenta.CalcularTotal(dgvProductos);
                Ventas venta = new Ventas
                {
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Total = total,
                    NombreCliente = nombreCliente,
                    DocumentoCliente = documentoCliente
                };

                // Generar PDF
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string carpetaRaiz = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
                string carpetaComprobantes = Path.Combine(carpetaRaiz, "Comprobantes generados");
                if (!Directory.Exists(carpetaComprobantes))
                {
                    Directory.CreateDirectory(carpetaComprobantes);
                }
                string ruta = Path.Combine(carpetaComprobantes, $"Comprobante_Venta_{txtbDocumentoCliente.Text}_{timestamp}.pdf");
                GeneradorPDF generador = new GeneradorPDF();
                generador.GenerarComprobante(venta, detallesVenta, ruta);

                // Mostrar mensaje
                MessageBox.Show($"Venta registrada exitosamente.\nComprobante generado en:\n{ruta}", "Éxito");


                dgvProductos.Rows.Clear();
                txtbNombreCliente.Clear();
                txtbDocumentoCliente.Clear();
                lblTotalOutput.Text = "S/. 0.00";

            }
            else
            {
                MessageBox.Show("Error al registrar venta: " + error);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalOutput_Click(object sender, EventArgs e)
        {

        }
    }
}
