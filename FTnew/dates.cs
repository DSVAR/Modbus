using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data;
using FTnew.scripts;

namespace FTnew
{
    class dates
    {
       static public byte adress = 01;

   
        static public List<byte> sentos = new List<byte>();
        
        
        static public SerialPort serial1;

        static public Thread timers, mes;

        static public byte[] time = { adress, 03, 05, 01, 00, 02 };// время
        static public byte[] measures = { adress, 03, 0x20, 08, 00, 02 };// давление


        static public bool change = false;

        static public void data()
        {

            //data_.dt.Clear();

            // dataG.Invoke((MethodInvoker)(() => dataG.Rows.Clear()));
      
         

            byte[] ToKAdress = { adress, 03, 01, 02, 00, 02 };//получить адрес
            byte[] typeD = { adress, 03, 01, 00, 00, 02 }; // Get type device
            byte[] ToGetVers = { adress, 03, 01, 04, 00, 04 };// Получить версию
          
                                                         // byte[] changeID = { adress, 10, 01, 02, 00, 02, 04, 00, 00, 00, };
            byte[] tempura = { adress, 03, 20, 04, 00, 02 };// температура
            byte[] Battery = { adress, 03, 20, 03, 00, 01 };//battery
            byte[] measures = { adress, 03, 0x20, 08, 00, 02 };// давление
            if (serial1.IsOpen)
            {

                values.va(ToKAdress);
                ChAd(values.DopByte);

              

                values.va(typeD);
                type(values.Dop16);
                                
                values.va(ToGetVers);
                versionPP(values.Dop16);

                while (true) { 
                values.va(time);
                Timing(values.Dop16);

                values.va(measures);
                measure.mes(values.Dop16);
               

                Thread.Sleep(10);
                }
            }
        }

        static public void ChAd(string adresing)
        {
           
            int korA = 0;

            string[] a =adresing.Split(' ');

            string justN = null;    
            for(int k = 3; k<7;k++)
            {
                
                    justN += a[k];
               
            }
            korA += Convert.ToInt32(justN);

            data_.sn[1] = korA.ToString();

            values.DopByte = null;
        }

        static public void type(string type)
        {
            string amaru = null;

            string[] types = type.Split(' ');
            for(int i=3; i < 7; i++)
            {
                amaru += types[i];
            }
            data_.VS[1] = "0x" + amaru.ToString();

            //  dataG.Invoke((MethodInvoker)(() => dataG.Rows.Add("Тип прибора","0x"+ amaru)));
            values.Dop16 = null;
            values.doop = false;
        }

        static public void versionPP(string vs)
        {
            string vers = null;
            string[] amor = vs.Split(' ');
            int one = 0, two = 0, third = 0, four = 0 ;

            one += Convert.ToInt32(amor[3]+amor[4]);
            two += Convert.ToInt32(amor[5] + amor[6]);
            third += Convert.ToInt32(amor[7] + amor[8]);
            four += Convert.ToInt32(amor[9] + amor[10]);

            vers = one + "." + two + "." + third + "." + four + ".";

            data_.VSP[1] = vers.ToString();

            // dataG.Invoke((MethodInvoker)(() => dataG.Rows.Add("Версия железа", vers)));
            values.VT = false;
            values.Dop16 = null;

        }


        static public void Timing(string akbar)
        {
            if(akbar!= null) { 
                string[] opr = akbar.Split(' ');
          
                string getTime = null;
          
               int convertTime = 0;
                for(int y=3; y < 7; y++)
                {
                getTime += opr[y];
                }
                convertTime = int.Parse(getTime, NumberStyles.HexNumber);


                  DateTime date = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(convertTime).ToLocalTime();

                data_.timos[1] = date.ToString();

                //dataG.Invoke((MethodInvoker)(()=>  dataG.Rows.Add("", date) ));
                values.timer = false;
                values.Dop16= null;
            }
        }


     


    }
}
