using MyProtocolsApp_Isaac.Services;
using MyProtocolsApp_Isaac.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocolsApp_Isaac
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

         //   DependencyService.Register<MockDataStore>();
            //definimos la forma de apilar paginas en la pantalla
            //y cual en la primera que mostremos
            MainPage = new NavigationPage(new AppLoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
