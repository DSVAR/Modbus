using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTnew
{
    class save
    {
        static public DataGridView dg ;


        static public void saving()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "ARBAH_TEST (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if( saveFile.ShowDialog()== DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFile.FileName, false, Encoding.Unicode);
                try
                {
                    List<int> col_n = new List<int>();
                    foreach (DataGridViewColumn col in dg.Columns)
                        if (col.Visible)
                        {
                            //sw.Write(col.HeaderText + "\t");
                            col_n.Add(col.Index);
                        }
                    string date = DateTime.Now.ToString();
                    sw.WriteLine("Время сохранения:"+date);
                    //sw.WriteLine();
                    int x = dg.RowCount;
                    if (dg.AllowUserToAddRows) x--;

                    for (int i = 0; i < x; i++)
                    {
                        for (int y = 0; y < col_n.Count; y++)
                            sw.Write(dg[col_n[y], i].Value + "\t");
                        sw.Write(" \r\n");
                    }
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }

            }
        }
    }
}
