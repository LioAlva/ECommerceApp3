using ECommerceApp3.Models;
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
    public class CustomerItemViewModel:Customer
    {
        #region Attibutes
        private NavigationService navigationService;
        #endregion

       // public ObservableCollection<Department> { get; set; }

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
            navigationService = new NavigationService();
        } 
        #endregion
    }
}
