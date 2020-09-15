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

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Select A.ID, A.Nombre, A.Descripcion, A.ImagenUrl, M.Descripcion Marca From ARTICULOS A, MARCAS M Where A.IDMarca=M.ID";
            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Nombre = lector.GetString(1);
                aux.Descripcion = lector.GetString(2);
                aux.UrlImage = (string)lector["ImagenUrl"];

                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)lector["Marca"];

                lista.Add(aux);
            }

            conexion.Close();
            return lista;

        }

        public void agregar(Articulo nuevo)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Insert into ARTICULOS (Nombre, Descripcion) Values ('" + nuevo.Nombre +  "', '" + nuevo.Descripcion + "')";
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();
        }
    }
}
