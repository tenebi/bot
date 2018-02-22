using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;

namespace tenebot.Services
{
    public class SettingsMiddleMan
    {
        public string ClientId { get; set; }
        public string BotToken { get; set; }
        public IList<string> OwnerIds { get; set; }
    }

    public static class Settings
    {
        private static string clientId;
        private static string botToken;
        private static IList<string> ownerIds;

        /// <summary>
        /// Returns the client ID as string
        /// </summary>
        public static string ClientId { get => clientId; }
        /// <summary>
        /// Returns the bot token as string
        /// </summary>
        public static string BotToken { get => botToken; }
        /// <summary>
        /// Returns a list of owner ids as strings
        /// </summary>
        public static IList<string> OwnerIds { get => ownerIds; }

        /// <summary>
        /// Loads the settings from a json file and stores it in the settings class variable
        /// </summary>
        /// <returns>Returns bool if it loaded or failed to load</returns>
        public static bool Load()
        {
            try
            {
                SettingsMiddleMan middleMan = JsonConvert.DeserializeObject<SettingsMiddleMan>("locationoffile");

                clientId = middleMan.ClientId;
                botToken = middleMan.BotToken;
                ownerIds = middleMan.OwnerIds;

                return true;
            }
            catch (Exception e)
            {
                Debugging.Log(new LogMessage(LogSeverity.Error, "Settings, Load()", $"Exception while trying to load settings from configuration", e));

                return false;
            }
        }
    }
}
