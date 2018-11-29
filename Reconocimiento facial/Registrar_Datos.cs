using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Reconocimiento_facial
{
    public partial class Registrar_Datos : Form
    {
        DBCon dbc = new DBCon();
        Image im;
        public Registrar_Datos(string nom, string curp, Image img, string fecha)
        {
            InitializeComponent();
            im = img;
            lblNombre.Text = nom;
            lblCurp.Text = curp;
            lblFecha.Text = fecha;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            dbc.ConvertImgToBinary(lblNombre.Text, lblCurp.Text, im, Convert.ToDouble(txtPeso.Text), Convert.ToDouble(txtAltura.Text), lblFecha.Text, Convert.ToInt32(txtEdad.Text), cbxTipoImagen.Text);
            this.Close();
            MessageBox.Show("Agregado correctamente", "Capturado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
