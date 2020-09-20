using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WinForms
{
    public partial class frmAlta : Form
    {

        private Articulo articulo = null;

        public frmAlta()
        {
            InitializeComponent();
        }

        public frmAlta(Articulo artic)
        {
            InitializeComponent();
            articulo = artic;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

               if(articulo == null)
               
               articulo = new Articulo();  //  si está vacio (porque no existe) lo crea. Sino, lo "recarga"

               articulo.Codigo = txtCodigo.Text;
               articulo.Nombre = txtNombre.Text;
               articulo.Descripcion = txtDescripcion.Text;
               articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
               articulo.ImagenUrl = txtUrlImagen.Text;
               articulo.Marca = (Marca)cboMarca.SelectedItem;
               articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                if (articulo.Id == 0)
                {
                    negocio.agregar(articulo);
                }
                else
                {
                    negocio.modificar(articulo);
                }

                MessageBox.Show("Operación realizada exitosamente", "Éxito");
                Close();
            }

            catch (Exception ex)
            {
                throw ex;       
            }
            

        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            cboMarca.DataSource = marca.listar();
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";
            cboMarca.SelectedIndex = -1;

            CategoriaNegocio negocio = new CategoriaNegocio();
            cboCategoria.DataSource = negocio.listar();
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";
            cboCategoria.SelectedIndex = -1;



            if(articulo != null)
            {
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;

                cboMarca.SelectedValue = articulo.Marca.Id;
                cboCategoria.SelectedValue = articulo.Categoria.Id;

                Text = "Modificar artículo";
            }
            
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                    e.Handled = true;
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                epError.SetError(txtNombre, ("Por favor, introduzca nombre de artículo..."));
                txtNombre.Focus();
            }
            else
            {
                epError.Clear();
            }
            
        }

        private void txtDescripcion_Validated(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Trim() == "")
            {
                epError.SetError(txtDescripcion, ("Por favor, introduzca una descripción..."));
                txtDescripcion.Focus();
            }
            else
            {
                epError.Clear();
            }
            
        }

        private void txtPrecio_Validated(object sender, EventArgs e)
        {
            if (txtPrecio.Text.Trim() == "")
            {
                epError.SetError(txtPrecio, ("Por favor, introduzca un precio..."));
                txtPrecio.Focus();
            }
            else
            {
                epError.Clear();
            }
            
        }

        //private void cargar()
        //{
        //    marcanegocio marcanegocio = new marcanegocio();
        //    categorianegocio categorianegocio = new categorianegocio();

        //    cbomarca.datasource = marcanegocio.listar();
        //    cbocategoria.datasource = categorianegocio.listar();
        //}
    }
}
