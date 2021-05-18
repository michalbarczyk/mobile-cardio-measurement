using MobileCardioMeasurement.Android.Services;
using MobileCardioMeasurement.Services;
using Android.OS;
using System.IO;
using File = Java.IO.File;

[assembly: Xamarin.Forms.Dependency(typeof(StorageService))]
namespace MobileCardioMeasurement.Android.Services
{
    public class StorageService : IStorageService
    {
        private const string EXTERNAL_STORAGE_APP_FOLDER = "MOBILE_CARDIO_MEASUREMENT";
        private const string RECORDS_FOLDER = "Records";
        public string GetAppFolderPath()
        {
            PrepareFolder();

            return Path.Combine(Environment.ExternalStorageDirectory.Path, EXTERNAL_STORAGE_APP_FOLDER, RECORDS_FOLDER);
        }

        private void PrepareFolder()
        {
            File external = new File(Environment.ExternalStorageDirectory.Path, EXTERNAL_STORAGE_APP_FOLDER);
            if (!external.Exists())
            {
                external.Mkdirs();
            }

            File records = new File(Environment.ExternalStorageDirectory.Path, Path.Combine(EXTERNAL_STORAGE_APP_FOLDER, RECORDS_FOLDER));
            if (!records.Exists())
            {
                records.Mkdirs();
            }
        }
    }
}