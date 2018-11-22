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
    public partial class Comparar : Form
    {
        
        DBCon dbc = new DBCon();
        int ini2 = 0, ini = 0, NumLabels, ContTrain, con=0;
        public string[] Labels, Keys;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> keys = new List<string>();
        public Comparar()
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

        private void btn_loadImgsBD_Click_1(object sender, EventArgs e)
        {
            
            groupBox2.Enabled = true;
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btn_maximize_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
        }

        private void btn_primero_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = true;
            pictureBox2.Image = dbc.ConvertByteToImg(0);
            lblKey2.Text = dbc.Key[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            lblKey2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ini2 = NumLabels - 1;
            pictureBox2.Image = dbc.ConvertByteToImg(ini2);
            lblKey2.Text = dbc.Key[ini2];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ini2 > 0)
            {
                ini2--;
                pictureBox2.Image = dbc.ConvertByteToImg(ini2);
                lblKey2.Text = dbc.Key[ini2];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ini2 < NumLabels - 1)
            {
                ini2++;
                pictureBox2.Image = dbc.ConvertByteToImg(ini2);
                lblKey2.Text = dbc.Key[ini2];
            }
        }

        private void lblComparar_Click(object sender, EventArgs e)
        {
            ResultadoComparar RC = new ResultadoComparar(pictureBox1.Image, pictureBox2.Image, lblKey.Text, lblKey2.Text);
            RC.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Comparar_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = dbc.ConvertByteToImg(0);
            lblKey2.Text = dbc.Key[0];
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

        private void btn_anterior_Click(object sender, EventArgs e)
        {
            if (ini > 0)
            {
                ini--;
                pictureBox1.Image = dbc.ConvertByteToImg(ini);
                lblKey.Text = dbc.Key[ini];
            }
        }

        private void btn_ultimo_Click(object sender, EventArgs e)
        {
            ini = NumLabels - 1;
            pictureBox1.Image = dbc.ConvertByteToImg(ini);
            lblKey.Text = dbc.Key[ini];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            lblKey.Text = "";
        }
    }
}
