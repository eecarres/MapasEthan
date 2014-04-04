using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapasEthan2
{
    public partial class PreferenciasArea : Form
    {
        public double areaMaxima = 30.0;
        public double desplazamientoMaximo = 0.5;
        public int opcion = 0;
        public int franjas = 2;

        


        public PreferenciasArea()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            opcion = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAreaMax_TextChanged(object sender, EventArgs e)
        {
            this.areaMaxima = double.Parse(txtAreaMax.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.desplazamientoMaximo = double.Parse(textBox1.Text);
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            opcion = comboBox1.SelectedIndex;
            if (opcion == 2 || opcion == 3)
            {
                SelectorNumerico selector = new SelectorNumerico();
                selector.ShowDialog();
                franjas = selector.franjas;
            }
        }

        private void PreferenciasArea_Load(object sender, EventArgs e)
        {
 
        }
    }
}
