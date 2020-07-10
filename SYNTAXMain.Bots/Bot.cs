using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using SYNTAXMain.Bots.Commands;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public Bot(IServiceProvider services)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<Configjson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            
            //Client.GuildAvailable += OnFindingTheGuild;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                DmHelp = false,
                Services = services
            };

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(3)
            });

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<BasicCommands>();
            Commands.RegisterCommands<TvpCommands>();
            Commands.RegisterCommands<BotmasterCommands>();
            Commands.RegisterCommands<ProfileCommands>();

            Client.ConnectAsync();
        }


        //private async Task OnFindingTheGuild(GuildCreateEventArgs e)
        //{
        //    var _startupchannel = e.Guild.Channels.Values.First(x => x.Id == 725160401150410822);
        //    await _startupchannel.SendMessageAsync("<@&714561011616841849> help me!").ConfigureAwait(false);
        //}

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

    }
}
