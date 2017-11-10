using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakiBot
{
    class BakiBot
    {
        DiscordClient discord;

        public BakiBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
 
            });
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '>';
                x.AllowMentionPrefix = true;
            });

            var commands = discord.GetService<CommandService>();

            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi!");
                });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzI0NTc2MzA2MzM3NTQ2MjYw.DCLsyw.cva9ixqVlQ1nLi1umLEuSlOyd4o", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
