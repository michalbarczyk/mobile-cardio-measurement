using MobCardioMeasurement.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using MobCardioMeasurement.ViewModels.Base;
using System.Threading.Tasks;
using Accord.Audio;

namespace MobCardioMeasurement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AudioService _audioService;
        private bool _isLoading;
        private string _path;
        private string _metadata;

        public event Action<IEnumerable<Int16>> DataCalculated; 
        public MainViewModel()
        {
            _audioService = new AudioService();

            MeasureCommand = new Command(async () =>
            {
                IsLoading = true;
                Path = await _audioService.RecordSample(TimeSpan.FromSeconds(6));
                IsLoading = false;
                
                var wav = new WavService(_path);
                
                var ecg = wav.Data;
                var max = ecg.Max();
                const int frameSize = 1000;
                var threshold = 0.15;

                var data = ecg.Select(val => (double) val).ToArray();
                var peaksAll = data.FindPeaks(); // indeksy peakow
                var size = peaksAll.Length;

                var dataMovingAvg = movingAvg(ecg, frameSize);
                var peaksMovingAvg = dataMovingAvg.FindPeaks();

                var rPeaks = peaksMovingAvg.Where(peak => dataMovingAvg[peak] > threshold).Select(peak => (short) peak).ToList();
                var rPeaksSize = rPeaks.Count;

                Metadata = $"Rate: {wav.SampleRate}\nLength: {wav.Data.Length}\nR peaks: {rPeaksSize}\n";

                DataCalculated?.Invoke(wav.Data);
            });
        }
        
        public double[] movingAvg(Int16[] data, int frameSize)
        {
            double sum = 0;
            double[] avgPeaks = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                int counter = 0;
                int index = counter;
                while (counter < frameSize)
                {
                    sum += data[index];
                    counter++;
                    index++;
                }

                avgPeaks[i] = sum / frameSize;
                sum = 0;
            }
            return avgPeaks;
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
        public Command MeasureCommand { get; }
    }
}