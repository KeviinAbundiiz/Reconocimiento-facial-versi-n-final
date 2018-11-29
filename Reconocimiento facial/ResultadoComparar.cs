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
            DBCon DC = new DBCon();
            DC.GeneraReporte("Manual", lblKey.Text, lblKey2.Text, lblPorcentaje.Text, fecha.Value.Date.ToString("yyyy-MM-dd"));
        }

        private void cargar()
        {
            
            try
            {
                int n;
                DBCon Con = new DBCon();
                Con.ObtenerBytesImagen();
                n = Con.ExtraerDatos(lblKey.Text);
                lblFecha1.Text = Con.Fecha[n].ToString();
                lblPeso1.Text = Con.Peso[n].ToString();
                lblAltura1.Text = Con.Altura[n].ToString();
                lblEdad1.Text = Con.Edad[n].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                int n;
                DBCon Con = new DBCon();
                Con.ObtenerBytesImagen();
                n = Con.ExtraerDatos(lblKey2.Text);
                lblFecha2.Text = Con.Fecha[n].ToString();
                lblPeso2.Text = Con.Peso[n].ToString();
                lblAltura2.Text = Con.Altura[n].ToString();
                lblEdad2.Text = Con.Edad[n].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comparar()
        {
            
            //---
            lblPeso.Text = porciento(Convert.ToDouble(lblPeso1.Text), Convert.ToDouble(lblPeso2.Text)).ToString()+"%";
            //---
            
            //--
            lblAltura.Text = porciento(Convert.ToDouble(lblAltura1.Text), Convert.ToDouble(lblAltura2.Text)).ToString()+"%";
            //--
            
            //---
            lblEdad.Text = porciento(Convert.ToDouble(lblEdad1.Text), Convert.ToDouble(lblEdad2.Text)).ToString() + "%";
            //---

            lblPorcentaje.Text = (porciento(Convert.ToDouble(lblPeso1.Text), Convert.ToDouble(lblPeso2.Text)) + porciento(Convert.ToDouble(lblAltura1.Text), Convert.ToDouble(lblAltura2.Text)) + porciento(Convert.ToDouble(lblEdad1.Text), Convert.ToDouble(lblEdad2.Text))+1).ToString()+" %";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int porciento(double num1, double num2)
        {
            if(num1 == num2)
            {
                return 33;
            }
            else
            {
                return 16;
            }
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
