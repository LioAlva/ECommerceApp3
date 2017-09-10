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


namespace ECommerceApp3.ViewModels
{
    public class CustomerItemViewModel:Customer
    {
        #region Attibutes
        private NavigationService navigationService;
        private NetService netService;
        private ApiService apiService;
        private DataService dataService;

        #endregion

        #region Properties
        //propiedad para vindar separtamentos
        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }

        public ObservableCollection<CityItemViewModel> Cities { get; set; }
        #endregion

        #region Commands

        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

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

            //ObservableCollection
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            Cities = new ObservableCollection<CityItemViewModel>();

            //Carga de Datos
            LoadDepartments();
        }
        #endregion

        #region Methods
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
