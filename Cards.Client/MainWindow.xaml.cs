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
using Cards.Client.Utils;

namespace Cards.Client
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



        //private void Button_click(object sender, RoutedEventArgs e)
        //{
        //    string bytesInString = FileHandler.GetImageInBase64(@"C:\Photo\PH\массаж.png");

        //    BitmapImage bitmap = FileHandler.GetBitmapImage(bytesInString);

        //    myImage.Source = bitmap;
        //}
    }
}
