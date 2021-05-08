using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileCardioMeasurement.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) 
                return;
            backingField = value;
            RaisePropertyChanged(propertyName);
        }
        
        public virtual void Prepare()
        {
            // default dummy implementation
        }
    }
    
    public abstract class BaseViewModel<TParam> : BaseViewModel where TParam : class
    {
        public virtual void Prepare(TParam param)
        {
            // default dummy implementation
        }
    }
}