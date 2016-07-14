using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Tasks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int MaxN = 10;

        Queue<int> _queue;
        List<int> _popList;

        public MainWindow()
        {
            _queue = new Queue<int>();
            _popList = new List<int>();

            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InputBox_NumberValidationInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            int getValue = 0;
            if (int.TryParse(InputBoxGet.Text, out getValue))
            {
                MaxN = getValue;
                await AsyncRequestRandom();
            }
            else
            {
                MessageBox.Show("Enter the N-number!");
            }
        }
        private async Task AsyncRequestRandom()
        {
            var uriString = string.Format($"https://www.random.org/integers/?num=10&min=1&max={MaxN.ToString()}&col=1&base=10&format=plain&rnd=new" );
            var request = (HttpWebRequest)WebRequest.Create(uriString);
            var response = (HttpWebResponse)await request.GetResponseAsync();

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        _queue.Enqueue(int.Parse(line));
                    }
                }
            }
            response.Close();
            UpdatePushBox();
        }

        private void UpdatePushBox()
        {
            var pushList = _queue.ToList();
            PushBox.ItemsSource = pushList;
        }
        private void UpdatePopBox()
        {
            PopBox.ItemsSource = null;
            PopBox.ItemsSource = _popList;
        }

        private void AddNubersFromBCL(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                _queue.Enqueue(random.Next(0,MaxN+1));
            }
            AsyncRequestRandom();
        }

        private void PopButton_Click(object sender, RoutedEventArgs e)
        {
            int getValue = 0;

            if (int.TryParse(InputBoxPop.Text, out getValue))
            {
                if (getValue > _queue.Count)
                    AddNubersFromBCL(getValue - _queue.Count);
             
                for (int i = 0; i < getValue; i++)
                    _popList.Add(_queue.Dequeue());

                UpdatePopBox();
                UpdatePushBox();

            }
            else
            {
                MessageBox.Show("Enter the N-number!");
            }
        }
    }
}
