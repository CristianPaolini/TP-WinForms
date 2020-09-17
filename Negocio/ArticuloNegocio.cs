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
            comando.CommandText = "Select A.ID, A.Nombre, a.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria From CATEGORIAS C, ARTICULOS A Where A.IdCategoria = c.Id"; //Como son las 3 tablas IDENTITY(1,1), puse ese AND o me listaba múltiples veces el mismo registro
            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();
                
                aux.Nombre = lector.GetString(1);
                aux.Descripcion = lector.GetString(2);
                    
                aux.ImagenUrl = (string) lector ["ImagenUrl"];
                aux.Precio = lector.GetSqlMoney(4);

              

                aux.Categoria = new Categoria();
                aux.Categoria.Descripcion = (string)lector["Categoria"];

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
                comando.CommandText = "Insert into ARTICULOS (Nombre, Descripcion, Precio, ImagenUrl, IdCategoria) Values (@Nombre, @Descripcion, @Precio, '', @IdCategoria)";
                comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
                
                comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
                comando.Parameters.AddWithValue("@Precio", nuevo.Precio);
                //comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenUrl);
               
                comando.Parameters.AddWithValue("@IdCategoria", nuevo.Categoria.Id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            

        }

        //public void eliminar(int idArticulo, int IdMarca, int IdCategoria)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    //List<Articulo> lista = new List<Articulo>();

        //    conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
        //    comando.CommandType = System.Data.CommandType.Text;
        //    comando.CommandText = "Delete From ARTICULOS Where Id=idArticulo AND IdMarca=IdMarca AND IdCategoria=IdCategoria";
            

        //}
    }

