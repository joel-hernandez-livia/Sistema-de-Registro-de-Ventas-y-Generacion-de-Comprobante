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
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            int indice_fila = dgvProductos.Rows.Add();
            DataGridViewRow fila = dgvProductos.Rows[indice_fila];

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnVender_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
