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
    public partial class SelectorNumerico : Form
    {

       public int franjas = 2;
        public SelectorNumerico()
        {
            InitializeComponent();
            domainUpDown1.SelectedIndex = 6;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            franjas = Int16.Parse(domainUpDown1.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
