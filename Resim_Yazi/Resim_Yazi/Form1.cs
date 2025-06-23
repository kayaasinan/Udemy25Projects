using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resim_Yazi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string resim;
        private void btnResimSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            resim = openFileDialog1.FileName;
        }
        Color renk;    
        private void btnRenkSec_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            renk=colorDialog1.Color;
        }
        Bitmap bmp;
        private void btnYazdir_Click(object sender, EventArgs e)
        {
            bmp=new Bitmap(resim);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawString(txtMetin.Text,new Font("Corbel",Convert.ToInt16(txtBoyut.Text),FontStyle.Bold),new SolidBrush(renk),20,30);
            pictureBox1.Image = bmp;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Resim|.jpg";
            saveFileDialog1.ShowDialog();
            bmp.Save(saveFileDialog1.FileName);
        }
    }
}
