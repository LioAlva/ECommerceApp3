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
using Xamarin.Forms.Maps;

namespace ECommerceApp3.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        #region Attributes
        private DataService dataService;
        private ApiService apiService;
        private NetService netService;

        private string productsfilter;
        private string customersfilter;

        //video 119 

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }
        //esto es xamarin maps
        public ObservableCollection<Pin> Pins { get; set; }

        public LoginViewModel NewLogin { get; set; }
        public UserViewModel  UserLoged { get; set; }
        public CustomerItemViewModel CurrentCustomer { get; set; }
        

        public string ProductsFilter
        {
            set
            {
                if (productsfilter != value)
                {
                    productsfilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsFilter"));
                    if (string.IsNullOrEmpty(productsfilter)) {//video 120
                        LoadLocalProduct();//local para no hacer tan pesado
                    }
                }
            }
            get
            {
                return productsfilter;
            }
        }

        public string CustomersFilter
        {
            set
            {
                if (customersfilter != value)
                {
                    customersfilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomersFilter"));
                    if (string.IsNullOrEmpty(customersfilter))
                    {//video 120
                        LoadLocalCustomers();//local para no hacer tan pesado
                    }
                }
            }
            get
            {
                return customersfilter;
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
            //customers
            Customers = new ObservableCollection<CustomerItemViewModel>();

            //Create Views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            CurrentCustomer = new CustomerItemViewModel();
            Pins = new ObservableCollection<Pin>();

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

            LoadCustomers();
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



        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
 
        public ICommand NewCustomerCommand{ get { return new RelayCommand(NewCustomer); } }

        private void NewCustomer()
        {

        }

        public ICommand SearchProductCommand { get {return new RelayCommand(SearchProduct); } }


        private void SearchProduct()
        {
                var products = dataService.GetProducts(ProductsFilter);

            //aca preguntamos si hay coneccion ECommerce 117

            ReloadProducts(products);
        }

        public ICommand SearchCustomerCommand { get { return new RelayCommand(SearchCustomer); } }

        private void SearchCustomer()
        {
            var customers = dataService.GetCustomers(CustomersFilter);

            //aca preguntamos si hay coneccion ECommerce 117

            ReloadCustomers(customers);
        }

        #endregion

        #region Methods

        public void SetGeolocation(string name,string address,double latitude,double longitude)
        {
            var position = new Position(latitude,longitude);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = name,
                Address = address
            };
            Pins.Add(pin);
        }


        public void SetCurrentCustomer(CustomerItemViewModel customerItemViewModel)
        {
            CurrentCustomer = customerItemViewModel;
        }



        private async void LoadCustomers()
        {
            var customers = new List<Customer>();

            if (netService.IsConnected())
            {//cuando hay conneccion lo guado los productos , 
                customers = await apiService.Get<Customer>("Customers");
                dataService.Save<Customer>(customers);
            }
            else
            {//cuando no  hay connecion jalomos de la BD local
                customers = dataService.Get<Customer>(true);
            }

            //aca preguntamos si hay coneccion ECommerce 117

            //120 ECCOMMERCE , para recargar products
            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers.OrderBy(c=>c.FirstName).ThenBy(c=>c.LastName))
            {
                Customers.Add(new CustomerItemViewModel
                {
                    Address = customer.Address,
                    City = customer.City,
                    CityId = customer.CityId,
                    CompanyCustomers = customer.CompanyCustomers,
                    CustomerId = customer.CustomerId,
                    Department=customer.Department,
                    DepartmentId = customer.DepartmentId,
                    FirstName = customer.FirstName,
                    IsUpdated = customer.IsUpdated,
                    LastName = customer.LastName,
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude,
                    Orders=customer.Orders,
                    Phone = customer.Phone,
                    Photo = customer.Photo,
                    Sales = customer.Sales,
                    UserName = customer.UserName
                });
            }
        }

        public void LoadUser(User user)
        {
            // var user = dataService.GetUser();  //aca muere
            //alinicio en nuevo cell sale error porque no hay usuario y asignas user null a fullname y photofullpth
            if (user != null)
            {
                UserLoged.FullName = user.FullName;
                //112 ahora mostramos la photo
                UserLoged.Photo = user.PhotoFullPath;
            }
        }

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
                products = await apiService.Get<Product>("Products");
                dataService.Save<Product>(products);
            }
            else
            {//cuando no  hay connecion jalomos de la BD local
                products = dataService.Get<Product>(true);
            }

            //aca preguntamos si hay coneccion ECommerce 117

            //120 ECCOMMERCE , para recargar products
            ReloadProducts(products);
        }


        private void LoadLocalProduct()
        {
            var products = dataService.Get<Product>(true);
            ReloadProducts(products);
        }

        private void LoadLocalCustomers()
        {
            var customers = dataService.Get<Customer>(true);
            ReloadCustomers(customers);
        }


        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();
            foreach (var product in products.OrderBy(p=>p.Description))
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
