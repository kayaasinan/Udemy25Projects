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
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }
        BonusOkulDataSetTableAdapters.Tbl_Dersler1TableAdapter ds = new BonusOkulDataSetTableAdapters.Tbl_Dersler1TableAdapter();
        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            dtDersListe.DataSource = ds.DersListesi();
        }
        private void FrmDersler_Load(object sender, EventArgs e)
        {

            dtDersListe.DataSource = ds.DersListesi();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            ds.DersEkle(txtDersAd.Text);
            MessageBox.Show("Ders Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dtDersListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDersId.Text = dtDersListe.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDersAd.Text = dtDersListe.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            ds.DersSilme(byte.Parse(txtDersId.Text));
            MessageBox.Show("Ders Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ds.DersGuncelle(txtDersAd.Text,byte.Parse(txtDersId.Text));
            MessageBox.Show("Ders Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
