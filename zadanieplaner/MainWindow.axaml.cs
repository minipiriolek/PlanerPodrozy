using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace zadanieplaner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            string city = City.Text;
            if (!string.IsNullOrWhiteSpace(city))
                Cities.Items.Add(city);
        }

        private async void Country_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            string[] paths = {
                "avares://zadanieplaner/Assets/francja.jpg",
                "avares://zadanieplaner/Assets/indie.jpg",
                "avares://zadanieplaner/Assets/egipt.jpg",
                "avares://zadanieplaner/Assets/dubaj.jpg",
                "avares://zadanieplaner/Assets/chorwacja.jpg",
                "avares://zadanieplaner/Assets/australia.jpg",
                "avares://zadanieplaner/Assets/grecja.jpg",
                "avares://zadanieplaner/Assets/dominikana.jpg",
                "avares://zadanieplaner/Assets/hiszpania.jpeg",
                "avares://zadanieplaner/Assets/cypr.jpg"
            };


            if (Country.SelectedIndex >= 0)
            {
                string path = paths[Country.SelectedIndex];

                await Task.Run(() =>
                {
                    try
                    {
                        using var stream = AssetLoader.Open(new Uri(path));
                        var bitmap = new Bitmap(stream);

                        Dispatcher.UIThread.InvokeAsync(() =>
                        {
                            ImgCountry.Source = bitmap;
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd przy wczytywaniu obrazu: {ex.Message}");
                    }
                });
            }
        }

        private void Button_OnClick_v2(object? sender, RoutedEventArgs e)
        {
            string country = (Country.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Nie wybrano";
            string attractions = string.Join(", ",
                CheckboxPanel.Children.OfType<CheckBox>().Where(cb => cb.IsChecked == true).Select(cb => cb.Content?.ToString()));
            string transport = RadioGroup1.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true)?.Content?.ToString() ?? "Brak";
            string cities = string.Join(", ", Cities.SelectedItems.Cast<string>());
            string date = TravelDate.SelectedDate?.ToString("dd.MM.yyyy") ?? "Nie wybrano";

            ResultWindow resultWindow = new(country, attractions, transport, cities, date);
            resultWindow.Show();
            Close();
        }
    }
}
