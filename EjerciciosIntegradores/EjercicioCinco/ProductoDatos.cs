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
            const string consulta = "SELECT Id, Nombre, Categoria, Precio " +
                                    "FROM Productos ORDER BY Nombre";

            using(SqlConnection conexion = new SqlConnection(connectionString))
            using(SqlCommand comando = new SqlCommand(consulta, conexion))
            using(SqlDataReader lector = AbrirLector(comando, conexion)) {
                while(lector.Read()) {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(lector["Id"]);
                    producto.Nombre = Convert.ToString(lector["Nombre"]);
                    producto.Categoria = Convert.ToString(lector["Categoria"]);
                    producto.Precio = Convert.ToDecimal(lector["Precio"]);
                    productos.Add(producto);
                }
            }

            return productos;
        }

        private static SqlDataReader AbrirLector(SqlCommand comando, SqlConnection conexion) {
            conexion.Open();
            return comando.ExecuteReader();
        }
    }
}
