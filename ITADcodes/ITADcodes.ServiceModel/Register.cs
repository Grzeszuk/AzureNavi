using FoodNavigator___Controller.Data_Encrypter;
using ServiceStack;
using ServiceStack.OrmLite;
using Simplify.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ITADcodes.ServiceModel
{

    [Route("/register", "GET")]
    [Route("/register/{username}/{password}/{email}", "GET")]
    public class RegisterUsername : IReturn<RegisterResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class RegisterResponse
    {
        public bool allow { get; set; }
        public string why { get; set; }
        public RegisterResponse(string usernameRequest,string password,string email)
        {
           
                var dbFactory = new OrmLiteConnectionFactory(
                @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                 SqliteDialect.Provider);

                var db = dbFactory.Open();

                db.CreateTable<User>();

                usernameRequest = Uri.UnescapeDataString(usernameRequest);
                password = Uri.UnescapeDataString(password.Replace('&', '%'));

            if (db.Exists<User>(x => x.username == usernameRequest.ToLower().Replace(' ', '-') || x.email==email) == false) 
                {
                    db.Insert(new User {username = usernameRequest.ToLower().Replace(' ', '-'), password = Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj"), email = email.ToLower().Replace(' ', '-') , active="false"});

                    var body = File.ReadAllText(@"D:\home\site\wwwroot\cerberus-fluid.html");
                    body = body.Replace("USERTOCHANGE",$"{usernameRequest}");
                    body = body.Replace("PASSWORDTOCHANGE", $"{Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj")}");
                    body = body.Replace("LINKTOCHANGE", $"https://foodnavigator.azurewebsites.net/registerconfirm/{usernameRequest.ToLower().Replace(' ', '-')}");

                    MailSender.Default.Send("twonoobsinc@gmail.com",email,"Thanks for registration",body,null);

                    allow = true;
                }
                else
                {
                    if (db.Exists<User>(x => x.username == usernameRequest.ToLower().Replace(' ', '-')) && db.Exists<User>(x => x.email == email) == false)
                    {
                        why = "username";
                    }
                    else
                    {
                        why = "email";
                    }
                    allow = false;
                }       
        }
    }
}