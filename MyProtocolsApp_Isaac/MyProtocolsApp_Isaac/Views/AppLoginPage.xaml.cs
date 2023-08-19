using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Isaac.ViewModels;
using Acr.UserDialogs;

namespace MyProtocolsApp_Isaac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {
        //se realiza el anclaje entre la vista y el vm que le da la funcionalidad
        UserViewModel viewModel;
        

        public AppLoginPage()
        {
            InitializeComponent();
            //esto vincula la vista con el vm y ademas crea la instancia del objeto
            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            { 
            TxtPassword.IsPassword =true;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //validacion del ingreso del usuario a la app

            if (TxtUserNamen.Text != null && !string.IsNullOrEmpty(TxtUserNamen.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                //si hay info en los cuadros de texto de email y pass se procede
                try
                {

                    // hacemos una animacion de espera
                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(2000);

                    string username = TxtUserNamen.Text.Trim();
                    string password = TxtPassword.Text.Trim();  

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        //si la validadcion es correcta se permite el ingreso al sistema
                        GlobalObjects.MyLocalUser = await viewModel.GetUserDataAsync(TxtUserNamen.Text.Trim());
                        //TODO: crear el objeto de usuario
                        await Navigation.PushAsync(new StartPage());
                        return;
                    }
                    else
                    {
                        //algo salio mal
                        await DisplayAlert("User Access Denied", "UserName or Password are incorrect", "OK");
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    //apagamos la animacion
                  UserDialogs.Instance.HideLoading();
                }
            }

            else
            {
                //algo salio mal
                await DisplayAlert("data required", "UserName or Password are required", "OK");
                return;
            }


        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignUpPage());
        }
    }
}