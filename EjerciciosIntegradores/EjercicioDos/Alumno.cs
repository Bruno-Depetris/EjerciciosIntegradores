using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosIntegradores.EjercicioDos {
    public class Alumno {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; }
        public int Legajo { get; set; }
        public int [ ] Notas { get; set; } = new int [3];

        public decimal CalcularPromedio() {
            int suma = 0;
            for(int i = 0; i < Notas.Length; i++) {
                suma += Notas[i];
            }
            return (decimal)suma / Notas.Length;
        }

        public bool EstaAprobado() {
            if(CalcularPromedio() >= 6) {
                return true;
            }
            return false;
        }
    }
}
