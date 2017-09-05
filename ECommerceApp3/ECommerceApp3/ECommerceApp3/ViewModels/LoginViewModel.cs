using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp3.ViewModels
{
    public class LoginViewModel
    {
        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public string IsRemembered { get; set; }
        #endregion
        
        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private void Login()
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
