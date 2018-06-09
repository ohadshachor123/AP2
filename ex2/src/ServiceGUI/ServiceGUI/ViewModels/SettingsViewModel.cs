using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
using Prism.Commands;
namespace ServiceGUI.ViewModels
{
    class SettingsViewModel : AbstractViewModel
    {
        // merely dry settings:
        private ISettingsModel model;
        public ObservableCollection<String> VM_HandlersList
        {
            get { return this.model.HandlersList; }
            set {this.model.HandlersList = value;}
        }

        public String VM_OutputDirectory
        {
            get { return this.model.OutputDirectory;}
            set {this.model.OutputDirectory = value;}
        }

        public String VM_SourceName
        {
            get { return this.model.SourceName; }
            set { this.model.SourceName = value; }
        }
        public String VM_LogName
        {
            get { return this.model.LogName; }
            set { this.model.LogName = value; }
        }

        public int VM_ThumbnailSize
        {
            get { return this.model.ThumbnailSize; }
            set { this.model.ThumbnailSize = value; }
        }

        // This property determines whether the remove button is enabled
        // and which function to call when it is clicked.
       public DelegateCommand<Object> RemoveHandler { get; set; }

        // string that hold the selection from the list of handlers.
        private string _currentlySelected;
        public String CurrentlySelected
        {
            get { return _currentlySelected; }
            set
            {

                _currentlySelected = value;
                // When a selection has been made, we need to change the property(enables the button/disables)
                var command = this.RemoveHandler as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
                NotifyPropertyChanged("CurrentlySelected");
            }
        }
        public SettingsViewModel(ISettingsModel model) : base(model)
        {
            this.model = model;
            _currentlySelected = null;
            this.RemoveHandler = new DelegateCommand<object>(Remove, CanRemove);
        }

        private bool CanRemove(object obj)
        {
            if (this._currentlySelected != null)
                return true;
            return false;
        }
        private void Remove(object obj)
        {
            // sends to the server the removal request.
            this.model.RemoveHandler(this._currentlySelected);
        }
    }
}
