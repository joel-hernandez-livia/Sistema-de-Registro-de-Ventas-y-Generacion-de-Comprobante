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
        private VentasDAO ventasDAO = new VentasDAO();
        private DetalleVentasDAO detalleDAO = new DetalleVentasDAO();
        private ProductoDAO productoDAO = new ProductoDAO();

        public bool RegistrarVentaConDetalles(DataGridView dgv, string nombreCliente, string documentoCliente, out string mensajeError)
        {
            mensajeError = "";

            if (dgv.Rows.Count == 0)
            {
                mensajeError = "No hay productos agregados.";
                return false;
            }

            decimal total = CalcularTotal(dgv);
            Ventas venta = new Ventas
            {
                Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Total = total,
                NombreCliente = nombreCliente,
                DocumentoCliente = documentoCliente
            };

            int idVenta = ventasDAO.RegistrarVenta(venta);

            foreach (DataGridViewRow fila in dgv.Rows)
            {
                string codigoProducto = fila.Cells["Codigo"].Value.ToString();
                int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                decimal precioUnit = Convert.ToDecimal(fila.Cells["PrecioUnit"].Value);
                string descripcion = fila.Cells["Descripcion"].Value.ToString();

                Productos producto = productoDAO.ObtenerPorCodigo(codigoProducto);
                if (producto == null)
                {
                    mensajeError = $"Producto con código '{codigoProducto}' no encontrado.";
                    return false;
                }

                DetalleVenta detalle = new DetalleVenta
                {
                    VentaId = idVenta,
                    ProductoId = producto.IdProductos,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnit,
                    NombreProducto = descripcion
                };

                detalleDAO.RegistrarDetalle(detalle);
            }

            return true;
        }

        public decimal CalcularTotal(DataGridView dgv)
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                total += Convert.ToDecimal(fila.Cells["Importe"].Value);
            }
            return total;
        }

        public Productos BuscarProductoPorCodigo(string codigo)
        {
            return productoDAO.ObtenerPorCodigo(codigo);
        }

        public bool ProductoYaAgregado(DataGridView dgv, string codigo, out int filaExistente)
        {
            filaExistente = -1;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["Codigo"].Value.ToString() == codigo)
                {
                    filaExistente = i;
                    return true;
                }
            }
            return false;
        }
    }

}
