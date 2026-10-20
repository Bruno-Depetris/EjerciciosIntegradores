using System.Net.NetworkInformation;

namespace EjerciciosIntegradores.EjercicioDos {
    public class Alumno {
        public string Nombre { get; set; }
        public int Legajo { get; set; }
        public int[] Notas { get; set; } = new int[3];

        public double CalcularPromedio() {
            int suma = 0;
            for(int i = 0; i < Notas.Length; i++) {
                suma += Notas[i];
            }
            return (double)suma / Notas.Length;
        }

        public bool EstaAprobado() {

            return CalcularPromedio() >= 6;
        }

        public override string ToString() {
            return Legajo + " - " + Nombre;
        }
    }
}
