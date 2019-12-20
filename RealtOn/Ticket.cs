﻿using System;
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



        public static DataSet GetTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daobjects = new SqlDataAdapter("Select tickets.id,status,FullName as 'User', stage,img,type,description from tickets left join users on users.id = userId where tickets.id = " + id + "", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "Ticket");
            return dsobjects;
        }
  

        public static string GetTypeTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            string type = new SqlCommand("Select type from tickets where tickets.id = " + id + "", objConn).ExecuteScalar().ToString();     
            return type;
        }
    }
}
