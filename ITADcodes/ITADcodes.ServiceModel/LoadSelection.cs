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
    [Route("/load", "GET")]
    [Route("/load/{username}/{password}", "GET")]
    public class LoadSelection : IReturn<LoadResponse>
    {
        public string username { get; set; }
        public  string password { get; set; }
    }

    public class LoadResponse
    {
        public List<Selection> selection { get; set; }
        public bool Errors { get; set; }
        public LoadResponse(string user,string password)
        {

            password = password.Replace('&', '%');
            password = Uri.UnescapeDataString(password);

            var dbFactory = new OrmLiteConnectionFactory(
               @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                SqliteDialect.Provider);

            var db = dbFactory.Open();
            selection = new List<Selection>();

            if (db.Exists<User>(x => x.username == user.ToLower().Replace(' ', '-') && x.password== Enrypter.Decrypt(password, "ifnerjf^$^jmernvjeaj")))
            {
               
                    var _user = db.Select<User>(x => x.username == user.ToLower())[0];

                try
                {
                    foreach (var item in _user.saved.Split(','))
                    {
                        if (item.Length <= 5) continue;
                        var tempSelection = item.Split('-');

                        if (tempSelection.Count() == 6)
                        {
                            selection.Add(new Selection(tempSelection[0], tempSelection[1], Convert.ToInt32(tempSelection[2]), tempSelection[3], Convert.ToInt32(tempSelection[4]), Convert.ToInt32(tempSelection[5])));
                        }
                        else if (tempSelection.Count() == 7)
                        {
                            selection.Add(new Selection($"{tempSelection[0]}-{tempSelection[1]}", tempSelection[2], Convert.ToInt32(tempSelection[3]), tempSelection[4], Convert.ToInt32(tempSelection[5]), Convert.ToInt32(tempSelection[6])));
                        }
                    }
                }
                catch
                {
                    Errors = true;
                }
                   
            }
            else
            {
                Errors = true;
            }
        }
    }
}
