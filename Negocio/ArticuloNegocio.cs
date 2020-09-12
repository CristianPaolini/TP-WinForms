using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List < Articulo > lista = new List<Articulo>();

            conexion.ConnectionString = "data source = HP240G6\\SQLEXPRESS; initial catalog = CATALOGO_DB; integrated security = sspi"; //Ver de hacer ruta genérica o cambiarla entre push y pull
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Select ID, Nombre, Descripcion, ImagenUrl From ARTICULOS";
            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Nombre = lector.GetString(1);
                aux.Descripcion = lector.GetString(2);
                aux.UrlImage = (string)lector["ImagenUrl"];

                lista.Add(aux);
            }

            conexion.Close();
            return lista;

        }
    }
}
