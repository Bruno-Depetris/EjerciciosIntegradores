namespace EjerciciosIntegradores.EjercicioUno {
    public class Producto {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public decimal CalcularValor() {
            return Precio * Cantidad;
        }
    }
}
