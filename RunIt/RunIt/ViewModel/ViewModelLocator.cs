/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RunIt"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using RunIt.Models;
using RunIt.View;

namespace RunIt.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static MainViewModel _main;
        private static AuthenticationViewModel _auth;
        private static PostTreeViewModel _posttree;

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}   
            SimpleIoc.Default.Register<AuthenticationViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PostTreeViewModel>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
            CleanMain();
            CleanAuth();
            CleanPostTree();
        }

        public static MainViewModel MainView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }

        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }
                return _main;
            }
        }

        public static void CleanMain()
        {
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        public static AuthenticationViewModel AuthView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthenticationViewModel>();
            }
        }

        public static void CreateAuth()
        {
            if (_auth == null)
            {
                _auth = new AuthenticationViewModel();
            }
        }

        public static AuthenticationViewModel AuthStatic
        {
            get
            {
                if (_auth == null)
                {
                    CreateAuth();
                }
                return _auth;
            }
        }

        public static void CleanAuth()
        {
            SimpleIoc.Default.Unregister<AuthenticationViewModel>();
            SimpleIoc.Default.Register<AuthenticationViewModel>();
        }

        public AuthenticationViewModel Auth
        {
            get
            {
                return AuthStatic;
            }
        }

        public static PostTreeViewModel PostTree
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PostTreeViewModel>();
            }
        }

        public static void CreatePostTree()
        {
            if (_posttree == null)
            {
                _posttree = new PostTreeViewModel();
            }
        }

        public static PostTreeViewModel PostTreeStatic
        {
            get
            {
                if (_posttree == null)
                {
                    CreatePostTree();
                }
                return _posttree;
            }
        }

        public static void CleanPostTree()
        {
            SimpleIoc.Default.Unregister<PostTreeViewModel>();
            SimpleIoc.Default.Register<PostTreeViewModel>();
        }

    }
}