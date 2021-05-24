using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MobCardioMeasurement.Services
{
	public class AudioService
	{
        private readonly IStorageService _storageService;
        private  AudioRecorderService _audioRecorder;

        public AudioService()
		{
            _storageService = DependencyService.Get<IStorageService>();
        }

        public async Task<string> RecordSample(TimeSpan sampleTimeSpan)
        {
            if (_audioRecorder is null)
            {
                await InitializeAsync();
            }

            _audioRecorder.TotalAudioTimeout = sampleTimeSpan;

            await StartRecording();  
           
            return _audioRecorder?.GetAudioFilePath();
        }

        private async Task StartRecording()
        {
            if (!_audioRecorder.IsRecording)
            {
                await await _audioRecorder.StartRecording();
            }
        }

        private async Task InitializeAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();

            if (status == PermissionStatus.Granted)
            {
                _audioRecorder = new AudioRecorderService
                {
                    StopRecordingAfterTimeout = true,
                    TotalAudioTimeout = TimeSpan.FromSeconds(10),
                    AudioSilenceTimeout = TimeSpan.FromSeconds(60),
                    FilePath = Path.Combine(await _storageService.GetAppFolderPathAsync(), $"sample{Guid.NewGuid()}.wav")
                };
            }
            else
            {
                throw new Exception($"{nameof(InitializeAsync)}: unable to access microphone");
            }
        }
    }
}