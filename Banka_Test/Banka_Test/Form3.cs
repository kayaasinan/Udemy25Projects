using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Banka_Test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=SINAN;Initial Catalog=DbBankaTest;Integrated Security=True;TrustServerCertificate=True");
        private void btnKayit_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", mtbTC.Text);
                komut.Parameters.AddWithValue("@p4", mtbTelefon.Text);
                komut.Parameters.AddWithValue("@p5", mtbHesap.Text);
                komut.Parameters.AddWithValue("@p6", txtSifre.Text);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {

                MessageBox.Show("Kişi Listeye Kaydedildi");
            }
            finally
            {
                if (baglanti.State==ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(100000,1000000);
            mtbHesap.Text = sayi.ToString();
        }
    }
}
