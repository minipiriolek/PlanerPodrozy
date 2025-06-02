using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Linq;

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

        private void Country_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            string[] paths = {
                "avares://Zadanie1/Assets/francja.jpg",
                "avares://Zadanie1/Assets/indie.jpg",
                "avares://Zadanie1/Assets/egipt.jpg",
                "avares://Zadanie1/Assets/dubaj.jpg",
                "avares://Zadanie1/Assets/chorwacja.jpg",
                "avares://Zadanie1/Assets/australia.jpg",
                "avares://Zadanie1/Assets/grecja.jpg",
                "avares://Zadanie1/Assets/dominikana.jpg",
                "avares://Zadanie1/Assets/hiszpania.jpeg",
                "avares://Zadanie1/Assets/cypr.jpg"
            };


            if (Country.SelectedIndex >= 0)
            {
                using var stream = AssetLoader.Open(new Uri(paths[Country.SelectedIndex]));
                ImgCountry.Source = new Bitmap(stream);
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
