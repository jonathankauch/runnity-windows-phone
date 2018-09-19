using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class PostTreeModel : BindableBase
    {
        #region Text
        private string _text;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { this.SetProperty(ref _text, value); }
        }
        #endregion

        #region Depth
        private int _depth;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public int Depth
        {
            get { return _depth; }
            set { this.SetProperty(ref _depth, value); }
        }
        #endregion

        #region Branch
        private int _branch;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public int Branch
        {
            get { return _branch; }
            set { this.SetProperty(ref _branch, value); }
        }
        #endregion

        #region Children
        private ObservableCollection<PostTreeModel> _children = new ObservableCollection<PostTreeModel>();
        /// <summary>
        /// Gets or sets the child items.
        /// </summary>
        public ObservableCollection<PostTreeModel> Children
        {
            get { return _children; }
            set { this.SetProperty(ref _children, value); }
        }
        #endregion
    }
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
