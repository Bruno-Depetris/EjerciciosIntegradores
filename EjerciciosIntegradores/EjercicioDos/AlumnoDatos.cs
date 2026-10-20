using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioDos {
    public class AlumnoDatos {
        private string connectionString = "";

        public void Guardar(Alumno alumno) {
            string consulta = "INSERT INTO Alumnos (Nombre, Legajo, Nota1, Nota2, Nota3) VALUES (@Nombre, @Legajo, @Nota1, @Nota2, @Nota3)";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                comando.Parameters.AddWithValue("@Legajo", alumno.Legajo);
                for(int i = 0; i < alumno.Notas.Length; i++) {
                    comando.Parameters.AddWithValue("@Nota" + (i + 1), alumno.Notas[i]);
                }

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<Alumno> ObtenerTodos() {
            List<Alumno> alumnos = new List<Alumno>();
            string consulta = "SELECT Nombre, Legajo, Nota1, Nota2, Nota3 FROM Alumnos";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while(lector.Read()) {
                    Alumno alumno = new Alumno();
                    alumno.Nombre = lector["Nombre"].ToString();
                    alumno.Legajo = Convert.ToInt32(lector["Legajo"]);
                    for(int i = 0; i < alumno.Notas.Length; i++) {
                        alumno.Notas[i] = Convert.ToInt32(lector["Nota" + (i + 1)]);
                    }
                    alumnos.Add(alumno);
                }
            }

            return alumnos;
        }
    }
}
