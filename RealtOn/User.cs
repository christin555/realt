using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtOn
{
    class User
    {

        string FName
        {
            get; set;
        }
        string SName
        {
            get; set;
        }
        string Number
        {
            get; set;
        }
        double Money
        {
            get; set;
        }


        enum Permissions
        {
            None,
            Guest,
            User,
            Admin
        }
        class Person
        {
  
            public const Permissions Permission = Permissions.None;
        }

        class Guest : Person
        {
            public new const Permissions Permission = Permissions.Guest;
        }
        public static object LogIn(string login , string password)
        {
            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            try
            {
                string id = new SqlCommand("select id from users where login = '" + login + "' and password = '" + password + "'", objConn).ExecuteScalar().ToString();
                if (Int32.TryParse(id, out int s))
                {
                      return id;
                }
            }
            catch { }
            return "error";

        }
        public static object GetUser(string id)
        {
            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daperson = new SqlDataAdapter("select * from users where id = '" + id +"'", objConn);
            DataSet dsperson = new DataSet();
            daperson.Fill(dsperson, "User");
         
            User p = new User();
         
            return p;
        }

    }
}

