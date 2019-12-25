using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtOn
{
    class Ticket
    {
        public enum TType
        {
            [Description("Продажа")]
            Sell = 1,
            [Description("Покупка")]
            Buy

        }

        public enum Status
        {
            [Description("Открыта")]
            Open = 1,
            [Description("Закрыта")]
            Closed,
            [Description("Отложена")]
            Otl,
            [Description("Отменена")]
            Otm

        }
        public enum Stage
        {
            [Description("Данные заполнены")]
            Open = 1,
            [Description("Документы загружены")]
            Closed,
            [Description("Договор подписан")]
            Otl,
            [Description("Постпродажа")]
            Otm

        }


        public static DataSet GetTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daobjects = new SqlDataAdapter("Select tickets.id,status,FullName as 'User', stage,img,type,description from tickets left join users on users.id = userId where tickets.id = " + id + "", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "Ticket");
            objConn.Close();

            return dsobjects;
        }
        public static DataSet GetTicketsList(int page, string filtr)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();


            SqlDataAdapter daobjects = new SqlDataAdapter("SELECT TOP 10 * FROM(Select Tickets.id, Tickets.description, Tickets.stage, Tickets.status, Tickets.type, Clients.FullName as Client, Users.FullName as 'User' from Tickets left join Clients on Clients.id = clientId left join Users on Users.id = userId " + filtr + " ORDER BY Tickets.id OFFSET " + (page * 10) + " ROWS) aliasname", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "ObjList");
            objConn.Close();
            return dsobjects;
        }

        public static string GetTypeTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            int type = Convert.ToInt32(new SqlCommand("Select type from tickets where tickets.id = " + id + "", objConn).ExecuteScalar());
            objConn.Close();
            return Tools.GetDescription((Ticket.TType)type);

        }
        public static string GetStatusTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            int status = Convert.ToInt32(new SqlCommand("Select status from tickets where tickets.id = " + id + "", objConn).ExecuteScalar());
            objConn.Close();
            return Tools.GetDescription((Ticket.Status)status);

        }

        public static string GetStageTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            int stage = Convert.ToInt32(new SqlCommand("Select stage from tickets where tickets.id = " + id + "", objConn).ExecuteScalar());
            objConn.Close();
            return Tools.GetDescription((Ticket.Stage)stage);

        }

        public static void ChangeStatus(int stage, string id_ticket)
        {
            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            new SqlCommand("update tickets set stage="+stage+" where id = " + id_ticket + "", objConn).ExecuteNonQuery();
            objConn.Close();
        }
    }
}
