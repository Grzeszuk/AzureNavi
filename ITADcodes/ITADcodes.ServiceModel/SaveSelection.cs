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
    [Route("/save", "GET")]
    [Route("/save/{username}/{password}/{location}/{type}/{radius}/{transport}/{foodindex}/{typeindex}", "GET")]
    public class SaveSelection : IReturn<SaveResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string radius { get; set;}
        public string transport { get; set; }       
        public int foodindex { get; set; }
        public int typeindex { get; set; }
    }

    public class SaveResponse
    {
        public bool _saved { get; set; }
        public SaveResponse(string user,string password,string location,string type,string radius,string transport,int foodindex,int typeindex)
        {

            password = password.Replace('&', '%');
            password = Uri.UnescapeDataString(password);
            user = Uri.UnescapeDataString(user);

            var dbFactory = new OrmLiteConnectionFactory(
                 @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                  SqliteDialect.Provider);

            var db = dbFactory.Open();

            if(db.Exists<User>(x=>x.username==user.ToLower() && x.password==Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj")))
            {
                try
                {
                    var _user = db.Select<User>(x => x.username == user.ToLower())[0];
                    _user.saved += $"{location.Replace('&','/')}-{type}-{radius}-{transport}-{foodindex}-{typeindex},";
                    db.Update<User>(_user);
                    _saved = true;
                }
                catch
                {
                    _saved = false;
                }
            }
            else
            {
                _saved = false;
            }
        }
    }
}
