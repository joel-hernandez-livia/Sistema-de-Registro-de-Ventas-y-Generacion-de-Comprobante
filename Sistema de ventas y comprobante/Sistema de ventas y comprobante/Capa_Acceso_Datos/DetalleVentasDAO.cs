using Sistema_de_ventas_y_comprobante.Capa_Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_ventas_y_comprobante.Capa_Acceso_Datos
{
    internal class DetalleVentasDAO
    {
        private string conexion = ConfigurationManager.ConnectionStrings["ConeccionBD"].ConnectionString;

        public void RegistrarDetalle(DetalleVenta detalle)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = @"INSERT INTO DetalleVenta (VentaId, ProductoId, Cantidad, PrecioUnitario, NombreProducto)
                             VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @NombreProducto)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VentaId", detalle.VentaId);
                cmd.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@NombreProducto", detalle.NombreProducto);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
