using Newtonsoft.Json;
using RunIt.Models;
using RunIt.Models.Singleton;
using RunIt.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {

        public LoginView()
        {
            this.InitializeComponent();
        }

        internal void Navigate(Uri pageUri)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>

        private void CleanUIBox()
        {
            emailBox.Text = "";
            passwordBox.Password = "";
        }

        public async void ConnectBtnOnClick(object sender, RoutedEventArgs e)
        {
            int b = await Api.ApiSingleton.SignIn(emailBox.Text, passwordBox.Password);
            switch (b)
            {
                case 0: // OK
                    Frame.Navigate(typeof(MainView));
                    CleanUIBox();
                    break;
                case 1: // Wrong Login /password
                    Api.ApiSingleton.PopupMsg("The username or password is incorrect please input again.");
                    break;
                case 2: // No Internet connection.
                    Api.ApiSingleton.PopupMsg("No Internet connection.");
                    break;
                default:
                    break;
            }
        }

        void RegisterBtnOnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterView));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void passwordBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void ForgotPasswordOnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ForgotPasswordView));
        }
    }
}
