using EjerciciosIntegradores.EjercicioCinco;
using EjerciciosIntegradores.EjercicioDos;
using EjerciciosIntegradores.EjercicioTres;
using EjerciciosIntegradores.EjercicioUno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerciciosIntegradores {
    internal static class Program {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // si quieren probar los otros ejercicios, cambien el nombre del formulario que se pasa a Application.Run()
            //o descomenten la línea del ejercicio que quieran probar y comenten la del ejercicio que no quieran probar
            // Application.Run(new Form_EjercicioUno());
            // Application.Run(new Form_EjercicioDos());
            // Application.Run(new Form_EjercicioTres());
            // Application.Run(new Form_EjercicioCuatro()); 
            Application.Run(new Form_EjercicioCinco());
        }
    }
}
