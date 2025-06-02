using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace zadanieplaner
{
    public partial class ResultWindow : Window
    {
        public ResultWindow(string country, string attractions, string transport, string cities, string date)
        {
            InitializeComponent();

            Summary.Text = $"Kraj: {country}\n" +
                           $"Atrakcje: {attractions}\n" +
                           $"Transport: {transport}\n" +
                           $"Miasta: {cities}\n" +
                           $"Data: {date}";
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e) {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "result.txt");
            using var writer = new StreamWriter(filePath);
            writer.Write(Summary.Text);
        }
    }
}