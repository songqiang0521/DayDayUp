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

using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;

namespace faceapi
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("24944e4f5715456ba4b22cc3fe238dd8");


        private async Task<Face[]> UploadAndDetectFaces(string imageFilePath)
        {
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    var faces = await faceServiceClient.DetectAsync(imageFileStream);
                    return faces;
                }
            }
            catch (Exception)
            {
                return new Face[0];
            }
        }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "JPEG Image(*.jpg)|*.jpg";
            var result = dlg.ShowDialog(this);
            if (!(bool)result)
            {
                return;
            }

            string filePath = dlg.FileName;
            var fileUri = new Uri(filePath);
            BitmapImage bitmapSource = new BitmapImage();
            bitmapSource.BeginInit();
            bitmapSource.CacheOption = BitmapCacheOption.None;
            bitmapSource.UriSource = fileUri;
            bitmapSource.EndInit();
            FacePhoto.Source = bitmapSource;


            Title = "Detecting";
            Face[] faces = await UploadAndDetectFaces(dlg.FileName);
            Title = string.Format("Detection finished.{0} face(s) detected", faces.Length.ToString());

            if (faces.Length > 0)
            {
                DrawingVisual visual = new DrawingVisual();
                DrawingContext context = visual.RenderOpen();
                context.DrawImage(bitmapSource,
                    new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
                double dpi = bitmapSource.DpiX;
                double resizeFactor = 96 / dpi;

                foreach (var face in faces)
                {
                    Title += face.FaceAttributes.Age.ToString("F2") + face.FaceAttributes.Gender;
                    context.DrawRectangle(
                        Brushes.Transparent,
                        new Pen(Brushes.Red, 2),
                        new Rect(face.FaceRectangle.Left * resizeFactor,
                        face.FaceRectangle.Top * resizeFactor,
                        face.FaceRectangle.Width * resizeFactor,
                        face.FaceRectangle.Height * resizeFactor));

                    //免费版本的可能只提供面部位置识别
                    //context.DrawLine(new Pen(Brushes.Blue, 2),
                    //    new Point(face.FaceLandmarks.MouthLeft.X * resizeFactor, face.FaceLandmarks.MouthLeft.Y),
                    //    new Point( face.FaceLandmarks.MouthRight.X, face.FaceLandmarks.MouthRight.Y));
                }


                context.Close();
                RenderTargetBitmap faceBitmap = new RenderTargetBitmap(
                    (int)(bitmapSource.PixelWidth * resizeFactor),
                    (int)(bitmapSource.PixelHeight * resizeFactor),
                    96, 96, PixelFormats.Pbgra32);

                faceBitmap.Render(visual);

                FacePhoto.Source = faceBitmap;
            }


        }
    }
}
