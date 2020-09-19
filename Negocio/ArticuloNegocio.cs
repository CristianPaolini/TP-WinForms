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
            comando.CommandText = "Select A.ID, A.Nombre, A.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id";
                //"Select A.ID, A.Nombre, A.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria From CATEGORIAS C, ARTICULOS A Where A.IdCategoria = c.Id"; 
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
                comando.CommandText = "Insert into ARTICULOS (Nombre, Descripcion, Precio, ImagenUrl, IdCategoria, IdMarca) Values (@Nombre, @Descripcion, @Precio, @ImagenUrl, @IdCategoria, @IdMarca)";
                comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
                comando.Parameters.AddWithValue("@Precio", nuevo.Precio);
                comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenUrl);
                comando.Parameters.AddWithValue("@IdCategoria", nuevo.Categoria.Id);
                comando.Parameters.AddWithValue("@IdMarca", nuevo.Marca.Id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            

        }

        
    }

