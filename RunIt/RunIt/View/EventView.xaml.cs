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
    public sealed partial class EventView : Page
    {
        ListEvent le;

        public EventView()
        {
            this.InitializeComponent();
        }

        public void DisplayEvents()
        {
            if (le != null)
            {
                foreach (var ev in le.events)
                {
                    string text = ev.name +" - ";
                    if (ev.start_date != null)
                    {
                        text = text + " - " + ev.start_date;
                    }
                    if (ev.start_date != null)
                    {
                        text = text + " - " + ev.city;
                    }
                    if (ev.description != null)
                    {
                        text = text + "\n" + ev.description;
                    }
                    string priver = "public";
                    if (ev._private == true)
                    {
                        priver = "private";
                    }
                    text = text + "\n" + priver;
                    ListViewEvents.Items.Add(text);
                }
            }
        }
        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            le = await ApiSingleton.GetEvents();
            DisplayEvents();
            SetDrawerLayout();
        }

        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
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
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(MainView)));
                        break;
                    case "News":
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
                        break;
                    case "History":
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(HistoryRun)));
                        break;
                    case "Group":
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(GroupView)));
                        break;
                case "Profile":
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(ProfileView)));
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

        private async void ListViewEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (le != null)
            {
                foreach (var ev in le.events)
                {
                    string str = ListViewEvents.SelectedItem.ToString();
                    if (str.Contains(ev.name) == true)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventPostView), ev));
                    }
                }
            }
        }

        private async void AddOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(AddEventView)));
        }
    }
}
