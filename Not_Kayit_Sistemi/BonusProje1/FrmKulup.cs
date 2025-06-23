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

namespace BonusProje1
{
    public partial class FrmKulup : Form
    {
        public FrmKulup()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=SINAN;Initial Catalog=BonusOkul;Integrated Security=True;TrustServerCertificate=True");
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Kulupler", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtKulupListe.DataSource = dt;
        }
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand kmtEkle = new SqlCommand("insert into Tbl_Kulupler (KulupAd) values (@p1)", conn);
            kmtEkle.Parameters.AddWithValue("@p1", txtKulupAd.Text);
            kmtEkle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kulüp Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dtKulupListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKulupId.Text = dtKulupListe.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKulupAd.Text = dtKulupListe.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand kmtSil = new SqlCommand("Delete from Tbl_Kulupler where KulupId=@p1", conn);
            kmtSil.Parameters.AddWithValue("@p1", txtKulupId.Text);
            kmtSil.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kulüp Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand kmtGuncelle = new SqlCommand("Update Tbl_Kulupler set KulupAd=@p1 where KulupId=@p2", conn);
            kmtGuncelle.Parameters.AddWithValue("@p1", txtKulupAd.Text);
            kmtGuncelle.Parameters.AddWithValue("@p2", txtKulupId.Text);
            kmtGuncelle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kulüp Güncelleme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
    }
}
