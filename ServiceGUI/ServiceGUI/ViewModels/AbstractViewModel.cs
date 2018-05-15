using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.ViewModels
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AbstractViewModel(INotifyPropertyChanged notifyChangedObject)
        {
            notifyChangedObject.PropertyChanged += ModelPropertyChangedDelegate;
        }

        public void ModelPropertyChangedDelegate(Object sender, PropertyChangedEventArgs args)
        {
            NotifyPropertyChanged("VM_" + args.PropertyName);
        }

        public void NotifyPropertyChanged(string propName)
        {
            Console.WriteLine("propert Changed " + propName);
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + propName));
        }
    }
}
