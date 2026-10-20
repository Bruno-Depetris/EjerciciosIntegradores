using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EjerciciosIntegradores.EjercicioUno;

namespace EjerciciosIntegradores {
    public partial class FormEjercicio1 : Form {
        private ProductoDatos datos = new ProductoDatos();

        public FormEjercicio1() {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            if(txtNombre.Text.Trim() == "") {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            if(!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0) {
                MessageBox.Show("El precio debe ser un número mayor que cero.");
                return;
            }

            if(!int.TryParse(txtStock.Text, out int stock) || stock < 0) {
                MessageBox.Show("El stock debe ser un número entero mayor o igual que cero.");
                return;
            }

            Producto producto = new Producto();
            producto.Nombre = txtNombre.Text.Trim();
            producto.Precio = precio;
            producto.Cantidad = stock;

            try {
                datos.Guardar(producto);
                MessageBox.Show("Producto guardado.");
                txtNombre.Clear();
                txtPrecio.Clear();
                txtStock.Clear();
            } catch(Exception ex) {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnListar_Click(object sender, EventArgs e) {
            try {
                List<Producto> productos = datos.ObtenerTodos();
                dgvProductos.DataSource = productos;

                decimal total = 0;
                foreach(Producto producto in productos) {
                    total += producto.CalcularValor();
                }

                lblTotal.Text = "Total inventario: $" + total.ToString("N2");
            } catch(Exception ex) {
                MessageBox.Show("Error al listar: " + ex.Message);
            }
        }
    }
}
