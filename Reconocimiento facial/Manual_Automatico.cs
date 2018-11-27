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
    public partial class Manual_Automatico : Form
    {
        public Manual_Automatico()
        {
            InitializeComponent();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            Comparar cmp = new Comparar();
            this.Close();
            cmp.ShowDialog();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            Comparar_Automatico CA = new Comparar_Automatico();
            this.Close();
            CA.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
