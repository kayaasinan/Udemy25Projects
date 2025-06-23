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
    public partial class FrmSinavNotlar : Form
    {
        public FrmSinavNotlar()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        BonusOkulDataSetTableAdapters.Tbl_NotlarTableAdapter ds = new BonusOkulDataSetTableAdapters.Tbl_NotlarTableAdapter();
        private void btnAra_Click(object sender, EventArgs e)
        {
            dtNotListesi.DataSource = ds.NotListesi(int.Parse(txtOgrenciId.Text));
            dtNotListesi.Columns["DersAd"].DisplayIndex = 1;
        }
        SqlConnection conn = new SqlConnection(@"Data Source=SINAN;Initial Catalog=BonusOkul;Integrated Security=True;TrustServerCertificate=True");
        private void FrmSinavNotlar_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Dersler", conn);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDersler.DisplayMember = "DersAd";
            cmbDersler.ValueMember = "DersId";
            cmbDersler.DataSource = dt;
            conn.Close();
        }
        int notId;
        private void dtNotListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notId = int.Parse(dtNotListesi.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtOgrenciId.Text = dtNotListesi.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSinav1.Text = dtNotListesi.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSinav2.Text = dtNotListesi.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSinav3.Text = dtNotListesi.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtSinavProje.Text = dtNotListesi.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOrtalama.Text = dtNotListesi.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDurum.Text = dtNotListesi.Rows[e.RowIndex].Cells[8].Value.ToString();

        }
        int s1, s2, s3, proje;
        double ort;
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            s1 = Convert.ToInt16(txtSinav1.Text);
            s2 = Convert.ToInt16(txtSinav2.Text);
            s3 = Convert.ToInt16(txtSinav3.Text);
            proje = Convert.ToInt16(txtSinavProje.Text);
            ort = (s1 + s2 + s3 + proje) / 4.0;
            txtOrtalama.Text = ort.ToString();
            if (ort >= 50)
            {
                txtDurum.Text = "True";
            }
            else txtDurum.Text = "False";

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(cmbDersler.SelectedValue.ToString()), int.Parse(txtOgrenciId.Text), byte.Parse(txtSinav1.Text), byte.Parse(txtSinav2.Text), byte.Parse(txtSinav3.Text), byte.Parse(txtSinavProje.Text), decimal.Parse(txtOrtalama.Text), bool.Parse(txtDurum.Text), notId);
            MessageBox.Show("Sınav Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
