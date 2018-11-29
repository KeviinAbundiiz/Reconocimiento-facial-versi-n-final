using Emgu.CV;
using Emgu.CV.Structure;
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
    public partial class Base_de_Datos : Form
    {
        DBCon dbc = new DBCon();
        int ini = 0, NumLabels, ContTrain, con = 0;
        public string[] Labels, Keys;

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            lblKey.Text = "";
        }

        private void btn_ultimo_Click(object sender, EventArgs e)
        {
            ini = NumLabels - 1;
            pictureBox1.Image = dbc.ConvertByteToImg(ini);
            lblKey.Text = dbc.Key[ini];
        }

        private void btn_anterior_Click(object sender, EventArgs e)
        {
            if (ini > 0)
            {
                ini--;
                pictureBox1.Image = dbc.ConvertByteToImg(ini);
                lblKey.Text = dbc.Key[ini];
            }
        }

        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            if (ini < NumLabels - 1)
            {
                ini++;
                pictureBox1.Image = dbc.ConvertByteToImg(ini);
                lblKey.Text = dbc.Key[ini];
            }
        }

        private void btn_primero_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BD_Datos BDD = new BD_Datos(pictureBox1.Image, lblKey.Text);
            BDD.ShowDialog();
            button2.PerformClick();
        }

        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> keys = new List<string>();

        public Base_de_Datos()
        {
            InitializeComponent();
            try
            {
                dbc.ObtenerBytesImagen();//carga de caras previus trainned y etiquetas para cada imagen                
                Labels = dbc.Name; //Labelsinfo.Split('%');//separo los nombres de los usuarios 
                NumLabels = dbc.TotalUser;// Convert.ToInt32(Labels[0]);//extraigo el total de usuarios registrados
                ContTrain = NumLabels;
                Keys = dbc.Key;


                for (int tf = 0; tf < NumLabels; tf++)//recorro el numero de nombres registrados
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    //LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(bmp));//cargo la foto con ese nombre
                    labels.Add(Labels[tf]);//cargo el nombre que se encuentre en la posicion del tf
                    keys.Add(Keys[tf]);
                }
            }
            catch (Exception e)
            {//Si la variable NumLabels es 0 me presenta el msj
                MessageBox.Show(e + " No hay ningún rostro en la Base de Datos, por favor añadir por lo menos una cara", "Cargar caras en tu Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_loadImgsBD_Click(object sender, EventArgs e)
        {
            dbc.ObtenerBytesImagen();
            groupBox2.Enabled = true;
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }
    }
}
