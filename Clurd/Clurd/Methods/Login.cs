using Newtonsoft.Json;
using Clurd.Model;
namespace Clurd.Methods
{
    public class Login
    {
        public bool login(string user, string password)
        {
            var credentials = JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText("./config.json"));
            if(user == credentials.username && password == credentials.password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
