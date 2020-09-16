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
            comando.CommandText = "Select A.ID, A.Codigo, A.Nombre, A.ImagenUrl, Precio, C.Descripcion Descripcion, M.Descripcion Marca From CATEGORIAS C, ARTICULOS A, MARCAS M Where A.IDMarca=M.ID and A.ID=C.Id"; //Como son las 3 tablas IDENTITY(1,1), puse ese AND o me listaba múltiples veces el mismo registro
            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Codigo = lector.GetString(1);
                aux.Nombre = lector.GetString(2);
                //aux.Descripcion = (string)lector["Descripcion"];
                aux.ImagenUrl = lector.GetString(3);
                aux.Precio = lector.GetDecimal(4);

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
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Insert into ARTICULOS (Nombre, Codigo, Descripcion, Precio, IdMarca, IdCategoria) Values (@Nombre, @Codigo, @Descripcion, @Precio, @IdMarca, @IdCategoria)";
            comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
            comando.Parameters.AddWithValue("@Codigo", nuevo.Codigo);
            comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
            comando.Parameters.AddWithValue("@Precio", nuevo.Precio);
            //comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenUrl); lo podemos agregar al textBox, seria similar a los demas
            comando.Parameters.AddWithValue("@IdMarca", nuevo.Marca.Id);
            comando.Parameters.AddWithValue("@IdCategoria", nuevo.Categoria.Id);
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public void eliminar(int idArticulo, int IdMarca, int IdCategoria)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            //List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Delete From ARTICULOS Where Id=idArticulo AND IdMarca=IdMarca AND IdCategoria=IdCategoria";
            

        }
    }
}
