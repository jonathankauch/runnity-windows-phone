using RunIt.Api;
using RunIt.Models;
using RunIt.Models.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class RegisterRun : Page
    {
        public RegisterRun()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Cel paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CongratsMessage.Text = "You have run " + Singleton.Instance.time + "s in " + Singleton.Instance.distance; 
        }

        private async void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            List<Coordinate> lco = new List<Coordinate>();

            Coordinate begin_co = new Coordinate();
            begin_co.latitude = Singleton.Instance.begin_latitude;
            begin_co.longitude = 2.35; //Singleton.Instance.begin_longitude;

            Coordinate co = new Coordinate();
            co.latitude = Singleton.Instance.latitude;
            co.longitude= Singleton.Instance.longitude;
            lco.Add(begin_co);
            lco.Add(co);

            DateTime dt = DateTime.Now;
            co.timestamp = DateTime.Now;
            await ApiSingleton.AddRun(Singleton.Instance.distance_meter, lco);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
        }

        private async void backBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
        }
    }
}
