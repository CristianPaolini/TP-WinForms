using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaOriginal;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             cargar();
        } 

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaOriginal = negocio.listar();
            dgvLista.DataSource = listaOriginal;
            dgvLista.Columns[0].Visible = false;
            dgvLista.Columns[1].Visible = false;
            dgvLista.Columns[4].Visible = false;
        }

        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                pbArticulo.Load(articulo.ImagenUrl);
            }
            catch (Exception)
            {
                
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlta alta = new frmAlta();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo artic;
            artic = (Articulo)dgvLista.CurrentRow.DataBoundItem;        

            frmAlta modificar = new frmAlta(artic);     // el nuevo constructor pasa los datos del actual elemento 
            modificar.ShowDialog();
            cargar();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.eliminar(((Articulo)dgvLista.CurrentRow.DataBoundItem).Id);
            cargar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)dgvLista.DataSource;
            List<Articulo> listaFiltrada = listaOriginal.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvLista.DataSource = listaFiltrada;

        }

        
        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFiltro.Text == "")
            {
                dgvLista.DataSource = listaOriginal;
            }
            else
            {
                List<Articulo> listaFiltrada = listaOriginal.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                dgvLista.DataSource = listaFiltrada;
            }
        }
    }
}
