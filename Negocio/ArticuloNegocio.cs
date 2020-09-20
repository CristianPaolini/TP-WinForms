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
            List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Select A.ID, A.Codigo Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id";
            //"Select A.ID, A.Nombre, A.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca From CATEGORIAS C, ARTICULOS A, Marcas M Where A.IdCategoria = c.Id and A.IdMarca = m.Id"; 
            comando.Connection = conexion;

            conexion.Open();    
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();

                aux.Id = (int)lector["ID"];
                aux.Codigo = (string)lector["Codigo"];
                aux.Nombre = lector.GetString(2);
                aux.Descripcion = lector.GetString(3);
                aux.ImagenUrl = (string)lector["ImagenUrl"];
                aux.Precio = lector.GetSqlMoney(5);

                aux.Categoria = new Categoria();
                aux.Categoria.Descripcion = (string)lector["Categoria"];
                aux.Categoria.Id = (int)lector["Id"]; //   ->  si no anda, puede ser poniendo solo "Id"

                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)lector["Marca"];
                aux.Marca.Id = (int)lector["Id"];          //   ->     x2

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
            comando.CommandText = "Insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio, ImagenUrl, IdCategoria, IdMarca) Values (@Codigo, @Nombre, @Descripcion, @Precio, @ImagenUrl, @IdCategoria, @IdMarca)";
            comando.Parameters.AddWithValue("@Codigo", nuevo.Codigo);
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

        public void modificar(Articulo artic)
        {
            
            try
            {
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                List<Articulo> lista = new List<Articulo>();

                conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Update ARTICULOS set Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio, ImagenUrl=@ImagenUrl, IdCategoria=@IdCategoria, IdMarca=@IdMarca where Id=@Id";

                comando.Parameters.AddWithValue("@Id", artic.Id);
                comando.Parameters.AddWithValue("@Codigo", artic.Codigo);
                comando.Parameters.AddWithValue("@Nombre", artic.Nombre);
                comando.Parameters.AddWithValue("@Descripcion",artic.Descripcion);
                comando.Parameters.AddWithValue("@Precio",artic.Precio);
                comando.Parameters.AddWithValue("@ImagenUrl",artic.ImagenUrl);
                comando.Parameters.AddWithValue("@IdCategoria",artic.Categoria.Id);
                comando.Parameters.AddWithValue("@IdMarca",artic.Marca.Id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();  
            }

            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void eliminar(int id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=CATALOGO_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Delete From ARTICULOS Where Id=@Id";

            comando.Parameters.AddWithValue("@Id", id);
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();

            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}

