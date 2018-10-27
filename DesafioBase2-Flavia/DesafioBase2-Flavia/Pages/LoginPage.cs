using DesafioBase2_Flavia.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBase2_Flavia.Pages
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
