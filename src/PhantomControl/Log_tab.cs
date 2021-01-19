using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhantomControl
{
    public partial class Log_tab : UserControl
    {
        public Log_tab()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
                UpdateStatusBarMessage.OnNewStatusMessage += UpdateStatusBarMessage_OnNewStatusMessage;
            }
        }

        public void UpdateStatusBarMessage_OnNewStatusMessage(string strMessage)
        {
            Func<int> del = delegate()
            {
                textbox_Log.AppendText(strMessage + System.Environment.NewLine);
                return 0;
            };

            Invoke(del);
        }

    }

    public delegate void AddStatusMessageDelegate(string strMessage);

    public static class UpdateStatusBarMessage
    {
        public static Form mainwin;
        public static event AddStatusMessageDelegate OnNewStatusMessage;
        public static void ShowStatusMessage(string strMessage)
        {
            ThreadSafeStatusMessage(strMessage);
        }

        private static void ThreadSafeStatusMessage(string strMessage)
        {
            if (mainwin != null && mainwin.InvokeRequired)                                                                      // we are in a different thread to the main window
                mainwin.Invoke(new AddStatusMessageDelegate(ThreadSafeStatusMessage), new object[] { strMessage });             // call self from main thread
            else
                OnNewStatusMessage(strMessage);
        }
    }
}
