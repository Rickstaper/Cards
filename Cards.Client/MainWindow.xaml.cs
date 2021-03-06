using System;
using System.Collections;
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
using Cards.Client.Models;
using Cards.Client.Utils;
using Cards.Client.Utils.Api;
using Cards.Client.Utils.Serialization;
using Microsoft.Win32;
using RestSharp;

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

        private void GetAllCards_ButtonClick(object sender, RoutedEventArgs e)
        {
            results.Children.Clear();

            IRestResponse response = ServerApi.GetAllCards();

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error");
                return;
            }

            IList<Card> cards = Serializer.Deserialize<IList<Card>>(response.Content);

            int i = 1;
            foreach (Card card in cards)
            {
                results.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200) });
                TextBlock idFromCard = new TextBlock();
                idFromCard.Text = card.Id.ToString();
                Grid.SetColumn(idFromCard, 0);
                Grid.SetRow(idFromCard, i);
                idFromCard.HorizontalAlignment = HorizontalAlignment.Center;

                results.Children.Add(idFromCard);

                TextBlock nameFromCard = new TextBlock();
                nameFromCard.Text = card.Name;
                Grid.SetColumn(nameFromCard, 1);
                Grid.SetRow(nameFromCard, i);
                nameFromCard.HorizontalAlignment = HorizontalAlignment.Center;

                results.Children.Add(nameFromCard);

                BitmapImage bitmap = FileHandler.GetBitmapImage(FileHandler.GetImageInBase64(card.Image));

                Image image = new Image();
                image.Source = bitmap;
                Grid.SetColumn(image, 2);
                Grid.SetRow(image, i++);
                image.HorizontalAlignment = HorizontalAlignment.Center;

                results.Children.Add(image);
            }
        }

        private void GetCardById_ButtonClick(object sender, RoutedEventArgs e)
        {
            results.Children.Clear();

            string idForSearch = inputIdForSearch.Text;

            IRestResponse response = ServerApi.GetCardById(idForSearch);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error");
                return;
            }

            Card card = Serializer.Deserialize<Card>(response.Content);

            results.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200) });
            TextBlock idFromCard = new TextBlock();
            idFromCard.Text = card.Id.ToString();
            Grid.SetColumn(idFromCard, 0);
            Grid.SetRow(idFromCard, 1);
            idFromCard.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(idFromCard);

            TextBlock nameFromCard = new TextBlock();
            nameFromCard.Text = card.Name;
            Grid.SetColumn(nameFromCard, 1);
            Grid.SetRow(nameFromCard, 1);
            nameFromCard.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(nameFromCard);

            BitmapImage bitmap = FileHandler.GetBitmapImage(FileHandler.GetImageInBase64(card.Image));

            Image image = new Image();
            image.Source = bitmap;
            Grid.SetColumn(image, 2);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(image);
        }

        private void CreateCard_ButtonClick(object sender, RoutedEventArgs e)
        {
            results.Children.Clear();

            string nameFromCreateCard = inputCardName.Text;
            byte[] imageInBytes = FileHandler.GetImageInBytesArray(resultOfFileDialog.Text);

            IRestResponse response = ServerApi.CreateCard(new Card { Id = Guid.NewGuid(), Name = nameFromCreateCard,
            Image = imageInBytes });

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Not created.");
                return;
            }

            Card card = Serializer.Deserialize<Card>(response.Content);

            results.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200) });
            TextBlock idFromCard = new TextBlock();
            idFromCard.Text = card.Id.ToString();
            Grid.SetColumn(idFromCard, 0);
            Grid.SetRow(idFromCard, 1);
            idFromCard.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(idFromCard);

            TextBlock nameFromCard = new TextBlock();
            nameFromCard.Text = card.Name;
            Grid.SetColumn(nameFromCard, 1);
            Grid.SetRow(nameFromCard, 1);
            nameFromCard.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(nameFromCard);

            BitmapImage bitmap = FileHandler.GetBitmapImage(FileHandler.GetImageInBase64(card.Image));

            Image image = new Image();
            image.Source = bitmap;
            Grid.SetColumn(image, 2);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Center;

            results.Children.Add(image);
        }

        private void OpenFileDialog_ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == true)
            {
                resultOfFileDialog.Text = openFileDialog.FileName;
            }
        }

        private void DeleteCard_ButtonClick(object sender, RoutedEventArgs e)
        {
            results.Children.Clear();

            string idForDelete = inputIdForDeleteCard.Text;
            Guid idForCheckParse;

            if (idForDelete == String.Empty || !Guid.TryParse(idForDelete, out idForCheckParse))
            {
                MessageBox.Show("Field for id doesn't contain id or id isn't correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IRestResponse response = ServerApi.DeleteCard(idForDelete);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Not deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void CompleteUpdate_ButtonClick(object sender, RoutedEventArgs e)
        {
            results.Children.Clear();

            string idForUpdate = inputIdForCompleteUpdate.Text;

            Guid idForCheckParse;

            if (idForUpdate == String.Empty || !Guid.TryParse(idForUpdate, out idForCheckParse))
            {
                MessageBox.Show("Field for id doesn't contain id or id isn't correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string nameForUpdate = inputNameForCompleteUpdate.Text;
            byte[] imageInBytes = FileHandler.GetImageInBytesArray(resultOfFileDialogForCompleteUpdate.Text);

            IRestResponse response = ServerApi.UpdateCard(idForUpdate, new CardForUpdateDto() { Name = nameForUpdate, Image = imageInBytes });

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show("Not created.");
                return;
            }
        }

        private void OpenFileDialogForCompleteUpdate_ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                resultOfFileDialogForCompleteUpdate.Text = openFileDialog.FileName;
            }
        }
    }
}
