using GalaSoft.MvvmLight;
using RunIt.Models.Singleton;

namespace RunIt.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                ////    // Code runs in Blend --> create design time data.
            }
            else
            {
            ////    // Code runs "for real"
            }
        }

        public string Firstname
        {
            get
            {
                return Singleton.Instance.User.firstname;
            }
        }

        public string Lastname
        {
            get
            {
                return Singleton.Instance.User.lastname;
            }
        }

        public string Email
        {
            get
            {
                return Singleton.Instance.User.email;
            }
        }

        public string ListEvents
        {
            get
            {
                return "List";
            }

        }

        public string ListRuns
        {
            get
            {
                return "run"; 
            }
        }
    }
}