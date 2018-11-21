using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reconocimiento_facial
{
    public partial class BD_Datos : Form
    {
        string curp_or;
        public BD_Datos(Image img, string Curp)
        {
            InitializeComponent();
            this.pictureBox1.Image = img;
            this.txtCurp.Text = Curp;
            this.curp_or = Curp;
            Cargar();
        }


        private void Cargar()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "Server=localhost;Port=3306;Database=facedata;Uid=root;Pwd=peluche785;SslMode=none";
            try
            {
                string query = "Select * From datos Where curp='" + txtCurp.Text + "';";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    txtPeso.Text = datos["peso"].ToString();
                    txtAltura.Text = datos["altura"].ToString();
                    txtEdad.Text = datos["edad"].ToString();
                    fecha.Text = datos["fecha"].ToString();
                }
                datos.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "Server=localhost;Port=3306;Database=facedata;Uid=root;Pwd=peluche785;SslMode=none";
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Update datos set curp='"+txtCurp.Text+"', peso='"+txtPeso.Text+"', edad='"+txtEdad.Text+"', fecha='"+fecha.Text+"', edad='"+txtEdad.Text+"' where curp='"+curp_or+"';";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Datos Actualizados");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
