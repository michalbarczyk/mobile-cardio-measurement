using MobileCardioMeasurement.ViewModels;
using Xamarin.Forms;

namespace MobileCardioMeasurement.Pages.Base
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