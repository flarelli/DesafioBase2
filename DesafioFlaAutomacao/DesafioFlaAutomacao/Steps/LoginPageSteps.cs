using AutomacaoBDD.Helpers;
using DesafioBase2_Flavia.Pages;
using System;
using TechTalk.SpecFlow;

namespace DesafioBase2_Flavia.DesafioFlaAutomacao.DesafioFlaAutomacao.Steps
{
    [Binding]
    public class LoginPageSteps
    {
        [AutoInstance]
        LoginPage _loginpage;

        [Given(@"que eu acesso a tela Mantis")]
        public void DadoQueEuAcessoATelaMantis()
        {
            _loginpage.AbrePaginaLogin();
       
        ScenarioContext.Current.Pending();
        }
        
        [When(@"eu informo os usuários")]
        public void QuandoEuInformoOsUsuarios()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"abre a tela")]
        public void EntaoAbreATela()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
