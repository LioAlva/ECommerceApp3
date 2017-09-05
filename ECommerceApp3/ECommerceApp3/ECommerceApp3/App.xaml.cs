
using ECommerceApp3.Pages;
using Xamarin.Forms;

namespace ECommerceApp3
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
     

        public App()
        {
            InitializeComponent();

            MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
