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

namespace EjerciciosIntegradores.EjercicioDos {
    public partial class Form_EjercicioDos : Form {
        private AlumnoDatos datos = new AlumnoDatos();
        public Form_EjercicioDos() {
            InitializeComponent();
            CargarComboBox(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            Alumno alumno = new Alumno();
            

            if(txtNombre.Text.Trim() == "") {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            if(!int.TryParse(txtLegajo.Text, out int legajo) || legajo <= 0) {
                MessageBox.Show("El legajo debe ser un número entero positivo.");
                return;
            }

            if(!int.TryParse(txtNota1.Text, out int nota1) || nota1 < 1 || nota1 > 10) {
                MessageBox.Show("La nota 1 debe ser un número entre 1 y 10.");
                return;
            }
            if(!int.TryParse(txtNota2.Text, out int nota2) || nota2 < 1 || nota2 > 10) {
                MessageBox.Show("La nota 2 debe ser un número entre 1 y 10.");
                return;
            }
            if(!int.TryParse(txtNota3.Text, out int nota3) || nota3 < 1 || nota3 > 10) {
                MessageBox.Show("La nota 3 debe ser un número entre 1 y 10.");
                return;
            }

            alumno.Nombre = txtNombre.Text.Trim();
            alumno.Legajo = legajo;
            alumno.Notas [0] = nota1;
            alumno.Notas [1] = nota2;
            alumno.Notas [2] = nota3;


            try {
                datos.Guardar(alumno);
                MessageBox.Show("Alumno guardado.");

                txtNombre.Clear();
                txtLegajo.Clear();
                txtNota1.Clear();
                txtNota2.Clear();
                txtNota3.Clear();

                CargarComboBox();

            } catch(Exception ex) {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e) {
            if(cmbAlumnos.SelectedIndex == 0) {
                MessageBox.Show("Seleccione un alumno.");
                cmbAlumnos.Focus(); 
                return;
            }


            int id = int.Parse(cmbAlumnos.Text.Split('-') [0]); //primer elemento del comboBox es el ID del alumno


            Alumno alumno = datos.ObtenerPorID(id);


            lblPromedio.Text = "Promedio: " + alumno.CalcularPromedio().ToString("0.00"); //el tostring con los 0.00 es para que siempre muestre dos decimales, aunque sean ceros

            if(alumno.EstaAprobado()) {
                lblEstado.Text = "Estado: Aprobado";
            } else {
                lblEstado.Text = "Estado: Desaprobado";
            }
        }

        private void CargarComboBox() {
            try {
                cmbAlumnos.Items.Clear();

                cmbAlumnos.Items.Insert(0, "Seleccione un alumno");
                cmbAlumnos.SelectedIndex = 0;

                foreach(Alumno alumno in datos.Obtener()) {

                    cmbAlumnos.Items.Add(alumno.AlumnoID.ToString() + "-" + alumno.Nombre.ToString() + "-" + alumno.Legajo.ToString());
                }

            } catch(Exception ex) {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }


    }
}

