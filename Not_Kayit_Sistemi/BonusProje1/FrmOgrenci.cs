using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje1
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        BonusOkulDataSetTableAdapters.DataTable1TableAdapter ds = new BonusOkulDataSetTableAdapters.DataTable1TableAdapter();
        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=SINAN;Initial Catalog=BonusOkul;Integrated Security=True;TrustServerCertificate=True");
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dtOgrenciListe.DataSource = ds.OgrenciListesi();
            conn.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Kulupler", conn);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKulup.DisplayMember = "KulupAd";
            cmbKulup.ValueMember = "KulupId";
            cmbKulup.DataSource = dt;
            conn.Close();
        }
        string cinsiyet = "";
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (rbKiz.Checked == true)
            {
                cinsiyet = "Kız";
            }
            if (rbErkek.Checked == true)
            {
                cinsiyet = "Erkek";
            }
            ds.OgrenciEkle(txtOgrenciAd.Text, txtOgrenciSoyad.Text, byte.Parse(cmbKulup.SelectedValue.ToString()), cinsiyet);
            MessageBox.Show("Öğrenci Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtOgrenciListe.DataSource = ds.OgrenciListesi();
        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            dtOgrenciListe.DataSource = ds.OgrenciListesi();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(txtOgrenciId.Text));
            MessageBox.Show("Öğrenci Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtOgrenciListe.DataSource = ds.OgrenciListesi();
        }

        private void dtOgrenciListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dtOgrenciListe.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtOgrenciAd.Text = dtOgrenciListe.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtOgrenciSoyad.Text = dtOgrenciListe.Rows[e.RowIndex].Cells[2].Value.ToString();
            cinsiyet = dtOgrenciListe.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (cinsiyet == "Kız")
            {
                rbKiz.Checked = true;
            }
            if (cinsiyet == "Erkek")
            {
                rbErkek.Checked = true;
            }

            cmbKulup.Text = dtOgrenciListe.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(txtOgrenciAd.Text, txtOgrenciSoyad.Text, byte.Parse(cmbKulup.SelectedValue.ToString()), cinsiyet, int.Parse(txtOgrenciId.Text));
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtOgrenciListe.DataSource = ds.OgrenciListesi();
        }

        private void rbKiz_CheckedChanged(object sender, EventArgs e)
        {

            if (rbKiz.Checked == true)
            {
                cinsiyet = "Kız";
            }
        }

        private void rbErkek_CheckedChanged(object sender, EventArgs e)
        {

            if (rbErkek.Checked == true)
            {
                cinsiyet = "Erkek";
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dtOgrenciListe.DataSource = ds.OgrenciGetir(txtAra.Text);
        }
    }
}
