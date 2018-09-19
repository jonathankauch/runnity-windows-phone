using RunIt.Api;
using RunIt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class PostCommentView : Page
    {
        Post post;
        ListComment lc;
        int _post_id;

        public PostCommentView()
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
            post = e.Parameter as Post;
            xPost.Text = "  " + post.content;
            _post_id = post.id;

            lc = await
                Api.ApiSingleton.GetComments();
            DisplayComment();
        }

        private string GetPostString(Post p)
        {
            return p.user.full_name + " - " + FirstCharUpperCase(p.created_at.ToString("MMMM dd, yyyy"))
                + "\r" + p.content;
        }

        private string FirstCharUpperCase(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private string GetComment(Comment comment)
        {
            return post.user.full_name + " - "
                            + FirstCharUpperCase(comment.created_at.ToString("MMMM dd, yyyy"))
                            + "\r" + comment.content;
        }

        private void DisplayComment()
        {
            if (lc != null)
            {
                foreach (var comment in lc.comments)
                {
                    if (post.id == comment.post_id)
                    {
                        ListViewComment.Items.Add(GetComment(comment));
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Could not find");
            }
        }


        private async void ListViewComment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lc != null)
            {
                foreach (var comment in lc.comments)
                {
                    if (GetComment(comment) == ListViewComment.SelectedItem.ToString())
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () => Frame.Navigate(typeof(CustomCommentView), comment));
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => Frame.Navigate(typeof(AddCommentView), post.id.ToString()));
        }

        private async void EditPostOnClick(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => Frame.Navigate(typeof(EditPostView), post));
        }

        private void xPost_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
