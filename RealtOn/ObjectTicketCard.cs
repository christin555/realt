using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtOn
{
    public partial class status : Form
    {
        public string id_object;
        public string id_ticket;
        public string currentdoc;
        DataTable ob; DataTable tk;
        DataTable docs;
        public status(string _id)
        {
            id_object = _id;
            InitializeComponent();
        }

        private void UpdateLabel()
        {
            type.Text = Ticket.GetTypeTicket(id_ticket).ToUpper();
            statusl.Text = Ticket.GetStatusTicket(id_ticket).ToUpper();

            stage.Text = Ticket.GetStageTicket(id_ticket).ToUpper();
        }

        private void ObjectCard_Load(object sender, EventArgs e)
        {

            dataGridView1.RowTemplate.Height = 50;
            dataGridView2.RowTemplate.Height = 28;
            id_ticket = Object.GetTicketID(id_object);
            label1.Text = "Заявка #" + id_ticket;
            tabledoc.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tabledoc.RowTemplate.Height = 50;
            GetDataTab1();
            GetDataTab2();
            CurrentDocShow();
            checkdocstatus();
            UpdateLabel();
        }

        private void GetDataTab2()
        {
            //tab2

            docs = Doc.GetDocsTicket(id_ticket).Tables[0];
            var dl = Doc.GetDocsList(tk.Rows[0]["type"].ToString());



            for (int i = 0; i < dl.Count(); i++)
            {
                tabledoc.Rows.Add();
                tabledoc.Rows[i].Cells[0].Value = dl[i];
                tabledoc.Rows[i].Cells[1].Value = Tools.GetDescription((Doc.DocTicket)dl[i]);

                if (FindCurrentValue(dl[i].ToString()) != "null")
                {
                    tabledoc.Rows[i].Cells[2].Value = "Загружено✓";
                    //  dataGridView3.Rows[i].Cells[2].Value = FindCurrentValue(i.ToString());

                    tabledoc.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(119, 221, 119);

                }
                else
                {
                    tabledoc.Rows[i].Cells[2].Value = "Загрузите документ!";
                    //  dataGridView3.Rows[i].Cells[2].Value = FindCurrentValue(i.ToString());
                    tabledoc.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(221, 173, 175);
                }

            }


            currentdoc = tabledoc.Rows[0].Cells[0].Value.ToString();
            stage.Text = Ticket.GetTypeTicket(id_ticket);
        }


        private void GetDataTab1()
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
            dataGridView1.Rows[0].Cells[1].Value = "#" + ob.Rows[0]["id"];
            dataGridView1.Rows[1].Cells[1].Value = ob.Rows[0]["client"] + "," + Environment.NewLine + "тел." + ob.Rows[0]["tel"];
            dataGridView1.Rows[2].Cells[1].Value = tk.Rows[0]["user"];
            dataGridView1.Rows[3].Cells[1].Value = Tools.GetDescription((Object.OType)ob.Rows[0]["type"]);
            dataGridView1.Rows[4].Cells[1].Value = String.Format("{0:C}", ob.Rows[0]["price"]);

            //2 бд
            dataGridView2.Rows[0].Cells[1].Value = Tools.GetDescription((Object.Renovation)ob.Rows[0]["renovation"]);
            dataGridView2.Rows[1].Cells[1].Value = ob.Rows[0]["rooms"];
            dataGridView2.Rows[2].Cells[1].Value = "Площадь: " + ob.Rows[0]["area"] + "кв.м";
            dataGridView2.Rows[3].Cells[1].Value = "Площадь: " + ob.Rows[0]["areakitchen"] + "кв.м";
            dataGridView2.Rows[4].Cells[1].Value = ob.Rows[0]["floor"];
            dataGridView2.Rows[5].Cells[1].Value = Tools.GetDescription((Object.Wall)ob.Rows[0]["wall"]);
            dataGridView2.Rows[6].Cells[1].Value = Tools.GetDescription((Object.Windows)ob.Rows[0]["windows"]);
            dataGridView2.Rows[7].Cells[1].Value = ob.Rows[0]["year"] + "г.";
            dataGridView2.Rows[8].Cells[1].Value = ob.Rows[0]["address"];


            textBox3.Text = tk.Rows[0]["description"].ToString();
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = System.Drawing.Color.Black;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"..\..\..\img\" + tk.Rows[0]["img"].ToString());
            }
            catch { }




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

            currentdoc = tabledoc.CurrentRow.Cells[0].Value.ToString();
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

                tabledoc.Rows[(Convert.ToInt32(currentdoc)) - 1].Cells[2].Value = "Загружено✓";
                tabledoc.Rows[(Convert.ToInt32(currentdoc)) - 1].Cells[2].Style.BackColor = Color.FromArgb(119, 221, 119);
                checkdocstatus();
            }

            //  GC.Collect(5, GCCollectionMode.Optimized);
        }

        private bool checkdocstatus()
        {
            int check = 0;
            for (int i = 0; i < tabledoc.Rows.Count; i++)
            {
                if (tabledoc.Rows[i].Cells[2].Value == "Загружено✓")
                {
                    check++;
                }
            }
            if (check == tabledoc.Rows.Count)
            {
                Ticket.ChangeStatus(2, id_ticket);
                UpdateLabel();
                return true;
            }
            else
            {
                Ticket.ChangeStatus(1, id_ticket);
                UpdateLabel();
            }
            return false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void stage_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "docx|*.docx";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string fileName = saveFileDialog1.FileName;


            //  string filepath = "C:\\Users\\Тина\\Downloads\\";
            //     string fileName = "Тестовый файл.docx";
            //Create a WebClient to use as our download proxy for the program.
            WebClient webClient = new WebClient();

            if (File.Exists(fileName) != true)// если файла нет то просто скачиваем
            {
                WebClient client = new WebClient();
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                client.DownloadFileAsync(new Uri("https://vscode.ru/filesForArticles/test.docx"), fileName);
            }
            else// если файл есть, удаляем и скачиваем новый
            {
                File.Delete(fileName);
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                client.DownloadFileAsync(new Uri("https://vscode.ru/filesForArticles/test.docx"), fileName);
            }
        }


        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        //This is your method that will pop when the AsyncCompletedEvent is fired,
        //this doesn't mean that the download was successful though which is why
        //it's misleading, it just means that the Async process completed.
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                pd.ShowDialog();

                ProcessStartInfo info = new ProcessStartInfo("C:\\Users\\Тина\\Desktop\\Вариант06.docx");

                info.Verb = "PrintTo";
              //gt  info.Arguments = pd.PrinterSettings.PrinterName;

                info.CreateNoWindow = true;

                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process.Start(info);
            }
        }
    }
}
