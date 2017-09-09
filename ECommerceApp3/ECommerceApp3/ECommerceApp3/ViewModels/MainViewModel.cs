using ECommerceApp3.Models;
using ECommerceApp3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp3.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        #region Attributes
        private DataService dataService;
        private ApiService apiService;
        private NetService netService;
        private string filter;
        //video 119 
        
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public LoginViewModel NewLogin { get; set; }
        public UserViewModel  UserLoged { get; set; }

        public string Filter
        {
            set
            {
                if (filter != value)
                {
                    filter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filter"));
                    if (string.IsNullOrEmpty(filter)) {//video 120
                        LoadLocalProduct();//local para no hacer tan pesado
                    }
                };
            }
            get
            {
                return filter;
            }
        }

    
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

            //ecommerce 116 para productos
            apiService = new ApiService();

            //esto es para networking 117 ecommerce
            netService = new NetService();

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

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand SearchProductCommand { get {return new RelayCommand(SearchProduct); } }

        private void SearchProduct()
        {
                var products = dataService.GetProducts(Filter);

            //aca preguntamos si hay coneccion ECommerce 117

            ReloadPorducts(products);
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
            var products = new List<Product>();

            if (netService.IsConnected())
            {//cuando hay conneccion lo guado los productos , 
                products = await apiService.GetProducts();
                dataService.SaveProducts(products);
            }
            else
            {//cuando no  hay connecion jalomos de la BD local
                products = dataService.GetProducts();
            }
            
            //aca preguntamos si hay coneccion ECommerce 117

            //120 ECCOMMERCE , para recargar products
            ReloadPorducts(products);
        }

        private void LoadLocalProduct()
        {
            var products = dataService.GetProducts();
            ReloadPorducts(products);
        }

        private void ReloadPorducts(List<Product> products)
        {
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(new ProductItemViewModel
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Company = product.Company,
                    CompanyId = product.CompanyId,
                    Description = product.Description,
                    Image = product.Image,
                    Inventories = product.Inventories,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Remarks = product.Remarks,
                    Stock = product.Stock,
                    Tax = product.Tax,
                    TaxId = product.TaxId
                });
            }
        }
        #endregion
    }
}
