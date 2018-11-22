using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Reconocimiento_facial
{
    public partial class ReporteImagenes : Form
    {
        public ReporteImagenes()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReporteImagenes_Load(object sender, EventArgs e)
        {
            try
            {
                string conect = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = UsersFace.mdb";
                DataTable results = new DataTable();
                using (OleDbConnection conexion = new OleDbConnection(conect))
                {
                    OleDbCommand cmd = new OleDbCommand(@"Select * from Reporte", conexion);
                    conexion.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(results);
                }

                //Solo si tienes las columnas creadas en tiempo de Diseño, descomentarizas la siguiente linea
                //dgv_Mostrar.AutoGenerateColumns = false;
                dataGridView1.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
