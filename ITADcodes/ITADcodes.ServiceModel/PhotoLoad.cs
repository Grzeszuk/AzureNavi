using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;

namespace ITADcodes.ServiceModel
{
    [Route("/photo", "GET")]
    [Route("/photo/{username}/{url}", "GET")]
    public class PhotoLoad :IReturn<PhotoResponse>
    {
        public string username { get; set; }
        public  string url { get; set; }
    }

    public class PhotoResponse
    {
        public bool alert { get; set; }
        public PhotoResponse(string username,string url)
        {
            var dbFactory = new OrmLiteConnectionFactory(
                @"Data Source= D:\home\site\wwwroot\mydb.db;Version=3;UseUTF16Encoding=True;",
                SqliteDialect.Provider);

            var db = dbFactory.Open();

            db.CreateTable<User>();

            username = Uri.UnescapeDataString(username);

            if (db.Exists<User>(x => x.username == username.ToLower().Replace(' ', '-')))
            {
                url = Uri.UnescapeDataString(url.Replace('^', '%'));
                var _user = db.Select<User>(x => x.username == username.ToLower())[0];
                _user.photourl = url;
                db.Update<User>(_user);
                alert = true;
            }
            else
            {
                alert = false;
            }
        }
    }
}
