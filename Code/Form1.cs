using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code
{
    public partial class Form1 : Form
    {
        bool isGenerated = false;
        bool isGenerated1 = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            isGenerated = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = barcode.Draw(txtBar.Text, 200);
        }

        private void кодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QrCode qr = new QrCode();
            qr.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)| *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить как...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Image Files(*.BMP)|*.BMP| Image Files(*.JPG)|*.JPG| Iamge Files(*.GIF)|*.GIF|" + 
                             "Image Files(*.PNG)|*.PNG| All files(*.*)|*.*";
                sfd.ShowHelp = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить","Ошибка",MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isGenerated)
            {
                PrintDialog pd = new PrintDialog();
                PrintDocument pDoc = new PrintDocument();
                pDoc.PrintPage += Print;
                pd.Document = pDoc;
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    pDoc.Print();
                }
            } 
        }

        private void Print(object sender, PrintPageEventArgs e)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(map, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(map, 0, 0);
            map.Dispose();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
    }
}

