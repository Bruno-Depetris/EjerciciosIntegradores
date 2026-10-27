using System.Collections;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioCinco {
    public class VentaDatos {
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

        public bool GuardarVenta(Hashtable carrito, ArrayList productos) {
            foreach (DictionaryEntry item in carrito) {
                int id = (int)item.Key;          
                int cantidad = (int)item.Value;  

                Producto producto = BuscarProducto(id, productos);

                // Si el producto no existe, cortamos
                if (producto == null) {
                    return false;
                }

                // Si no se pudo guardar la venta, cortamos
                if (!CrearVenta(producto, cantidad)) {
                    return false;
                }
            }

            return true; // Se guardó todo bien
        }

        public bool CrearVenta(Producto producto, int cantidad) {
            string consulta = $"INSERT INTO Ventas (ProductoId, Cantidad, Subtotal) VALUES ({producto.Id}, {cantidad}, {producto.CalcularSubtotal(cantidad)})";
            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);
            conexion.Open();
            if (comando.ExecuteNonQuery() > 0) {
                return true;
            }
            return false;

        }

       
        private Producto BuscarProducto(int id, ArrayList productos) {
            foreach (Producto producto in productos) {
                if (producto.Id == id) {
                    return producto;
                }
            }

            return null; 
        }
    }
}
