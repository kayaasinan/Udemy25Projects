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
using System.Xml.Serialization;

namespace Sifreli_Veriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=Rehber;Integrated Security=True;TrustServerCertificate=True");
        string Sifrele(string veri)
        {
            #region Veri Şifreleme Metodu
            byte[] veriDizisi = ASCIIEncoding.ASCII.GetBytes(veri);
            return Convert.ToBase64String(veriDizisi);
            #endregion
        }
        string SifreCoz(string sifreAc)
        {
            #region Şifre Çözme Metodu
            byte[] sifreCoz = Convert.FromBase64String(sifreAc);
            return ASCIIEncoding.ASCII.GetString(sifreCoz);
            #endregion
        }
        void Listele()
        {
            #region SQL'deki Şifreli Verileri DataGrid'e Ekleme
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLVERILER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            #endregion
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            #region SQL'e Şifreli Veri Gönderme
            try
            {
                string ad = Sifrele(txtAd.Text);
                string soyad = Sifrele(txtSoyad.Text);
                string mail = Sifrele(txtMail.Text);
                string sifre = Sifrele(txtSifre.Text);
                string hesapNo = Sifrele(txtHesapNo.Text);

                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
                komut.Parameters.AddWithValue("@p1", ad);
                komut.Parameters.AddWithValue("@p2", soyad);
                komut.Parameters.AddWithValue("@p3", mail);
                komut.Parameters.AddWithValue("@p4", sifre);
                komut.Parameters.AddWithValue("@p5", hesapNo);
                komut.ExecuteNonQuery();
                MessageBox.Show("Veriler Eklendi");
                Listele();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnSifreCoz_Click(object sender, EventArgs e)
        {
            #region DataGrid'deki Verilerin Şifresini Açma
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                row.Cells["AD"].Value = SifreCoz(row.Cells["AD"].Value.ToString());
                row.Cells["SOYAD"].Value = SifreCoz(row.Cells["SOYAD"].Value.ToString());
                row.Cells["MAIL"].Value = SifreCoz(row.Cells["MAIL"].Value.ToString());
                row.Cells["SIFRE"].Value = SifreCoz(row.Cells["SIFRE"].Value.ToString());
                row.Cells["HESAPNO"].Value = SifreCoz(row.Cells["HESAPNO"].Value.ToString());
            }
            #endregion
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtMail.Clear();
            txtSifre.Clear();
            txtHesapNo.Clear();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
