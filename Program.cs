using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace TVEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Default";
            string currentLabel = "start";
            string currentGame = "SPECIAL:NOGAME";
            bool running = true;
            bool gameRunning = false;

            JObject json = JsonConvert.DeserializeObject<JObject>("");

            Console.WriteLine("TVEngine has booted. /help for more info.");

            void AskForCommand()
            {
                if (gameRunning && json["labels"][currentLabel]["action"]?.ToString() == "ASK")
                {
                    Console.WriteLine(json["labels"][currentLabel]["message"]?.ToString().Replace("{name}", name));
                }
                else if (gameRunning && json["labels"][currentLabel]["action"]?.ToString() == "END")
                {
                    gameRunning = false;
                    currentLabel = "start";
                }

                string cmd = Console.ReadLine();

                if (cmd.StartsWith("/"))
                {
                    if (cmd.StartsWith("/name"))
                    {
                        Console.WriteLine("So you want to set your name? Just type it and hit enter.");
                        name = Console.ReadLine();
                        Console.WriteLine("That's a beautiful name, " + name + "!");
                    }
                    else if (cmd.StartsWith("/help"))
                    {
                        Console.WriteLine("The following system commands are available: \n" +
                            "/help - shows all commands\n" +
                            "/name - set the special variable \"name\"\n" +
                            "/game - loads a game into memory.\n" +
                            "/end - ends the game and goes back to the prompt\n" +
                            "/quit - quit TVE\n" +
                            "/about - about and credits\n" +
                            "/save - save game\n" +
                            "/load - load game - only load for the game in which the save point was created");
                    }
                    else if (cmd.StartsWith("/quit"))
                    {
                        running = false;
                    }
                    else if (cmd.StartsWith("/end"))
                    {
                        gameRunning = false;
                        currentLabel = "start";
                    }
                    else if (cmd.StartsWith("/about"))
                    {
                        Console.WriteLine("TVEngine, a project by CRG Media Group.\n" +
                            "TVEngine, a program by CRG Media Group // CharlieRG. Running v0.1.0, public beta.\n" +
                            "This program is under the GNU 3.0 license. See more at the GitHub repository.\n" +
                            "TVE also uses JSON.NET, which is under the MIT license and made by NewtonSoft.");
                    }
                    else if (cmd.StartsWith("/game"))
                    {
                        Console.WriteLine("Enter path.");
                        string ncmd = Console.ReadLine();
                        if (!File.Exists(@"" + ncmd))
                        {
                            Console.WriteLine("File not found.");
                        }
                        else if (Path.GetExtension(@"" + ncmd) != ".tves")
                        {
                            Console.WriteLine("Not a TVEngine file.");
                        }
                        else
                        {
                            gameRunning = true;
                            currentGame = ncmd;
                            string src = File.ReadAllText(@"" + ncmd);
                            json = JsonConvert.DeserializeObject<JObject>(src);
                        }
                    }
                    else if (cmd.StartsWith("/save"))
                    {
                        if (currentGame == "SPECIAL:NOGAME")
                        {
                            Console.WriteLine("No game is running, therefore no game can be saved.");
                        }
                        else
                        {
                            Console.WriteLine("Enter save path (folder to save in).");
                            string npath = Console.ReadLine();

                            string toText = "{ \"save\": { \"point\" : \"" + currentLabel + "\" } }";

                            File.WriteAllText(npath, toText);

                            Console.WriteLine("Finished.");
                        }
                    }
                    else if (cmd.StartsWith("/load"))
                    {
                        if (currentGame == "SPECIAL:NOGAME")
                        {
                            Console.WriteLine("No game is running, therefore no game can be saved.");
                        }
                        else
                        {
                            Console.WriteLine("Enter path.");
                            string lcmd = Console.ReadLine();
                            if (!File.Exists(@"" + lcmd))
                            {
                                Console.WriteLine("File not found.");
                            }
                            else
                            {
                                JObject fJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(@"" + lcmd));
                                currentLabel = fJson["save"]["point"]?.ToString();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("System Command Unknown.");
                    }
                }
                else
                {
                    if (gameRunning)
                    {
                        if (json["labels"][currentLabel]["responses"][cmd] != null)
                        {
                            string toExecute = json["labels"][currentLabel]["responses"][cmd]?.ToString();
                            currentLabel = toExecute;
                        }
                        else
                        {
                            Console.WriteLine("That is not a valid option.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("A game is not running. To run system commands, use /.");
                    }

                }
            }
            while (running)
            {
                AskForCommand();
            }
        }
    }
}
