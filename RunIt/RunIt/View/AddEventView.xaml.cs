using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddEventView : Page
    {
        public AddEventView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async void EventRegisterBtnOnClick(object sender, RoutedEventArgs e)
        {
            bool check = privateBox.IsChecked.Value;
            if (eventBox.Text != string.Empty || descriptionBox.Text != string.Empty ||
                cityBox.Text != string.Empty)
            {
                await Api.ApiSingleton.AddEvent(eventBox.Text, descriptionBox.Text, cityBox.Text, check);
                Frame.GoBack();
                return;
            }
            Api.ApiSingleton.PopupMsg("Please fill all the boxes.");
        }

        void backBtnOnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
