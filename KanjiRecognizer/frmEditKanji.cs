using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

using KanjiRecognizer.Source;
using System.Reflection;

namespace KanjiRecognizer
{

    #region Enums
    public enum WidthRectanglePointer
    {
        Big = 21,
        Medium = 13,
        Small = 7
    }

    public enum WidthCirclePointer
    {
        Big = 31,
        Medium = 20,
        Small = 10
    }

    public enum TypePointer
    {
        Rectangle,
        Circle
    }
    #endregion

    public partial class frmEditKanji : Form
    {
        #region Builder
        public frmEditKanji(Image baseImage = null)
        {
            InitializeComponent();
            widthCirclePointer = WidthCirclePointer.Small;
            typePointer = TypePointer.Circle;
            selectedPencilBox = picBoxCircleSmall;
            colorFront = Color.Black;
            colorBack = Color.White;
            toolStripLabel_dimension.Text = "-";
            if (baseImage != null)
                this.Set(ImageAPI.AlltoBMP(baseImage));
        }
        #endregion

        #region Metodos

        //Evento: Nuevo
        private void toolStripNew_Click(object sender, EventArgs e)
        {
            New();
        }
        //Evento: Abrir
        private void toolStripOpen_Click(object sender, EventArgs e)
        {
            Open();
        }
        //Evento: Guardar
        private void toolStripSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        //Evento: Aceptar
        private void toolStripButton_accept_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        //Evento: Cerrar
        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Crea una imagen en blanco
        /// </summary>
        public void New()
        {
            //Pregunta por el tamaño de la imagen
            int width, height;
            using (var createImgForm = new frmCreateImage())
            {
                var result = createImgForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    width = createImgForm.width;
                    height = createImgForm.height;
                }
                else
                    return;
            }

            //Crea la imagen
            Bitmap bmp = new Bitmap(width, height);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
            NewImage(bmp);

            //Habilita las herramientas
            EnableOptions(true);
        }

        /// <summary>
        /// Establece para editar una imagen presente en memoria 
        /// </summary>
        public void Set(Bitmap bmp)
        {
            EnableOptions(true);
            NewImage(bmp);
        }

        /// <summary>
        /// Abre una imagen
        /// </summary>
        public void Open()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    NewImage((Bitmap)Bitmap.FromFile(ofd.FileName));
                    EnableOptions(true);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error has ocurred when loading image.Please try with 24bpp images if you can't see it.","Atention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Save an image
        /// </summary>
        public void Save()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Windows Bitmap (*.BMP)|*.bmp|Joint Photographic Experts Group (*.JPG;*.JIF;*.JPEG)|*.jpg;*.jif;*.jpeg|Portable Network Graphics(*.PNG)|*.png";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        image.Save(sfd.FileName, ImageFormat.Bmp);
                        break;

                    case 2:
                        image.Save(sfd.FileName, ImageFormat.Jpeg);
                        break;

                    case 3:
                        image.Save(sfd.FileName, ImageFormat.Png);
                        break;
                }
            }
        }

        /// <summary>
        /// Assign the image to the pictureBox
        /// </summary>
        /// <param name="bmp"></param>
        private void NewImage(Bitmap bmp) 
        {
            this.image = bmp;
            picImg.Image = this.image;
            picImg.Refresh();
            toolStripLabel_dimension.Text = this.image.Width + " x " + this.image.Height;
            graphics = Graphics.FromImage(this.image);
        }

        /// <summary>
        /// Enable the options to process the image
        /// </summary>
        /// <param name="view"></param>
        private void EnableOptions(bool view) 
        {
            panelImg.Visible =
            groupBoxPencil.Visible =
            groupBoxColor.Visible = view;
        }

        #endregion

        #region Selección del pincel
        //Small rectangle 
        private void picBoxRecSmall_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        //Medium rectangle 
        private void picBoxRecMedium_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        //Big rectangle
        private void picBoxRecBig_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        //Small circle
        private void picBoxCircleSmall_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        //Medium circle
        private void picBoxCircleMedium_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        //Big circle
        private void picBoxCircleBig_Click(object sender, EventArgs e)
        {
            clickPictureBox((PictureBox)sender);
        }
        /// <summary>
        /// Set the width and type to pointer
        /// </summary>
        /// <param name="picBox"></param>
        private void clickPictureBox(PictureBox picBox)
        {
            //Hace visible la seleccion
            if (selectedPencilBox != picBox)
            {
                selectedPencilBox.BorderStyle = BorderStyle.None;
                picBox.BorderStyle = BorderStyle.Fixed3D;
                selectedPencilBox = picBox;
            }

            //Establece el ancho y forma
            string name = picBox.Name;
            switch (name)
            {
                case "picBoxCircleBig":
                    widthCirclePointer = WidthCirclePointer.Big;
                    typePointer = TypePointer.Circle;
                    break;
                case "picBoxCircleMedium":
                    widthCirclePointer = WidthCirclePointer.Medium;
                    typePointer = TypePointer.Circle;
                    break;
                case "picBoxCircleSmall":
                    widthCirclePointer = WidthCirclePointer.Small;
                    typePointer = TypePointer.Circle;
                    break;
                case "picBoxRecBig":
                    widthRectanglePointer = WidthRectanglePointer.Big;
                    typePointer = TypePointer.Rectangle;
                    break;
                case "picBoxRecMedium":
                    widthRectanglePointer = WidthRectanglePointer.Medium;
                    typePointer = TypePointer.Rectangle;
                    break;
                case "picBoxRecSmall":
                    widthRectanglePointer = WidthRectanglePointer.Small;
                    typePointer = TypePointer.Rectangle;
                    break;
            }
        }
        #endregion

        #region Pintado o Borrado
        private void picImg_MouseDown(object sender, MouseEventArgs e)
        {
            PaintOrErase(e);
        }

        private void picImg_MouseMove(object sender, MouseEventArgs e)
        {
            PaintOrErase(e);
        }

        private void picImg_MouseEnter(object sender, EventArgs e)
        {
            SetMouseCursor();
        }

        private void picImg_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Pinta o borra la imagen
        /// </summary>
        /// <param name="e"></param>
        private void PaintOrErase(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DrawFigure(colorFront, e);
                picImg.Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                DrawFigure(colorBack, e);
                picImg.Refresh();
            }
        }

        /// <summary>
        /// Dibuja la figura sobre la imagen
        /// </summary>
        /// <param name="color"></param>
        /// <param name="e"></param>
        private void DrawFigure(Color color, MouseEventArgs e)
        {
            pen = new Pen(color, 1);
            switch (typePointer)
            {
                case TypePointer.Rectangle:
                    rect = new Rectangle(e.X, e.Y, (int)widthRectanglePointer, (int)widthRectanglePointer);
                    graphics.DrawRectangle(pen, rect);
                    graphics.FillRectangle(new SolidBrush(color), rect);
                    break;

                case TypePointer.Circle:
                    rect = new Rectangle(e.X, e.Y, (int)widthCirclePointer, (int)widthCirclePointer);
                    graphics.DrawEllipse(pen, rect);
                    graphics.FillEllipse(new SolidBrush(color), rect);
                    break;
            }
        }
        
        /// <summary>
        /// Cambia el cursor al seleccionado
        /// </summary>
        private void SetMouseCursor()
        {
            string name = selectedPencilBox.Name;
            switch (name)
            {
                case "picBoxCircleBig":
                    Cursor = GetCursor("CursorCircleBig");
                    break;
                case "picBoxCircleMedium":
                    Cursor = GetCursor("CursorCircleMedium");
                    break;
                case "picBoxCircleSmall":
                    Cursor = GetCursor("CursorCircleSmall");
                    break;
                case "picBoxRecBig":
                    Cursor = GetCursor("CursorRectBig");
                    break;
                case "picBoxRecMedium":
                    Cursor = GetCursor("CursorRectMedium");
                    break;
                case "picBoxRecSmall":
                    Cursor = GetCursor("CursorRectSmall");
                    break;
            }
        }
        
        /// <summary>
        /// Trae de los recursos el cursor especificado
        /// </summary>
        private static Cursor GetCursor(string cursorName)
        {
            var buffer = Properties.Resources.ResourceManager.GetObject(cursorName) as byte[];
            using (var m = new MemoryStream(buffer))
            {
                return new Cursor(m);
            }
        }

        #endregion

        #region Cambia el color del pincel
        //Color Black 
        private void picBlack_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Black, e);
        }
        //Color Gray
        private void picDarkGray_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Gray, e);
        }
        //Color Maroon
        private void picMaroon_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Maroon, e);
        }
        //Color Olive
        private void picOlive_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Olive, e);
        }
        //Color Green
        private void picGreen_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Green, e);
        }
        //Color Teal
        private void picTeal_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Teal, e);
        }
        //Color Navy
        private void picNavy_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Navy, e);
        }
        //Color Purple
        private void picPurple_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Purple, e);
        }
        //Color 192,192,0
        private void pic192_192_0_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(192, 192, 0), e);
        }
        //Color 64,64,64
        private void pic64_64_64_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(64, 64, 64), e);
        }
        //Color 128,128,255
        private void pic128_128_255_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(128, 128, 255), e);
        }
        //Color 255,128,0
        private void pic255_128_0_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(255, 128, 0), e);
        }
        //Color White
        private void picWhite_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.White, e);
        }
        //Color LightGray
        private void picLightGray_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Gray, e);
        }
        //Color Red
        private void picRed_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Red, e);
        }
        //Color Yellow
        private void picYellow_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Yellow, e);
        }
        //Color Lime
        private void picLime_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Lime, e);
        }
        //Color Cyan
        private void picCyan_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Cyan, e);
        }
        //Color Blue
        private void picBlue_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Blue, e);
        }
        //Color Fuchsia
        private void picFuchsia_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.Fuchsia, e);
        }
        //Color 255,255,192
        private void pic255_255_192_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(255, 255, 192), e);
        }
        //Color 192,255,192
        private void pic192_255_192_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(192, 255, 192), e);
        }
        //Color 192,255,255
        private void pic192_255_255_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(192, 255, 255), e);
        }
        //Color 255,192,128
        private void pic255_192_128_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeColor(Color.FromArgb(255, 192, 128), e);
        }

        /// <summary>
        /// Change the color to pencil 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="e"></param>
        private void ChangeColor(Color color, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                colorFront = color;
                picFront.BackColor = color;
            }
            else
                if (e.Button == MouseButtons.Right)
                {
                    colorBack = color;
                    picBack.BackColor = color;
                }
        }

        #endregion

        //Variables
        public Bitmap image { get; private set; }

        PictureBox selectedPencilBox;
        WidthRectanglePointer widthRectanglePointer;
        WidthCirclePointer widthCirclePointer;
        TypePointer typePointer;
        Pen pen;
        Graphics graphics;
        Rectangle rect;
        Color colorFront, colorBack;
    }
}