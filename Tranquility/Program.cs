using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Tranquility.Helpers;
using Tranquility.Modules;
using Tranquility.Modules.Aimbot;
using Tranquility.Modules.Anabot;
using Tranquility.Modules.Triggerbot;
using Tranquility.Modules.Widowbot;
using static Tranquility.Helpers.PrettyLog;

namespace Tranquility
{
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // Set the title of the window.
            Console.Title = "Dropbox";

            LogInfo("Initializing settings.");
            var settingsManager = new SettingsManager();
            settingsManager.LoadSettingsFromDefaultPath();
            LogInfo("Settings loaded successfully.");

            var MouseProxy = new MouseProxy();
            MouseHelper.Proxy = MouseProxy;

            // Start the aimbot.
            var aimbot = new Aimbot();
            var triggerbot = new Triggerbot();
            var widowbot = new Widowbot();
            var anabot = new Anabot();
            var drawhelper = new DrawHelper(aimbot); // aimbot default


            LogInfo("Initialization complete. If you're lost, type help in this console.");

            new Thread(delegate ()
            {
                while (true)
                {
                    var commandArgs = Console.ReadLine()?.ToLower().Split(' ').ToList();

                    if (commandArgs == null) continue;

                    var commandRoot = commandArgs[0];
                    commandArgs.RemoveAt(0);

                    switch (commandRoot)
                    {
                        case "close": 
                            MouseProxy.Close();
                            Process.GetCurrentProcess().Close();
                            break;
                        case "aimbot":
                        case "aim":
                            aimbot.HandleCommand(commandArgs);
                            break;
                        case "anabot":
                        case "ana":
                            anabot.HandleCommand(commandArgs);
                            break;
                        case "widowbot":
                        case "widow":
                            widowbot.HandleCommand(commandArgs);
                            break;
                        case "triggerbot":
                        case "trigger":
                            triggerbot.HandleCommand(commandArgs);
                            break;
                        case "settings":
                            settingsManager.HandleCommand(commandArgs);
                            break;
                        case "help":
                            LogInfo("\nSoftware written by syscall78 and updated by Roast.\nIf you paid money for this, you've been scammed!\n\n" +
                                    "Available commands:\n" +
                                    "aimbot, aim\t\t- Send commands to aimbot\n" +
                                    "anabot, ana\t\t- Send commands to anabot\n" +
                                    "widowbot, widow\t\t- Send commands to widowbot\n" +
                                    "triggerbot, trigger\t- Send commands to triggerbot\n" +
                                    "settings\t\t- Send commands to settings manager\n" +
                                    "clear, cls\t\t- Clear the console window\n" +
                                    "help\t\t\t- Print this text again.\n");
                            break;
                        case "clear":
                        case "cls":
                            Console.Clear();
                            break;
                        default:
                            LogError($"No command matching '{commandRoot}', please enter a valid command or type 'help'.");
                            break;
                    }
                }
            }).Start();

            while (true)
            {
                // Register keypresses here.
                if (MouseHelper.GetAsyncKeyState(0x71) < 0) // Numpad1
                {
                    SettingsManager.Aimbot.ForceHeadshot = !SettingsManager.Aimbot.ForceHeadshot;
                    LogInfo($"Force headshot: { SettingsManager.Aimbot.ForceHeadshot}");

                    // MouseHelper.keybd_event(0x61, 0, 0x2, 0);
                    Thread.Sleep(200);
                }

                // toggle Fov box
                if (MouseHelper.GetAsyncKeyState(0x70) < 0) // Numpad2
                {
                    drawhelper.ToggleFov();
                    // MouseHelper.keybd_event(0x62, 0, 0x2, 0);
                    Thread.Sleep(200);
                }

                Thread.Sleep(1);
            }

           

        }
    }
}
