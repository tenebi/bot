using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;

namespace tenebot.Modules.RiotAPI
{
    public static class RIOTAPI
    {

        /// <summary>
        /// API Key, taken from configuration.json
        /// </summary>
        public static RiotApi riotApi = RiotApi.NewInstance("RGAPI-c69e1110-5cb8-42f1-8ec9-4e9ae6015899");

        public static string currentVersion = riotApi.LolStaticData.GetVersions(Region.EUW).First();
        
        


    }
}
