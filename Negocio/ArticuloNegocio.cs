using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;
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
            comando.CommandText = "Select A.ID, A.Codigo Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, Precio, C.Descripcion Categoria, C.Id IdCat, M.Id IdMarca, M.Descripcion Marca From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id"; 
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
                aux.Categoria.Id = (int)lector["IdCat"];

                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)lector["Marca"];
                aux.Marca.Id = (int)lector["IdMarca"];

                lista.Add(aux);
            }

            conexion.Close();
            return lista;

        }

        public void agregar(Articulo nuevo)
        {

            AccesoDatos conexion = new AccesoDatos();

            conexion.setearQuery("Insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio, ImagenUrl, IdCategoria, IdMarca) Values (@Codigo, @Nombre, @Descripcion, @Precio, @ImagenUrl, @IdCategoria, @IdMarca)");
            conexion.agregarParametro("@Codigo", nuevo.Codigo);
            conexion.agregarParametro("@Nombre", nuevo.Nombre);
            conexion.agregarParametro("@Descripcion", nuevo.Descripcion);
            conexion.agregarParametro("@Precio", nuevo.Precio);
            conexion.agregarParametro("@ImagenUrl", nuevo.ImagenUrl);
            conexion.agregarParametro("@IdCategoria", nuevo.Categoria.Id);
            conexion.agregarParametro("@IdMarca", nuevo.Marca.Id);
            conexion.ejecutarAccion();

        }

        public void modificar(Articulo artic)
        {
            AccesoDatos conexion = new AccesoDatos();

            try
            {
                conexion.setearQuery("Update ARTICULOS set Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio, ImagenUrl=@ImagenUrl, IdCategoria=@IdCategoria, IdMarca=@IdMarca where Id=@Id");
                conexion.agregarParametro("@Id", artic.Id);
                conexion.agregarParametro("@Codigo", artic.Codigo);
                conexion.agregarParametro("@Nombre", artic.Nombre);
                conexion.agregarParametro("@Descripcion",artic.Descripcion);
                conexion.agregarParametro("@Precio",artic.Precio);
                conexion.agregarParametro("@ImagenUrl",artic.ImagenUrl);
                conexion.agregarParametro("@IdCategoria",artic.Categoria.Id);
                conexion.agregarParametro("@IdMarca",artic.Marca.Id);
                conexion.ejecutarAccion();  
            }

            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void eliminar(int id)
        {
            AccesoDatos conexion = new AccesoDatos();

            try
            {
                conexion.setearQuery("Delete From ARTICULOS Where Id=@Id");
                conexion.agregarParametro("@Id", id);
                conexion.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

