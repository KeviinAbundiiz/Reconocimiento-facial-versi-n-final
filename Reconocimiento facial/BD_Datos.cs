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
            int n;
            DBCon Con = new DBCon();
            Con.ObtenerBytesImagen();
            n = Con.ExtraerDatos(curp_or);
            txtCurp.Text = Con.Key[n].ToString();
            fecha.Text = Con.Fecha[n].ToString();
            txtPeso.Text = Con.Peso[n].ToString();
            txtAltura.Text = Con.Altura[n].ToString();
            txtEdad.Text = Con.Edad[n].ToString();
            pictureBox1.Image = Con.ConvertByteToImg(n);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DBCon DC = new DBCon();
            DC.EditarDatos(txtCurp.Text, fecha.Text, txtPeso.Text, txtAltura.Text, txtEdad.Text, curp_or);
            this.Close();
        }
    }
}
