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
            Articulo nuevo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            nuevo.Nombre = txtNombre.Text;
            
            nuevo.Descripcion = txtDescripcion.Text;
            nuevo.Precio = Convert.ToDecimal(txtPrecio.Text);
            nuevo.ImagenUrl = txtUrlImagen.Text;
            nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
            negocio.agregar(nuevo);

            MessageBox.Show("Artículo agregado exitosamente", "Éxito");
            Close();

        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
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
            epError.Clear();
        }

        private void txtDescripcion_Validated(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Trim() == "")
            {
                epError.SetError(txtDescripcion, ("Por favor, introduzca una descripción..."));
                txtDescripcion.Focus();
            }
            epError.Clear();
        }

        private void txtPrecio_Validated(object sender, EventArgs e)
        {
            if (txtPrecio.Text.Trim() == "")
            {
                epError.SetError(txtPrecio, ("Por favor, introduzca una descripción..."));
                txtPrecio.Focus();
            }
            epError.Clear();
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
