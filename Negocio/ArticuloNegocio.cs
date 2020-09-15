using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;
using Negocio;

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
                aux.Codigo = lector.GetString(1);
                aux.Nombre = lector.GetString(2);
                aux.Descripcion = lector.GetString(3);
                aux.ImagenUrl = (string)lector["ImagenUrl"];

                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)lector["Marca"];

                aux.Categoria = new Categoria();
                aux.Categoria.Descripcion = (string)lector["Descripcion"];

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
            comando.CommandText = "Insert into ARTICULOS (Nombre, Descripcion, IdMarca) Values (@Nombre, @Descripcion, @IdMarca)";
            comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
            //comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenUrl);
            comando.Parameters.AddWithValue("@IdMarca", nuevo.Marca.Id);
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();
        }
    }
}
