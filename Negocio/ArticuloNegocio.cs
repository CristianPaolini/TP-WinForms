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
            comando.CommandText = "Select ID, Nombre, Descripcion From ARTICULOS";
            comando.Connection = conexion;


        }
    }
}
