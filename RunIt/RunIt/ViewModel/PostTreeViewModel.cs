using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunIt.Common;
using RunIt.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace RunIt.ViewModel
{
    public class PostTreeViewModel : BindableBase, INotifyPropertyChanged
    {
        public RelayCommand<object> cmdTreeSelected { get; private set; }

        private int _itemId;
        private Random _rand = new Random();

        #region TreeItems 
        private ObservableCollection<PostTreeModel> _treeItems;
        public ObservableCollection<PostTreeModel> TreeItems
        {
            get { return _treeItems; }
            set { this.SetProperty(ref _treeItems, value); }
        }
        #endregion

        private string _SelectedItem = string.Empty;
        public string SelectedItem
        {
            get
            {
                return _SelectedItem;
            }

            set
            {
                if (_SelectedItem == value)
                { return; }

                _SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public PostTreeViewModel()
        {
            _itemId = 1;
            TreeItems = BuildTree(5, 5);
            cmdTreeSelected = new RelayCommand<object>(TreeViewItemSelectionChanged);
        }

        private ObservableCollection<PostTreeModel> BuildTree(int depth, int branches)
        {
            var tree = new ObservableCollection<PostTreeModel>();

            if (depth > 0)
            {
                var depthIndices = Enumerable.Range(0, branches).Shuffle();

                for (int i = 0; i < branches; i++)
                {
                    var d = depthIndices[i] % depth;
                    var b = _rand.Next(branches / 2, branches);
                    tree.Add(
                        new PostTreeModel
                        {
                            Branch = b,
                            Depth = d,
                            Text = "Item " + _itemId++,
                            Children = BuildTree(d, b)
                        });
                }
            }
            return tree;
        }

        private void TreeViewItemSelectionChanged(object itm)
        {
            if (itm != null)
            {
                if (itm is PostTreeModel)
                {
                    PostTreeModel itmObj = (PostTreeModel)itm;

                    if (itmObj.Depth == 0)
                        itmObj.Branch = 0;

                    SelectedItem = itmObj.Text.ToString() + " is in position " + itmObj.Depth + " having " + itmObj.Branch + " Branches.";
                }
            }
        }
    }
}
