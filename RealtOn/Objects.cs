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
        public string filtr;
        public Objects()
        {
            InitializeComponent();
        }
        public Objects(string _filtr)
        {
            InitializeComponent();
            filtr = _filtr;
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
            dataGridView1.Columns["Params"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns["Contacts"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.Height = 100;
            GetObjects(0);
           
        }

        private void GetObjects(int page)
        {
           
            var ds = Object.GetObjectsList(page, filtr).Tables[0];
            // Object.GetDescription(Object.Status.Active);
            //  dataGridView1.Rows.Add(ds.Rows);
            try
            {
                dataGridView1.Rows.Clear();
            }
            catch { };
         
            for (int i=0;i< ds.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["id"].Value = ds.Rows[i]["id"];
                dataGridView1.Rows[i].Cells["Otype"].Value = Object.GetDescription((Object.OType)ds.Rows[i]["type"]);
                dataGridView1.Rows[i].Cells["Params"].Value = "Отделка: " + Object.GetDescription((Object.Renovation)ds.Rows[i]["renovation"]) + Environment.NewLine+"Комн: " + ds.Rows[i]["rooms"] + Environment.NewLine +"Этаж: "+ ds.Rows[i]["floor"] + Environment.NewLine + "Площадь: " + ds.Rows[i]["area"] + Environment.NewLine + "Стены: " + Object.GetDescription((Object.Wall)ds.Rows[i]["wall"]) +Environment.NewLine + "Год: " + ds.Rows[i]["year"];
                dataGridView1.Rows[i].Cells["price"].Value = String.Format("{0:C}", ds.Rows[i]["price"]);
                dataGridView1.Rows[i].Cells["client"].Value = ds.Rows[i]["FullName"];
                dataGridView1.Rows[i].Cells["contacts"].Value = "Тел.: " + ds.Rows[i]["tel"] + Environment.NewLine + "E-mail: " + ds.Rows[i]["email"];
          
            }
            pagination();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void pagination()
        {
            button5.Enabled =((dataGridView1.Rows.Count < 10)?  false : true);
            button4.Enabled = ((Convert.ToInt32(textBox1.Text)==1) ? false : true);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) != 1) textBox1.Text = (Convert.ToInt32(textBox1.Text) - 1).ToString();
                GetObjects(Convert.ToInt32(textBox1.Text) - 1);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt32(textBox1.Text) + 1).ToString();
            GetObjects(Convert.ToInt32(textBox1.Text) - 1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form ifrm = new ObjectFilter();
            ifrm.Show(); // отображаем Form1
            this.Close(); // закрываем Form2 (this - текущая форма)
                          // не используйте данный способ, правильный ниже
        }

        private void button1_Click(object sender, EventArgs e)
        {

            filtr = "";
            GetObjects(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filtr = "where Tickets.userId=1";
            GetObjects(0);
        }
    }
}
