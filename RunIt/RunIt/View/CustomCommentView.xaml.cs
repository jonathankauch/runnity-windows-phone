using RunIt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CustomCommentView : Page
    {

        Comment comment;
        public CustomCommentView()
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
            comment = e.Parameter as Comment;
            commentBox.Text = comment.content;
        }

        private async void EditCommentBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.EditComment(commentBox.Text, comment.id, comment.post_id);
            Frame.GoBack();
        }

        private void BackOnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void DeleteCommentBtnOnClick(object sender, RoutedEventArgs e)
        {
            await Api.ApiSingleton.DeleteComment(comment.id);
            Frame.GoBack();
        }
    }
}
