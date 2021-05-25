using MobCardioMeasurement.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using MobCardioMeasurement.ViewModels.Base;
using System.Threading.Tasks;

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

                Metadata = $"Rate: {wav.SampleRate}\nLength: {wav.Data.Length}";

                DataCalculated?.Invoke(wav.Data);
            });
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