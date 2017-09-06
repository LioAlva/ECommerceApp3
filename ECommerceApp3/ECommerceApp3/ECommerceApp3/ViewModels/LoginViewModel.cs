using ECommerceApp3.Services;
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
        #region Attributes
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        #endregion

        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
            IsRemembered = true;
            apiService = new ApiService();
        } 
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage("Error","Debes Ingresar un Usuario");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debes Ingresar una Contraseña");
                return;
            }
            var response = await apiService.Login(User,Password);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error",response.Message);
                return;
            }

            navigationService.SetMainPage();
        }
        #endregion

    }
}
