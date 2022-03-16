using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Cards.Client.Utils
{
    public static class FileHandler
    {
        public static byte[] GetImageInBytesArray(string imagePath)
        {   
            using(System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    byte[] bytes = ms.ToArray();

                    return bytes;
                }
            }
        }

        public static string GetImageInBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static BitmapImage GetBitmapImage(string base64String)
        {
            System.Drawing.Image image;

            try
            {
                using (var stream = new MemoryStream(Convert.FromBase64String(base64String)))
                {
                    image = System.Drawing.Image.FromStream(stream);
                }

                using (var stream = new MemoryStream())
                {
                    BitmapImage bitmap = new BitmapImage();

                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Position = 0;

                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    return bitmap;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
