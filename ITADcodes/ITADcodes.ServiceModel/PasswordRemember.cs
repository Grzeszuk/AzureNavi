using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodNavigator___Controller.Data_Encrypter;
using ServiceStack;
using ServiceStack.OrmLite;
using Simplify.Mail;

namespace ITADcodes.ServiceModel
{
    [Route("/passwordr", "GET")]
    [Route("/passwordr/{email}", "GET")]
    public class PasswordRemember : IReturn<PasswordRememberResponse>
    {
        public string email { get; set; }
    }

    public class PasswordRememberResponse
    {
        public PasswordRememberResponse(string email)
        {
            var dbFactory = new OrmLiteConnectionFactory(
                @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                SqliteDialect.Provider);

            var db = dbFactory.Open();

            if (!db.Exists<User>(x => x.email == email)) return;
            {
                var _user = db.Select<User>(x => x.email==email)[0];

                var body = File.ReadAllText(@"D:\home\site\wwwroot\passwordremember.html");
                body = body.Replace("USERTOCHANGE", $"{_user.username}");
                body = body.Replace("PASSWORDTOCHANGE", $"{_user.password}");

                MailSender.Default.Send("twonoobsinc@gmail.com", email, "Password recorvery", body, null);
            }
        }
    }
}
