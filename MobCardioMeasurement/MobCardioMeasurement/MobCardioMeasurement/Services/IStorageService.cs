using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobCardioMeasurement.Services
{
    public interface IStorageService
    {
        Task<string> GetAppFolderPathAsync();
    }
}
