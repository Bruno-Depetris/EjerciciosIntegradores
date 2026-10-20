namespace EjerciciosIntegradores.EjercicioCuatro {
    public class Administrativo : Empleado {
        public override string Tipo {
            get { return "Administrativo"; }
        }

        public override decimal CalcularSueldo() {
            return SueldoBase;
        }
    }
}
