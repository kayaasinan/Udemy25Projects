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
using System.IO;

namespace Ogrenci_Etut_Plan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=EtutProje;Integrated Security=True;TrustServerCertificate=True");
        void DersListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLDERSLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDers.ValueMember = "DERSID";
            cmbDers.DisplayMember = "DERSAD";
            cmbDers.DataSource = dt;

        }
        void EtutListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Execute Etut", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void DoluEtut()
        {
            SqlDataAdapter da = new SqlDataAdapter("Execute EtutDolu", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DersListesi();
            EtutListesi();
            BransListesi();
        }

        private void cmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLOGRETMEN Where BRANSID=" + cmbDers.SelectedValue, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbOgretmen.ValueMember = "OGRTID";
            cmbOgretmen.DisplayMember = "AD";
            cmbOgretmen.DataSource = dt;
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLETUT (DERSID,OGRETMENID,TARIH,SAAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", cmbDers.SelectedValue);
                komut.Parameters.AddWithValue("@p2", cmbOgretmen.SelectedValue);
                komut.Parameters.AddWithValue("@p3", mtbTarih.Text);
                komut.Parameters.AddWithValue("@p4", mtbSaat.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Etüt Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                    EtutListesi();
                }
            }
        }

        private void btnAlinanEtut_Click(object sender, EventArgs e)
        {
            DoluEtut();
        }

        private void btnBosEtut_Click(object sender, EventArgs e)
        {
            EtutListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtEtutId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnEtutVer_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLETUT set OGRENCIID=@p1,DURUM=@p2 where ID=@p3 ", baglanti);
            komut.Parameters.AddWithValue("@p1", txtOgrenciID.Text);
            komut.Parameters.AddWithValue("@p2", "True");
            komut.Parameters.AddWithValue("@p3", txtEtutId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt Tanımlandı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFotograf_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void btnOgrenciEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLOGRENCİ (AD,SOYAD,SINIF,TELEFON,MAIL,FOTOGRAF) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtSinif.Text);
            komut.Parameters.AddWithValue("@p4", mtbTelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", pictureBox1.ImageLocation);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Listeye Eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            #region Aynı Ders Sistemde Var Mı?
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBLDERSLER WHERE DERSAD = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtDersAd.Text);
            int ayniDers = (int)komut.ExecuteScalar();
            #endregion Ders Varsa 1 Döner
            if (ayniDers > 0)
            {
                MessageBox.Show("Bu ders zaten eklenmiş", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut1 = new SqlCommand("INSERT INTO TBLDERSLER (DERSAD) VALUES (@p1)", baglanti);
                komut1.Parameters.AddWithValue("@p1", txtDersAd.Text);
                komut1.ExecuteNonQuery();
                MessageBox.Show("Ders Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLOGRETMEN (AD,SOYAD,BRANSID) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtOgretmenAd.Text);
            komut.Parameters.AddWithValue("@p2", txtOgretmenSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbOgretmenBrans.SelectedValue);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğretmen Listeye Eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        void BransListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLDERSLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbOgretmenBrans.ValueMember = "DERSID";
            cmbOgretmenBrans.DisplayMember = "DERSAD";
            cmbOgretmenBrans.DataSource = dt;
        }
    }
}
