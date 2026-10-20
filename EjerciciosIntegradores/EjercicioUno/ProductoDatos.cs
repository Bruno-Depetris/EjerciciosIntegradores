using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioUno {
    public class ProductoDatos {
        private string connectionString = "";

        public void Guardar(Producto producto) {
            string consulta = "INSERT INTO Productos (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock)";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Stock", producto.Cantidad);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<Producto> ObtenerTodos() {
            List<Producto> productos = new List<Producto>();
            string consulta = "SELECT Nombre, Precio, Stock FROM Productos";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while(lector.Read()) {
                    Producto producto = new Producto();
                    producto.Nombre = lector["Nombre"].ToString();
                    producto.Precio = Convert.ToDecimal(lector["Precio"]);
                    producto.Cantidad = Convert.ToInt32(lector["Stock"]);
                    productos.Add(producto);
                }
            }

            return productos;
        }
    }
}
