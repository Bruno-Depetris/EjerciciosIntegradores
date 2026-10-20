using System;
using System.Windows.Forms;
using EjerciciosIntegradores.EjercicioDos;

namespace EjerciciosIntegradores {
    public partial class FormEjercicio2 : Form {
        private AlumnoDatos datos = new AlumnoDatos();

        public FormEjercicio2() {
            InitializeComponent();
        }

        private void FormEjercicio2_Load(object sender, EventArgs e) {
            CargarAlumnos();
        }

        private void CargarAlumnos() {
            try {
                cmbAlumnos.Items.Clear();
                foreach(Alumno alumno in datos.ObtenerTodos()) {
                    cmbAlumnos.Items.Add(alumno);
                }
            } catch(Exception ex) {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            if(txtNombre.Text.Trim() == "") {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            if(!int.TryParse(txtLegajo.Text, out int legajo) || legajo <= 0) {
                MessageBox.Show("El legajo debe ser un número entero positivo.");
                return;
            }

            Alumno alumno = new Alumno();
            alumno.Nombre = txtNombre.Text.Trim();
            alumno.Legajo = legajo;

            TextBox[] cajasNotas = { txtNota1, txtNota2, txtNota3 };
            for(int i = 0; i < cajasNotas.Length; i++) {
                if(!int.TryParse(cajasNotas[i].Text, out int nota) || nota < 1 || nota > 10) {
                    MessageBox.Show("La nota " + (i + 1) + " debe ser un número entre 1 y 10.");
                    return;
                }
                alumno.Notas[i] = nota;
            }

            try {
                datos.Guardar(alumno);
                MessageBox.Show("Alumno guardado.");
                txtNombre.Clear();
                txtLegajo.Clear();
                txtNota1.Clear();
                txtNota2.Clear();
                txtNota3.Clear();
                CargarAlumnos();
            } catch(Exception ex) {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e) {
            if(cmbAlumnos.SelectedItem == null) {
                MessageBox.Show("Seleccione un alumno.");
                return;
            }

            Alumno alumno = (Alumno)cmbAlumnos.SelectedItem;
            lblPromedio.Text = "Promedio: " + alumno.CalcularPromedio().ToString("0.00");

            if(alumno.EstaAprobado()) {
                lblEstado.Text = "Estado: Aprobado";
            } else {
                lblEstado.Text = "Estado: Desaprobado";
            }
        }
    }
}
