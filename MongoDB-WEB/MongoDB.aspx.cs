using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
//此外，你将频繁的用到下面这些 using 语句中的一条或多条:
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using Model;
using BLL;

namespace MongoDB_WEB
{
    public partial class MongoDB : System.Web.UI.Page
    {

        MongoServer server;
        MongoDatabase db;
        public string html = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //string connection = "mongodb://localhost:27017";
            //_server = MongoServer.Create(connection);
            //_database = _server.GetDatabase("test");

            //var connectionString = "mongodb://localhost:27017";
            //MongoClient client = new MongoClient(connectionString);
            //server = client.GetServer();
            //db = server.GetDatabase("mydb");
            if (!IsPostBack)
            {
                GetData();
            }
        }

        public void GetData()
        {
            html = "";
            //var collection = db.GetCollection<User>("entities");
            //List<User> list = collection.FindAllAs<User>().ToList();

            BLL.LoginBLL bll = new LoginBLL();
            List<Model.Login> list = bll.GetAll();
            if (list == null) return;
            foreach (Model.Login user in list)
            {
                html += "<tr>";
                html += "<td>ID:" + user.Id + "/   </td>";
                html += "<td>Name:" + user.LoginName + "/       </td>";
                html += "<td>Password:" + user.Password + "/     </td>";
                html += "<td>status:" + user.Status + "/     </td>";
                html += "<td>RegTime:" + user.RegTime + "/     </td>";
                html += "</tr>";
            }
        }

        protected void seve_Click(object sender, EventArgs e)
        {
            Model.Login model = new Model.Login()
            {
                LoginName = TXT_NAME.Text,
                Password = TXT_PAS.Text,
                Status = 1,
                RegTime = DateTime.Now.ToString()
            };

            BLL.LoginBLL bll = new LoginBLL();
            bll.Add(model);
            GetData();
            //var collection = db.GetCollection<User>("entities");

            //User entity = new User { Name = TXT_NAME.Text, Sex = TXT_SEX.Text };
            //collection.Insert(entity);

            //GetData();
            //var id = entity.Id;

            //var query = Query<User>.EQ(u => u.Id, id);
            //entity = collection.FindOne(query);

            //entity.Name = "Dick";
            //collection.Save(entity);

            //var update = Update<User>.Set(u => u.Name, "Harry");
            //collection.Update(query, update);

            //collection.Remove(query);

            //var list = db.GetCollection<User>("entities");

        }

    }

    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}