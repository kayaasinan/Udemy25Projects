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

namespace Pastane_Maliyet_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=PastaneMaliyet;Integrated Security=True;TrustServerCertificate=True");
        void MalzemeListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLMALZEMELER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void UrunListesi()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From TBLURUNLER", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        void Urunler()
        {
            baglanti.Open();
            SqlDataAdapter da3 = new SqlDataAdapter("select * from TBLURUNLER", baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            cmbUrun.ValueMember = "URUNID";
            cmbUrun.DisplayMember = "AD";
            cmbUrun.DataSource = dt3;
            baglanti.Close();

        }
        void Malzemeler()
        {
            baglanti.Open();
            SqlDataAdapter da4 = new SqlDataAdapter("select * from TBLMALZEMELER", baglanti);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            cmbMalzeme.ValueMember = "MALZEMEID";
            cmbMalzeme.DisplayMember = "AD";
            cmbMalzeme.DataSource = dt4;
            baglanti.Close();
        }
        void KasaTablosu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * From TBLKASA", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MalzemeListesi();
            Urunler();
            Malzemeler();
        }

        private void btnMalzemeListesi_Click(object sender, EventArgs e)
        {
            MalzemeListesi();
        }

        private void btnUrunListesi_Click(object sender, EventArgs e)
        {
            UrunListesi();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKasa_Click(object sender, EventArgs e)
        {
            KasaTablosu();
        }

        private void btnMalzemeEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMALZEMELER (AD,STOK,FIYAT,NOTLAR) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMalzemeAd.Text);
            komut.Parameters.AddWithValue("@p2", decimal.Parse(txtMalzemeStok.Text));
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtMalzemeFiyat.Text));
            komut.Parameters.AddWithValue("@p4", txtNotlar.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Malzeme listeye eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MalzemeListesi();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLURUNLER (AD) values (@p1)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün listeye eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UrunListesi();
        }

        private void btnUrunOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFIRIN (URUNID,MALZEMEID,MIKTAR,MALIYET) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbUrun.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cmbMalzeme.SelectedValue);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtMiktar.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtMaliyet.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Malzemeler oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listBox1.Items.Add(cmbMalzeme.Text + " - " + txtMaliyet.Text + " TL");
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            double maliyet;
            if (txtMiktar.Text == "")
            {
                txtMiktar.Text = "0";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLMALZEMELER WHERE MALZEMEID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbMalzeme.SelectedValue);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtMaliyet.Text = dr[3].ToString();
            }
            baglanti.Close();
            if (cmbMalzeme.Text.ToLower() == "yumurta")
            {
                maliyet = Convert.ToDouble(txtMiktar.Text) * 6;
            }
            else
            {
                maliyet = Convert.ToDouble(txtMaliyet.Text) / 1000 * Convert.ToDouble(txtMiktar.Text);
            }
            txtMaliyet.Text = maliyet.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            txtUrunID.Text =dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtUrunAd.Text =dataGridView1.Rows[secilen].Cells[1].Value.ToString();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(MALIYET) FROM TBLFIRIN WHERE URUNID=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",txtUrunID.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunMaliyet.Text = dr[0].ToString();
            }
            baglanti.Close();
        }
    }
}
