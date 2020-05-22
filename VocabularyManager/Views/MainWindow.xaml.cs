using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VocabularyManager.Models;

namespace VocabularyManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

  
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            textBox.Text = slider.Value.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SystemVolumeConfigurator volume = new SystemVolumeConfigurator();

            volume.SetVolume((float)slider.Value);
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            SystemVolumeConfigurator volume = new SystemVolumeConfigurator();

            slider.Value = volume.GetVolume();

            textBox.Text = slider.Value.ToString();
        }
    }
}
