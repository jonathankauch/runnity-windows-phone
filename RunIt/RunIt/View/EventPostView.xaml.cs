using RunIt.Models;
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
    public sealed partial class EventPostView : Page
    {
        Event ev;
        ListPost lp;

        public EventPostView()
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
            ev = e.Parameter as Event;
            eventPostTxt.Text = "Event - " + ev.name;
            getListPostEvent();
        }

    private async void getListPostEvent()
        {
            lp = await Api.ApiSingleton.GetPostToEvent(ev.id);
            displayPostEvent();
        }

        private void displayPostEvent()
        {
            if (lp != null)
            {
                foreach (var post in lp.posts)
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

        private async void BackOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventView)));
        }

        private async void ListViewNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lp != null)
            {
                foreach (var post in lp.posts)
                {
                    if (GetPostString(post) == ListViewNews.SelectedItem.ToString())
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EditPostView), post));
                    }
                }
            }
        }

        private void ResetPageCache()
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }
        private async void AddPostBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.AddPostToEvent(ev.id, newPostBox.Text);
            ResetPageCache();
            Frame.Navigate(typeof(EventPostView), ev);
        }

        private async void EditOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(EventModifDeleteView), ev));
        }
    }
}
