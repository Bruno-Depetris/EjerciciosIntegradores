using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioCinco {
    public class ProductoDatos {
        private string connectionString = "";

        public List<Producto> ObtenerTodos() {
            List<Producto> productos = new List<Producto>();
            string consulta = "SELECT Id, Nombre, Categoria, Precio FROM Productos";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while(lector.Read()) {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(lector["Id"]);
                    producto.Nombre = lector["Nombre"].ToString();
                    producto.Categoria = lector["Categoria"].ToString();
                    producto.Precio = Convert.ToDecimal(lector["Precio"]);
                    productos.Add(producto);
                }
            }

            return productos;
        }
    }
}
