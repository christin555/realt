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
    public partial class Objects : Form
    {
        public Objects()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Objects();
            ifrm.Show(); // отображаем Form1
            this.Close(); // закрываем Form2 (this - текущая форма)
                          // не используйте данный способ, правильный ниже
        }

        private void Objects_Load(object sender, EventArgs e)
        {

        }
    }
}
