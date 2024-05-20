using System;
using UiPath.CodedWorkflows;

namespace GoogleTestWIthUipathCoded.Hooks{


    public partial class CodedWorkflow : CodedWorkflowBase, IBeforeAfterRun
    {
      private string browser = JsonReader.GetSpecificValueFromProjectJson("environment.json", "browserType");
        
        public void After(AfterRunContext context)
        {
            BaseClass bclass = new BaseClass();
            bclass.CloseBrowser(browser);
            
        }

        public void Before(BeforeRunContext context)
        {
            
            OsUtils.KillAllProcesses(browser);
            string relativeFilePath = context.RelativeFilePath;
            Console.WriteLine("Test Running......"+relativeFilePath);

        }     
        
    }
        
        
        
}