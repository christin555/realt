﻿using System;
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
    public partial class Tickets : Form
    {
        public string filtr;
        public string idUser;

        public Tickets()
        {
            idUser = Auth.id;
            InitializeComponent();
        }
     
        private void Tickets_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns["User"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            GetTickets(0);

        }

        private void GetTickets(int page)
        {

            var ds = Ticket.GetTicketsList(page, filtr).Tables[0];
            // Object.GetDescription(Object.Status.Active);
            //  dataGridView1.Rows.Add(ds.Rows);
            try
            {
                dataGridView1.Rows.Clear();
            }
            catch { };

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["id"].Value = ds.Rows[i]["id"];
                dataGridView1.Rows[i].Cells["Stage"].Value = Tools.GetDescription((Ticket.Stage)ds.Rows[i]["Stage"]);
                dataGridView1.Rows[i].Cells["Status"].Value = Tools.GetDescription((Ticket.Status)ds.Rows[i]["Status"]);
                dataGridView1.Rows[i].Cells["Ttype"].Value = Tools.GetDescription((Ticket.TType)ds.Rows[i]["type"]);
                dataGridView1.Rows[i].Cells["Description"].Value = ds.Rows[i]["Description"];
            //    dataGridView1.Rows[i].Cells["User"].Value = "Отделка: " + Tools.GetDescription((Object.Renovation)ds.Rows[i]["renovation"]) + Environment.NewLine + "Комн: " + ds.Rows[i]["rooms"] + Environment.NewLine + "Этаж: " + ds.Rows[i]["floor"] + Environment.NewLine + "Площадь: " + ds.Rows[i]["area"] + Environment.NewLine + "Стены: " + Tools.GetDescription((Object.Wall)ds.Rows[i]["wall"]) + Environment.NewLine + "Год: " + ds.Rows[i]["year"];
            //   dataGridView1.Rows[i].Cells["price"].Value = String.Format("{0:C}", ds.Rows[i]["price"]);
                dataGridView1.Rows[i].Cells["Client"].Value = ds.Rows[i]["Client"];
               // dataGridView1.Rows[i].Cells["Contacts"].Value = "Тел.: " + ds.Rows[i]["tel"] + Environment.NewLine + "E-mail: " + ds.Rows[i]["email"];
                dataGridView1.Rows[i].Cells["User"].Value = ds.Rows[i]["User"];

            }
            pagination();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) != 1)
                textBox1.Text = (Convert.ToInt32(textBox1.Text) - 1).ToString();
            GetTickets(Convert.ToInt32(textBox1.Text) - 1);

        }

   
        private void button1_Click(object sender, EventArgs e)
        {

            filtr = "";
            GetTickets(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filtr = " where Tickets.userId="+ idUser + " ";
            GetTickets(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt32(textBox1.Text) + 1).ToString();
            GetTickets(Convert.ToInt32(textBox1.Text) - 1);
        }

        private void pagination()
        {
            button5.Enabled = ((dataGridView1.Rows.Count < 10) ? false : true);
            button4.Enabled = ((Convert.ToInt32(textBox1.Text) == 1) ? false : true);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Ticket.GetObject(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Form ifrm = new status(id.ToString());
            ifrm.Show();
             this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Objects();
            ifrm.Show();
            this.Close();
        }
    }
}
