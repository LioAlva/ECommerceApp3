
using ECommerceApp3.Models;
using ECommerceApp3.Pages;
using ECommerceApp3.Services;
using Xamarin.Forms;

namespace ECommerceApp3
{
    public partial class App : Application
    {
        #region Atributtes
        public DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }

        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();
            dataService = new DataService();
            var user = dataService.GetUser();
            //si hay usuario y si esta recordado
            if (user!=null && user.IsRemembered)
            {
                App.CurrentUser = user;//sera para cuando vamos a desloguear saber  ue objeto se deslogueara
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
                
//            MainPage = new MasterPage();
        }
        #endregion

        #region Methods
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
        #endregion
    }
}
