using Sistema_de_ventas_y_comprobante.Capa_Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Sistema_de_ventas_y_comprobante.Capa_Acceso_Datos
{
    internal class ProductoDAO
    {
        private string conexion = ConfigurationManager.ConnectionStrings["ConeccionBD"].ConnectionString;

        public Productos ObtenerPorCodigo(string codigo)
        {
            Productos producto = null;
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT * FROM Productos WHERE CodigoProducto = @codigo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    producto = new Productos
                    {
                        IdProductos = (int)reader["IdProductos"],
                        CodigoProducto = reader["CodigoProducto"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Stock = (int)reader["Stock"]
                    };
                }
            }
            return producto;
        }
    }
}
