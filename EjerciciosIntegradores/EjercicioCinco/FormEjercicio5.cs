using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using EjerciciosIntegradores.EjercicioCinco;

namespace EjerciciosIntegradores {
    public partial class FormEjercicio5 : Form {
        private ProductoDatos productoDatos = new ProductoDatos();
        private VentaDatos ventaDatos = new VentaDatos();

        private string[] categorias = { "Electronica", "Libreria", "Hogar" };
        private List<Producto> productos = new List<Producto>();
        private Hashtable carrito = new Hashtable();
        private int[] unidadesPorCategoria = new int[3];
        private decimal[,] resumen = new decimal[3, 2];

        public FormEjercicio5() {
            InitializeComponent();

            dgvDetalle.Columns.Add("Producto", "Producto");
            dgvDetalle.Columns.Add("Categoria", "Categoría");
            dgvDetalle.Columns.Add("Cantidad", "Cantidad");
            dgvDetalle.Columns.Add("Precio", "Precio");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal");
        }

        private void FormEjercicio5_Load(object sender, EventArgs e) {
            try {
                foreach(Producto producto in productoDatos.ObtenerTodos()) {
                    if(Array.IndexOf(categorias, producto.Categoria) >= 0) {
                        productos.Add(producto);
                        cmbProducto.Items.Add(producto);
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e) {
            if(cmbProducto.SelectedItem == null) {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            if(!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0) {
                MessageBox.Show("La cantidad debe ser un número entero positivo.");
                return;
            }

            Producto producto = (Producto)cmbProducto.SelectedItem;

            if(carrito.ContainsKey(producto.Id)) {
                carrito[producto.Id] = (int)carrito[producto.Id] + cantidad;
            } else {
                carrito.Add(producto.Id, cantidad);
            }

            int indice = Array.IndexOf(categorias, producto.Categoria);
            unidadesPorCategoria[indice] += cantidad;
            resumen[indice, 0] += cantidad;
            resumen[indice, 1] += producto.CalcularSubtotal(cantidad);

            txtCantidad.Clear();
            MostrarDetalle();
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e) {
            if(carrito.Count == 0) {
                MessageBox.Show("No hay productos en la venta.");
                return;
            }

            try {
                foreach(DictionaryEntry item in carrito) {
                    Producto producto = BuscarProducto((int)item.Key);
                    ventaDatos.GuardarLinea(producto, (int)item.Value);
                }

                MessageBox.Show("Venta guardada.");
                carrito.Clear();
                unidadesPorCategoria = new int[3];
                resumen = new decimal[3, 2];
                MostrarDetalle();
            } catch(Exception ex) {
                MessageBox.Show("Error al guardar la venta: " + ex.Message);
            }
        }

        private Producto BuscarProducto(int id) {
            foreach(Producto producto in productos) {
                if(producto.Id == id) {
                    return producto;
                }
            }
            return null;
        }

        private void MostrarDetalle() {
            dgvDetalle.Rows.Clear();
            foreach(DictionaryEntry item in carrito) {
                Producto producto = BuscarProducto((int)item.Key);
                int cantidad = (int)item.Value;
                dgvDetalle.Rows.Add(
                    producto.Nombre,
                    producto.Categoria,
                    cantidad,
                    producto.Precio.ToString("N2"),
                    producto.CalcularSubtotal(cantidad).ToString("N2"));
            }

            decimal total = 0;
            string texto = "";
            for(int i = 0; i < categorias.Length; i++) {
                total += resumen[i, 1];
                texto += "\n" + categorias[i] + ": " + unidadesPorCategoria[i] + " unidades - $" + resumen[i, 1].ToString("N2");
            }

            lblTotal.Text = "Total: $" + total.ToString("N2") + texto;
        }
    }
}
