using Emgu.CV;
using Emgu.CV.Structure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reconocimiento_facial
{
    public partial class Comparar_Automatico : Form
    {
        DBCon dbc = new DBCon();
        int ini = 0, NumLabels, ContTrain, con = 0;
        public string[] Labels, Keys;

        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> keys = new List<string>();

        public Comparar_Automatico()
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
            groupBox2.Enabled = true;
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            lblKey.Text = "";
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            ExtraerComparar();
        }

        string MaxCurp = "";
        double MaxPor = -1;
        byte[] MaxFace;

        private void ExtraerComparar()
        {
            DBCon DB = new DBCon();
            MySqlConnection con = DB.MySql();
            try
            {
                string query = "Select * From datos ;";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    Compara(lblKey.Text, datos["curp"].ToString());
                }
                
                datos.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lblKeyMax.Text = MaxCurp;
            string ResPor = Regex.Replace(MaxPor.ToString(), @"-", "");
            lblProcentaje.Text = ResPor;
            Image FetImg;
            MemoryStream ms = new MemoryStream(MaxFace);
            FetImg = Image.FromStream(ms);
            pcbMax.Image = FetImg;

        }



        private void Compara(string curp, string comparar)
        {
            double Peso=0, Altura=0, Edad=0;
            DBCon DB = new DBCon();
            MySqlConnection con = DB.MySql();
            try
            {
                string query = "Select * From datos where curp='"+curp+"';";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    Peso = Convert.ToDouble(datos["peso"].ToString());
                    Altura = Convert.ToDouble(datos["altura"].ToString());
                    Edad = Convert.ToDouble(datos["edad"].ToString());

                }

                datos.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //-------------------------
            double pes, alt, ed;
            try
            {
                string query = "Select * From datos where curp='" + comparar + "' and curp!='"+curp+"';";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    pes = Peso - Convert.ToDouble(datos["peso"].ToString());
                    alt = Altura - Convert.ToDouble(datos["altura"].ToString());
                    ed = Edad - Convert.ToDouble(datos["edad"].ToString());
                    if ((pes + alt + ed) > MaxPor)
                    {
                        MaxCurp = comparar;
                        MaxPor = (pes + alt + ed);

                        DBCon DC = new DBCon();
                        string sql = "SELECT Face FROM UserFaces WHERE Code='"+comparar+"';";
                        OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, DC.conn);
                        DataTable dt = new DataTable();
                        adaptador.Fill(dt);
                        int cont = dt.Rows.Count;
                        for (int i = 0; i < cont; i++)
                        {
                            
                            MaxFace = (byte[])dt.Rows[i]["Face"];
                        }
                        DC.conn.Close();

                    }
                }

                datos.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (ini > 0)
            {
                ini--;
                pictureBox1.Image = dbc.ConvertByteToImg(ini);
                lblKey.Text = dbc.Key[ini];
            }
        }

        private void btn_primero_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = dbc.ConvertByteToImg(0);
            lblKey.Text = dbc.Key[0];
        }
    }
}
