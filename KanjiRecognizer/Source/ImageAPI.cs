﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Collections;

namespace KanjiRecognizer.Source
{
    public static class ImageAPI
    {
        /// <summary>
        /// Convierte la imagen en un bitmap.
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
                //BMP: no hay necesidad de conversion
                resultImage = image as Bitmap;
            }
            else
            {
                //Otro
                resultImage = new Bitmap(image);
            }
            return resultImage;
        }

        /// <summary>
        /// Escala el bitmap a las dimensiones deseadas.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        /// <param name="width"> Ancho deseado</param>
        /// <param name="height"> Alto deseado</param>
        public static Bitmap ResizeBitmap(Bitmap image, int width, int height)
        {
            Bitmap resultImage = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(resultImage))
            {
                graph.SmoothingMode = SmoothingMode.HighQuality;
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graph.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graph.DrawImage(image, -1, -1, width + 2, height + 2);
            }
            return resultImage;
        }

        /// <summary>
        /// Convierte el bitmap a blanco y negro utilzando un threshold específico.
        /// </summary>
        /// <param name="sourceImage">Imagen fuente</param>
        /// <param name="threshold">Threshold que se utilizara para determinar si un pixel es blanco o negro</param>
        public static Bitmap BitmapToMonochrome(Bitmap sourceImage, float threshold)
        {
            using (Graphics gr = Graphics.FromImage(sourceImage)) // SourceImage is a Bitmap object
            {
                var gray_matrix = new float[][] { 
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 }, 
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 }, 
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 }, 
                new float[] { 0,      0,      0,      1, 0 }, 
                new float[] { 0,      0,      0,      0, 1 } 
                };

                var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(gray_matrix));
                ia.SetThreshold(threshold); 
                var rc = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                gr.DrawImage(sourceImage, rc, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, ia);
            }

            return sourceImage;
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
        /// Genera un hash SHA1 a partir del contenido de una imagen.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        public static string GenerateSHA1HashFromImage(Image image)
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

        /// <summary>
        /// Genera un hash basado en SHA512 de largo definido a partir del contenido de una imagen.
        /// Lo devuelve como un array de bits.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        /// <param name="size">Largo en bits del hash</param>
        public static BitArray GenerateBitHashFromImage(Image image, int size)
        {
            //Hash de salida (Vector del tamaño deseado inicializado en 0)
            int sizeInBytes = (int) Math.Ceiling(size / 8f);
            byte[] hash = Enumerable.Repeat((byte) 0x00, sizeInBytes).ToArray();

            //Extrae el byteArray de la imagen
            byte[] imageBytes = ByteArrayFromImage(image);

            //Genera un hash de 192 bytes a base de aplicar el SHA512
            byte[] SHAComposedHash = new byte[192];
            using (var sha512 = new SHA512Cng())
            {
                //Hash de los bytes de la imagen
                Buffer.BlockCopy(sha512.ComputeHash(imageBytes), 0, SHAComposedHash, 0, 64);

                //Hash de los bytes invertidos de la imagen
                byte[] notImageBytes = new byte[imageBytes.Length];
                for (int i = 0; i < imageBytes.Length; i++)
                    notImageBytes[i] = (byte)~imageBytes[i];
                Buffer.BlockCopy(sha512.ComputeHash(notImageBytes), 0, SHAComposedHash, 64, 64);

                //Hash de los bytes del mismo hash
                byte[] currentHash = new byte[128];
                Buffer.BlockCopy(SHAComposedHash, 0, currentHash, 0, 128);
                Buffer.BlockCopy(sha512.ComputeHash(currentHash), 0, SHAComposedHash, 128, 64);
            }

            //Completa en unidades de bytes el largo faltante
            int iterations = 0;
            while (sizeInBytes > iterations * 192)
            {
                int toCopy = sizeInBytes - iterations * 192;
                Buffer.BlockCopy(SHAComposedHash, 0, hash, iterations++ * 192, Math.Min(192, toCopy));
            }

            //Recorta los bits sobrantes
            BitArray allBits = new BitArray(hash);
            BitArray finalBits = new BitArray(size);
            for (int i = 0; i < size; i++)
            {
                finalBits.Set(i, allBits.Get(i));
            }

            return finalBits;
        }
        
        /// <summary>
        /// Genera un byte array con el contenido raw de la imagen.
        /// </summary>
        /// <param name="image">Imagen fuente</param>
        private static byte[] ByteArrayFromImage (Image image)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                byteArray = stream.ToArray();
                stream.Close();
            }
            return byteArray;
        }
    }
}
