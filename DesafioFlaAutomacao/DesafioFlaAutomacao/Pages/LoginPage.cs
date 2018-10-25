using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;

namespace DesafioFlaAutomacao.Pages
{
    public class LoginPage
    {
        #region Ações
        public static void AbrePaginaLogin()
        {
            PageBase.NavigateTo(ConfigurationManager.AppSettings["Mantis"]);
        }

        #endregion
    }
}
