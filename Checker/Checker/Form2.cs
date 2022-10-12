using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checker
{
    public partial class Form2 : Form
    {
        public Form2(

    Data data)
        {
            this.InitializeComponent();
            this.textBox1.Text = data.URL;
            this.textBox2.Text = data.Username;
            this.textBox3.Text = data.Pass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
