using ECommerceApp3.Models;
using ECommerceApp3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp3.ViewModels
{
    public class LoginViewModel:INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        //ECOMMERCE 118
        private NetService netService;
        public bool isRunning;//creamos estapropiedad junto con el evento
        public DataService dataService;
        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        public bool IsRunning //servira para el activity indicator 
        {
            set
            {
                if (isRunning!=value) {
                    isRunning = value;
                    /* if (PropertyChanged!=null) {
                         PropertyChanged(this, new PropertyChangedEventArgs("IsRunning"));
                     }*///es lo mismo que lo de abajo
                    PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            dataService = new DataService();
            netService = new NetService();

            IsRemembered = true;
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

            IsRunning = true;
            var response = new Response();//video 118 para logear ssin conexion.
            if (netService.IsConnected())
            {
                response = await apiService.Login(User, Password);
            }
            else
            {
                response = dataService.Login(User, Password);
            }
           
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error",response.Message);
                return;
            }
            var user = (User)response.Result;
            user.IsRemembered = IsRemembered;
            user.Password = Password;

            dataService.InsertUser(user);

            navigationService.SetMainPage(user);
        }
        #endregion

    }
}
