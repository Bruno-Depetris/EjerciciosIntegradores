using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosIntegradores.EjercicioDos {
    public class AlumnoDatos {

        private string connectionString =
           "Data Source=(localdb)\\MSSQLLocalDB;" +
           "Initial Catalog=Purebas;" +
           "Integrated Security=True;" +
           "Persist Security Info=False;" +
           "Pooling=False;" +
           "MultipleActiveResultSets=False;" +
           "Encrypt=False;" +
           "TrustServerCertificate=False;" +
           "Application Name=EjerciciosIntegradores";


        public bool Guardar(Alumno alumn) {
            string consulta = $"INSERT INTO Alumnos (Nombre, Legajo, Nota1, Nota2, Nota3) VALUES ('{alumn.Nombre}', '{alumn.Legajo}', {alumn.Notas[0]}, {alumn.Notas[1]}, {alumn.Notas[2]})";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();
            if(comando.ExecuteNonQuery() > 0) {
                return true;
            }
            return false;
        }

        public ArrayList Obtener() {
            ArrayList alumnos = new ArrayList();

            string consulta = "SELECT  Id, Nombre, Legajo, Nota1, Nota2, Nota3 FROM Alumnos";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            SqlDataReader lector = comando.ExecuteReader();

            while(lector.Read()) {
                Alumno alumn = new Alumno();
                alumn.AlumnoID = int.Parse(lector ["Id"].ToString());
                alumn.Nombre = lector ["Nombre"].ToString();
                alumn.Legajo = int.Parse(lector ["Legajo"].ToString());
                alumn.Notas [0] = Convert.ToInt32(lector ["Nota1"]);
                alumn.Notas [1] = Convert.ToInt32(lector ["Nota2"]);
                alumn.Notas [2] = Convert.ToInt32(lector ["Nota3"]);
                alumnos.Add(alumn);
            }

            return alumnos;
        }

        public Alumno ObtenerPorID(int id) {
            Alumno alumn = new Alumno();

            string consulta = "SELECT Nombre, Legajo, Nota1, Nota2, Nota3 FROM Alumnos WHERE Id = " + id;

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            SqlDataReader lector = comando.ExecuteReader();

            while(lector.Read()) {
                
                alumn.Nombre = lector ["Nombre"].ToString();
                alumn.Legajo = int.Parse(lector ["Legajo"].ToString());
                alumn.Notas [0] = Convert.ToInt32(lector ["Nota1"]);
                alumn.Notas [1] = Convert.ToInt32(lector ["Nota2"]);
                alumn.Notas [2] = Convert.ToInt32(lector ["Nota3"]);
                
            }

            return alumn;
        }
    }
}
