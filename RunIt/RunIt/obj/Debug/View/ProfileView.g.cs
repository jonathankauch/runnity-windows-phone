﻿

#pragma checksum "C:\Users\jp\Documents\Visual Studio 2015\Projects\WindowsPhone\RunIt\RunIt\View\ProfileView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "670CF6D95F83065C7C9383D9CE9F33C0"
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
    partial class ProfileView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 149 "..\..\View\ProfileView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SaveOnClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 157 "..\..\View\ProfileView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BackOnClick;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 134 "..\..\View\ProfileView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ListViewSelected;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 24 "..\..\View\ProfileView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.DrawerIcon_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


