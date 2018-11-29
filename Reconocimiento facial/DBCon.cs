using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconocimiento_facial
{
    public class DBCon
    {
        public OleDbConnection conn;
        public string[] Name, Key, Peso, Altura, Fecha, Edad, Tipo_Imagen;
        private byte[] face;
        public List<byte[]> Face = new List<byte[]>();
        public int TotalUser;

        
        public DBCon()
        {
            conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = UsersFace.mdb");
            conn.Open();
        }

        public bool GuardarImagen(string Name, string Code, byte[] abImagen, double peso, double altura, string fecha, int edad, string tipo_imagen)
        {
            OleDbCommand comm = new OleDbCommand("INSERT INTO UserFaces (Name,Code,Face,peso,altura, fecha, edad, tipo_imagen) VALUES ('" + Name + "','" + Code + "',?,'"+peso+"','"+altura+"','"+fecha+"','"+edad+"','"+tipo_imagen+"')", conn);           
            OleDbParameter parImagen = new OleDbParameter("@Face", OleDbType.VarBinary, abImagen.Length);
            parImagen.Value = abImagen;
            comm.Parameters.Add(parImagen);            
            int iResultado = comm.ExecuteNonQuery();
            conn.Close();
            return Convert.ToBoolean(iResultado);
        }

        public void EditarDatos(string curp, string fecha, string peso, string altura, string edad, string curp_o)
        {
            try
            {
                OleDbCommand comm = new OleDbCommand("UPDATE UserFaces SET Code='" + curp + "', fecha='" + fecha + "',peso='" + peso + "', altura='" + altura + "', edad='" + edad + "' WHERE Code='" + curp_o + "'", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                System.Windows.Forms.MessageBox.Show("Datos Actualizados");
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
        }

        public void GeneraReporte(string modo, string curp1, string curp2, string porciento, string fecha)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OleDbCommand comm = new OleDbCommand("INSERT INTO Reportes (modo, curp1, curp2, porciento, fecha) VALUES('"+modo+"', '"+curp1+"', '"+curp2+"', '"+porciento+"', '"+fecha+"')", conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        public int ExtraerDatos(string curp)
        {
            for(int i = 0; i < TotalUser; i++)
            {
                if (Key[i] == curp)
                {
                    return i;
                }
            }
            return -1;
        }



        public DataTable ObtenerBytesImagen()
        {
            string sql = "SELECT IdImage,Name,Code,Face,peso,altura,fecha,edad,tipo_imagen FROM UserFaces";

            OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conn);
        
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
          
            int cont = dt.Rows.Count;
            Name = new string[cont];
            Key = new string[cont];
            Peso = new string[cont];
            Altura = new string[cont];
            Fecha = new string[cont];
            Edad = new string[cont];
            Tipo_Imagen = new string[cont];

            for (int i = 0; i < cont; i++)
            {
                Name[i] = dt.Rows[i]["Name"].ToString();
                Key[i] = dt.Rows[i]["Code"].ToString();
                Peso[i] = dt.Rows[i]["peso"].ToString();
                Altura[i] = dt.Rows[i]["altura"].ToString();
                Fecha[i] = dt.Rows[i]["fecha"].ToString();
                Edad[i] = dt.Rows[i]["edad"].ToString();
                Tipo_Imagen[i] = dt.Rows[i]["tipo_imagen"].ToString();
                face = (byte[])dt.Rows[i]["Face"];
                Face.Add(face);
            }
            TotalUser = dt.Rows.Count;
            conn.Close();
            return dt;
        }

        public void ConvertImgToBinary(string Name, string Code, Image Img, double peso, double altura, string fecha, int edad, string tipo_imagen)
        {
            Bitmap bmp = new Bitmap(Img);
            MemoryStream MyStream = new MemoryStream();
            bmp.Save(MyStream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] abImagen = MyStream.ToArray();
            GuardarImagen(Name, Code, abImagen, peso, altura, fecha, edad, tipo_imagen);
        }

        public Image ConvertByteToImg( int con)
        {
            Image FetImg;
            byte[] img = Face[con];
            MemoryStream ms = new MemoryStream(img);
            FetImg = Image.FromStream(ms);
            ms.Close();
            return FetImg;

        }

        public void BorrarRegistros()
        {
            //conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = UsersFace.mdb");
            //conn.Open();


            //string sql = "DELETE FROM UserFaces WHERE Name = ''";


            //OleDbCommand cmd = new OleDbCommand(sql,conn);

            
            OleDbCommand comm = new OleDbCommand("DELETE FROM UserFaces WHERE Name = 'marko'", conn);


        }

        public bool GenerarReporte(string name, string date)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Reporte(Nombre,Fecha) VALUES('" + name + "','" + date + "')", conn);
            int iResultado = cmd.ExecuteNonQuery();
            conn.Close();
            return Convert.ToBoolean(iResultado);
        }
    }
}
