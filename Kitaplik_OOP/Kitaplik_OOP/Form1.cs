using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kitaplik_OOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        KitapVT kitapSinif=new KitapVT();
        private void btnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = kitapSinif.Liste();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kitap kitaplar = new Kitap();
            kitaplar.Adi=txtAd.Text;
            kitaplar.Yazari=txtYazar.Text;
            kitapSinif.KitapEkle(kitaplar);
            MessageBox.Show("Kitap Listeye Eklendi");
            kitapSinif.Liste();
            
        }
    }
}
