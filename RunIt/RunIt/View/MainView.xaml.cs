using RunIt.Models;
using RunIt.Models.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace RunIt.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        private Geolocator locator = null;
        private CoreDispatcher dispatcher;

        private static double _longitude = 0;
        private static double _latitude = 0;

        private double _distance = 0;
        private double _time;

        private int stockage = 0;

        DispatcherTimer dispatcherTimer;
        int timesTicked = 0;

        public MainView()
        {
            this.InitializeComponent();
            xMap.MapServiceToken = "AnfD9NW7Gnkzsiqk9UcEwYcCzwxiUX_PkdOvhrPpg6og6VvcGJKSKK4JYWjovtle";
            locator = new Geolocator();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dispatcher = Window.Current.CoreWindow.Dispatcher;
            GetMyLocation();
            SetDrawerLayout();
        }

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
                    break;
                case "News":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(NewsView)));
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

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }

        private void setContentName(Button btn, string name)
        {
            btn.Content = name;
        }

        private void DrawPoint(Geoposition position)
        {
            var pushpin = new Windows.UI.Xaml.Shapes.Ellipse();
            pushpin.Fill = new SolidColorBrush(Colors.CornflowerBlue);
            pushpin.Height = 10;
            pushpin.Width = 10;
            xMap.Children.Add(pushpin);
            xMap.DesiredPitch = 0;
            MapControl.SetLocation(pushpin, position.Coordinate.Point);
            MapControl.SetNormalizedAnchorPoint(pushpin, new Point(0.5, 0.5));
        }



        private async void GetMyLocation()
        {
            ToggleProgressBar(true, "Loading");
            try
            {
                Geolocator locator = new Geolocator();
                locator.DesiredAccuracy = PositionAccuracy.High;
                _time = DateTime.Now.TimeOfDay.TotalSeconds;

                Geoposition position = await locator.GetGeopositionAsync();
                _latitude = position.Coordinate.Point.Position.Latitude;
                _longitude = position.Coordinate.Point.Position.Longitude;
                DrawPoint(position);
                await xMap.TrySetViewAsync(position.Coordinate.Point, 15, 0, 0, MapAnimationKind.Bow);

                stockage = 1;
                Singleton.Instance.longitude = _longitude;
                Singleton.Instance.latitude = _latitude;
            }
            catch (Exception ex)
            {
                MessageDialog ErrorDialog = new MessageDialog(ex.Message);
                await ErrorDialog.ShowAsync();
            }
            ToggleProgressBar(false);
        }

        private async void ToggleProgressBar(bool toggle, string message = "")
        {
            StatusBarProgressIndicator progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            if (toggle)
            {
                progressbar.Text = message;
                await progressbar.ShowAsync();
            }
            else
            {
                await progressbar.HideAsync();
            }
        }

        void xQuit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private long CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lng2 - lng1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                    + Math.Cos(ToRadians(lat1))
                    * Math.Cos(ToRadians(lat2)) * Math.Sin(dLon / 2)
                    * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            long distanceInMeters = (long)Math.Round(6371000 * c);
            return distanceInMeters;
        }

        private double SetAndCheckSpeed()
        {
            double totalDistance = 0;
            double totalTime = 0;
            totalDistance += _distance;
            totalTime += _time;
            return (totalDistance / (totalTime)) * 3.6;
        }

        async private void locator_PositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Geoposition geoPosition = e.Position;
                List<BasicGeoposition> poslist = new List<BasicGeoposition>();
                MapPolyline line = new MapPolyline();
                line.StrokeColor = Colors.CornflowerBlue;
                line.StrokeThickness = 10;
                double tmp_latitude = geoPosition.Coordinate.Point.Position.Latitude;
                double tmp_longitude = geoPosition.Coordinate.Point.Position.Longitude;
                double tmp_time = DateTime.Now.TimeOfDay.TotalSeconds;
                poslist.Add(new BasicGeoposition()
                {
                    Latitude = _latitude,
                    Longitude = _longitude
                });
                poslist.Add(new BasicGeoposition()
                {
                    Latitude = tmp_latitude,
                    Longitude = tmp_longitude
                });
                _distance += CalculateDistance(_latitude, _longitude, tmp_latitude, tmp_longitude);
                double diff = tmp_time - _time;
                Singleton.Instance.distance = (Math.Round(SetAndCheckSpeed(), 3) * 10000).ToString() + "m.";
                System.Diagnostics.Debug.WriteLine("Distance == " + Singleton.Instance.distance);
                DistanceValue.Text = Singleton.Instance.distance;
                Singleton.Instance.distance_meter = Math.Round(SetAndCheckSpeed(), 3) * 10000;
                //                xScore.Content = Math.Round(SetAndCheckSpeed(), 3) + " m/s";
                _time = tmp_time;
                _latitude = tmp_latitude;
                _longitude = tmp_longitude;
                line.Path = new Geopath(poslist);
                xMap.MapElements.Add(line);
            });
        }

        void EventRegisterBtnOnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEventView));
        }

        private void PlayBtnOnClick(object sender, RoutedEventArgs e)
        {
            DispatcherTimerSetup();
            locator = new Geolocator();
            locator.MovementThreshold = 0.0001;
            locator.PositionChanged +=
                new TypedEventHandler<Geolocator,
                    PositionChangedEventArgs>(locator_PositionChanged);
            unCollapsed();
        }
        
        private void unCollapsed()
        {
            StopButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Collapsed;
        }

        private void Collapsed()
        {
            StopButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;
        }

        void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            TimeSpan t = TimeSpan.FromSeconds(timesTicked);
            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds);
            TimeValue.Text = answer;
            timesTicked++;
        }

        private void PauseBtnOnClick(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            Collapsed();
        }

        private int StringToTime(string str)
        {
            int n = TimeSpan.Parse(str).Seconds;
            return n;
        }

        private async void StopBtnOnClick(object sender, RoutedEventArgs e)
        {
            Singleton.Instance.time = StringToTime(TimeValue.Text);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Frame.Navigate(typeof(RegisterRun)));
        }
    }
}
