using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using static System.Net.Mime.MediaTypeNames;

namespace WeatherApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string curruntCity = "None";
        string url;
        HttpWebRequest httpWebRequest;
        HttpWebResponse httpWebResponse;
        string response;

        void UpdateWeather()
        {
            if (curruntCity != "None")
            {
                url = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&lang=ru&appid=77ed7ce399e19d2297c27c42d5c8aa78", curruntCity);

                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не верно введен город!");
                    return;
                }
                


                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                Temperature.Text = String.Format("{0}°C", (int)weatherResponse.Main.Temp);
                Weather.Text = weatherResponse.Weather[0].Description;

                switch (weatherResponse.Weather[0].Description)
                {
                    case "пасмурно":
                        
                        break;
                    case "небольшой проливной дождь":
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Дождь_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "небольшой дождь":
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Дождь_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "переменная облачность":
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Облачно_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "ясно":
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Солнце_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "небольшая облачность":
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Облачно_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        ImageWeather.Source = new BitmapImage(new Uri(@"/Source/Крестик_Иконка.png", UriKind.RelativeOrAbsolute));
                        break;

                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            UpdateWeather();
            //Logo.Source = new BitmapImage(new Uri(@"/Source/Облачно_Иконка.png", UriKind.RelativeOrAbsolute));

        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();

        }

        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(GridCity.Visibility == Visibility.Hidden)
                GridCity.Visibility = Visibility.Visible;
            else
                GridCity.Visibility = Visibility.Hidden;
        }

        private void CurrentMoskow_Click(object sender, RoutedEventArgs e)
        {
            CurrentCity.Text = "Москва";
            curruntCity = "Москва";
            UpdateWeather();
        }

        private void CurrentChelyabinsk_Click(object sender, RoutedEventArgs e)
        {
            CurrentCity.Text = "Челябинск";
            curruntCity = "Челябинск";
            UpdateWeather();

        }

        private void CurrentPiter_Click(object sender, RoutedEventArgs e)
        {
            CurrentCity.Text = "Санкт-Петербург";
            curruntCity = "Санкт-Петербург";
            UpdateWeather();
        }

        private void CurrentKazan_Click(object sender, RoutedEventArgs e)
        {
            CurrentCity.Text = "Казань";
            curruntCity = "Казань";
            UpdateWeather();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            curruntCity = InputCity.Text;
            CurrentCity.Text = curruntCity;
            UpdateWeather();
        }
    }
}
