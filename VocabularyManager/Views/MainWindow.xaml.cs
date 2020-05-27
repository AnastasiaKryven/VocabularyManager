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
using VocabularyManager.Services;
using VocabularyManagerService.Services;

namespace VocabularyManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientObserver observer = new ClientObserver(); 

        public MainWindow()
        {
            InitializeComponent();
            //var client1 = new IpcClient<string>(".", "test", observer).Create();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            textBox.Text = slider.Value.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var client1 = new IpcClient<string>(".", "test", observer).Create();
            observer.OnNext(slider.Value.ToString());
            //    SystemVolumeConfigurator volume = new SystemVolumeConfigurator();

            //    volume.SetVolume((float)slider.Value);


        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            SystemVolumeConfigurator volume = new SystemVolumeConfigurator();

            slider.Value = volume.GetVolume();

            textBox.Text = slider.Value.ToString();
        }
    }
}
