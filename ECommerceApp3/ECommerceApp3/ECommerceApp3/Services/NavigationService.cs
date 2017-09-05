using ECommerceApp3.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Services
{
    public class NavigationService
    {
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
                default:
                    break;
            }
        }
    }
}
