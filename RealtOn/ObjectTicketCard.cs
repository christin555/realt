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
        public string currentdoc;
        DataTable ob; DataTable tk;
        DataTable docs;
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
            dataGridView3.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.RowTemplate.Height = 50;
            GetData();
            CurrentDocShow();
          
        }
        private void GetData()
        {    
            //tab1   

             ob = Object.GetObject(id_object).Tables[0];
             tk = Ticket.GetTicket(id_ticket).Tables[0];

      
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
            dataGridView2.Rows[8].Height = 28;

            //1 бд
            dataGridView1.Rows[0].Cells[1].Value = "#"+ob.Rows[0]["id"];
            dataGridView1.Rows[1].Cells[1].Value = ob.Rows[0]["client"] +","+ Environment.NewLine + "тел." + ob.Rows[0]["tel"];
            dataGridView1.Rows[2].Cells[1].Value = tk.Rows[0]["user"];
            dataGridView1.Rows[3].Cells[1].Value = Tools.GetDescription((Object.OType)ob.Rows[0]["type"]);
            dataGridView1.Rows[4].Cells[1].Value = String.Format("{0:C}", ob.Rows[0]["price"]);

            //2 бд
            dataGridView2.Rows[0].Cells[1].Value = Tools.GetDescription((Object.Renovation)ob.Rows[0]["renovation"]);
            dataGridView2.Rows[1].Cells[1].Value = ob.Rows[0]["rooms"];
            dataGridView2.Rows[2].Cells[1].Value = "Площадь: " + ob.Rows[0]["area"]+ "кв.м";
            dataGridView2.Rows[3].Cells[1].Value = "Площадь: " + ob.Rows[0]["areakitchen"] + "кв.м";
            dataGridView2.Rows[4].Cells[1].Value = ob.Rows[0]["floor"];
            dataGridView2.Rows[5].Cells[1].Value = Tools.GetDescription((Object.Wall)ob.Rows[0]["wall"]);
            dataGridView2.Rows[6].Cells[1].Value = Tools.GetDescription((Object.Windows)ob.Rows[0]["windows"]);
            dataGridView2.Rows[7].Cells[1].Value = ob.Rows[0]["year"]+"г.";
            dataGridView2.Rows[8].Cells[1].Value = ob.Rows[0]["address"];


            textBox3.Text = tk.Rows[0]["description"].ToString();
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = System.Drawing.Color.Black;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"..\..\..\img\" + tk.Rows[0]["img"].ToString());

            //tab2

            docs = Doc.GetDocsTicket(id_ticket).Tables[0];
            var dl = Doc.GetDocsList(tk.Rows[0]["type"].ToString());
            for (int i = 0; i < dl.Count(); i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = dl[i];
                dataGridView3.Rows[i].Cells[1].Value = Tools.GetDescription((Doc.DocTicket)dl[i]);

                if (FindCurrentValue(i.ToString()) != "null")
                {
                    dataGridView3.Rows[i].Cells[2].Value = "Загружено✓";
                    dataGridView3.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(119, 221, 119);

                }
                else
                {
                    dataGridView3.Rows[i].Cells[2].Value = "Загрузите документ!";
                    dataGridView3.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(221, 173, 175);
                }

            }

            
           currentdoc = dataGridView3.Rows[0].Cells[0].Value.ToString();
           label3.Text = Ticket.GetTypeTicket(id_ticket);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string FindCurrentValue(string cdoc)
        {
            string result = "null";
            for (int i = 0; i < docs.Rows.Count; i++)
            {
                if (docs.Rows[i]["doc"].ToString() == cdoc)
                {
                    result = docs.Rows[i]["value"].ToString();
                    break;
                }
            }
            //  string[] doclist = Ticket.GetDoc(id_ticket);
            return result;
        }

        private void CurrentDocShow()
        {
            

                label2.Text = Tools.GetDescription((Doc.DocTicket)Convert.ToInt32(currentdoc));
                string value = FindCurrentValue(currentdoc);
                if (value != "null")
                    pictureBox7.Image = Image.FromFile(Application.StartupPath + @"..\..\..\docs\" + value);
                else
                {
                    pictureBox7.Image = null;
                    label2.Text = "Загрузите документ";
                }
           
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            currentdoc = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            CurrentDocShow();
            
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
     
            string namefile = "";         
            var ofd = new OpenFileDialog();
            ofd.Filter = "jpg|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                namefile = System.Guid.NewGuid() + ".jpeg";
                pictureBox7.Image = Image.FromFile(ofd.FileName);
                pictureBox7.Image.Save(Application.StartupPath + @"..\..\..\docs\" + namefile);
                Doc.DocAdd(namefile, id_ticket, currentdoc);
                docs = Doc.GetDocsTicket(id_ticket).Tables[0];
                label2.Text = "Документ загружен";
            }
        
            //  GC.Collect(5, GCCollectionMode.Optimized);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
