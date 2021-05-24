using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobCardioMeasurement.Services
{
    public class PermissionHelper
    {
        
        public PermissionHelper()
        {
            
        }

        public async Task<bool> EnsureHasPermission<T>() where T : Permissions.BasePermission, new()
        {
            
            if (await Permissions.CheckStatusAsync<T>() == PermissionStatus.Granted)
            {
                
                return true;
            }


            MainThread.BeginInvokeOnMainThread(() => Permissions.RequestAsync<T>());

            var result = await Permissions.CheckStatusAsync<T>() == PermissionStatus.Granted;
            
            return result;
        }
    }
}
