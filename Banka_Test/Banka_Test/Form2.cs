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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=DbBankaTest;Integrated Security=True;TrustServerCertificate=True");
        public string hesap;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblHesapNo.Text = hesap;
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * from TBLKISILER where HESAPNO=@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", hesap);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    lblAdSoyad.Text = dr[1] + " " + dr[2];
                    lblTC.Text = dr[3].ToString();
                    lblTelefon.Text = dr[4].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            #region Alıcı Hesabın Para Artışı
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update TBLHESAP set BAKIYE=BAKIYE+@p1 where HESAPNO=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", decimal.Parse(txtTutar.Text));
                komut.Parameters.AddWithValue("@p2", mtbHesap.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("İşleminiz Gerçekleşti");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
            #endregion

            #region Gönderici Hesabın Para Azalaşı
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update TBLHESAP set BAKIYE=BAKIYE-@p1 where HESAPNO=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", decimal.Parse(txtTutar.Text));
                komut.Parameters.AddWithValue("@p2", hesap);
                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
            #endregion
        }
    }
}
