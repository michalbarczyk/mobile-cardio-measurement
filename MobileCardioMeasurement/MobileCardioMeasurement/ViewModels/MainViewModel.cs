using MobileCardioMeasurement.Services;
using Xamarin.Forms;

namespace MobileCardioMeasurement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AudioService _audioService;
        public MainViewModel()
        {
            _audioService = new AudioService();
            StartRecordingCommand = new Command(async () => await _audioService.StartRecording());
            StopRecordingCommand = new Command(async () => await _audioService.StopRecording());
            PlayCommand = new Command(() => _audioService.Play());
        }
        
        public Command StartRecordingCommand { get; }
        public Command StopRecordingCommand { get; }
        public Command PlayCommand { get; }
    }
}