using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_ventas_y_comprobante.Capa_Modelo
{
    public class Productos
    {
        public int IdProductos { get; set; }
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
