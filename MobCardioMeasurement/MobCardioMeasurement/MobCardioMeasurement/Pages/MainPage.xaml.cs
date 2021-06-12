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
            ChartRawSignal.Chart = new LineChart
            {
                IsAnimated = false
            };
//            MovingAverage.Chart = new LineChart
//            {
//                IsAnimated = false
//            };
//            Peaks.Chart = new PointChart
//            {
//                IsAnimated = false
//            };
            base.ViewModel.DataCalculated += OnDataCalculated;
            base.ViewModel.MovingAverageCalculated += OnMovingAverageCalculated;
            base.ViewModel.PeaksCalculated += OnPeaksCalculated;
        }

        private void OnDataCalculated(IEnumerable<double> data)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ChartRawSignal.Chart.Entries = data
                    .Where((x, idx) => idx % 20 == 0) // print only some records
                    .Select(x => new ChartEntry((float)x))
                    .ToList();
            });
        }

        private void OnMovingAverageCalculated(IEnumerable<double> movAvg)
        {
//            Device.BeginInvokeOnMainThread(() =>
//            {
//                MovingAverage.Chart.Entries = movAvg
//                    .Where((x, idx) => idx % 600 == 0) // print only some records
//                    .Select(x => new ChartEntry((float)x))
//                    .ToList();
//            });
        }

        private void OnPeaksCalculated(IEnumerable<double> peaks)
        {
//            Device.BeginInvokeOnMainThread(() =>
//            {
//                Peaks.Chart.Entries = peaks
//                    //.Where((x, idx) => idx % 600 == 0) // print only some records
//                    .Select(x => new ChartEntry((float)x))
//                    .ToList();
//            });
        }
    }
}