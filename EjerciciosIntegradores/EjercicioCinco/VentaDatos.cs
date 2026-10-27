using System;
using System.Collections;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace EjerciciosIntegradores.EjercicioCinco {
    // la clase no se debe prestar a dudas.
    // es decir , no se debe llamar VentaDatos a una clase que maneje productos, por ejemplo.
    // pero ademas la clase no debe ser abstracta con respecto a su uso, es decir , si aca se maneja la venta de productos, no se debe llamar VentaDatos
    // a una clase que maneje la venta de servicios, por ejemplo.
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

        public bool GuardarVenta(Hashtable prodCant) {

            // la venta es de varios productos, es decir, una venta puede tener varios productos,
            // por lo que se debe recorrer el carrito y guardar cada producto en la base de datos

            //dictionaryentry es una clase que representa un par clave-valor en un diccionario en este caso hashtable,
            //por lo que se puede recorrer el hashtable con un foreach y obtener la clave y el valor de cada elemento
            // si no quiero usar dictionaryentry, puedo usar var y luego castear a int, pero es menos legible
            // ejemplo:
            // foreach (var item in prodCant) {
            //  int id = int.Parse(item.Key.ToString());
            //  int cantidad = int.Parse(item.Value.ToString());
            // }

            foreach (DictionaryEntry item in prodCant) {

                int id = int.Parse(item.Key.ToString());
                int cantidad = int.Parse(item.Value.ToString());
                ProductoDatos prod = new ProductoDatos();

                Producto producto = prod.BuscarProducto(id);

                //si el producto no existe, cortamos
                if (producto == null) {
                    return false;
                }

                //si no se pudo guardar la venta, cortamos
                //podriamos mostrar un mensaje aqui


                Venta venta = new Venta();

                venta.ProductoId = producto.Id;
                venta.Fecha = DateTime.Now;
                venta.Cantidad = cantidad;
                venta.PrecioUnitario = producto.Precio;
                venta.Subtotal = producto.CalcularSubtotal(cantidad);

                if (!CrearVenta(venta)) {
                    return false;
                }
            }

            return true;
        }

        //creamos la funcion CrearVenta que recibe un producto y la cantidad, y guarda la venta en la base de datos
        public bool CrearVenta(Venta venta) {
            string consulta = $"INSERT INTO DetalleVentas (ProductoId, Fecha, Cantidad, PrecioUnitario, Subtotal) VALUES ({venta.ProductoId}, '{venta.Fecha.ToString("yyyy-MM-dd HH:mm:ss")}', {venta.Cantidad}, {venta.PrecioUnitario.ToString()}, {venta.Subtotal.ToString()})";
            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);
            conexion.Open();
            int filasAfectadas = comando.ExecuteNonQuery();

            conexion.Close();
            if (filasAfectadas > 0) {
                return true;
            }
            return false;


            // lo puedo hacer de esta forma? si pero no tengo control al momento de hacer el close de la conexion, por lo que es mejor hacerlo como lo hice arriba
          
            //if (comando.ExecuteNonQuery() > 0) {
            //    return true;
            //}
            //return false;

        }



    }
}
