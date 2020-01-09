using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log_file_util
{
    public static class ControlInvoker
    {
        public static void Invoke(this Control control, MethodInvoker invoker, params object[] args)
        {
            control.Invoke(invoker, args);
        }
    }
}
