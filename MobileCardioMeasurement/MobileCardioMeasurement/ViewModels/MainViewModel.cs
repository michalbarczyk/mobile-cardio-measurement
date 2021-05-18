using MobileCardioMeasurement.Services;
using System;
using Xamarin.Forms;
using MobileCardioMeasurement.ViewModels.Base;
using System.Threading.Tasks;

namespace MobileCardioMeasurement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AudioService _audioService;
        private bool _isLoading;
        private string _path;
        public MainViewModel()
        {
            _audioService = new AudioService();
            
            StartRecordingCommand = new Command(async () =>
            {
                IsLoading = true;
                //Path = await _audioService.RecordSample(TimeSpan.FromSeconds(10));
                await Task.Delay(TimeSpan.FromSeconds(10));
                IsLoading = false;     
            });

            StopRecordingCommand = new Command(async () => { });

            PlayCommand = new Command(() => _audioService.PlaySample());
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
        public Command StartRecordingCommand { get; }
        public Command StopRecordingCommand { get; }
        public Command PlayCommand { get; }
    }
}