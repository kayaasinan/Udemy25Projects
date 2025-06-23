using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kelime_Ogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti=new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\skaya\Desktop\dbSozluk.accdb");
        Random rand = new Random();
        int sure = 90;
        int kelime = 0;
        void KelimeGetir()
        {
            int sayi = rand.Next(1, 2489);
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtIngilizce.Text = dr[1].ToString();
                lblCevap.Text = dr[2].ToString();
                lblCevap.Text=lblCevap.Text.ToLower();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            KelimeGetir();
            timer1.Start();
        }

        private void txtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (txtTurkce.Text==lblCevap.Text)
            {
                kelime++;
                lblKelime.Text = kelime.ToString();
                KelimeGetir();
                txtTurkce.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if (sure == 0)
            {
                txtTurkce.Enabled = false;
                txtIngilizce.Enabled = false;
                txtIngilizce.Clear();
                timer1.Stop();

                baglanti.Open();
                OleDbCommand komut=new OleDbCommand("insert into Dogrular (ToplamDogru) values (@p1)",baglanti);
                komut.Parameters.AddWithValue("@p1",kelime);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Doğru Sayısı= " + kelime);

            }
        }
    }
}
