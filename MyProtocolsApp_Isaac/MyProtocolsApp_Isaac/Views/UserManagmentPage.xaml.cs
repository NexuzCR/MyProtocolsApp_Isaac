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
    public partial class UserManagmentPage : ContentPage
    {
        UserViewModel ViewModel;
        public UserManagmentPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new UserViewModel();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            TxtID.Text = GlobalObjects.MyLocalUser.IDUsuario.ToString();
            TxtEmail.Text = GlobalObjects.MyLocalUser.Correo;
            TxtName.Text = GlobalObjects.MyLocalUser.Nombre;
            TxtPhoneNumber.Text = GlobalObjects.MyLocalUser.Telefono;
            TxtBackupEmail.Text = GlobalObjects.MyLocalUser.CorreoRespaldo;
            TxtAddress.Text = GlobalObjects.MyLocalUser.Direccion;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (validateFiels())
            {
                // sacar un respaldo del usuario global tal y como esta en este momento
                // por si algo sale mal en el proceso de update, reversar los cambios
                UserDTO BackupLocaluser = new UserDTO();
                BackupLocaluser = GlobalObjects.MyLocalUser;

                try
                {
                    GlobalObjects.MyLocalUser.Nombre = TxtName.Text.Trim();
                    GlobalObjects.MyLocalUser.CorreoRespaldo = TxtBackupEmail.Text.Trim();
                    GlobalObjects.MyLocalUser.Telefono = TxtPhoneNumber.Text.Trim();
                    GlobalObjects.MyLocalUser.Direccion = TxtAddress.Text.Trim();

                    var answer = await DisplayAlert("???", "Are you sure to continue updating user info?", "Yes", "No");

                    if (answer)
                    {
                        bool R = await ViewModel.UpDateUser(GlobalObjects.MyLocalUser);
                        if (R)
                        {
                            await DisplayAlert(":)", "user update!!", " Ok");
                            await Navigation.PopAsync();

                        }
                        else
                        {
                            await DisplayAlert(":(", "something went wrong!!", " Ok");
                            await Navigation.PopAsync();
                        }
                    }
                }
                catch (Exception)
                {
                    //si algo sale mal regresamos los cambios 
                    GlobalObjects.MyLocalUser = BackupLocaluser;
                    throw;
                }
            }
        }
        private bool validateFiels()
        {
            bool R = false;
            if (TxtName != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtBackupEmail.Text != null && !string.IsNullOrEmpty(TxtBackupEmail.Text.Trim()) &&
                TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
            {
                //en este caso estab todos los datos requeridos
                R = true;
            }
            else
            {
                // si falta algun dato obligatorio
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    DisplayAlert("validation Failed!", "The Name is required", "ok");
                    TxtName.Focus();
                    return false;

                }
                if (TxtBackupEmail.Text == null || string.IsNullOrEmpty(TxtBackupEmail.Text.Trim()))
                {
                    DisplayAlert("validation Failed!", "The BackUp Email is required", "ok");
                    TxtBackupEmail.Focus();
                    return false;
                }
                if (TxtPhoneNumber.Text == null || string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    DisplayAlert("validation Failed!", "The Phone Number is required", "ok");
                    TxtPhoneNumber.Focus();
                    return false;
                }
            }
                    return R;
        }
    }
}