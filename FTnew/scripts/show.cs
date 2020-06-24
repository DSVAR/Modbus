using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FTnew.scripts
{
    class show
    {
        

        static public void pr()
        {
            texs(values.rich2, Environment.NewLine, Color.Blue);
            texs(values.rich2, "Пришло: " + values.kls.ToString() + " байт", Color.Blue);
            texs(values.rich2, Environment.NewLine, Color.Blue);
            texs(values.rich2, values.value, Color.Green);
            texs(values.rich2, Environment.NewLine, Color.Blue);
            values.kls = values.kls - send.sed.Length;
            texs(values.rich2, Environment.NewLine, Color.Blue);
            texs(values.rich2, "Пришло: " + values.kls.ToString() + " байт", Color.Lime);
            texs(values.rich2, Environment.NewLine, Color.Blue);
            texs(values.rich2, values.otherB, Color.Purple);
            texs(values.rich2, Environment.NewLine, Color.Blue);

          //  values.awake.Abort();
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
