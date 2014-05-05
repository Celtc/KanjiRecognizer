using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace KanjiRecognizer.Source
{
    /// <summary>
    /// Clase dedicada a almacener la imagen de un kanji junto con su data.
    /// </summary>
    public class Kanji
    {
        public Kanji(Image image, string name, string description)
        {
            this.name = name;
            this.sourceImage = image;
            this.description = description;
        }

        public string name { get; set; }
        public string description { get; set; }
        public Image sourceImage { get; private set; }
    }
}
