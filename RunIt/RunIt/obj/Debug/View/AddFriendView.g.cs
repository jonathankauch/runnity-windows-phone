﻿

#pragma checksum "C:\Users\jp\Documents\Visual Studio 2015\Projects\WindowsPhone\RunIt\RunIt\View\AddFriendView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8CFDD336507919DD38226B7518598965"
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
    partial class AddFriendsView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 39 "..\..\View\AddFriendView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendInvitBtnOnClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 46 "..\..\View\AddFriendView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.backBtnOnClick;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


