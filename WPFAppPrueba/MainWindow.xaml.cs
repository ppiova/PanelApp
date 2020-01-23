﻿using System;
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
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(delegate (object s, EventArgs a) {
                updTimer();
                updArduinoValues();
            });
            _timer.Start();

            serialPort.ReadTimeout = 1000; //establezco el tiempo de espera cuando una operación de lectura no finaliza
            serialPort.Open(); //abro una nueva conexión de puerto serie

        }


        private void updArduinoValues()
        {
            if (serialPort.IsOpen) //chequeo que el puerto este abierto
            {
                try
                {
                    //print(serialPort.ReadLine().ToString());

                    string actual_line = serialPort.ReadLine().ToString();
                    //print(actual_line);

                    string[] datos = actual_line.Split('|');

                    if(Convert.ToInt32(datos[0]) == 0 && Convert.ToInt32(datos[1]) == 0)
                    {
                        lbl_error_01.Text = "Sensor 1 afectado. Controlar";
                        //si los valores del primer sensor son 0, hay un problema con el mismo (quemado, desconectado, etc)
                        //se utilizarán los valores de contingencia (sensor 2)
                        lbl_temp_int.Text = datos[2] + " C°";
                        lbl_humedad_int.Text = Convert.ToInt32(datos[3]).ToString() + " %";
                    }
                    else
                    {
                        lbl_error_01.Text = "";
                        //todo normal
                        lbl_temp_int.Text = datos[0] + " C°";
                        lbl_humedad_int.Text = Convert.ToInt32(datos[1]).ToString() + " %";
                    }
                    
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
