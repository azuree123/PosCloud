using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;



namespace WeeklyMail
{
    class ServiceLog
    {

         
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    
        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void SendClientEmails()
        {
            List<Client> clients=new List<Client>();
            SqlConnection conn;
            SqlCommand comm;
            conn = new SqlConnection(@"Data Source=192.168.100.2;Initial Catalog=POSWeb;User Id=sa;Password=Tech@381;");
            comm = new SqlCommand("Select From PosCloud.TransMasters as a  where a.TransDate = @p1", conn);

            comm.Parameters.AddWithValue("@p1", DateTime.Today.AddDays(-7));
            conn.Open();
            SqlDataReader rd = comm.ExecuteReader();
            while (rd.Read())
            {
                clients = DataReaderMapToList<Client>(rd);

            }


            foreach (var client in clients)
            {
                SendEmail(
                    client.Email,
                    "This is testing mail",
                    "hello"
                    );
            }
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static void SendEmail(String ToEmail,String Subj, string Message)
        {
            //Reading sender Email credential from web.config file  
          

        

        string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
            string Pass = ConfigurationManager.AppSettings["Password"].ToString();
            int Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());

            //creating the object of MailMessage  
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
            mailMessage.Subject = Subj; //Subject of Email  
            mailMessage.Body = Message; //body or message of Email  
            mailMessage.IsBodyHtml = true;

            string[] ToMuliId = ToEmail.Split(',');
            foreach (string ToEMailId in ToMuliId)
            {
                mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id  
            }
            
            SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
            smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

            //network and security related credentials  

            smtp.EnableSsl = false;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = Pass;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = Port;
            smtp.Send(mailMessage); //sending Email  
        }

      
    }
}
