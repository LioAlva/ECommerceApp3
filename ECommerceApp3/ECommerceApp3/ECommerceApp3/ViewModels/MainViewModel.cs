using ECommerceApp3.Models;
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
        private ApiService apiService;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public LoginViewModel NewLogin { get; set; }
        public UserViewModel  UserLoged { get; set; }  
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //esto es para SingletoN
            instance = this;

            //Create observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductItemViewModel>();

            //Create Views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();

            //Instance services
            dataService = new DataService();

            //Load Data
            LoadMenu();
            // LoadUser();//ECommerce 113, al inicio no tengo usuario es un coletaso del logeo anterior no del de ahora y para solucionarlo 
            //para ello creo un Singleton que es una clase statica para instanciar algun metodo.
            LoadProducts();
        }

        #endregion

        //aqui soluciona el load user.
        #region Singleton

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion


        #region Methods
         public void LoadUser(User user)
        {
           // var user = dataService.GetUser();  //aca muere
            //alinicio en nuevo cell sale error porque no hay usuario y asignas user null a fullname y photofullpth
            if (user!=null)
            {
                UserLoged.FullName = user.FullName;
                //112 ahora mostramos la photo
                UserLoged.Photo = user.PhotoFullPath;
            }
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


        private async void LoadProducts()
        {
         //var products=await apiService.ge
        }
        #endregion
    }
}
