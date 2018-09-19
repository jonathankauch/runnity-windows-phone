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
    public sealed partial class FriendsView : Page
    {
        Friendship friendship;

        public FriendsView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            friendship = await ApiSingleton.GetFriendship();
            SetDrawerLayout();
            DisplayInvit();
            DisplayFriends();
        }

        private void DisplayInvit()
        {
            if (friendship != null)
            {
                if (friendship.requests != null)
                {
                    foreach (var f in friendship.requests)
                    {
                        ListViewPendingFriends.Items.Add(GetSelectedPendingFriend(f));
                    }
                }
            }
        }

        private void DisplayFriends()
        {
            if (friendship != null)
            {
                if (friendship.friends != null)
                {
                    System.Diagnostics.Debug.WriteLine("FRIEND");
                    foreach (var f in friendship.friends)
                    {
                        ListViewFriends.Items.Add(GetSelectedFriend(f));
                    }
                }
            }
        }

        private void ListViewPendingFriends_SelectionChanged(object sender, RoutedEventArgs e) 
        {

        }

        private void SetDrawerLayout()
        {
            DrawerLayout.InitializeDrawerLayout();
            ListMenuItems.ItemsSource = Singleton.Instance.MenuItems.ToList();
        }

        private Request GetRequestedFriend(string str)
        {
            if (friendship != null)
            {
                foreach (var f in friendship.requests)
                {
                    string spf = GetSelectedPendingFriend(f);
                    int i= spf.IndexOf("-");
                    int j= str.IndexOf("-");

                    spf = spf.Substring(i + 2);
                    str = str.Substring(j + 2);

                    try
                    {
                        int spf_id = Int32.Parse(spf);
                        int str_id = Int32.Parse(str);
                        bool compareS = spf_id.Equals(str_id);
                        if (compareS == true)
                        {
                            return f;
                        }
                    }
                    catch (FormatException e)
                    {
                        System.Diagnostics.Debug.WriteLine("Can not convert exception: " + e);
                    }

                }
            }
            return null;
        }

        private async void ListViewPendingFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected_req_friend = ListViewPendingFriends.SelectedValue as string;
            Request req;
            req = GetRequestedFriend(selected_req_friend);
            if (req == null)
            {
                System.Diagnostics.Debug.WriteLine("Could not select pending friend");
                return;
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => Frame.Navigate(typeof(AcceptView), req));
        }

        private void ListViewFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }


        private string GetSelectedPendingFriend(Request f)
        {
            return f.firstname + " " + f.lastname + " wants to add you as a friend. ";
        }

        private string GetSelectedFriend(Friends f)
        {
            return f.firstname + " " + f.lastname;
        }

        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
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
                case "Profile":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(ProfileView)));
                    break;
                case "Event":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventView)));
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

        private async void AddOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(AddFriendsView)));
        }


    }
}
