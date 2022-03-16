using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Cards.Client.Utils
{
    public static class FileHandler
    {
        public static byte[] GetImageByteArray(string imagePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
            

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        public static BitmapImage GetBitmapImage(string base64String)
        {
            System.Drawing.Image image;

            using (var stream = new MemoryStream(Convert.FromBase64String(base64String)))
            {
                image = System.Drawing.Image.FromStream(stream);
            }

            using (var stream = new MemoryStream())
            {
                BitmapImage bitmap = new BitmapImage();

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Position = 0;

                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                return bitmap;
            }
        }
    }
}
