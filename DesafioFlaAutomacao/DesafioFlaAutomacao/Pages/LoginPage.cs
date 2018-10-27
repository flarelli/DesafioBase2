﻿using AutomacaoBDD.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;


namespace DesafioFlaAutomacao.Pages
{
    public class LoginPage : PageBase
    {
        #region Ações
        public void AbrePaginaLogin()
        {
            NavigateTo(ConfigurationManager.AppSettings["Mantis"]);
        }

        #endregion
    }
}
