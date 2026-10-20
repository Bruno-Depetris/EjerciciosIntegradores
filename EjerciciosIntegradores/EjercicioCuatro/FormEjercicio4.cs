using System;
using System.Windows.Forms;
using EjerciciosIntegradores.EjercicioCuatro;

namespace EjerciciosIntegradores {
    public partial class FormEjercicio4 : Form {
        private EmpleadoDatos datos = new EmpleadoDatos();

        public FormEjercicio4() {
            InitializeComponent();

            dgvEmpleados.Columns.Add("Nombre", "Nombre");
            dgvEmpleados.Columns.Add("Tipo", "Tipo");
            dgvEmpleados.Columns.Add("SueldoBase", "Sueldo base");
            dgvEmpleados.Columns.Add("SueldoFinal", "Sueldo final");

            cmbTipo.SelectedIndex = 0;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e) {
            txtVentas.Enabled = cmbTipo.Text == "Vendedor";
            if(!txtVentas.Enabled) {
                txtVentas.Clear();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            if(txtNombre.Text.Trim() == "") {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            if(!decimal.TryParse(txtSueldoBase.Text, out decimal sueldoBase) || sueldoBase <= 0) {
                MessageBox.Show("El sueldo base debe ser un número mayor que cero.");
                return;
            }

            Empleado empleado;

            if(cmbTipo.Text == "Vendedor") {
                if(!decimal.TryParse(txtVentas.Text, out decimal ventas) || ventas < 0) {
                    MessageBox.Show("Las ventas deben ser un número mayor o igual que cero.");
                    return;
                }
                Vendedor vendedor = new Vendedor();
                vendedor.VentasMensuales = ventas;
                empleado = vendedor;
            } else {
                empleado = new Administrativo();
            }

            empleado.Nombre = txtNombre.Text.Trim();
            empleado.SueldoBase = sueldoBase;

            try {
                datos.Guardar(empleado);
                MessageBox.Show("Empleado guardado.");
                txtNombre.Clear();
                txtSueldoBase.Clear();
                txtVentas.Clear();
            } catch(Exception ex) {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnListar_Click(object sender, EventArgs e) {
            try {
                dgvEmpleados.Rows.Clear();
                foreach(Empleado empleado in datos.ObtenerTodos()) {
                    dgvEmpleados.Rows.Add(
                        empleado.Nombre,
                        empleado.Tipo,
                        empleado.SueldoBase.ToString("N2"),
                        empleado.CalcularSueldo().ToString("N2"));
                }
            } catch(Exception ex) {
                MessageBox.Show("Error al listar: " + ex.Message);
            }
        }
    }
}
