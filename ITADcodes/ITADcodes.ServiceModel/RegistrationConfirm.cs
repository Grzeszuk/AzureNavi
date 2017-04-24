using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITADcodes.ServiceModel
{
    [Route("/registerconfirm", "GET")]
    [Route("/registerconfirm/{username}", "GET")]
    public class RegistrationConfirm
    {
        public string username { get; set; }

    }

    public class RegistrationConfirmResponse
    {
        public string alert { get; set; }
        public RegistrationConfirmResponse(string username)
        {
            var dbFactory = new OrmLiteConnectionFactory(
              @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
               SqliteDialect.Provider);

            var db = dbFactory.Open();

            db.CreateTable<User>();

            if (((db.Exists<User>(x => x.username == username.ToLower().Replace(' ', '-')))))
            {
                var _user = db.Select<User>(x => x.username == username)[0];
                _user.active = "true";
                db.Update<User>(_user);

                _user = db.Select<User>(x => x.username == username)[0];
                alert = _user.active=="true" ? $"your account is now: active" : $"your account is now: disabled";
            }
        }
    }
}
