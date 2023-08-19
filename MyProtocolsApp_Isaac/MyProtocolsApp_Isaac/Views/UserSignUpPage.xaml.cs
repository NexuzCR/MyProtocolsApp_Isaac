using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Isaac.ViewModels;
using MyProtocolsApp_Isaac.Models;

namespace MyProtocolsApp_Isaac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSignUpPage : ContentPage

    {

        UserViewModel viewModel;
        public UserSignUpPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();
            LoadUserRolesAsync(); 
        }


        //funcion que permite la carga de los roles de los usuarios
        private async void LoadUserRolesAsync()
        {
            PkrUserRole.ItemsSource = await viewModel.GetAllUserRolesAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //capturamos el rol que hayamos seleccionado en el picker
            UserRole SelectedUserRole = PkrUserRole.SelectedItem as UserRole;
            bool R = await viewModel.AddUserAsync(TxtEmail.Text.Trim(),TxtPassword.Text.Trim(),TxtName.Text.Trim(),TxtBackupEmail.Text.Trim(),TxtPhoneNumber.Text.Trim(),TxtAddress.Text.Trim(),SelectedUserRole.UserRoleId);

            if (R)
            {
                await DisplayAlert(":)", "User Create OK!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Somenthig went wrong... ", "OK");
            }


        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}