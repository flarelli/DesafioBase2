using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DesafioBase2_Flavia.Utilitarios
{
    [Binding]
    public class TestBase
    {
        
        [BeforeScenario]
        public void Initialize()
        {
            DriverFactory.InitializeDriver(ConfigurationManager.AppSettings["BROWSER"]);           
        }
    }
}
