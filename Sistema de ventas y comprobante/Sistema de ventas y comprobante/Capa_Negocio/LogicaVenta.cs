using Sistema_de_ventas_y_comprobante.Capa_Acceso_Datos;
using Sistema_de_ventas_y_comprobante.Capa_Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_ventas_y_comprobante.Capa_Negocio
{
    public class LogicaVenta
    {
        private ProductoDAO productoDAO = new ProductoDAO();

        public Productos BuscarProductoPorCodigo(string codigo)
        {
            return productoDAO.ObtenerPorCodigo(codigo);
        }

        public bool ProductoYaAgregado(DataGridView dgv, string codigo, out int filaExistente)
        {
            filaExistente = -1;
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                if (fila.Cells["Codigo"].Value?.ToString() == codigo)
                {
                    filaExistente = fila.Index;
                    return true;
                }
            }
            return false;
        }

        public decimal CalcularTotal(DataGridView dgv)
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                if (fila.Cells["Importe"].Value != null)
                {
                    total += Convert.ToDecimal(fila.Cells["Importe"].Value);
                }
            }
            return total;
        }
    }

}
