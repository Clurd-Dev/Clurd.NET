using Newtonsoft.Json;
using Clurd.Model;
namespace Clurd.Methods
{
    public class ConfigUpdater
    {
        public bool UpdateConfig(string path, string user, string password)
        {
            var configstring = File.ReadAllText("./config.json");
            var config = JsonConvert.DeserializeObject<Config>(configstring);
            config.path = path;
            config.username = user;
            config.password = password;
            try
            {
                File.WriteAllText("./config.json", JsonConvert.SerializeObject(config));
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
