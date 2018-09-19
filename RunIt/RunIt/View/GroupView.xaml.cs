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
    public sealed partial class GroupView : Page
    {
        ListGroup lg;

        public GroupView()
        {
            this.InitializeComponent();
        }

        private void SetDrawerLayout()
        {
            DrawerLayout.InitializeDrawerLayout();

            ListMenuItems.ItemsSource = Singleton.Instance.MenuItems.ToList();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetDrawerLayout();
            init();
        }
        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }

        public async void ListViewSelected(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = ListMenuItems.SelectedValue as string;
            switch (selecteditem)
            {
                case "Run":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(MainView)));
                    break;
                case "History":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(HistoryRun)));
                    break;
                case "Event":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventView)));
                    break;
                case "News":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
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

        private async void init()
        {
            lg = await Api.ApiSingleton.GetGroups();
            DisplayGroup();
        }

        private void ResetPageCache()
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }

        private async void AddPostBtnOnClick(object sender, RoutedEventArgs e)
        {
        }

        private async void ListViewGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lg != null)
            {
                foreach (var gr in lg.groups)
                {
                    string str = ListViewGroups.SelectedItem.ToString();
                    if (str.Contains(gr.name) == true)
                    {
                        System.Diagnostics.Debug.WriteLine("CLICK ON == " + gr.name);
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(GroupEditView), gr));
                        //                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(GroupRegister), gr));
                    }
                }
            }
        }

        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
        }


        private async void AddOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(AddGroupView)));
        }

        private void DisplayGroup()
        {
            if (lg != null)
            {
                foreach (var group in lg.groups)
                {
                    string str = group.name;

                    string priver = "public";
                    if (group.private_status== true) {
                        priver = "private"; }
                    str = str + "\n" + priver;

                    string register = "Unregistered";
                    if (group.is_register == true)
                    {
                        register = "Registered";
                    }
                    str = str + "\n" + register;

                    ListViewGroups.Items.Add(str);
                }
            }
        }

    }
}
