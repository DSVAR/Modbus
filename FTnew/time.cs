using FTnew.scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTnew
{
    class time
    {

        static public void timer()
        {
            byte[] writeTime = { dates.adress,10, 05, 01, 00, 02,04 };
            List<byte> Ntime = new List<byte>();
            string kap;
            int unixTime = (int)(DateTime.UtcNow.AddSeconds(10800) - new DateTime(2000, 1, 1)).TotalSeconds;
            kap = Convert.ToString(unixTime, 16);

            var bytes = new byte[kap.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(kap.Substring(i * 2, 2), NumberStyles.HexNumber);

            for (int i = 0; i < writeTime.Length; i++)
            {
                Ntime.Add(byte.Parse(writeTime[i].ToString(), NumberStyles.HexNumber));
            }
            
            for(int j=0; j< bytes.Length; j++)
            {
                Ntime.Add(bytes[j]);
            }
            writeTime = Ntime.ToArray();

            values.va(writeTime);
          
        }


    }
}
