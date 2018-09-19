using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using RunIt.Models;
using RunIt.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunIt.ViewModel
{
    public class AuthenticationViewModel : ViewModelBase
    {
        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                if (value != _CurrentUser)
                {
                    _CurrentUser = value;
                    RaisePropertyChanged("CurrentUser");
                }
            }
        }

        public AuthenticationViewModel()
        {
//            CurrentUser = new User("denise@runit.fr", "devdev");
            CurrentUser = new User("monique.serf@yopmail.com", "devdev");
        }

        public async void Authenticate()
        {
            int ret = await Api.ApiSingleton.SignIn(_CurrentUser.email, _CurrentUser.password);
            if (ret == 0)
            {
                await Api.ApiSingleton.UserProfile();
            }
        }

        public void LogOff()
        {
        }

    }
}
