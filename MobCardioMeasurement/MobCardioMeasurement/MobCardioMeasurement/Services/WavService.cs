using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace MobCardioMeasurement.Services
{
    public class WavService
    {
        public int ProcessWav(string path)
        {
            //            byte[] content = File.ReadAllBytes(path);
            //
            //            byte[] bytes = { content[24], content[25], content[26], content[27] };
            //
            //            if (BitConverter.IsLittleEndian)
            //                Array.Reverse(bytes);
            //
            //            int i = BitConverter.ToInt32(bytes, 0);
            //
            //            return i;

            int bitrate;
            using (var f = File.OpenRead(path))
            {
                f.Seek(28, SeekOrigin.Begin);
                byte[] val = new byte[4];
                f.Read(val, 0, 4);
                bitrate = BitConverter.ToInt32(val, 0);
            }

            return bitrate;
        }
    }
}
