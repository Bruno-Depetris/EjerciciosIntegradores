using System;
using System.Collections;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioCinco {
    public class ProductoDatos {
        private string connectionString =
           "Data Source=DESKTOP-OANB4MT;" +
           "Initial Catalog=EjerciciosIntegradores;" +
           "Integrated Security=True;" +
           "Persist Security Info=False;" +
           "Pooling=False;" +
           "MultipleActiveResultSets=False;" +
           "Encrypt=False;" +
           "TrustServerCertificate=False;" +
           "Application Name=EjerciciosIntegradores";

        public ArrayList ObtenerTodos() {
            ArrayList productos = new ArrayList();
            string consulta = "SELECT Id, Nombre, Categoria, Precio " +
                                    "FROM Productos ORDER BY Nombre";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);
            conexion.Open();
            SqlDataReader lector = comando.ExecuteReader();
            

            while (lector.Read()) {
                Producto producto = new Producto();
                producto.Id = Convert.ToInt32(lector["Id"]);
                producto.Nombre = Convert.ToString(lector["Nombre"]);
                producto.Categoria = Convert.ToString(lector["Categoria"]);
                producto.Precio = Convert.ToDecimal(lector["Precio"]);
                productos.Add(producto);
            }

            conexion.Close();

            return productos;
        }
        public Producto BuscarProducto(int id) {
            string consulta = $"SELECT Id, Nombre, Categoria, Precio " +
                                    $"FROM Productos WHERE Id = {id}";
            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);
            conexion.Open();
            //abro la conexión antes de ejecutar el lector
            SqlDataReader lector = comando.ExecuteReader();
            
            
            if (lector.Read()) {
                Producto producto = new Producto();
                producto.Id = Convert.ToInt32(lector["Id"]);
                producto.Nombre = Convert.ToString(lector["Nombre"]);
                producto.Categoria = Convert.ToString(lector["Categoria"]);
                producto.Precio = Convert.ToDecimal(lector["Precio"]);


                
                return producto;
            }
            //cierro la conexión después de ejecutar el lector
            conexion.Close();
            return null;
        }
    }
}
