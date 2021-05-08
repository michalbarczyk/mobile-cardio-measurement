using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;

namespace MobileCardioMeasurement.Services
{
	public class AudioService
	{
		private AudioRecorderService _audioRecorder;
		private AudioPlayer _audioPlayer;
		
		public AudioService()
		{
			_audioRecorder = new AudioRecorderService
			{
				StopRecordingAfterTimeout = false,
				TotalAudioTimeout = TimeSpan.FromSeconds(15),
				AudioSilenceTimeout = TimeSpan.FromSeconds(2)
			};
			_audioPlayer = new AudioPlayer();
		}
		
		public async Task StartRecording()
        {
            if (!_audioRecorder.IsRecording)
            {
	            await await _audioRecorder.StartRecording();
            }  
        }
           
        public async Task StopRecording()
        {
	        await _audioRecorder.StopRecording();
        }

        public void Play()
        {
	        var filePath = _audioRecorder.GetAudioFilePath();
	        if (filePath != null)
	        {
		        _audioPlayer.Play(filePath);
	        }
        }
	}
}