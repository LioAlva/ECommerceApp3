using ECommerceApp3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ECommerceApp3.Services
{
    public class NetService
    {//para saber si esta conectado o no.
        public bool IsConnected()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return  networkConnection.IsConnected;
        }
    }
}
