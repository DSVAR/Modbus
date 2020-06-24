using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTnew.scripts
{
    class values
    {

        static public UInt16 crc;
        static public List<byte> sentos = new List<byte>();


        static public SerialPort serial1;

        static public RichTextBox rich2;

        static public byte[] sendAd;

     static public  List<byte> get = new List<byte>();
     static public List<int> gets = new List<int>();


        static public string DopByte, DopVT = null;
        static public string Dop16 = null;
        static public bool doop, VT, timer = false;
        static public string otherB = null;


        static public int l = 0;
        static public int kls = 0;
        static public string p = null;
        static public string value = null;

        static public Thread awake = new Thread(show.pr);
        static public void va(byte[] kos)
        {
            if (serial1.IsOpen)
            {

           

                string sent = null;
                for (int i = 0; i < kos.Length; i++)
                    sentos.Add(kos[i]);


                crc16(kos, kos.Length);
                sendAd = BitConverter.GetBytes(crc);

                for (int j = 0; j < sendAd.Length; j++)
                    sentos.Add(sendAd[j]);

                sendAd = null;
                send.sed = sentos.ToArray();

                for (int k = 0; k < send.sed.Length; k++)
                {
                    if (send.sed[k] < 10)
                        sent += "0" + Convert.ToString(send.sed[k], 16) + " ";
                    else
                        sent += Convert.ToString(send.sed[k], 16) + " ";
                }
                sent = sent.ToUpper();

                texs(rich2, Environment.NewLine, Color.Blue);
                texs(rich2, "Послали:" + send.sed.Length + " Байт", Color.Blue);
                texs(rich2, Environment.NewLine, Color.Blue);
                texs(rich2, sent, Color.Blue);
                texs(rich2, Environment.NewLine, Color.Blue);

                Thread.Sleep(Form1.delay);
                serial1.Write(send.sed, 0, send.sed.Length);
                sentos.Clear();
                print();

            }
            else
            {
                MessageBox.Show("Настройте и откройте порт");
            }

            
        }



        static public void print()
        {
            //   being:


           int l = 0;
           int kls = 0;
           string p = null;
           string value = null;

            get.Clear();
            gets.Clear();
            otherB = null;

            if (serial1.IsOpen)
            {
                while (kls == 0)
                {
                    Thread.Sleep(45);
                    kls = serial1.BytesToRead;
                }


                for (int t = 0; t < kls; t++)
                {
                    gets.Add(serial1.ReadByte());


                    if (l != send.sed.Length)
                    {
                        if (gets[t] < 10)
                        {
                            value += "0" + Convert.ToString(gets[t], 16) + " ";
                        }
                        else
                        {
                            value += Convert.ToString(gets[t], 16) + " ";

                        }
                        l++;
                        value = value.ToUpper();

                    }
                    else
                    {
                        if (gets[t] < 10)
                        {
                            otherB += "0" + Convert.ToString(gets[t], 16) + " ";
                            p += Convert.ToString(gets[t], 10) + " ";
                        }
                        else
                        {
                            otherB += Convert.ToString(gets[t], 16) + " ";
                            p += Convert.ToString(gets[t], 10) + " ";
                        }
                    }

                                    }


                if (otherB != null)
                { 
                DopByte = p;
                otherB = otherB.ToUpper();
                Dop16 = otherB;
                }
                //      
                //show.pr();
                awake.Start();
            }

//
          
            //    goto being;
          

        }
     

        public static UInt16 crc16(byte[] buf, int len)
        {
            crc = 0xFFFF;
            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];
                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        static public void texs(RichTextBox box, string text, Color color)
        {
            try
            {
                if (text != null)
                {
                    box.Invoke((MethodInvoker)(() => box.SelectionStart = box.TextLength));
                    box.Invoke((MethodInvoker)(() => box.SelectionLength = 0));
                    box.Invoke((MethodInvoker)(() => box.SelectionColor = color));
                    box.Invoke((MethodInvoker)(() => box.AppendText(text)));
                    box.Invoke((MethodInvoker)(() => box.SelectionColor = box.ForeColor));
                    box.Invoke((MethodInvoker)(() => box.ScrollToCaret()));
                }
            }
            catch { }
        }
    }
}
