using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobCardioMeasurement.Services;
using MobileCardioMeasurement.Android.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(StorageService))]
namespace MobileCardioMeasurement.Android.Services
{
    public class StorageService : IStorageService
    {
        private const string EXTERNAL_STORAGE_APP_FOLDER = "MOB_CARDIO_MEASUREMENT";
        private const string RECORDS_FOLDER = "Records";

        public async Task<string> GetAppFolderPathAsync()
        {
            var statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            var statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (statusRead == PermissionStatus.Granted && statusWrite == PermissionStatus.Granted)
            {
                var path = Path.Combine(Environment.ExternalStorageDirectory.Path, EXTERNAL_STORAGE_APP_FOLDER, RECORDS_FOLDER);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }

            return null;
        }      
    }
}