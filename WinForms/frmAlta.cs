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
        public frmAlta()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = Convert.ToDecimal(txtPrecio.Text);
                nuevo.ImagenUrl = txtUrlImagen.Text;
                nuevo.Marca = (Marca)cboMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
               
                negocio.agregar(nuevo);

                MessageBox.Show("Artículo agregado exitosamente", "Éxito");
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

            CategoriaNegocio negocio = new CategoriaNegocio();
            cboCategoria.DataSource = negocio.listar();
            
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
