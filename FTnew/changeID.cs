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
    class changeID
    {
        static public DataGridView data;

        static public int ad;
        static public string sifr;

        static public void sen ()
        {
            byte[] writeID = { dates.adress, 10, 01, 02, 00, 02, 04 };
            List<byte> merin = new List<byte>();
            ad = Convert.ToInt32(data[1, 0].Value);
            sifr = Convert.ToString(ad, 16);
            switch (sifr.Length)
            {
                case 1 :
                    sifr = "000" + sifr;

                    break;
                case 2:
                    sifr = "00" + sifr;
                    break;
                case 3:
                    sifr = "0" + sifr;
                    break;
            }
            
            var bytes = new byte[sifr.Length/2];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(sifr.Substring(i * 2, 2), NumberStyles.HexNumber);

            

            
            for(int y=0; y < writeID.Length; y++)
            {
                merin.Add(byte.Parse(writeID[y].ToString(), NumberStyles.HexNumber));
            }
            merin.Add(0);
            merin.Add(0);
            for (int k=0; k < bytes.Length; k++)
            {
                merin.Add(bytes[k]);
            }

            writeID = merin.ToArray();


            values.va(writeID);
        }

    }
}
