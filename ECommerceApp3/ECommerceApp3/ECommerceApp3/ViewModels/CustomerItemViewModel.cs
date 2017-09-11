using ECommerceApp3.Models;
using ECommerceApp3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Media;
using ECommerceApp3.Classes;

namespace ECommerceApp3.ViewModels
{
    public class CustomerItemViewModel:Customer,INotifyPropertyChanged //esto es para que en tiempo de ejecucion la foto se de cuenta cn eentos
    {
        #region Attibutes
        private NavigationService navigationService;
        private NetService netService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private bool isRunning;
        //ECOMERCE 134
        private ImageSource imageSource;//XAMARIN FORMS
        #endregion

        //tiene que ver con el refresque de la foto
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        //propiedad para vindar separtamentos
        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }

        public ObservableCollection<CityItemViewModel> Cities { get; set; }

        public ImageSource ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get
            {
                return imageSource;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        #endregion

        #region Commands


        public ICommand NewCustomerCommand { get { return new RelayCommand(NewCustomer); } }

        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }

        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

        private async void NewCustomer()
        {
            if (!Utilities.IsValidEmail(UserName))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un Email  valido");
                return;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage("Error","Debe ingresar nombre");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar Apellidos");
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un telefono");
                return;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una direccion");
                return;
            }

            if (DepartmentId==0)
            {
                await dialogService.ShowMessage("Error", "Debe seleccionar un departamento");
                return;
            }

            if (CityId== 0)
            {
                await dialogService.ShowMessage("Error", "Debe seleccionar una ciudad");
                return;
            }



        }

        private async void TakePicture()
        {
            isRunning = true;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.ShowMessage("Error","no se puede acceder a la camara");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Photos",
                Name = "NewCustomer.jpg"
            });

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
            isRunning = false;
        }

        private async void CustomerDetail()
        {
            var customerItemViewModel = new CustomerItemViewModel
            {
                Address = Address,
                City = City,
                CityId =CityId,
                CompanyCustomers = CompanyCustomers,
                CustomerId = CustomerId,
                Department = Department,
                DepartmentId =DepartmentId,
                FirstName = FirstName,
                IsUpdated = IsUpdated,
                LastName = LastName,
                Latitude = Latitude,
                Longitude = Longitude,
                Orders = Orders,
                Phone = Phone,
                Photo = Photo,
                Sales = Sales,
                UserName =UserName
            };
            //COMO CREO  UNA INSTANCIA DE LA MAIN VIEW MODEL QUE YA ESTA INSTANCIADA

            var mainViewModel = MainViewModel.GetInstance();//obtenemos una instacia del mainview model por que a ella 
            //le tengo que establecer el usuario
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await navigationService.Navigate("CustomerDetailPage");//el servicio de navegacion pinta la pagina
        }
        #endregion

        #region Constructors
        public CustomerItemViewModel()
        {
            //services
            navigationService = new NavigationService();
            netService = new NetService();
            apiService = new ApiService();
            dataService = new DataService();
            //ecommerce 134
            dialogService = new DialogService();

            //ObservableCollection
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            Cities = new ObservableCollection<CityItemViewModel>();

            //Carga de Datos
            LoadDepartments();
            LoadCities();
        }
        #endregion

        #region Methods
        private async void LoadCities()
        {
            var cities = new List<City>();

            if (netService.IsConnected())
            {//cuando hay conneccion lo guado los productos , 
                cities = await apiService.Get<City>("Cities");
                dataService.Save<City>(cities);
            }
            else
            {//cuando no  hay connecion jalomos de la BD local
                cities = dataService.Get<City>(true);
            }
            //aca preguntamos si hay coneccion ECommerce 117

            //120 ECCOMMERCE , para recargar products
            ReloadCities(cities);
        }

        private void ReloadCities(List<City> cities)
        {
            Cities.Clear();
            foreach (var city in cities.OrderBy(d => d.Name))
            {
                Cities.Add(new CityItemViewModel
                {
                    CityId=city.CityId,
                    Customers=city.Customers,
                    Department =city.Department,
                    DepartmentId=city.DepartmentId,
                    Name=city.Name
                });
            }

        }

        private async void LoadDepartments()
        {
            var departments = new List<Department>();

            if (netService.IsConnected())
            {//cuando hay conneccion lo guado los productos , 
                departments = await apiService.Get<Department>("Departments");
                dataService.Save<Department>(departments);
            }
            else
            {//cuando no  hay connecion jalomos de la BD local
                departments = dataService.Get<Department>(true);
            }
            //aca preguntamos si hay coneccion ECommerce 117

            //120 ECCOMMERCE , para recargar products
            ReloadDepartments(departments);
        }


        private void ReloadDepartments(List<Department> departments)
        {
            Departments.Clear();
            foreach (var department in departments.OrderBy(d => d.Name))
            {
                Departments.Add(new DepartmentItemViewModel
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartmentId = department.DepartmentId,
                    Name = department.Name,
                });
            }
        }
        #endregion
    }
}
