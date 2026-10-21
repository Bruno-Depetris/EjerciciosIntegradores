using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosIntegradores.EjercicioTres {
    public class Libro {
        public int LibroID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }

        public string ObtenerDescripcion() {
            return Titulo + " " + Autor + " " + Anio;
        }
    }
}
