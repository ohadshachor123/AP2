using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    public class SettingsModel : AbstractModel, ISettingsModel
    {
        private ObservableCollection<String> _handlersList;
        private string _outputDirectory;
        private string _sourceName;
        private string _logName;
        private int _thumbnailSize;

        public ObservableCollection<String> HandlersList
        {
            get{ return _handlersList;}
            set
            {
                _handlersList = value;
                NotifyPropertyChanged("ListHandlers");
            }
        }

        public String OutputDirectory
        {
            get { return _outputDirectory; }
            set { _outputDirectory = value; NotifyPropertyChanged("OutputDirectory"); }
        }

        public String SourceName
        {
            get { return _sourceName;}
            set
            {
                _sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }

        public String LogName
        {
            get { return _logName; }
            set
            {
                _logName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        public int ThumbnailSize
        {
            get { return _thumbnailSize; }
            set
            {
                _thumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }
        public SettingsModel()
        {
            ObservableCollection<String> lst = new ObservableCollection<String>();
            lst.Add("AAA");
            lst.Add("BBB");
            ThumbnailSize = 5;
            LogName = "LOG NAME BOUNDED";
            SourceName = "SOURCE NAME BOUNDED";
            OutputDirectory = "Output Dir Binded";
            HandlersList = lst;
        }

    }
}
