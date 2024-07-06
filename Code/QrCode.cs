using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code
{
    public partial class QrCode : Form
    {
        private bool isGeneratedqr = false;
        public QrCode()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                isGeneratedqr = true;
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                Zen.Barcode.CodeQrBarcodeDraw qrbarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pictureBox2.Image = qrbarcode.Draw(txtQr.Text, 200);
            } catch
            {
                MessageBox.Show("Max text: 200 or Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
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
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isGeneratedqr)
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
            Bitmap map = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.DrawToBitmap(map, new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height));
            e.Graphics.DrawImage(map, 0, 0);
            map.Dispose();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)| *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox2.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
    }
}
