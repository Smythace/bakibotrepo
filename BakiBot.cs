using Discord;                          //These top two "usings" are part of the discord API and are NEEDED for the bot to work.
using Discord.Commands;

using System;                           //These are just the C# commands that the program will use.
using System.Collections.Generic;       //They're pretty important, so don't delete them.
using System.Linq;                      //Then again, you never need to edit them at all.
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BakiBot
{
    class BakiBot
    {
        DiscordClient discord;          //This allows the bot to run and connect to a server or channel like a person would normally.
        CommandService commands;        //This allows you to create commands in the code below.

        public BakiBot()
        {

            discord.SetGame("Try >what"); //This is the bot "message of the day"
            string errortemplate = "Error: user {0} . ah, ah, ah! You didn't say the magic word!"; //This is for error messages when someone without admin privelages attempts to use a command they shouldn't
            Random rand = new Random(); //Allows me to randomly generate a number wherever needed
            string usb = ("E:\\BakiBot\\ConsoleApp1\\ConsoleApp1\\"); //A faster way to use the SendFile command instead of typing out the entire path
            string open = ("``` " + Environment.NewLine);  //There is a function in discord where typing ``` before and after your message
            string close = (Environment.NewLine + " ```"); //Will make it look embedded into the background of the window and look like a console
                                                           //It's only use is to make the messages aesthetically pleasing


            string adminfo = "These are commands only available to users with admin privelages" + Environment.NewLine +
                "adminfo - like the info command, but for admins" + Environment.NewLine +
                "purge - removes the past 100 messages from a channel" + Environment.NewLine +
                "botmsg - sets the MOTD for the bot";

            //This is the assigned string variable used later in the "info" and "what" commands
            string help = "These are the available commands ('>' is the prefix): " + Environment.NewLine +
            "If you have any suggestions or questions, ask <@" + 198904018058215425 + "> , or Ash on Facebook." + Environment.NewLine +
            "-------------------------------------------------------------------------------------------------" + Environment.NewLine +
            "info/what - you just did this command, im sure you know what it does." + Environment.NewLine +
            "baki - displays a random out of context bakis screenshot." + Environment.NewLine +
            "tweet - sends a random one of Connor's Tweets." + Environment.NewLine +
            "6roll - rolls a random number from 1 to 6." + Environment.NewLine +
            "jeff - kaplan" + Environment.NewLine +
            "mfw - >mfw" + Environment.NewLine +
            "lottery - tags a random user" + Environment.NewLine +
            "shrug - shrug lenny" + Environment.NewLine +
            "lenny - normal lenny" + Environment.NewLine +
            "banana - WHO???" + Environment.NewLine +
            "fireflies - all the lyrics to fireflies" + Environment.NewLine +
            "info overwatch - type for more details" + Environment.NewLine +
            "todd - Will send a photo of Sir Todd Howard lord of re-releases";

            string infoverwatch = "This set of commands will allow you to see specific information about overwatch heroes and maps." + Environment.NewLine +
                "hero [hero] abilities - tells you everything about just a hero. Their health, dps, cooldown times, etc." + Environment.NewLine +
                "hero [hero] counters - tells you what this hero counters, and what they are countered by" + Environment.NewLine +
                "map [map] - a brief overview of the map followed by locations of chokes and health packs";

            //This is a list of a bunch of tweets from Connor (@BlackOps7115). The first 30 or so were every single tweet in order.
            //I then realised that connor was actually VERY active on twitter, and then just chose another 20 randomly.

            string[] ConTweet = File.ReadAllLines(usb + "Connor_Tweets.txt");

            string[] Users =
            {
                "281248543912493057",
                "83629972928860160",
                "198904018058215425",
                "159575089719934976",
                "188058531184771074",
                "175902310394757120",
                "130408097930805249",
                "129555477225799680",
                "253235429887836160",
            };
            
            //Used for logging the bot's client into Discord
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '>';//This is the prefix that is needed to use a command in discord.
                x.AllowMentionPrefix = true;//This is allowing users to instead tag the bot if they happen to forget the prefix
            });


            commands = discord.GetService<CommandService>();//This grabs the command service from the discord API

            commands.CreateCommand("baki") //Will send a random out of context bakis groupchat screenshot.
               .Do(async (e) =>
               {
                   await e.Channel.SendFile(usb + "OOCB/_(" + rand.Next(99) + ").jpg");
               });

            commands.CreateCommand("todd") //Will send a photo of Sir Todd Howard lord of Pre-orders
               .Do(async (e) =>
               {
                   await e.Channel.SendFile(usb + "Todd_Howard\\" + rand.Next(253) + ".jpg");
               });

            commands.CreateCommand("scarce")//A poorly edited picture of scarce (edited by me)
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Scarce_had_to.jpg");
                });

            commands.CreateCommand("jeff") //Beautiful picture of jeff kaplan from the overwatch team
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "jeffkaplan.jpg");
                });

            //Anything below this comment is a command where the reply is purely text based, however some still read from files.
            commands.CreateCommand("info") //Sends a list of commands to the server
                 .Do(async (e) =>
                 {
                     await e.Channel.SendMessage(open + help + close);
                 });

            commands.CreateCommand("info overwatch") //Read the above comment, but add overwatch.
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(open + infoverwatch + close);
                });

            commands.CreateCommand("what") //Read the above comment, but remove overwatch.
                 .Do(async (e) =>
                 {
                     await e.Channel.SendMessage(open + help + close);
                 });

            commands.CreateCommand("lottery") //tags a random user
                .Do(async (e) =>
                {
                    int randomUserIndex = rand.Next(8);
                    string UserToPost = Users[rand.Next(Users.Length)];
                    await e.Channel.SendMessage("Oi <@" + UserToPost + ">");
                });

            commands.CreateCommand("tweet") //Will send a random one of Connor's tweets from the list from earlier in the program.
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(open + ConTweet[rand.Next(50)] + close);
                });

            commands.CreateCommand("banana")//Nobody knows who this person is, but they go by the name banana
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Who is <@" + 255829139179831296 + "> ?!");
                });

            commands.CreateCommand("6roll") //Sends a random number from 1 to 6
                .Do(async (e) =>
                {
                    int roll = rand.Next(1, 6);
                    await e.Channel.SendMessage(open + "You rolled a " + roll + "!" + close);
                });

            commands.CreateCommand("mfw") //Just replies back >mfw
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(open + ">mfw" + close);
                });

            commands.CreateCommand("shrug") //¯\_(ツ)_/¯
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(open + "¯\"_(ツ)_/¯" + close);
                });

            commands.CreateCommand("fireflies") //lyrics for some good stuff
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Audible_God.txt");
                });

            commands.CreateCommand("lenny") //( ͡° ͜ʖ ͡°)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(open + "( ͡° ͜ʖ ͡°)" + close);
                });

//Anything below this section is commands specific to overwatch hero abilities and counters, as well as map details.

            commands.CreateCommand("hero doomfist abilities")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Overwatch/Abilities/Doomfist.txt");
                });

            commands.CreateCommand("hero genji abilities")
                .Do(async (e) =>
               {
                   await e.Channel.SendFile(usb + "Overwatch/Abilities/Genji.txt");
               });
            commands.CreateCommand("hero mccree abilities")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Overwatch/Abilities/McCree.txt");
                });

            commands.CreateCommand("hero pharah abilities")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Overwatch/Abilities/Pharah.txt");
                });
            commands.CreateCommand("hero reaper abilities")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Overwatch/Abilities/Reaper.txt");
                });
            commands.CreateCommand("hero soldier abilities")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile(usb + "Overwatch/Abilities/Soldier.txt");
                });

            //Below this point are admin commands, only users with administrator privelages may use the commands

            commands.CreateCommand("purge") //This command will delete the past 100 messages in the channel it was used.
                     .Do(async (e) =>
                     {
                         if (e.User.ServerPermissions.Administrator == true) //Makes sure that the user who attempted to use the command has admin privelages
                         {
                             Message[] messagesToDelete;
                             messagesToDelete = await e.Channel.DownloadMessages(100);
                             await e.Channel.DeleteMessages(messagesToDelete);
                         }
                         else
                         {
                             string errorMessage = string.Format(errortemplate, e.User);
                             await e.Channel.SendMessage(errorMessage);
                         }
                     });

            commands.CreateCommand("botmsg").Parameter("message", ParameterType.Multiple) //This command will allow an admin to change what the bot is playing from within the server
                     .Do(async (e) =>
                     {
                         if (e.User.ServerPermissions.Administrator == true)
                         {
                             string botmsg = "";
                             for (int i = 0; i < e.Args.Length; i++)
                             {
                                 botmsg += e.Args[i].ToString() + " ";
                             }
                             discord.SetGame(botmsg);
                             await e.Channel.SendMessage("The bot message of the day is now " + botmsg);
                         }
                         else
                         {
                             string errorMessage = string.Format(errortemplate, e.User);
                             await e.Channel.SendMessage(errorMessage);
                         }
                     });

            commands.CreateCommand("adminfo").Parameter("message", ParameterType.Multiple) //This command will allow an admin to change what the bot is playing from within the server
                     .Do(async (e) =>
                     {
                         if (e.User.ServerPermissions.Administrator == true)
                         {
                             await e.Channel.SendMessage(open + adminfo + close);
                         }
                         else
                         {
                             string errorMessage = string.Format(errortemplate, e.User);
                             await e.Channel.SendMessage(errorMessage);
                         }
                     });

            //This is the token that is used for the bot to log in to discord.
            //DO NOT, and I seriously mean it, ABSOLUTELY DO NOT DELETE IT.
            //NOR SHOULD YOU SHARE IT WITH ANYBODY.
            //As long as you have this token, you can remake the bot over and over again.
            //If someone ELSE has the token, they can make their own code but still log in with the bot.
            //And if that someone has malicious intent, you're screwed.
            //However, If for SOME ODD REASON you decide to delete it or share it,
            //It can be retrieved on the discord site. Just search for discord applications.
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("ah, ah, ah! You didn't say the magic word!", TokenType.Bot);
            });
        }

        //Just a sub for allowing the bot to send messages.
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
