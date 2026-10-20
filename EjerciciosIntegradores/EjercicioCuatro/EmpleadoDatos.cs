using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioCuatro {
    public class EmpleadoDatos {
        private string connectionString = "";

        public void Guardar(Empleado empleado) {
            string consulta = "INSERT INTO Empleados (Nombre, Tipo, SueldoBase, VentasMensuales) VALUES (@Nombre, @Tipo, @SueldoBase, @VentasMensuales)";

            object ventas = DBNull.Value;
            if(empleado is Vendedor) {
                ventas = ((Vendedor)empleado).VentasMensuales;
            }

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@Tipo", empleado.Tipo);
                comando.Parameters.AddWithValue("@SueldoBase", empleado.SueldoBase);
                comando.Parameters.AddWithValue("@VentasMensuales", ventas);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<Empleado> ObtenerTodos() {
            List<Empleado> empleados = new List<Empleado>();
            string consulta = "SELECT Nombre, Tipo, SueldoBase, VentasMensuales FROM Empleados";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while(lector.Read()) {
                    Empleado empleado;

                    if(lector["Tipo"].ToString() == "Vendedor") {
                        Vendedor vendedor = new Vendedor();
                        vendedor.VentasMensuales = Convert.ToDecimal(lector["VentasMensuales"]);
                        empleado = vendedor;
                    } else {
                        empleado = new Administrativo();
                    }

                    empleado.Nombre = lector["Nombre"].ToString();
                    empleado.SueldoBase = Convert.ToDecimal(lector["SueldoBase"]);
                    empleados.Add(empleado);
                }
            }

            return empleados;
        }
    }
}
