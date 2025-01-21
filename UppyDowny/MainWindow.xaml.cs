using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UppyDowny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Don't mercilessly vaporize text (ask users to save)
            editor.Text = "";
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog {
                FileName = "",
                DefaultExt = ".ud",
                Filter = "UpDown files|*.ud"
            };

            bool? result = openDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    using StreamReader reader = new(openDialog.FileName);
                    string text = reader.ReadToEnd();
                    editor.Text = text;
                }
                catch (IOException exception)
                {
                    Console.WriteLine($"The file could not be read: {exception.Message}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog {
                FileName = "",
                DefaultExt = ".ud",
                Filter = "UpDown files|*.ud"
            };

            bool? result = saveDialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(saveDialog.FileName, editor.Text);
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            // This is the big one, so it's going in its own function
            UpDown.Interpret(editor.Text);
        }
    }
}