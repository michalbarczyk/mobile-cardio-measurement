using MobCardioMeasurement.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using MobCardioMeasurement.ViewModels.Base;
using System.Threading.Tasks;
using Accord;
using Accord.Audio;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace MobCardioMeasurement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AudioService _audioService;
        private bool _isLoading;
        private string _path;
        private string _metadata;
        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public event Action<IEnumerable<double>> DataCalculated;
        public event Action<IEnumerable<double>> PeaksCalculated;
        public event Action<IEnumerable<double>> MovingAverageCalculated;
        public MainViewModel()
        {
            _audioService = new AudioService();

            MeasureCommand = new Command(async () =>
            {
                IsLoading = true;
                
                Path = await _audioService.RecordSample(TimeSpan.FromSeconds(10));

                var wav = new WavService(_path);

                var movAvgData = wav.Data.Where((x, i) => i % 48 == 0).Select(x => (double)x).ToArray();

                var peaksMovAvgData = movAvgData.FindPeaks();
                // debug: Metadata = $"{nameof(peaksMovAvgData)}: {peaksMovAvgData.Length}";

                const double treshold = 6450.0;

                var beats = peaksMovAvgData.Where(p => movAvgData[p] > treshold).ToArray();
                Metadata = $"BPM: {beats.Length * 6}"; // since there are 6 10sec periods in one minute

                DataCalculated?.Invoke(movAvgData);
                IsLoading = false;
            });
        }
        
        private double[] MovingAvg(double[] data, int frameSize)
        {
            return Enumerable
                .Range(0, data.Count() - frameSize)
                .Select(n => data.Skip(n).Take(frameSize).Average())
                .ToArray();
        }
        
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }
        public string Metadata
        {
            get => _metadata;
            set => SetProperty(ref _metadata, value);
        }

        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        public Command MeasureCommand { get; }

        private double[] MovAvg(double[] data, int rad)
        {
            List<double> movAvg = new List<double>();

            for (int i = rad; i < data.Length - rad; i++)
            {
                var sum = 0.0;
                for (int j = i - rad; j <= i + rad; j++)
                {
                    sum += data[j];
                }
                movAvg.Add(sum / (1.0 + 2.0 * rad));

                
                Debug.WriteLineIf(i % 10000 == 0, $"CURRENT [ {i}/{data.Length - rad} ] [ {100*i/(data.Length - rad)}% ]");
            }

            return movAvg.ToArray();
        }
    }
}