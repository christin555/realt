using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace RealtOn
{
   public class Object
    {
      
        public enum Status
        {
            [Description("Активен")]
            Active = 1,
            [Description("Закрыт")]
            Closed
                
        }
        public Status _Status;

        public enum Wall
        {
            [Description("Блочные")]
            Block = 1,
            [Description("Деревянные")]
            Wood,
            [Description("Монолитные")]
            Monol,
            [Description("Панельные")]
            Panel
        }
        public enum Windows
        {
            [Description("Пластиковые")]
            Block = 1,
            [Description("Деревянные")]
            Wood,
            [Description("Алюминиевые")]
            Alumin,
            [Description("Пластиковые/Деревянные")]
            Combo
        }
        public enum Renovation
        {
            [Description("Черновая")]
            Black = 1,
            [Description("Косметический")]
            Cosmetic,
            [Description("Современный")]
            Modern
          
        }
        public enum OType
        {
            [Description("Квартира")]
            Адфе = 1,
            [Description("Пансионат")]
            Pans,
            [Description("Малосемейка")]
            Smallfam,
            [Description("Общежитие")]
            Obshezh
        }

   

        public static DataSet GetObjectsList(int page, string filtr)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn  = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daobjects= new SqlDataAdapter("SELECT TOP 10 * FROM(Select Objects.id,area, adresses.value as address,floor,rooms, wall,renovation,year,Objects.type,price, FullName as client,Tel,Email from Objects left join Tickets on Tickets.objectId= Objects.id left join Clients on Clients.id=clientId left join adresses on adresses.id=addressId "+filtr+" ORDER BY Objects.id OFFSET "+(page*10)+" ROWS) aliasname", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "ObjList");
            objConn.Close();       
            return dsobjects;
        }
        public static DataSet GetObject(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daobjects = new SqlDataAdapter("Select Objects.id,area, adresses.value as address,floor,rooms, areakitchen,windows,wall,renovation,year,Objects.type,price, FullName as client,Tel,Email from Objects left join Tickets on Tickets.objectId= Objects.id left join Clients on Clients.id=clientId left join adresses on adresses.id=addressId where objects.id =" + id+"", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "Obj");
            objConn.Close();

            return dsobjects;
        }
        public static string GetTicketID(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            string idTicket = new SqlCommand("Select id from tickets where objectId =" + id + "", objConn).ExecuteScalar().ToString();
            objConn.Close();

            return idTicket;
        }
    }
}
