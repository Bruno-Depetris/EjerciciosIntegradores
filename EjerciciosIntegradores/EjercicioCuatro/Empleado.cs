namespace EjerciciosIntegradores.EjercicioCuatro {
    public abstract class Empleado {
        public string Nombre { get; set; }
        public decimal SueldoBase { get; set; }

        public abstract string Tipo { get; }

        public abstract decimal CalcularSueldo();
    }
}
