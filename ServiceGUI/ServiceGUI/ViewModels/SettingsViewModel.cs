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

       public DelegateCommand<Object> RemoveHandler { get; set; }

        private string _currentlySelected;
        public String CurrentlySelected
        {
            get { return _currentlySelected; }
            set
            {
                _currentlySelected = value;
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
            //sent tcp command
            this.model.RemoveHandler(this._currentlySelected);
        }
    }
}
