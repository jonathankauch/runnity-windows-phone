using RunIt.Api;
using RunIt.Models;
using RunIt.Models.Singleton;
using System;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ProfileView : Page
    {
        public ProfileView()
        {
            this.InitializeComponent();
            DisplayProfile();
            SetDrawerLayout();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DisplayProfile()
        {
            User u = Singleton.Instance.User;
            xFirstName.Text = u.firstname;
            xLastName.Text = u.lastname;
            xEmail.Text = u.email;
            xPhone.Text = IsEmpty(u.phone);
            xAddress.Text = IsEmpty(u.address);
        }

        private string IsEmpty(string block)
        {
            if (String.IsNullOrEmpty(block)) {
                return "";
            }
            return block;
        }

        private void SetDrawerLayout()
        {
            DrawerLayout.InitializeDrawerLayout();

            ListMenuItems.ItemsSource = Singleton.Instance.MenuItems.ToList();
        }

        public async void ListViewSelected(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = ListMenuItems.SelectedValue as string;
            switch (selecteditem)
            {

                case "Run":
                    break;
                case "News":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
                    break;
                case "History":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(HistoryRun)));
                    break;
                case "Event":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventView)));
                    break;
                case "Group":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(GroupView)));
                    break;
                case "Friends":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(FriendsView)));
                    break;
                case "Sign out":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(LoginView))); ;
                    break;
                default:
                    break;
            }
        }

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }

        private async void SaveOnClick(object sender, RoutedEventArgs e)
        {
            await ApiSingleton.UpdateProfile(xEmail.Text, xFirstName.Text, xLastName.Text, IsEmpty(xPhone.Text), IsEmpty(xAddress.Text));
            Frame.GoBack();
        }

        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
        }
    }
}
