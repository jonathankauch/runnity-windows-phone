﻿

#pragma checksum "C:\Users\jp\Documents\Visual Studio 2015\Projects\WindowsPhone\RunIt\RunIt\View\FriendsView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D696CFBC32B1B3EE99BD8FD9FD13D4EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RunIt.View
{
    partial class FriendsView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 85 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BackOnClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 92 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AddOnClick;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 68 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ListViewSelected;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 51 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ListViewFriends_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 34 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ListViewPendingFriends_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 24 "..\..\View\FriendsView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.DrawerIcon_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


