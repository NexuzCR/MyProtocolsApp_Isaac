using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Isaac.ViewModels;

namespace MyProtocolsApp_Isaac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProtocolListPage : ContentPage
    {
        ProtocolViewModel ProtocolViewModel;

        public ProtocolListPage()
        {
            InitializeComponent();

            BindingContext = ProtocolViewModel = new ProtocolViewModel();

            LoadProtocolList();
        }



        private async void LoadProtocolList()
        {
            LvList.ItemsSource = await ProtocolViewModel.GetProtocolsAsync(GlobalObjects.MyLocalUser.IDUsuario);

        }
    }
}