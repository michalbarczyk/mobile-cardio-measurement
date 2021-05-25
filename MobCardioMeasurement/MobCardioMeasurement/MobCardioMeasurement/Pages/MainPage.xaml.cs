using System;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using MobCardioMeasurement.Pages.Base;
using MobCardioMeasurement.ViewModels;
using Xamarin.Forms;


namespace MobCardioMeasurement.Pages
{
    public partial class MainPage : BasePage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            Chart.Chart = new LineChart
            {
                IsAnimated = false
            };
            base.ViewModel.DataCalculated += OnDataCalculated;
        }

        private void OnDataCalculated(IEnumerable<Int16> data)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Chart.Chart.Entries = data
                    .Where((x, idx) => idx % 600 == 0) // print only some records
                    .Select(x => new ChartEntry((float)x))
                    .ToList();
            });
        }
    }
}