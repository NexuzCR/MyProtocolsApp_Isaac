using MyProtocolsApp_Isaac.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyProtocolsApp_Isaac.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}