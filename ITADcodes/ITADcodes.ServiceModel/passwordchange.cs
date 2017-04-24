using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodNavigator___Controller.Data_Encrypter;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text.Common;
using Simplify.Mail;

namespace ITADcodes.ServiceModel
{
    [Route("/passwordc", "GET")]
    [Route("/passwordc/{username}/{password}", "GET")]
    public class PasswordChange : IReturn<PasswordChangeResponse>
    {
        public string username { get; set; }
        public string password { get; set; }

    }

    public class PasswordChangeResponse
    {
        public bool alert { get; set; }
    
        public PasswordChangeResponse(string username,string password)
        {

            var dbFactory = new OrmLiteConnectionFactory(
                @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                SqliteDialect.Provider);

            var db = dbFactory.Open();

            db.CreateTable<User>();

            if (db.Exists<User>(x => x.username == username.ToLower().Replace(' ', '-')))
            {
                var x_password = Uri.UnescapeDataString(password.Replace('&', '%'));
                var _user = db.Select<User>(x => x.username == username.ToLower())[0];
                _user.key = RandomString(10);

                var body = File.ReadAllText(@"D:\home\site\wwwroot\passwordchange.html");
                body = body.Replace("USERTOCHANGE", $"{_user.username}");
                body = body.Replace("PASSWORDTOCHANGE", $"{Enrypter.Decrypt(x_password, "ifnerjf^$^jmernvjeaj")}");
                body = body.Replace("LINKTOCHANGE", $"https://foodnavigator.azurewebsites.net/password/{_user.username}/{password}/{_user.key}");
                MailSender.Default.Send("twonoobsinc@gmail.com",_user.email, "Password change", body, null);

                db.Update<User>(_user);
                alert = true;
            }      
            else
            {
                alert = false;
            }
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length-1)]).ToArray());
        }
    }
}
