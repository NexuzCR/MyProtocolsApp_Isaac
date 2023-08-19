using MyProtocolsApp_Isaac.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using MyProtocolsApp_Isaac.Models;

namespace MyProtocolsApp_Isaac.ViewModels
{
    internal class UserViewModel : BaseViewModel
    {
        //el vm funciona como fuente entre el modelo y la vista
        //en sentido teorico el vm siente los cambios de las vista
        //y los pasa al modelo de forma auntomatica o viceversa
        //segun se use en uno o dos sentidos

        //tambien se puede usar (como en este caso particular
        //simplemente como indicador de procesos mas adelante se usara
        //commands y bindings en dos sentidos
        //primero en formato de funciones clasicas 

        public User MyUser { get; set; } 
        

        public UserRole MyUserRole { get; set; } 
        
        public UserDTO MyUserDTO { get; set; }  
        public UserViewModel()
        {
            MyUser = new User();
            MyUserRole = new UserRole();    
            MyUserDTO = new UserDTO();
        }

        //funciones

        //Funcion que carga los datos del objeto de usuario global
        public async Task<UserDTO> GetUserDataAsync(string PEmail)
        {
            if (IsBusy) return null;
            IsBusy = true;
            try
            {
             UserDTO userDTO = new UserDTO();
                userDTO = await MyUserDTO.GetUserInfo(PEmail);

                if (userDTO == null) return null;

                return userDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }
        }


        //funciones para validar el ingreso del usuario al app por medio del login

        public async Task<bool> UserAccessValidation(String pEmail, String pPassword)
        {
            //debemos poder controlar que no se ejecute la operacion mas de una vez
            // en este caso hay una funcion pensada para eso en baseviewmodel que
            //fue heredada al definir esta clase
            //se usara una propiedad llamada isBusy para indicar que esta en proceso de ejecucion
            //mientras su valor sea verdadero

            //control de bloqueo de funcionalidad 
            if(IsBusy) return false;
         IsBusy = true;
            
            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateUserLogin();

                return R;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }

        }

        //carga la lista de roles, que se usaran en el piker de roles en la 
        //creacion de un usuario nuevo
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            try
            {
                List<UserRole> roles = new List<UserRole>();

                roles = await MyUserRole.GetAllUserRolesAsync();

                if(roles == null)
                {
                    return null;
                }

                return roles;
                    
            }
            catch (Exception)
            {

                throw;
            }
        }

        //funcion de creacion de usuario nuevo

        public async Task<bool> AddUserAsync(string pEmail, string pPassword, string pName,string pBackupEmail,String pPhoneNumber,string pAddress, int pUserRoleID)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {

                MyUser = new User();

                MyUser.Email = pEmail;
                MyUser.Password = pPassword;
                MyUser.Name = pName;
                MyUser.BackUpEmail = pBackupEmail;
                MyUser.PhoneNumber = pPhoneNumber;
                MyUser.Address = pAddress;
                MyUser.UserRoleId = pUserRoleID;

                bool R = await MyUser.AddUserAsync();
                return R;

            }
            catch (Exception)
            {

                throw;
            }
            finally 
            { IsBusy = false; }

        }


        public async Task<bool> UpDateUser(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                MyUserDTO = pUser;
            bool R = await MyUserDTO.UPdateUserAsync();

             
                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }


    }
}
