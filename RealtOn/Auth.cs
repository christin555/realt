using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtOn
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (User.LogIn(textBox1.Text, textBox2.Text).ToString() == "error")
                label1.Text = "Ошибка";
            else
            {
                Form ifrm = new Objects();
                ifrm.Show(); // отображаем Form1
                this.Hide();

            }
        }
    }
}
