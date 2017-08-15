using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using Microsoft.Win32;
using System.IO;

namespace Text_Editor_v3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        private bool ChangesMade = false;
        private string Changes = " There are no changes ";

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            timer.Start();
        }

        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            if (ChangesMade)
            {
                if (MessageBox.Show("Save changes?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    textBox.Text = "";
                    Clear_Text();
                }
                else
                {
                    Save_File();
                    Clear_Text();
                }
            }
            else
            {
                Clear_Text();
            }
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            if (ChangesMade)
            {
                if (MessageBox.Show("Save changes?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    Open_File();
                }
                else
                {
                    Save_File();
                    Open_File();
                }
            }
            else
            {
                Open_File();
            }

        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            Save_File();
        }

        private void Open_File()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
                ChangesMade = false;
                Changes = " There are no changes ";
            }
        }

        private void Save_File()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                ChangesMade = false;
            }
        }

        private void Clear_Text()
        {
            textBox.Text = "";
            ChangesMade = false;
            Changes = " There are no changes ";
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            if (ChangesMade)
            {
                if (MessageBox.Show("Save changes?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    timer.Stop();
                    Application.Current.Shutdown();
                }
                else
                {
                    Save_File();
                    timer.Stop();
                    Application.Current.Shutdown();
                }
            }
            else
            {
                timer.Stop();
                Application.Current.Shutdown();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                StatusText.Content = textBox.Text.Length + " caracters | " + Changes;
            }
            else
            {
                StatusText.Content = Changes;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangesMade = true;
            Changes = " There are changes ";
        }

        private void Boldfont(object sender, RoutedEventArgs e)
        {
            
        }

        private void Italicfont(object sender, RoutedEventArgs e)
        {

        }

        private void Underlinefont(object sender, RoutedEventArgs e)
        {

        }
    }
}
