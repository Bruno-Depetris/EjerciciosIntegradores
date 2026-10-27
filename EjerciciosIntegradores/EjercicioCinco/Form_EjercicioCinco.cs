using System;
using System.Collections;
using System.Windows.Forms;

namespace EjerciciosIntegradores.EjercicioCinco {
    public partial class Form_EjercicioCinco : Form {
        private ProductoDatos productoDatos = new ProductoDatos();
        private VentaDatos ventaDatos = new VentaDatos();
        private string[] categorias = { "Electronica", "Libreria", "Hogar" };
        private ArrayList productos = new ArrayList();     
        private Hashtable carrito = new Hashtable();       //clave-valor => producto - cantidad
        private int[] unidadesPorCategoria = new int[3];  
        private decimal[,] resumen = new decimal[3, 2];    

        public Form_EjercicioCinco() {
            InitializeComponent();

       
            dgvDetalle.Columns.Add("Producto", "Producto");
            dgvDetalle.Columns.Add("Categoria", "Categoría");
            dgvDetalle.Columns.Add("Cantidad", "Cantidad");
            dgvDetalle.Columns.Add("Precio", "Precio");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal");

            CargarProductos();
        }

        private void CargarProductos() {
            try {
                productos = productoDatos.ObtenerTodos();

                cmbProducto.Items.Clear();
                cmbProducto.Items.Insert(0, "Seleccione un producto");

                foreach (Producto producto in productos) {
                    if (ObtenerCategoria(producto.Categoria) != -1) {
                        cmbProducto.Items.Add(producto.Id + " - " + producto.Nombre);
                    }
                }

                cmbProducto.SelectedIndex = 0;
            } catch (Exception ex) {
                MessageBox.Show("No se pudieron cargar los productos.\n" + ex.Message);
            }
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


            if (carrito.ContainsKey(producto.Id)) {
                
                int cantidadAnterior = Convert.ToInt32(carrito[producto.Id]);
                carrito[producto.Id] = cantidadAnterior + cantidad;
            } else {
                
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
                if (ventaDatos.GuardarVenta(carrito, productos)) {
                    MessageBox.Show("Venta guardada correctamente.");
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

           
            unidadesPorCategoria = new int[3];
            resumen = new decimal[3, 2];

            decimal total = 0;

            foreach (Producto producto in productos) {
            
                if (!carrito.ContainsKey(producto.Id)) {
                    continue;
                }

                int categoria = ObtenerCategoria(producto.Categoria);
                if (categoria == -1) {
                    continue;
                }

                int cantidad = Convert.ToInt32(carrito[producto.Id]);
                decimal subtotal = producto.CalcularSubtotal(cantidad);

                
                unidadesPorCategoria[categoria] += cantidad;
                resumen[categoria, 0] = unidadesPorCategoria[categoria];
                resumen[categoria, 1] += subtotal;

               
                dgvDetalle.Rows.Add(producto.Nombre,producto.Categoria,cantidad,producto.Precio.ToString(),subtotal.ToString());

                total += subtotal;
            }

            lblTotal.Text = "Total: " + total.ToString();
        }

        
        private int ObtenerCategoria(string nombreCategoria) {
            for (int i = 0; i < categorias.Length; i++) {
                if (categorias[i].ToLower() == nombreCategoria.ToLower()) {
                    return i;
                }
            }

            return -1;
        }
    }
}