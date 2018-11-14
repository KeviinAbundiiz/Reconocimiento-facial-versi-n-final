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
        public Registrar_Datos(string nom, string curp, string fecha)
        {
            InitializeComponent();
            lblNombre.Text = nom;
            lblCurp.Text = curp;
            lblFecha.Text = fecha;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "Server=localhost;Port=3306;Database=facedata;Uid=root;Pwd=peluche785;SslMode=none";
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "insert into datos values('" + lblCurp.Text + "', '" + txtPeso.Text + "', '" + txtAltura.Text + "', '" + lblFecha.Text + "', '" + txtEdad.Text + "');";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            MessageBox.Show("Agregado correctamente", "Capturado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
