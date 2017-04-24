using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodNavigator___Controller.Data_Encrypter;
using ServiceStack;
using ServiceStack.OrmLite;

namespace ITADcodes.ServiceModel
{
    [Route("/password", "GET")]
    [Route("/password/{username}/{password}/{key}", "GET")]
    public class PasswordChanger : IReturn<PasswordChangerResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string key { get; set; }
    }

    public class PasswordChangerResponse
    {
        public string alert { get; set;}
        public PasswordChangerResponse(string username, string password,string key)
        {
            var dbFactory = new OrmLiteConnectionFactory(
                @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                SqliteDialect.Provider);

            var db = dbFactory.Open();
            db.CreateTable<User>();


            if (!db.Exists<User>(x => x.username == username)) return;
            {
                var _user = db.Select<User>(x => x.username == username.ToLower())[0];

                if (_user.key == key) return;
                _user.password = Enrypter.Decrypt(Uri.UnescapeDataString(password.Replace('&', '%')),
                    "ifnerjf^$^jmernvjeaj");
                db.Update<User>(_user);
                alert = "Your password was changed";
            }
        }
    }
}
