namespace EjerciciosIntegradores.EjercicioTres {
    public class Libro {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }

        public string ObtenerDescripcion() {
            return "Título: " + Titulo + "\nAutor: " + Autor + "\nAño: " + Anio;
        }

        public override string ToString() {
            return Titulo;
        }
    }
}
