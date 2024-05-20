using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using GoogleTestWIthUipathCoded.ObjectRepository;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace GoogleTestWIthUipathCoded{
    public  class OsUtils
    {
        
        public static void KillAllProcesses(string appName)
        {
            string[] cmdString = null;

            switch (appName.ToLower())
            {
                case "chrome":
                    cmdString = new string[] { "chrome.exe", "chromedriver.exe" };
                    break;
                case "ie":
                    cmdString = new string[] { "iexplore.exe", "IEDriverServer.exe" };
                    break;
                case "edge":
                    cmdString = new string[] { "msedge.exe", "msedgedriver.exe" };
                    break;
                case "provider98":
                case "reportworx":
                case "eligible2000":
                case "smsportal":
                case "cignaportal":
                    cmdString = new string[] { "mstsc.exe" };
                    break;
                case "notepad":
                    cmdString = new string[] { "notepad.exe", "Winium.Desktop.Driver.exe" };
                    break;
                case "firefox":
                    cmdString = new string[] { "firefox.exe", "geckodriver.exe" };
                    break;
                default:
                    cmdString = new string[] { appName };
                    break;
            }

            foreach (string cmd in cmdString)
            {
                string runCMD = $"taskkill /f /im {cmd} /t";
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", $"/c {runCMD}");
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();

                    Console.WriteLine($"Closing {cmd}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        
    }
}