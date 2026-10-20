using System;
using System.Collections;
using System.Data.SqlClient;

namespace EjerciciosIntegradores.EjercicioTres {
    public class LibroDatos {
        private string connectionString = "";

        public void Guardar(Libro libro) {
            string consulta = "INSERT INTO Libros (Titulo, Autor, Anio) VALUES (@Titulo, @Autor, @Anio)";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Titulo", libro.Titulo);
                comando.Parameters.AddWithValue("@Autor", libro.Autor);
                comando.Parameters.AddWithValue("@Anio", libro.Anio);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public ArrayList ObtenerTodos() {
            ArrayList libros = new ArrayList();
            string consulta = "SELECT Titulo, Autor, Anio FROM Libros";

            using(SqlConnection conexion = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while(lector.Read()) {
                    Libro libro = new Libro();
                    libro.Titulo = lector["Titulo"].ToString();
                    libro.Autor = lector["Autor"].ToString();
                    libro.Anio = Convert.ToInt32(lector["Anio"]);
                    libros.Add(libro);
                }
            }

            return libros;
        }
    }
}
