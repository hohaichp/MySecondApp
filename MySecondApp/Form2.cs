using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySecondApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // MessageBox.Show("Form2 Loading...");
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            // MessageBox.Show("Form2 Activated...");
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            // MessageBox.Show("Form2 Shown...");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirm Operation", MessageBoxButtons.YesNo);
            e.Cancel = (result == DialogResult.No);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this.ShowForm3();
            ColorDialog dlg = new ColorDialog();
            dlg.Color = Color.Red;
            
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show("You picked " + dlg.Color.ToString());
            }
        }

        private void ShowForm3()
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            // form3.Visible = true;
        }
    }
}
