using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosIntegradores.EjercicioTres {
    public class BibliotecaDatos {

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


        public bool Guardar(Libro libro) {
            string consulta = $"INSERT INTO Libros (Titulo, Autor, Anio) VALUES ('{libro.Titulo}','{libro.Autor}',{libro.Anio})";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();
            if(comando.ExecuteNonQuery() > 0) {
                return true;
            }
            return false;
        }

        public ArrayList Obtener() {
            ArrayList datos = new ArrayList();

            string consulta = "SELECT Id, Titulo, Autor, Anio FROM Libros";

            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();
            SqlDataReader lector = comando.ExecuteReader();

            while(lector.Read()) {
                Libro libro = new Libro();
                libro.LibroID = int.Parse(lector ["Id"].ToString());
                libro.Titulo = lector ["Titulo"].ToString();
                libro.Autor = lector ["Autor"].ToString();
                libro.Anio = int.Parse(lector ["Anio"].ToString());

                datos.Add(libro);
            }

            return datos;
        }

        public Libro ObtenerLibro(int id) {
            Libro libroObtenido = new Libro();
            string consulta = "SELECT Id, Titulo, Autor, Anio FROM Libros WHERE Id = " + id;


            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(consulta, conexion);

            conexion.Open();
            SqlDataReader lector = comando.ExecuteReader();

            while(lector.Read()) {

                libroObtenido.LibroID = int.Parse(lector ["Id"].ToString());
                libroObtenido.Titulo = lector ["Titulo"].ToString();
                libroObtenido.Autor = lector ["Autor"].ToString();
                libroObtenido.Anio = int.Parse(lector ["Anio"].ToString());

 
            }

            return libroObtenido;
        }
    }
}
