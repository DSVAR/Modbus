using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using FTnew.scripts;

namespace FTnew
{
    public partial class data_ : Form
    {
        static public Thread sendos;
        static public RichTextBox kol1;
        static public int ches;

        static public DataTable dt=new DataTable();

        static public DataRow sn, VS, VSP, timos, Resistor, Battery, measure;
        public data_()
        {
            InitializeComponent();
          

            changeID.data = dataGridView1;

            save.dg = dataGridView1;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Обновить") {
               
            sendos= new Thread(Thr);
                sendos.Priority = ThreadPriority.AboveNormal;
            sendos.Start();
            button1.Text = "Остановить";
            }
            else
            {

                if (sendos.IsAlive)
                {
                    sendos.Abort();

                }
                button1.Text = "Обновить";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeID.sen();
         
        }

       
        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
          
        }

       static public void Thr() 
        {
         
            
                try {
                  
                 kol1.Invoke((MethodInvoker)(() => kol1.Text=null));
                        
                        dates.data();
                              
                }
                catch
                {
                    
                    
                }
            

        }

        private void data__Load(object sender, EventArgs e)
        {
            dt.Clear();

            DataColumn name = new DataColumn("Названия", typeof(string));
            DataColumn data = new DataColumn("Значения", typeof(string));
            try
            {
                dt.Columns.AddRange(new DataColumn[] { name, data });


            }
            catch { }

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns["Column1"].DataPropertyName = "Названия";
            dataGridView1.Columns["Column2"].DataPropertyName = "Значения";

            sn = dt.NewRow();
            sn[0] = "Серийный номер";
            sn[1] = "";

            VS = dt.NewRow();
            VS[0] = "Тип прибора";
            VS[1] = "";

            VSP = dt.NewRow();
            VSP[0] = "Версия железа";
            VSP[1] = "";

            timos = dt.NewRow();
            timos[0] = "Время:";
            timos[1] = "";

            Resistor = dt.NewRow();
            Resistor[0] = "Резистивиметр";
            Resistor[1] = "40%";

            measure = dt.NewRow();
            measure[0] = "Давление";
            measure[1] = "";

            Battery = dt.NewRow();
            Battery[0] = "Батарея";
            Battery[1] = "3.20V";

          
            dt.Rows.Add(sn);
            dt.Rows.Add(VS);
            dt.Rows.Add(VSP);
            dt.Rows.Add(timos);
            dt.Rows.Add(Resistor);
            dt.Rows.Add(measure);
            dt.Rows.Add(Battery);

            dataGridView1.DataSource = dt;
        }

        private void data__FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sendos.IsAlive!=null) { 
                sendos.Abort();
                
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            save.saving();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            time.timer();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
