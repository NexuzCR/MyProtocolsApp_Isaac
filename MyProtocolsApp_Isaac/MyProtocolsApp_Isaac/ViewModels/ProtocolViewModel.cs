using MyProtocolsApp_Isaac.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyProtocolsApp_Isaac;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyProtocolsApp_Isaac.ViewModels
{
    public class ProtocolViewModel : BaseViewModel
    {
        public Protocol MyProtocol { get; set; }

        public ProtocolViewModel()
        {
            MyProtocol = new Protocol();      
        }

    
        //funciones

        public async Task<ObservableCollection<Protocol>> GetProtocolsAsync(int pUserID)
        {
            if (IsBusy) return null;
            IsBusy = true;
            try
            {

                ObservableCollection<Protocol> protocols = new ObservableCollection<Protocol>();
                MyProtocol.UserId = pUserID;
                protocols = await MyProtocol.GetProtocolListByUserID();

                if (protocols == null)
                {
                    return null;
                }
                return protocols;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

    }
}
