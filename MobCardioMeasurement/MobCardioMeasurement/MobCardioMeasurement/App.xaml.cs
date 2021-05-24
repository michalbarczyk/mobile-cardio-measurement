using MobCardioMeasurement.Pages;
using MobCardioMeasurement.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobCardioMeasurement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            NavigationService.Instance.Navigate(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
