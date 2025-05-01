using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_ventas_y_comprobante.Capa_Modelo
{
    public class Ventas
    {
        public int IdVentas {  get; set; }
        public string Fecha { get; set; }
        public decimal Total {  get; set; }
        public string NombreCliente { get; set; }
        public string DocumentoCliente { get; set; }
    }
}
