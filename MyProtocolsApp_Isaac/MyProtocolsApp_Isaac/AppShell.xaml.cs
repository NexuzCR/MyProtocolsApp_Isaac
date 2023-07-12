using MyProtocolsApp_Isaac.ViewModels;
using MyProtocolsApp_Isaac.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyProtocolsApp_Isaac
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
