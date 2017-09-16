using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Services
{
    public class GelocatorService
    {
        public double Latitud { get; set; }
        public double Longitude { get; set; }
        public async Task GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            //var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var location = await locator.GetPositionAsync();
            Latitud = location.Latitude;
            Longitude = location.Longitude;
        }
    }
}
