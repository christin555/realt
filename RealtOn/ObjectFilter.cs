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
    public partial class ObjectFilter : Form
    {
        public ObjectFilter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
          // richTextBox1.Text =  dadata.SuggestAddress(textBox1.Text.ToString()).ToString();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
          

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           /*  if (comboBox1.Text.ToString() != "")
            {
                var dadata = new SuggestClientDadata();
                IList<Dadata.Model.Suggestion<Dadata.Model.Address>> array = dadata.SuggestAddress(comboBox1.Text.ToString());

                comboBox1.Items.Clear();

                for (int i = 0; i < array.Count; i++)
                {
                    comboBox1.Items.Add(array[i].value);

                }
            }
            */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
            {
                var dadata = new SuggestClientDadata();
                Dadata.Model.Suggestion < Dadata.Model.Address>[] array = dadata.SuggestAddress(textBox1.Text.ToString()).ToArray();

                listBox1.Items.Clear();
                listBox1.Visible = true;
                for (int i = 0; i < array.Count() ; i++)
                {
                    listBox1.Items.Add(array[i].value);

                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = !listBox1.Visible;
        }

        private void ObjectFilter_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Objects();
            ifrm.Show(); // отображаем Form1
           // this.Close(); // закрываем Form2 (this - текущая форма)
                          // не используйте данный способ, правильный ниже
        }
    }
}
