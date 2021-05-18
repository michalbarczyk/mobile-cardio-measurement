using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MobileCardioMeasurement.Services
{
	public class AudioService
	{
		private AudioRecorderService _audioRecorder;
		private AudioPlayer _audioPlayer;
        private readonly PermissionHelper _permissionHelper;
        private readonly IStorageService _storageService;

        public AudioService()
		{
            _permissionHelper = new PermissionHelper();
            _storageService = DependencyService.Get<IStorageService>();           
			_audioPlayer = new AudioPlayer();
            _audioRecorder = null;
        }        

        public void PlaySample()
        {
	        var filePath = _audioRecorder?.GetAudioFilePath();
	        if (filePath != null)
	        {
		        _audioPlayer.Play(filePath);
	        }      
        }

        public async Task<string> RecordSample(TimeSpan sampleTimeSpan)
        {
            await _permissionHelper.EnsureHasPermission<Permissions.StorageWrite>();
          
            _audioRecorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = sampleTimeSpan,
                AudioSilenceTimeout = TimeSpan.FromSeconds(60),
                FilePath = Path.Combine(_storageService.GetAppFolderPath(), $"sample{Guid.NewGuid().ToString()}.wav")
            };

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
    }
}