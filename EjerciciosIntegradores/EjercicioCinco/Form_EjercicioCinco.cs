using System;
using System.Collections;
using System.Windows.Forms;

namespace EjerciciosIntegradores.EjercicioCinco {
    public partial class Form_EjercicioCinco : Form {
        // variables con acceso global para poder acceder a ellas desde cualquier metodo de la clase Form_EjercicioCinco


        //clases que manejan los datos de la base de datos, estas clases se encargan de obtener los productos y guardar las ventas
        private ProductoDatos productoDatos = new ProductoDatos();
        private VentaDatos ventaDatos = new VentaDatos();

        //arraylist que contiene todos los productos que se encuentran en la base de datos, se carga al iniciar el formulario
        private ArrayList productos = new ArrayList();

        //hashtable que contiene los productos que se agregan al carrito, la clave es el id del producto y el valor es la cantidad
        private Hashtable carrito = new Hashtable();       //clave-valor => producto cantidad
  

        public Form_EjercicioCinco() {
            InitializeComponent();

            //creo todas las columnas del datagridview
            dgvDetalle.Columns.Add("Column_Producto", "Producto");
            dgvDetalle.Columns.Add("Column_Categoria", "Categoría");
            dgvDetalle.Columns.Add("Column_Cantidad", "Cantidad");
            dgvDetalle.Columns.Add("Column_Precio", "Precio");
            dgvDetalle.Columns.Add("Column_Subtotal", "Subtotal");

            CargarComboBox();
        }


        //esta funcion sirve para cargar el combobox con los productos que se encuentran en la base de datos
        private void CargarComboBox() {
            try {
                //obtengo todos los productos de la base de datos y los guardo en un arraylist
                productos = productoDatos.ObtenerTodos();

                cmbProducto.Items.Clear();
                cmbProducto.Items.Insert(0, "Seleccione un producto");

                foreach (Producto producto in productos) {
                    
                    cmbProducto.Items.Add(producto.Id + " - " + producto.Nombre);
                    
                }

                cmbProducto.SelectedIndex = 0;
            } catch (Exception ex) {
                MessageBox.Show("No se pudieron cargar los productos.\n" + ex.Message);
            }
        }

        // esta clase sirve para buscar un producto en el arraylist de productos por su id y devolverlo, si no lo encuentra devuelve null
        private Producto BuscarProducto(int id) {
            foreach (Producto producto in productos) {
                if (producto.Id == id) {
                    return producto;
                }
            }

            return null;
        }

        private void MostrarDetalle() {
            dgvDetalle.Rows.Clear();
            decimal total = 0;

            //recorro el arraylist de productos y por cada producto obtengo la cantidad que se encuentra en el hashtable carrito, luego calculo el subtotal y lo agrego al datagridview
            foreach (Producto producto in productos) {

                // obtengo la cantidad del producto en el carrito, si no existe la clave en el hashtable, devuelve 0
                int cantidad = Convert.ToInt32(carrito[producto.Id]);
                decimal subtotal = producto.CalcularSubtotal(cantidad);

                if (cantidad > 0) {

                    dgvDetalle.Rows.Add(producto.Nombre, producto.Categoria, cantidad, producto.Precio.ToString(), subtotal.ToString());
                }

                total += subtotal;
            }

            lblTotal.Text = "Total: " + total.ToString();
        }

        private void btnAgregar_Click(object sender, EventArgs e) {
            if (cmbProducto.SelectedIndex == 0) {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            int cantidad;

            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0) {
                MessageBox.Show("Ingrese una cantidad entera positiva.");
                return;
            }

            int id = int.Parse(cmbProducto.Text.Split('-')[0]);

            Producto producto = BuscarProducto(id);

            if (producto == null) {
                MessageBox.Show("No se encontró el producto.");
                return;
            }

            // que evaluo aca? bueno , si el producto ya esta en el carrito, le sumo la cantidad ingresada a la cantidad anterior, sino lo agrego al carrito con la cantidad ingresada
            //containskey me devuelve true si la clave existe en el hashtable, sino false
            if (carrito.ContainsKey(producto.Id)) {
                //si la clave no existe, le agrego la clave con la cantidad ingresada, sino le sumo la cantidad ingresada a la cantidad anterior



                // si el producto ya esta en el carrito, le sumo la cantidad ingresada a la cantidad anterior
                int cantidadAnterior = Convert.ToInt32(carrito[producto.Id]);
                //teniendo en cuenta esa CLAVE la obtengo y le sumo la cantidad ingresada
                carrito[producto.Id] = cantidadAnterior + cantidad;
            } else {
                // si la clave no existe en el hashtable, le agrego la clave con la cantidad ingresada
                // si no existe quiere decir que es la primera vez que se agrega el producto al carrito, entonces le agrego la clave con la cantidad ingresada
                carrito.Add(producto.Id, cantidad);
            }

            txtCantidad.Clear();
            MostrarDetalle();
        }

        
        private void btnGuardarVenta_Click(object sender, EventArgs e) {
            if (carrito.Count == 0) {
                MessageBox.Show("Agregue algún producto antes de guardar.");
                return;
            }

            try {
                if (ventaDatos.GuardarVenta(carrito)) {
                    MessageBox.Show("Venta guardada correctamente.");
                    // importante el clear del carrito, sino se van a seguir sumando los productos a la venta anterior
                    carrito.Clear();
                    MostrarDetalle();
                }
            } catch (Exception ex) {
                MessageBox.Show("No se pudo guardar la venta.\n" + ex.Message);
            }
        }

        
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e) {
            txtCantidad.Clear();
        }
    }
}