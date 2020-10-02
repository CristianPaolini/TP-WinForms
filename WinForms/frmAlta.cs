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
                Validacion Val = new Validacion();
                bool[] comprobacion = Val.validacionesfrmAlta(txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, txtPrecio.Text, cboMarca.SelectedIndex, cboCategoria.SelectedIndex);
                if (comprobacion[0] && comprobacion[1] && comprobacion[2] && comprobacion[3] && comprobacion[4] && comprobacion[5])
                {
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
                else
                {
                    if (!comprobacion[0]) txtCodigo.BackColor = Color.Red;
                    if (!comprobacion[1]) txtNombre.BackColor = Color.Red;
                    if (!comprobacion[2]) txtDescripcion.BackColor = Color.Red;
                    if (!comprobacion[3]) txtPrecio.BackColor = Color.Red;
                    if (!comprobacion[4]) cboMarca.BackColor = Color.Red;
                    if (!comprobacion[5]) cboCategoria.BackColor = Color.Red;

                    MessageBox.Show("Campos faltantes", "Error en la carga");
                }
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
                txtCodigo.Text = articulo.Codigo;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                txtUrlImagen.Text = articulo.ImagenUrl;

                cboMarca.SelectedValue = articulo.Marca.Id;
                cboCategoria.SelectedValue = articulo.Categoria.Id;

                Text = "Modificar artículo";
            }
            
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                    e.Handled = true;
            txtPrecio.BackColor = System.Drawing.Color.White;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNombre.BackColor = System.Drawing.Color.White;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCodigo.BackColor = System.Drawing.Color.White;
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDescripcion.BackColor = System.Drawing.Color.White;
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMarca.BackColor = System.Drawing.Color.White;
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCategoria.BackColor = System.Drawing.Color.White;
        }
    }
}
