using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Globalization;
using FTnew.scripts;

namespace FTnew
{
    public partial class Form1 : Form
    {




        static public   string otherB = null;
        static public string name, speed, text, readT = null;
        static  public int delay = 0;
        static public Thread printing;

   
      
        public Form1()
        {
            InitializeComponent();
            // доступ к форме
            send.richT = richTextBox1;

            dates.serial1 = serialPort1;
           
            data_.kol1 = richTextBox1;

            values.serial1 = serialPort1;
            values.rich2 = richTextBox2;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] port = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(port);

            name = comboBox1.Text = comboBox1.Items[0].ToString();

            speed = comboBox2.Text = comboBox2.Items[6].ToString();

            delay = Convert.ToInt32(textBox1.Text);
            printing = new Thread(print);
         


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            name = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                  
                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.BaudRate = Convert.ToInt32(speed);
                        serialPort1.PortName = name;
                        serialPort1.Open();
                    }

                if (button2.Text == "Открыть порт")
                {

                    //printing = new Thread(print);
                    //printing.Start();
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    textBox1.Enabled = false;
                    button2.Text = "Закрыть порт";
                }
                else
                {
                    printing.Abort();
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    textBox1.Enabled = true;
                    button2.Text = "Открыть порт";
                }
            }
            catch
            {
                MessageBox.Show("Порт уже используется, выберите другой порт");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            speed = comboBox2.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (printing.IsAlive==true) { 
            printing.Abort();
            serialPort1.Close();
            }

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            delay = Convert.ToInt32(textBox1.Text);
        }

        private void смотретьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

           

            data_ dat = new data_();
            dat.Name = "mdi";
            if (!Application.OpenForms.Cast<Form>().Any(f => f.Name == "mdi"))
            {/*запуск окна*/
                dat.Show();
            }
           
            dat.BringToFront();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            string kl = null;
            try { 
                if (!serialPort1.IsOpen)
                {
               
                        serialPort1.BaudRate = Convert.ToInt32(speed);
                        serialPort1.PortName = name;
                        serialPort1.Open();
                
              

                }
            }
            catch { MessageBox.Show(name + "Доступ закрыт"); }
              
                send.sendes();
            for(int j=0; j < send.sed.Length; j++)
            {
                if (send.sed[j] < 10)
                {
                    kl += "0" + Convert.ToString(send.sed[j], 16) + " ";
                }
                else
                {
                    kl += Convert.ToString(send.sed[j], 16) + " ";
                }

               
            }
            if(kl!=null)
            kl=kl.ToUpper();
            texs(richTextBox2, Environment.NewLine, Color.Blue);
            texs(richTextBox2,"отправка",Color.Blue);
            texs(richTextBox2, Environment.NewLine, Color.Blue);
            texs(richTextBox2, kl, Color.Green);
            texs(richTextBox2, Environment.NewLine, Color.Blue);

            if (serialPort1.IsOpen)
            serialPort1.Write(send.sed, 0, send.sed.Length);

            print();

        }


        void print()
        {
     //   being:
            int l = 0;
            int kls=0;
            string p = null;
            string value = null;
            otherB = null;
            List<byte> get = new List<byte>();
            List<int> gets = new List<int>();

     
      
            if (serialPort1.IsOpen)
            {
            
              while (kls==0)
              {
                    Thread.Sleep(delay);
                    if (serialPort1.IsOpen)
                        kls = serialPort1.BytesToRead;
                    else
                        MessageBox.Show("Доступ к порту закрыт");
                   
              }

          
                    for(int t=0; t < kls; t++)
                    {
                      gets.Add(serialPort1.ReadByte());


                         if (l != send.sed.Length) 
                          { 
                            if (gets[t] < 10) 
                            {
                                value +="0"+ Convert.ToString(gets[t], 16) + " ";
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
              


                texs(richTextBox2, Environment.NewLine, Color.Blue);
                texs(richTextBox2, "Пришло: " + kls.ToString() + " байт", Color.Blue);
                texs(richTextBox2, Environment.NewLine, Color.Blue);
                texs(richTextBox2, value, Color.Green);
                texs(richTextBox2, Environment.NewLine, Color.Blue);


                if (otherB != null)
                {
               
                  

                    texs(richTextBox2, Environment.NewLine, Color.Blue);
                    kls = kls - send.sed.Length;
                    texs(richTextBox2, Environment.NewLine, Color.Blue);
                    texs(richTextBox2, "Пришло: " +kls.ToString() + " байт", Color.Lime);
                    texs(richTextBox2, Environment.NewLine, Color.Blue);
                    texs(richTextBox2, otherB, Color.Purple);
                    texs(richTextBox2, Environment.NewLine, Color.Blue);

                    //  MessageBox.Show(p.ToString());
                }
              
            }
          
           
            texs(richTextBox2, Environment.NewLine, Color.Blue);
        
            get.Clear();

            //    goto being;
            printing.Abort();
            
        }

        void texs(RichTextBox box, string text, Color color)
        {
            if (text != null) { 
                box.Invoke((MethodInvoker)(() =>box.SelectionStart = box.TextLength));
                box.Invoke((MethodInvoker)(() => box.SelectionLength = 0));
                box.Invoke((MethodInvoker)(() => box.SelectionColor = color));
                box.Invoke((MethodInvoker)(() => box.AppendText(text)));
                box.Invoke((MethodInvoker)(() => box.SelectionColor = box.ForeColor));
                box.Invoke((MethodInvoker)(() => box.ScrollToCaret()));
            }
            else
            {
                MessageBox.Show("В порт надо отправлять байты, а не пустую строку");
            }
        }
      
    }
}