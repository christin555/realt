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
    class Doc
    {
        public enum DocTicket
        {
            [Description("Паспорт")]
            Passport = 1,
            [Description("Техпаспорт")]
            RegistrationCertificate,
            [Description("Выписка из ЕГРН")]
            ExtractEGRN,
            [Description("Выписка с лицевого счета")]
            AccountStatement,
            [Description("Разрешение органов опеки и попечительства")]
            ResolutionGuardianship,
            [Description("Свидетельство о праве собственности")]
            CertificateOwnership,
            [Description("Нотариально заверенное согласие супруга на продажу квартиры")]
            SpouseConsent,
            [Description("Нотариально заверенная доверенность")]
            Procuration,
            [Description("Свидетельство о браке или разводе")]
            MarriageCertificate

        }
        public static int[] GetDocsList(string typeTicket)
        {
            int[] docslist = { };

            if (typeTicket == "1")
            {
                int[] docslistProd = { 1, 2,3,4,5,6,7,8, };
                docslist = docslistProd;
            }
            if (typeTicket == "2")
            {
                int[] docslistPok= {1};
                docslist = docslistPok;
            }
            
            return docslist;
        }
        public static DataSet GetDocsTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter docsticket= new SqlDataAdapter("Select doc,value from docs where ticketId = " + id + "", objConn);
            DataSet dsdocsticket = new DataSet();
            docsticket.Fill(dsdocsticket, "Docs");
            return dsdocsticket;

        }
        public static void DocAdd(string filename,string idticket, string iddoc)
        {
            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
           new SqlCommand("if not EXISTS(Select id from docs where ticketid="+ idticket + " AND doc="+ iddoc + ") Insert into docs values('" + idticket + "','"+ iddoc + "','"+ filename+ "') else update docs set value='"+filename+ "' where ticketid=" + idticket + " AND doc=" + iddoc + "", objConn).ExecuteNonQuery();
            
         
        }

    }
}
