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
            nuevo.Codigo = txtCodigo.Text;
            nuevo.Descripcion = txtDescripcion.Text;
            nuevo.Precio = Convert.ToDecimal(txtPrecio.Text);
            nuevo.ImagenUrl = txtImagenUrl.Text;
            nuevo.Marca = (Marca)cboMarca.SelectedItem;
            nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
            negocio.agregar(nuevo);

            MessageBox.Show("Artículo agregado exitosamente", "Éxito");
            Close();

        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            cboMarca.DataSource = marcaNegocio.listar();
            cboCategoria.DataSource = categoriaNegocio.listar();

        }
    }
}
