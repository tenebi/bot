using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonGenerator
{
    class Settings
    {
        public string ClientId { get; set; }
        public string BotToken { get; set; }
        public IList<string> OwnerIds { get; set; }
    }

    class Program
    {
        static string GenerateJson()
        {
            Settings settings = new Settings
            {
                ClientId = "client_id_here",
                BotToken = "bot_token_here",
                OwnerIds = new List<string> { "owner_id", "or_multiple" }
            };

            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }

        static void WriteToFile(string s)
        {
            File.WriteAllText("configuration.json", s);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Generating empty configuration file in executable folder...");
            WriteToFile(GenerateJson());
            Console.WriteLine("Done, press enter to exit.");
            Console.ReadKey();
        }
    }
}
