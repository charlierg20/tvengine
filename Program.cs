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
                            "/game <path> - loads a game. Beware that the program will crash if it is not formatted correctly.\n" +
                            "/end - ends the game and goes back to the prompt\n" +
                            "/quit - quit TVE\n" +
                            "/about - about and credits");
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
                    else if (cmd.StartsWith("/game "))
                    {
                        string ncmd = cmd.Replace("/game ", "");
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
                            string src = File.ReadAllText(@"" + ncmd);
                            json = JsonConvert.DeserializeObject<JObject>(src);
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
