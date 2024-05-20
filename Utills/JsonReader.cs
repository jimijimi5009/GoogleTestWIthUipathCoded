using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
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
    public class JsonReader
    {
     
        
        public static string GetSpecificValueFromProjectJson(string jsonFileName, string propertyName)
        {
            try
            {
                // string filePath = Path.Combine(Environment, "TestData", jsonFileName);


                string baseDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(baseDirectory, "Environment", jsonFileName);
                
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"JSON file not found: {filePath}");
                }

                string jsonContent = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(jsonContent);

                JToken value = jsonObject[propertyName];
                if (value != null)
                {
                    return value.ToString();
                }
                else
                {
                    throw new Exception($"Property '{propertyName}' not found in JSON.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading or processing JSON file: {e.Message}", e);
            }
        }
        
        
        
    }
}