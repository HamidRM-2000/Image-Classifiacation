using Image_ClassificationML.Model;
using Microsoft.Win32;
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

namespace Image_Classification
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

      private async void btnSelect_Click(object sender, RoutedEventArgs e)
      {
         ModelOutput predictionResult = new ModelOutput() { Prediction = "Something went wrong" };
         OpenFileDialog file = new OpenFileDialog();
         file.Filter = "Image Files (*.png , *.jpeg , *.jpg)|*.png; *.jpeg; *.jpg";
         if (file.ShowDialog() == true)
         {
            ImageSource.Source = new BitmapImage(new Uri(file.FileName));
            lbl_image.Visibility = Visibility.Hidden;
            lbl_result.Content = "in proccess ...";
            await Task.Run(() =>
             {
                ModelInput sampleData = new ModelInput()
                {
                   ImageSource = file.FileName
                };
                predictionResult = ConsumeModel.Predict(sampleData);
             });
            lbl_result.Content = predictionResult.Prediction;
         }
      }
   }
}
