using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerciciosIntegradores.EjercicioTres {
    public partial class Form_EjercicioTres : Form {

        BibliotecaDatos biblioteca = new BibliotecaDatos();
        public Form_EjercicioTres() {
            InitializeComponent();
            CargarCB();
        }

        private void btnCargar_Click(object sender, EventArgs e) {
            if(cmbLibros.SelectedIndex == 0) {
                MessageBox.Show("Debe seleccionar un libro válido");
                return;
            }
            int id = int.Parse(cmbLibros.Text.Split('-') [0]);
            Libro libroEncontrado = biblioteca.ObtenerLibro(id);
            lblDescripcion.Text = libroEncontrado.ObtenerDescripcion();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            Libro nuevolibro = new Libro();

            nuevolibro.Titulo = txtTitulo.Text;
            nuevolibro.Autor = txtAutor.Text;
            nuevolibro.Anio = int.Parse(txtAnio.Text);

            try {
                biblioteca.Guardar(nuevolibro);
                MessageBox.Show("Libro cargado!");
                txtTitulo.Clear();
                txtAutor.Clear();
                txtAnio.Clear();
                CargarCB();
            } catch(Exception ex) {
                MessageBox.Show("Hubo un error al cargar el libro \n" + ex.Message);
            }
        }

        private void CargarCB() {
            ArrayList datos = new ArrayList();
            datos = biblioteca.Obtener();
            cmbLibros.Items.Clear();
            cmbLibros.Items.Insert(0, "Selecciona un libro");
            cmbLibros.SelectedIndex = 0;
            foreach(Libro libro in datos) {
                cmbLibros.Items.Add(libro.LibroID + "-" + libro.Titulo);
            }
            
        }
    }
}
