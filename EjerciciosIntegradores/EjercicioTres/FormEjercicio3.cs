using System;
using System.Collections;
using System.Windows.Forms;
using EjerciciosIntegradores.EjercicioTres;

namespace EjerciciosIntegradores {
    public partial class FormEjercicio3 : Form {
        private LibroDatos datos = new LibroDatos();
        private ArrayList libros = new ArrayList();

        public FormEjercicio3() {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            if(txtTitulo.Text.Trim() == "" || txtAutor.Text.Trim() == "") {
                MessageBox.Show("El título y el autor no pueden estar vacíos.");
                return;
            }

            if(!int.TryParse(txtAnio.Text, out int anio) || anio <= 0 || anio > DateTime.Now.Year) {
                MessageBox.Show("El año debe ser un número entero válido.");
                return;
            }

            Libro libro = new Libro();
            libro.Titulo = txtTitulo.Text.Trim();
            libro.Autor = txtAutor.Text.Trim();
            libro.Anio = anio;

            try {
                datos.Guardar(libro);
                MessageBox.Show("Libro guardado.");
                txtTitulo.Clear();
                txtAutor.Clear();
                txtAnio.Clear();
            } catch(Exception ex) {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e) {
            try {
                libros = datos.ObtenerTodos();

                cmbLibros.Items.Clear();
                foreach(Libro libro in libros) {
                    cmbLibros.Items.Add(libro);
                }
            } catch(Exception ex) {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private void cmbLibros_SelectedIndexChanged(object sender, EventArgs e) {
            if(cmbLibros.SelectedItem != null) {
                Libro libro = (Libro)cmbLibros.SelectedItem;
                lblDescripcion.Text = libro.ObtenerDescripcion();
            }
        }
    }
}
