using System.Collections.Generic;
using System.Configuration;
using TechTalk.SpecFlow;
using System.Reflection;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using NUnit.Framework;
using System.IO;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.MarkupUtils;

namespace AutomacaoBDD.Helpers
{
    [Binding]
    public class TestBase
    {
        private static List<string> listaErrosCenarios = new List<string>();
        private static int qntCenario = 0;
        private static int qntCenarioSucesso = 0;
        private static double percentagemTestes = 0;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string TestType = Utilitarios.GetFrameworkTestType();



        private static List<string> listaSteps = new List<string>();
        private static List<string> ListaPrint = new List<string>();

        public static object SendEmail { get; private set; }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var dateTime = DateTime.Now.ToString("dd-MM-yyyy");

            var reportPath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Report");

            if (!Directory.Exists(reportPath + "\\" + dateTime))
            { Directory.CreateDirectory(reportPath + "\\" + dateTime); }


            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);


            if (!File.Exists(reportPath + "\\" + dateTime + "\\" + "Report " + tempo + ".html"))
            {
                var htmlReporter = new ExtentHtmlReporter(reportPath + "\\" + dateTime + "\\" + "Report " + tempo + ".html");
                htmlReporter.LoadConfig(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "extent-config.xml"));

                extent = new ExtentReports();

                //string css = htmlReporter.Configuration().CSS;
                extent.AttachReporter(htmlReporter);
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
            for (int i = 0; i <= ScenarioContext.Current.Count; i++)
            {
                if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "OK")
                    qntCenarioSucesso = qntCenarioSucesso + 1;
            }

        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //se excluir este método, habilitar a linha de criação do featurename no método Initialize
            if (scenario == null)
                featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }
        [AfterStep]
        public void InsertReportinSteps()
        {
            if (!listaSteps.Contains(ScenarioStepContext.Current.StepInfo.Text))
            {
                listaSteps.Add(ScenarioStepContext.Current.StepInfo.Text);

                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
                MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
                object TestResult = getter.Invoke(ScenarioContext.Current, null);

                var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                var status = ScenarioContext.Current.ScenarioExecutionStatus.ToString();
                var stepPending = TestResult.ToString();


                if (status == "OK")
                {
                    if (stepType == "Given")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "When")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "Then")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "And")
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());

                }
                else if (status == "TestError")
                {
                    var erroException = ScenarioContext.Current.TestError.InnerException;

                    if (erroException == null)
                    {
                        if (stepType == "Given")
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "When")
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "Then")
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                    }
                    else
                    {
                        if (stepType == "Given")
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "When")
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "Then")
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                    }
                }

                if (stepPending == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "When")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "Then")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }

                status = null;
                stepPending = null;

            }
            else listaSteps.Clear();

        }

        [BeforeScenario]
        public void Initialize()
        {

            if (DriverFactory.INSTANCE == null)
            {
                DriverFactory.InitializeDriver(ConfigurationManager.AppSettings["BROWSER"]);
                scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
                qntCenario = qntCenario + 1;
            }
        }

        [AfterScenario]
        public void Close()
        {
            DriverFactory.CloseDriver();
        }
    }
}
