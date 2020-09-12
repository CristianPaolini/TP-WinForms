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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dgvLista.DataSource = negocio.listar();
            dgvLista.Columns[2].Visible = false;
        }

        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                pbArticulo.Load(articulo.UrlImage);
            }
            catch (Exception)
            {
                
            }

        }
    }
}
