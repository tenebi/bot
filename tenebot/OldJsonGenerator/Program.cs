using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonGenerator
{
    public static class OldJsonGenerator
    {
        /// <summary>
        /// Generates a json file, meant for tenebots configuration.
        /// </summary>
        /// <param name="clientId">Client id</param>
        /// <param name="botToken">Bot token</param>
        /// <param name="ownerIds">One or multiple owner ids</param>
        /// <returns>The generated json string</returns>
        public static string GenerateJson(string clientId, string botToken, List<string> ownerIds, string adminChannel)
        {
            Settings settings = new Settings
            {
                ClientId = "client_id_here",
                BotToken = "bot_token_here",
                OwnerIds = new List<string> { "owner_id", "or_multiple" },
                AdminChannel = "admin_channel_name"
            };

            if (clientId != "")
                settings.ClientId = clientId;
            if (botToken != "")
                settings.BotToken = botToken;
            if (ownerIds.Capacity != 0)
                settings.OwnerIds = ownerIds;
            if (adminChannel != "")
                settings.AdminChannel = adminChannel;

            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }
    }

    class Settings
    {
        public string ClientId { get; set; }
        public string BotToken { get; set; }
        public IList<string> OwnerIds { get; set; }
        public string AdminChannel { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a new configuration.json, leave blank for placeholder value");
            Console.Write("\nClient ID: ");
            string clientId = Console.ReadLine();
            Console.Write("\nBot token: ");
            string botToken = Console.ReadLine();

            List<string> ownerIds = new List<string>();

            Console.WriteLine("\nEnter to confirm, leave blank to end");
            int counter = 0;
            while (true)
            {
                counter++;

                Console.Write($"Owner {counter} ID: ");
                string OwnerId = Console.ReadLine();
                if (OwnerId != "")
                    ownerIds.Add(OwnerId);
                else
                    break;
            }

            Console.Write("\nAdmin channel: ");
            string adminChannel = Console.ReadLine();

            Console.WriteLine("\nGenerating empty configuration file in executable folder...");
            File.WriteAllText("configuration.json", OldJsonGenerator.GenerateJson(clientId, botToken, ownerIds, adminChannel));
            Console.WriteLine("Done, press enter to exit.");
            Console.ReadKey();
        }
    }
}
