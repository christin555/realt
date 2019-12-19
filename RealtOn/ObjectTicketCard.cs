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
    public partial class ObjectTicketCard : Form
    {
        public string id_object;
        public string id_ticket;
        public ObjectTicketCard(string _id)
        {
            id_object = _id;
            InitializeComponent();
        }

        private void ObjectCard_Load(object sender, EventArgs e)
        {
                   
            dataGridView1.RowTemplate.Height = 50;
            dataGridView2.RowTemplate.Height = 28;
            id_ticket = Object.GetTicketID(id_object);
            label1.Text = "Заявка #" + id_ticket;    
            GetData();
         
        }
        private void GetData()
        { 
            var ob = Object.GetObject(id_object).Tables[0];
            var tk = Ticket.GetTicket(id_ticket).Tables[0];

      
            dataGridView1.Rows.Add(4);
            dataGridView2.Rows.Add(8);
            //1 т
            dataGridView1.Rows[0].Cells[0].Value = "Код объекта";
            dataGridView1.Rows[1].Cells[0].Value = "Клиент";
            dataGridView1.Rows[2].Cells[0].Value = "Риэлтор";
            dataGridView1.Rows[3].Cells[0].Value = "Тип";
            dataGridView1.Rows[4].Cells[0].Value = "Стоимость";
            dataGridView1.Rows[4].Height = 50;

            //2 т
            dataGridView2.Rows[0].Cells[0].Value = "Отделка";
            dataGridView2.Rows[1].Cells[0].Value = "Кол-во комнат";
            dataGridView2.Rows[2].Cells[0].Value = "Площадь";
            dataGridView2.Rows[3].Cells[0].Value = "Площадь кухни";
            dataGridView2.Rows[4].Cells[0].Value = "Этаж";
            dataGridView2.Rows[5].Cells[0].Value = "Стены";
            dataGridView2.Rows[6].Cells[0].Value = "Окна";
            dataGridView2.Rows[7].Cells[0].Value = "Год постройки/сдачи";
            dataGridView2.Rows[8].Cells[0].Value = "Адрес";

            //1 бд
            dataGridView1.Rows[0].Cells[1].Value = "#"+ob.Rows[0]["id"];
            dataGridView1.Rows[1].Cells[1].Value = ob.Rows[0]["client"] +","+ Environment.NewLine + "тел." + ob.Rows[0]["tel"];
            dataGridView1.Rows[2].Cells[1].Value = tk.Rows[0]["user"];
            dataGridView1.Rows[3].Cells[1].Value = Object.GetDescription((Object.OType)ob.Rows[0]["type"]);
            dataGridView1.Rows[4].Cells[1].Value = String.Format("{0:C}", ob.Rows[0]["price"]);

            //2 бд
            dataGridView2.Rows[0].Cells[1].Value = Object.GetDescription((Object.Renovation)ob.Rows[0]["renovation"]);
            dataGridView2.Rows[1].Cells[1].Value = ob.Rows[0]["rooms"];
            dataGridView2.Rows[2].Cells[1].Value = "Площадь: " + ob.Rows[0]["area"]+ "кв.м";
            dataGridView2.Rows[3].Cells[1].Value = "Площадь: " + ob.Rows[0]["areakitchen"] + "кв.м";
            dataGridView2.Rows[4].Cells[1].Value = ob.Rows[0]["floor"];
            dataGridView2.Rows[5].Cells[1].Value = Object.GetDescription((Object.Wall)ob.Rows[0]["wall"]);
            dataGridView2.Rows[6].Cells[1].Value = Object.GetDescription((Object.Windows)ob.Rows[0]["windows"]);
            dataGridView2.Rows[7].Cells[1].Value = ob.Rows[0]["year"]+"г.";
            dataGridView2.Rows[8].Cells[1].Value = ob.Rows[0]["address"];


            textBox3.Text = tk.Rows[0]["description"].ToString();
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = System.Drawing.Color.Black;

            pictureBox1.Image = Properties.Resources.img_89501;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
          
                Form ifrm = new Objects();
                ifrm.Show(); // отображаем Form1
                             // this.Close(); // закрываем Form2 (this - текущая форма)
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
