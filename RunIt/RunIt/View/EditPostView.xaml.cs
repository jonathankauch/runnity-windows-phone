using RunIt.Models;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EditPostView : Page
    {
        Post post;
        public EditPostView()
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
            post = e.Parameter as Post;
            postBox.Text = post.content;
        }

        private async void DeletePostBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.DeletePost(post.id);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
//            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
        }

        private async void EditPostBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.EditPost(postBox.Text, post.id);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.GoBack());
//            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
        }

        private void BackOnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
