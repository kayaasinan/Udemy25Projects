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

namespace Akaryakıt_Istasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=AkaryakıtIstasyon;Integrated Security=True;TrustServerCertificate=True");
        void Listele()
        {
            //Kurşunsuz 95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLYAKIT where PETROLTUR='Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblKursunsuz95Litre.Text = dr[4].ToString();
            }
            baglanti.Close();

            //Kurşunsuz 97
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from TBLYAKIT where PETROLTUR='Kurşunsuz97'", baglanti);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblKursunsuz97.Text = dr1[3].ToString();
                progressBar2.Value = int.Parse(dr1[4].ToString());
                lblKursunsuz97Litre.Text = dr1[4].ToString();
            }
            baglanti.Close();

            //Euro Dizel
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from TBLYAKIT where PETROLTUR='EuroDizel'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblEuroDizel.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lblEuroDizelLitre.Text = dr3[4].ToString();
            }
            baglanti.Close();

            //Pro Dizel
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from TBLYAKIT where PETROLTUR='ProDizel'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblProDizel.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lblProDizelLitre.Text = dr4[4].ToString();
            }
            baglanti.Close();

            //Gaz
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from TBLYAKIT where PETROLTUR='Gaz'", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblGaz.Text = dr5[3].ToString();
                progressBar5.Value = int.Parse(dr5[4].ToString());
                lblGazLitre.Text = dr5[4].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select * from tblkasa",baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblKasa.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lblKursunsuz95.Text);
            litre = (double)numericUpDown1.Value;
            tutar = kursunsuz95 * litre;
            txtKursunsuz95Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(lblKursunsuz97.Text);
            litre = (double)numericUpDown2.Value;
            tutar = kursunsuz97 * litre;
            txtKursunsuz97Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double euroDizel, litre, tutar;
            euroDizel = Convert.ToDouble(lblEuroDizel.Text);
            litre = (double)numericUpDown3.Value;
            tutar = euroDizel * litre;
            txtEuroDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double proDizel, litre, tutar;
            proDizel = Convert.ToDouble(lblProDizel.Text);
            litre = (double)numericUpDown4.Value;
            tutar = proDizel * litre;
            txtProDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lblGaz.Text);
            litre = (double)numericUpDown5.Value;
            tutar = gaz * litre;
            txtGazFiyat.Text = tutar.ToString();
        }

        private void btnDoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,YAKITTURU,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)",baglanti);
                komut.Parameters.AddWithValue("@p1",txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2","Kurşunsuz95");
                komut.Parameters.AddWithValue("@p3",numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4",decimal.Parse(txtKursunsuz95Fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1",baglanti);
                komut1.Parameters.AddWithValue("@p1",decimal.Parse(txtKursunsuz95Fiyat.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLYAKIT set STOK=STOK-@p1 where PETROLTUR='Kurşunsuz95'",baglanti);
                komut2.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış işlemi yapıldı");
                Listele();
            }

            if (numericUpDown2.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,YAKITTURU,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz97");
                komut.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtKursunsuz97Fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut1.Parameters.AddWithValue("@p1", decimal.Parse(txtKursunsuz97Fiyat.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLYAKIT set STOK=STOK-@p1 where PETROLTUR='Kurşunsuz97'", baglanti);
                komut2.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış işlemi yapıldı");
                Listele();
            }

            if (numericUpDown3.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,YAKITTURU,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "EuroDizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtEuroDizelFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut1.Parameters.AddWithValue("@p1", decimal.Parse(txtEuroDizelFiyat.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLYAKIT set STOK=STOK-@p1 where PETROLTUR='EuroDizel'", baglanti);
                komut2.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış işlemi yapıldı");
                Listele();
            }

            if (numericUpDown4.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,YAKITTURU,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "ProDizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtProDizelFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut1.Parameters.AddWithValue("@p1", decimal.Parse(txtProDizelFiyat.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLYAKIT set STOK=STOK-@p1 where PETROLTUR='ProDizel'", baglanti);
                komut2.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış işlemi yapıldı");
                Listele();
            }

            if (numericUpDown5.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,YAKITTURU,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "Gaz");
                komut.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtGazFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut1.Parameters.AddWithValue("@p1", decimal.Parse(txtGazFiyat.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLYAKIT set STOK=STOK-@p1 where PETROLTUR='Gaz'", baglanti);
                komut2.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış işlemi yapıldı");
                Listele();
            }
        }

        private void btn95_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLYAKIT set STOK=STOK+1000 where PETROLTUR='Kurşunsuz95'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR-(SELECT ALISFIYAT*1000 FROM TBLYAKIT WHERE PETROLTUR='Kurşunsuz95')",baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            Listele();
        }
    }
}
