using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace FTnew
{
    class send
    {
        static public RichTextBox richT;
       
  
       static public List<byte> ded = new List<byte>();
       // static public byte[] ded= { };
        static public byte[] sed = { };
     

       static public int k = 1;
        static public int j,l = 0;
        static public string kl=null;
        static public string hex = "0x";

        static public UInt16 crc;
        static public int CRC,i;

      
        static public void sendes()
        {
            ded.Clear();
            j = 0;
            int global = 0;
         
            string[] stext = richT.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (i = 0; i < stext.Length; i++)
            {
                if (stext[i] == "CRC" || stext[i] == "crc")
                {
                    global++;
                }
             
            }
                if (global != 0)
                {
                    CRC = stext.Length;
                    CRC--;
          
                    for(int j=0; j < CRC; j++)
                {
                        try { 
                            ded.Add(byte.Parse(stext[j], NumberStyles.HexNumber));
                        }
                        catch
                        {
                        MessageBox.Show("строка имела неверный формат");
                        }
                    }

                     sed = ded.ToArray();
                try { 
                     crc16(sed, CRC);
                    }
                catch { }
                    byte[] bytes = BitConverter.GetBytes(crc);


                    for (int j = 0; j < bytes.Length; j++)
                    {
                     ded.Add(bytes[j]);
                 //    ded.Add(byte.Parse(bytes[j].ToString(), NumberStyles.HexNumber));
                    }
                    sed = null;
                    sed = ded.ToArray();
                    CRC = 0;
                }
            else
            {
                try 
                { 
                    for (int j = 0; j < stext.Length; j++)
                    {
                        ded.Add(byte.Parse(stext[j], NumberStyles.HexNumber));
                    }
                }
                catch
                {
                    MessageBox.Show("Строка имела не верный синтаксис");
                }
                global = 0;
                sed = null;
                CRC = 0;
                sed = ded.ToArray();
            }

        
        }


        public static UInt16 crc16(byte[] buf, int len)
        {
            if (buf != null)
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
            }
            return crc;
        }


      
    }
}
