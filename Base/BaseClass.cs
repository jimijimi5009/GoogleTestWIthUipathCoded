using System;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleTestWIthUipathCoded;
using UiPath.CodedWorkflows;
using UiPath.UIAutomationNext.Activities.Design.ActivityFactory;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using UiPath.Workflow.Wizards;


namespace GoogleTestWIthUipathCoded{
    public class BaseClass : CodedWorkflow
    {

        private UiTargetApp screen;

        protected UiTargetApp LunchBrowser(string environment, string appName)
        {

            // Retrieve the browser type and URL from JSON using the keys
            string browser = JsonReader.GetSpecificValueFromProjectJson("environment.json", "browserType");
            string url = JsonReader.GetSpecificValueFromProjectJson("environment.json", $"{environment.ToUpper()}_{appName.ToUpper()}_URL");


            // Determine the browser type and executable using a dictionary
            var browserMapping = new Dictionary<string, (NBrowserType, string)>
            {
                ["chrome"] = (NBrowserType.Chrome, "chrome.exe"),
                ["edge"] = (NBrowserType.Edge, "msedge.exe"),
                ["ie"] = (NBrowserType.IE, "iexplore.exe"),
                ["firefox"] = (NBrowserType.Firefox, "firefox.exe")
            };

            if (!browserMapping.TryGetValue(browser.ToLower(), out var browserInfo))
                throw new ArgumentException("Unsupported browser type");

            // Set up the app model and options using object initializer syntax
            var appModel = new TargetAppModel().WithUrl(url, $"<html app = '{browserInfo.Item2}'/>", browserInfo.Item1);
            var taOptions = new TargetAppOptions
            {
                OpenMode = NAppOpenMode.Always,
                //IsIncognito = true,    // Turn of Incognito
                WebDriverMode = NWebDriverMode.WithGUI,
                WindowResize = NWindowResize.Maximize,
                InteractionMode = NInteractionMode.Simulate,
                AttachMode = NAppAttachMode.ByInstance,
            };

            // Open the application screen
             screen= uiAutomation.Open(appModel, taOptions);
             return screen;

        }
        
        
    
        
         public void Type(string selector,string data){
            
          screen.TypeInto(UiPath.UIAutomationNext.API.Models.Target.FromSelector(selector),data);
        }


        public void CloseBrowser(string browserName)
        {
            Process[] processes = Process.GetProcessesByName(browserName);
            if (processes.Length > 0)
            {
                Process browserProcess = processes[0];
                if (!browserProcess.HasExited)
                {
                    try
                    {
                        // Close all tabs except the first one (main window)
                        browserProcess.CloseMainWindow();
                        WaitForExitOrTimeout(browserProcess, 10000); // Wait for the process to exit gracefully with a timeout of 10 seconds

                        // Close the browser process if it's still running
                        if (!browserProcess.HasExited)
                        {
                            browserProcess.Kill();
                            WaitForExitOrTimeout(browserProcess, 10000); // Wait for the process to exit forcefully with a timeout of 10 seconds
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred while closing the browser: {ex.Message}");
                        // Log the error or handle it as per your requirements
                    }
                }
            }
            else
            {
                Console.WriteLine($"No {browserName} process found.");
            }
        }

        private void WaitForExitOrTimeout(Process process, int timeoutMilliseconds)
        {
            DateTime startTime = DateTime.Now;
            while (!process.HasExited)
            {
                if ((DateTime.Now - startTime).TotalMilliseconds >= timeoutMilliseconds)
                {
                    // Timeout reached
                    throw new TimeoutException($"Process did not exit within {timeoutMilliseconds} milliseconds.");
                }
                System.Threading.Thread.Sleep(100); // Poll every 100 milliseconds
            }
        }

    }
}