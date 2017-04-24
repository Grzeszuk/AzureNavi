using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodNavigator___Controller.Data_Encrypter;

namespace ITADcodes.ServiceModel
{
    [Route("/delete", "GET")]
    [Route("/delete/{username}/{password}/{deleteString}", "GET")]
    public class Delete : IReturn<DeleteResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string deleteString { get; set; }
    }

    public class DeleteResponse
    {
        public string _deleted { get; set; }
        public bool _errors { get; set; }

        public DeleteResponse(string user,string password,string delete)
        {

            _deleted = delete;

            var dbFactory = new OrmLiteConnectionFactory(
              @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
               SqliteDialect.Provider);

            var db = dbFactory.Open();


            password = password.Replace('&', '%');
            password = Uri.UnescapeDataString(password);
            delete = delete.Replace('&', '/');

            if (db.Exists<User>(x => x.username == user.ToLower().Replace(' ', '-') && x.password== Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj")))
            {
                try
                {
                    var _user = db.Select<User>(x => x.username == user)[0];

                    var history = _user.saved.Split(',');
                    _user.saved = "";

                    foreach (var item in history)
                    {
                        if(item!=delete)
                        {
                            _user.saved += $"{item},";
                        }
                    }

                    db.Update<User>(_user);

                    _errors = false;
                }
                catch
                {
                    _errors = true;
                }
            }
            else
            {
                _errors = true;
            }
        }
    }
}
