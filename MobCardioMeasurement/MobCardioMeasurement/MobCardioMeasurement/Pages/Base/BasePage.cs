using MobCardioMeasurement.ViewModels;
using Xamarin.Forms;
using MobCardioMeasurement.ViewModels.Base;

namespace MobCardioMeasurement.Pages.Base
{
    public abstract class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel, new()
    {
        public TViewModel ViewModel { get; }

        public BasePage()
        {
            BindingContext = ViewModel = new TViewModel();
        }
    }
}