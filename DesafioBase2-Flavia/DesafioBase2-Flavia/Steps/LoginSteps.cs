using DesafioBase2_Flavia.Pages;
using System;
using TechTalk.SpecFlow;

namespace DesafioBase2_Flavia.Steps
{
    [Binding]
    public class LoginSteps
    {
        LoginPage _loginpage = new LoginPage();

        [Given(@"que acesso a tela do Mantis")]
        public void DadoQueAcessoATelaDoMantis()
        {
            _loginpage.AbrePaginaLogin();
        }
    }
}
