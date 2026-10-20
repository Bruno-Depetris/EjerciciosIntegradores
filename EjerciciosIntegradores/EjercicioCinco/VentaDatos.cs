using System;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioCinco {
    public class VentaDatos {
        private string connectionString = "";

        public void GuardarLinea(Producto producto, int cantidad) {
            string consulta = "INSERT INTO Ventas (Fecha, ProductoId, Cantidad, PrecioUnitario, Subtotal) VALUES (@Fecha, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal)";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ProductoId", producto.Id);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@PrecioUnitario", producto.Precio);
                comando.Parameters.AddWithValue("@Subtotal", producto.CalcularSubtotal(cantidad));

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
