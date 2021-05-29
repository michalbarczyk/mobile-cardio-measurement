using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MobCardioMeasurement.Extensions;


namespace MobCardioMeasurement.Services
{
    public class WavService
    {
        private readonly byte[] _bytes;
        public WavService(string filepath) => _bytes = File.ReadAllBytes(filepath);
        
        public Int32 SampleRate => BitConverter.ToInt32(_bytes.SubArray(24, 4), 0);
        public Int16 BitsPerSample => BitConverter.ToInt16(_bytes.SubArray(34, 2), 0);
        public Int16[] Data => GetData();
        private Int16[] GetData()
        {
            const int dataStartIndex = 44;
            int length = _bytes.Length;

            Int16[] data = new Int16[(length - dataStartIndex) / 2];

            for (int index = 0; index < data.Length; index++)
            {
                data[index] = BitConverter.ToInt16(_bytes.SubArray(dataStartIndex + 2 * index, 2), 0);
            }

            return data;
        }
    }
}
