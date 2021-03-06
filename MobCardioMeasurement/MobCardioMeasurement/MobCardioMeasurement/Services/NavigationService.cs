using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using MobCardioMeasurement.Pages;
using MobCardioMeasurement.Pages.Base;
using MobCardioMeasurement.ViewModels;
using Xamarin.Forms;
using MobCardioMeasurement.ViewModels.Base;

namespace MobCardioMeasurement.Services
{
    public class NavigationService
    {
        public Task Navigate<TViewModel, TParam>(BasePage<TViewModel> page, TParam param) 
            where TViewModel : BaseViewModel<TParam>, new() 
            where TParam : class
        {
            page.ViewModel.Prepare(param);
            return PushAsync(page);
        }

        public Task Navigate<TViewModel>(BasePage<TViewModel> page) 
            where TViewModel : BaseViewModel, new()
        {
            page.ViewModel.Prepare();
            return PushAsync(page);
        }
        
        public Task Navigate<TPage, TViewModel>()
            where TPage : BasePage<TViewModel>, new()
            where TViewModel : BaseViewModel, new()
        {
            var page = new TPage();
            page.ViewModel.Prepare();
            return PushAsync(page);
        }
        
        public Task Navigate<TPage, TViewModel, TParam>(TParam param)
            where TPage : BasePage<TViewModel>, new()
            where TViewModel : BaseViewModel<TParam>, new()
            where TParam : class
        {
            var page = new TPage();
            page.ViewModel.Prepare(param);
            return PushAsync(page);
        }

        private Task PushAsync<TViewModel>(BasePage<TViewModel> page)
            where TViewModel : BaseViewModel, new()
        {
            return (Application.Current.MainPage as NavigationPage)?.PushAsync(page);
        }

        private static NavigationService _navigationService;
        public static NavigationService Instance => _navigationService ?? (_navigationService = new NavigationService());
    }
}