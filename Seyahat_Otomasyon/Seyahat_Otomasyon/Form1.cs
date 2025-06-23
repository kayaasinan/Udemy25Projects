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

namespace Seyahat_Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=YolcuBilet;Integrated Security=True;TrustServerCertificate=True");
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLYOLCU (AD,SOYAD,TELEFON,TC,MAIL,CINSIYET) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtYolcuAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYolcuSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mtbTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mtbTC.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yolcu bilgileri sisteme kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKaptanEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKAPTAN (KAPTANNO,ADSOYAD,TELEFON) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKaptanNo.Text);
            komut.Parameters.AddWithValue("@p2", txtKaptanAd.Text);
            komut.Parameters.AddWithValue("@p3", mtbKaptanTel.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaptan bilgisi sisteme kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void brnSeferOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFER (KALKIS,VARIS,TARIH,SAAT,KAPTAN,FIYAT) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKalkis.Text);
            komut.Parameters.AddWithValue("@p2", txtVaris.Text);
            komut.Parameters.AddWithValue("@p3", mtbTarih.Text);
            komut.Parameters.AddWithValue("@p4", mtbSaat.Text);
            komut.Parameters.AddWithValue("@p5", mtbKaptan.Text);
            komut.Parameters.AddWithValue("@p6", txtFiyat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sefer bilgisi siteme kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            SeferListesi();
        }
        void SeferListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLSEFER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SeferListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtSeferNumara.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtKoltuk.Text = "9";
        }

        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFERDETAY (SEFERNO,YOLCUTC,KOLTUK) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtSeferNumara.Text);
            komut.Parameters.AddWithValue("@p2", mtbYolcuTC.Text);
            komut.Parameters.AddWithValue("@p3", txtKoltuk.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Rezarvaston bilgileri sisteme kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
