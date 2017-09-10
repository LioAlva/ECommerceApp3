using ECommerceApp3.Models;
using ECommerceApp3.Pages;
using ECommerceApp3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Services
{
    public class NavigationService
    {
        private DataService dataService;

        public NavigationService()
        {
            dataService = new DataService();
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                
                case "CustomersPage":
                    await App.Navigator.PushAsync(new CustomersPage());
                    ; break;
                case "DeliveriesPage":
                    await App.Navigator.PushAsync(new DeliveriesPage());
                    ; break;
                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    ; break;
                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    ; break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    ; break;
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    ; break;
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    ; break;
                case "CustomerDetailPage":
                    await App.Navigator.PushAsync(new CustomerDetailPage());
                    ; break;
                case "LogutPage":
                    Logout();
                    ; break;
                default:
                    break;
            }
        }

        public  User GetCurrentUser()
        {
            return App.CurrentUser;
        }

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false; //lo dejamos de recordar por que hicimos un logut ,ahora creamos un servicio que nos ayudaraupdetear uduarios
            dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new LoginPage();
        }

        internal void SetMainPage(User user)
        {
            //ya tenemos el usuario y a esta instanciado en el mainviewmodel

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoadUser(user);
            App.CurrentUser = user;//esta propiedad creo para guardar al usduario cuando doy logout y luego pueda conectarme si internet
            App.Current.MainPage = new MasterPage();    
        }
    }
}
