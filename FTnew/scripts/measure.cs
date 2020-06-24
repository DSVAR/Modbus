using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace FTnew.scripts
{
    class measure
    {

        static public void mes (string mea)
            {
            if (mea != null) { 
                int mess = 0;

                string[] a = mea.Split(' ');

                string justM = null;
                for (int k = 3; k < 7; k++)
                {

                
                        justM += a[k];

                }
              //  mess = Convert.ToInt32(justM);

                mess = int.Parse(justM, NumberStyles.HexNumber);

                data_.measure[1] = mess.ToString();

                values.Dop16= null;
            }

        }
    }
}
