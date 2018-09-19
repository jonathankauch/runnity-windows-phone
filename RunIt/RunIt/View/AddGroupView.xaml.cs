﻿using System;
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
    public sealed partial class AddGroupView : Page
    {
        public AddGroupView()
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

        private async void GroupRegisterBtnOnClick(object sender, RoutedEventArgs e)
        {
            bool check = privateBox.IsChecked.Value;
            if (groupBox.Text != String.Empty)
            {
                await Api.ApiSingleton.AddGroup(groupBox.Text, descriptionBox.Text, check);
                Frame.GoBack();
                return;
            }
            Api.ApiSingleton.PopupMsg("Please fill group name box.");
        }

        private void backBtnOnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
