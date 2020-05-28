using System;
using NamedPipeWrapper;
using System.Windows;
using System.Windows.Controls;
using VocabularyManager.Models;
using VocabularyManager.Services;
using VocabularyManagerService.Services;

namespace VocabularyManager
{
    public partial class MainWindow : Window
    {

        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("test");


        public MainWindow()
        {
            InitializeComponent();
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
        }

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            richTextBox.Dispatcher.Invoke(new Action(delegate
            {
                AddLine("Server: " + message);
            }));
        }

        private void OnDisconnected(NamedPipeConnection<string, string> connection)
        {
            richTextBox.Dispatcher.Invoke(new Action(delegate
            {
                AddLine("Disconnected from server");
            }));
        }

        private void AddLine(string html)
        {
            richTextBox.Dispatcher.Invoke(new Action(delegate
            {
                richTextBox.AppendText(Environment.NewLine + html);
            }));
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            textBox.Text = slider.Value.ToString();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            _client.PushMessage(slider.Value.ToString());
        }
    }
}
