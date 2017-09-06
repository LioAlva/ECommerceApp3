using ECommerceApp3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private DataService dataService;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public LoginViewModel NewLogin { get; set; }
        public UserViewModel  UserLoged { get; set; }  
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            dataService = new DataService();
            LoadMenu();
            LoadUser();
        }
        #endregion

        #region Methods
        private void LoadUser()
        {
            var user = dataService.GetUser();
            UserLoged.FullName = user.FullName;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel {
                Icon= "ic_action_product.png",
                PageName="ProductsPage",
                Title="Productos"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "CustomersPage",
                Title = "Clientes"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_order.png",
                PageName = "OrdersPage",
                Title = "Pedidos"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_delivery.png",
                PageName = "DeliveriesPage",
                Title = "Entregas"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_sync.png",
                PageName = "SyncPage",
                Title = "Sincronizar"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_setup.png",
                PageName = "SetupPage",
                Title = "Configuracion"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_logut.png",
                PageName = "LogutPage",
                Title = "Cerrar Sesión"
            });
        } 
        #endregion
    }
}
