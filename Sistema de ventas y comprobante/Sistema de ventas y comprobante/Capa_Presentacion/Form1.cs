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

namespace Sistema_de_ventas_y_comprobante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //dgvProductos.AllowUserToAddRows = false;

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
        }
        private LogicaVenta logicaVenta = new LogicaVenta();
        private void button1_Click(object sender, EventArgs e)
        {
            //int indice_fila = dgvProductos.Rows.Add();
           // DataGridViewRow fila = dgvProductos.Rows[indice_fila];



            string codigo = txtbCodigoProducto.Text.Trim();
            int cantidad = int.Parse(txtbCantidadProducto.Text.Trim());

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
                dgvProductos.Rows.RemoveAt(dgvProductos.SelectedRows[0].Index);
                lblTotalOutput.Text = logicaVenta.CalcularTotal(dgvProductos).ToString("0.00");
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
