using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageConverter
{
    public class ImageConverter
    {
        public static BitmapImage ToBitmapImage(string imagePath)
        {
            BitmapImage bitmapImage = null;

            try
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(imagePath)))
                {
                    stream.Position = 0;

                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.StreamSource = stream;
                    imageSource.EndInit();

                    bitmapImage = imageSource;
                }
            }
            catch (Exception ex)
            {
                // обработка ошибок
            }

            return bitmapImage;
        }
    }
}
