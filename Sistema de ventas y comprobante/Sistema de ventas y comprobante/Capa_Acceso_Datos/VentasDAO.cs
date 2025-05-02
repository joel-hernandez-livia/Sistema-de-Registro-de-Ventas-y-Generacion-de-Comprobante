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
    internal class VentasDAO
    {
        private string conexion = ConfigurationManager.ConnectionStrings["ConeccionBD"].ConnectionString;

        public int RegistrarVenta(Ventas venta)
        {
            int idVenta = 0;

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = @"INSERT INTO Ventas (Fecha, Total, NombreCliente, DocumentoCliente)
                             VALUES (@Fecha, @Total, @NombreCliente, @DocumentoCliente);
                             SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                cmd.Parameters.AddWithValue("@Total", venta.Total);
                cmd.Parameters.AddWithValue("@NombreCliente", venta.NombreCliente);
                cmd.Parameters.AddWithValue("@DocumentoCliente", venta.DocumentoCliente);

                conn.Open();
                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return idVenta;
        }
    }

}
