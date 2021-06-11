using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HK_Matcher
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

        private void Quit(Object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoadLastSession(Object sender, RoutedEventArgs e)
        {

        }

        private void LoadDefaultDir(Object sender, RoutedEventArgs e)
        {
            CompareToPath.Text = @"C:\Program Files\Steam\steamapps\common\Hollow Knight\hollow_knight_Data";
        }

        private void RunMatch(Object sender, RoutedEventArgs e)
        {
            try
            {
                Matcher matcher = new(CompareToPath.Text, CompareFromPath.Text, this);

                matcher.Run();
            }
            catch(Exception ex)
            {
                Output.Text = $"Match failed: \n{ex}: {ex.Message}\n\n{ex.StackTrace}";
            }
        }
    }
}
