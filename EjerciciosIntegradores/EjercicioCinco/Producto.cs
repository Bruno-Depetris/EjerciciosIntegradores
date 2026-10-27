using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosIntegradores.EjercicioCinco {

    public class Producto {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        public decimal CalcularSubtotal(int cantidad) {
            return Precio * cantidad;
        }
    }
}
