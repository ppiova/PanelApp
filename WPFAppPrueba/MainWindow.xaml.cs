using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFAppPrueba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            var _timer = new DispatcherTimer();
            // Set the Interval to 2 seconds 
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            // Set the callback to just show the time ticking away 
            // NOTE: We are using a control so this has to run on 
            // the UI thread 
            _timer.Tick += new EventHandler(delegate (object s, EventArgs a) {
                updTimer();
            });
            // Start the timer 
            _timer.Start();
        }
        
        public void updTimer()
        {
            lbl_horario.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }

    }
}
