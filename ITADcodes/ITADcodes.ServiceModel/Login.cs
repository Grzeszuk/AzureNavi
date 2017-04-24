using FoodNavigator___Controller.Data_Encrypter;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ITADcodes.ServiceModel
{

    [Route("/login", "GET")]
    [Route("/login/{username}/{password}", "GET")]
    public class LoginUsername : IReturn<LoginResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public bool allow { get; set; }

        public string photo { get; set; }
        public LoginResponse(string usernameRequest, string password)
        {
            var dbFactory = new OrmLiteConnectionFactory(
            @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
             SqliteDialect.Provider);

            var db = dbFactory.Open();

            db.CreateTable<User>();

            usernameRequest = Uri.UnescapeDataString(usernameRequest);
            password = password.Replace('&','%');
            password = Uri.UnescapeDataString(password);
          

            if (((db.Exists<User>(x => x.username == usernameRequest.ToLower().Replace(' ', '-') && x.password== Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj") && x.active=="true"))))
            {
                allow = true;
                var _user = db.Select<User>(x => x.username == usernameRequest)[0];
                photo =_user.photourl;
            }
            else
            {
                allow = false;
                photo = "false";
            }
        }

    }
}
