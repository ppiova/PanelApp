using System;
using System.IO.Ports;
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
        private string buffer { get; set; }

        private SerialPort serialPort = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);

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


            serialPort.ReadTimeout = 1000; //establezco el tiempo de espera cuando una operación de lectura no finaliza
            serialPort.Open(); //abro una nueva conexión de puerto serie


            readArduino();
        }


        private void readArduino()
        {
            if (serialPort.IsOpen) //chequeo que el puerto este abierto
            {
                try
                {
                    //print(serialPort.ReadLine().ToString());

                    string actual_line = serialPort.ReadLine().ToString();
                    //print(actual_line);

                    string[] datos = actual_line.Split('|');

                    lbl_temp_int.Text = datos[0] + " C°";
                    lbl_humedad_int.Text = datos[1] + " %";
                }
                catch (Exception e)
                {
                    
                }

            }
        }
        
        public void updTimer()
        {
            string horas = DateTime.Now.Hour.ToString();
            string minutos = DateTime.Now.Minute.ToString();
            string segundos = DateTime.Now.Second.ToString();

            if (horas.Length == 1)
                horas = "0" + horas;
            if (minutos.Length == 1)
                minutos = "0" + minutos;
            if (segundos.Length == 1)
                segundos = "0" + segundos;

            lbl_hours.Text = horas + ":" + minutos + ":" + segundos;
        }

    }
}
