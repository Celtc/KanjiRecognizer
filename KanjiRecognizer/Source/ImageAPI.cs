using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace KanjiRecognizer.Source
{
    class ImageAPI
    {
        /// <summary>
        /// Convierte la imagen en un <typeparamref name="Bitmap"/>
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        public static Bitmap AlltoBMP(Image image)
        {
            //Imagen de salida
            Bitmap resultImage;

            //Obtiene la extension
            ImageFormat imageFormat = image.RawFormat;

            //Revisa el formato inicial
            if (imageFormat == ImageFormat.Bmp)
            {
                //BMP
                resultImage = image as Bitmap;
            }
            else
            {
                //Otro
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Bmp);
                    resultImage = new Bitmap(stream);
                    stream.Close();
                }
            }
            return resultImage;
        }

        /// <summary>
        /// Escala el <typeparamref name="Bitmap"/> a las dimensiones deseadas
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        /// <param name="width"> <typeparamref name="Int32"/> que especifica el ancho deseado</param>
        /// <param name="height"> <typeparamref name="Int32"/> que especifica el alto deseado</param>
        public static Bitmap ResizeBitmap(Bitmap image, int width, int height)
        {
            Bitmap resultImage = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(resultImage))
            {
                graph.DrawImage(image, 0, 0, width, height);
            }
            return resultImage;
        }

        /// <summary>
        /// Genera un hash a partir del contenido de una imagen.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        public static string GenerateHashFromImage_DEPRECATED(Image image)
        {
            string hash = string.Empty;

            //Extrae el contenido raw
            Bitmap bitmap = AlltoBMP(image);
            int[] pixels = new int[bitmap.Width * bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int pixelValue = Math.Abs(bitmap.GetPixel(i, j).ToArgb());
                    pixels[i * bitmap.Width + j] = pixelValue;
                }
            }

            //Genera el hash
            try
            {
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    byte[] pixelsByte = new byte[pixels.Length * sizeof(int)];
                    Buffer.BlockCopy(pixels, 0, pixelsByte, 0, pixelsByte.Length);
                    hash = Convert.ToBase64String(sha1.ComputeHash(pixelsByte));
                }
            }
            catch { }

            return hash;
        }

        /// <summary>
        /// Genera un hash a partir del contenido de una imagen.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        public static string GenerateHashFromImage(Image image)
        {
            string hash = string.Empty;
            try
            {
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    byte[] byteArray = new byte[0];
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.Save(stream, ImageFormat.Bmp);
                        byteArray = stream.ToArray();
                        stream.Close();
                    }
                    hash = Convert.ToBase64String(sha1.ComputeHash(byteArray));
                }
            }
            catch { }

            return hash;
        }
    }
}
