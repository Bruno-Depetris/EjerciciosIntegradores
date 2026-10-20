namespace EjerciciosIntegradores.EjercicioCuatro {
    public class Vendedor : Empleado {
        public decimal VentasMensuales { get; set; }

        public override string Tipo {
            get { return "Vendedor"; }
        }

        public override decimal CalcularSueldo() {
            return SueldoBase + VentasMensuales * 0.05m;
        }
    }
}
