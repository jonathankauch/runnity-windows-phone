using Newtonsoft.Json;
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
using Windows.Web.Http;
using Windows.Web.Http.Filters;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class NewsView : Page
    {
        ListPost ld;

        public NewsView()
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
            SetDrawerLayout();
            ld = await 
                Api.ApiSingleton.GetPost();
            DisplayPost();
        }

        /*
        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
        }
        */

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
                case "History":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(HistoryRun)));
                    break;
                case "Event":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventView)));
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

        public void DisplayPost()
        {
            if (ld != null)
            {
                foreach (var post in ld.posts)
                {
                    ListViewNews.Items.Add(GetPostString(post));
                }
            }
        }

        private string FirstCharUpperCase(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private string GetPostString(Post p)
        {
            return p.user.full_name + " - " + FirstCharUpperCase(p.created_at.ToString("MMMM dd, yyyy"))
                + "\r" + p.content;
        }

        private async void ListViewNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ld != null)
            {
                foreach (var post in ld.posts)
                {
                    if (GetPostString(post) == ListViewNews.SelectedItem.ToString())
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(PostCommentView), post));
                    }
                }
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(AddPostView)));
        }

        private void ResetPageCache()
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }

        private async void AddPostBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.AddPost(newPostBox.Text);
            ResetPageCache();
            Frame.Navigate(typeof(NewsView));
        }


    }
}
