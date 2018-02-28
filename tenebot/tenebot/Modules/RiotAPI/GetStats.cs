using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using System.Threading.Tasks;
using Discord;
using tenebot.Services;

namespace tenebot.Modules.RiotAPI
{
    public class GetStats : ModuleBase<SocketCommandContext>
    {

        Region REGION = new Region();

        EmbedBuilder helpEmbed = new EmbedBuilder()
            .WithTitle(":thinking: Hmm... that region doesn't seem to exist!")
            .WithColor(Color.LightGrey)
            .AddField("Here, try these for regions:", "`EUW`  `EUNE`  `TR`  `OCE`  `NA`  `KR`  `JP`  `LAN`  `BR`  `LAS`");
        EmbedBuilder errorEmbed = new EmbedBuilder()
            .WithTitle("Uh oh!")
            .WithColor(Color.DarkRed);
        EmbedBuilder statEmbed = new EmbedBuilder();

        [Command("lolstats")]
        public async Task getStats(string region, [Remainder] string summonerName)
        { 
            switch (region.ToUpper())
            {
                case "EUW":
                    REGION = Region.EUW;
                    break;
                case "EUNE":
                    REGION = Region.EUNE;
                    break;
                case "TR":
                    REGION = Region.TR;
                    break;
                case "RU":
                    REGION = Region.RU;
                    break;
                case "OCE":
                    REGION = Region.OCE;
                    break;
                case "NA":
                    REGION = Region.NA;
                    break;
                case "KR":
                    REGION = Region.KR;
                    break;
                case "JP":
                    REGION = Region.JP;
                    break;
                case "LAN":
                    REGION = Region.LAN;
                    break;
                case "BR":
                    REGION = Region.BR;
                    break;
                case "LAS":
                    REGION = Region.LAS;
                    break;
                default:
                    await ReplyAsync("", false, helpEmbed.Build());
                    return;
            }

            Debugging.Log("RiotAPI", $"Region selected => {REGION}");

           
            

            var summoner = RIOTAPI.riotApi.Summoner.GetBySummonerName(REGION, summonerName);
            var top3masteries = RIOTAPI.riotApi.ChampionMastery.GetAllChampionMasteries(REGION, summoner.Id).Take(3);

            Debugging.Log("RiotAPI", $"{summoner.Name} - building statistics field...");


            foreach (var mastery in top3masteries)
            {
                int champid = (int)mastery.ChampionId;
                var champ = RIOTAPI.riotApi.LolStaticData.GetChampionById(REGION, mastery.ChampionId);
                statEmbed.AddInlineField(champ.Name, mastery.ChampionPoints.ToString() + $" mastery points (Level {mastery.ChampionLevel})");
                Debugging.Log("RiotAPI", $"Adding field with {champ.Name}");
            }

            statEmbed.WithTitle($"{summoner.Name}'s LoL statistics")
                .AddInlineField("Total mastery score:", RIOTAPI.riotApi.ChampionMastery.GetChampionMasteryScore(REGION, summoner.Id))
                .WithThumbnailUrl("http://ddragon.leagueoflegends.com/cdn/" + RIOTAPI.currentVersion + "/img/profileicon/" + summoner.ProfileIconId + ".png");



            await ReplyAsync("", false, statEmbed.Build());
            
        }

    }
}
