using System;
using System.Collections;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioUno {
    public class ProductoDatos {
        private string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=Purebas;" +
            "Integrated Security=True;" +
            "Persist Security Info=False;" +
            "Pooling=False;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "Application Name=EjerciciosIntegradores";

        public bool Guardar(Producto producto) {

            string consulta = $"INSERT INTO Productos (Nombre, Precio, Stock) VALUES ('{producto.Nombre}', {producto.Precio}, {producto.Cantidad})";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();
            if( comando.ExecuteNonQuery() > 0 ) {
                return true;
            }
            return false;

        }

        public ArrayList ObtenerTodos() {

            ArrayList productos = new ArrayList();

            string consulta = "SELECT Nombre, Precio, Stock FROM Productos";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            SqlDataReader lector = comando.ExecuteReader();

            while(lector.Read()) {
                Producto producto = new Producto();
                producto.Nombre = lector ["Nombre"].ToString();
                producto.Precio = Convert.ToDecimal(lector ["Precio"]);
                producto.Cantidad = Convert.ToInt32(lector ["Stock"]);
                
                productos.Add(producto);
            }


            return productos;
        }
    }
}
