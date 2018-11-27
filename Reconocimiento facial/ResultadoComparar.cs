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
    public partial class ResultadoComparar : Form
    {
        public ResultadoComparar(Image im1, Image im2, string key, string key2)
        {
            InitializeComponent();
            picA.Image = im1;
            picB.Image = im2;
            lblKey.Text = key;
            lblKey2.Text = key2;
            cargar();
            comparar();
        }

        private void cargar()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "Server=localhost;Port=3306;Database=facedata;Uid=root;Pwd=peluche785;SslMode=none";
            try
            {
                string query = "Select * From datos Where curp='" + lblKey.Text + "';";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    lblFecha1.Text = datos["fecha"].ToString();
                    lblPeso1.Text = datos["peso"].ToString();
                    lblAltura1.Text = datos["altura"].ToString();
                    lblEdad1.Text = datos["edad"].ToString();
                }
                datos.Close();
                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                string query = "Select * From datos Where curp='" + lblKey2.Text + "';";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    lblFecha2.Text = datos["fecha"].ToString();
                    lblPeso2.Text = datos["peso"].ToString();
                    lblAltura2.Text = datos["altura"].ToString();
                    lblEdad2.Text = datos["edad"].ToString();
                }
                datos.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comparar()
        {
            
            //---
            lblPeso.Text = Convert.ToDouble((Convert.ToInt32(lblPeso1.Text) - Convert.ToInt32(lblPeso2.Text)) / 100.00).ToString()+"%";
            //---
            
            //--
            lblAltura.Text = Convert.ToDouble((Convert.ToDouble(lblAltura1.Text) - Convert.ToDouble(lblAltura2.Text)) / 100).ToString()+"%";
            //--
            
            //---
            lblEdad.Text = Convert.ToDouble((Convert.ToDouble(lblEdad1.Text) - Convert.ToDouble(lblEdad2.Text))/100).ToString() + "%";
            //---

            lblPorcentaje.Text = (Convert.ToDouble((Convert.ToInt32(lblPeso1.Text) - Convert.ToInt32(lblPeso2.Text)) / 100.00) + Convert.ToDouble((Convert.ToDouble(lblAltura1.Text) - Convert.ToDouble(lblAltura2.Text)) / 100) + Convert.ToDouble((Convert.ToDouble(lblEdad1.Text) - Convert.ToDouble(lblEdad2.Text)) / 100)).ToString()+" %";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            //Regresa a menu principal
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
