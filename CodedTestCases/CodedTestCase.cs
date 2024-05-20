using GoogleTestWIthUipathCoded;
using GoogleTestWIthUipathCoded.Base;
using UiPath.CodedWorkflows;
using UiPath.UIAutomationNext.API.Models;

namespace GoogleTestWIthUipathCoded.CodedTestCases{
    public class CodedTestCase : BaseClass
    {
        private UiTargetApp screen;

            
        [TestCase]
        public void Execute()
        {
            // Arrange
            Log("Test run started for CodedTestCase.");
            
            LunchBrowser("stage","google");
            Type("<webctrl id='APjFqb' tag='TEXTAREA' />","Hello World");
           
            
        }
    }
}