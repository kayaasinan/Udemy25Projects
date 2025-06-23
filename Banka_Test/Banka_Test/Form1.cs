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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=DbBankaTest;Integrated Security=True;TrustServerCertificate=True");
        private void lnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr=new Form3();
            fr.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From TBLKISILER where HESAPNO=@p1 and SIFRE=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", mtbHesap.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    Form2 fr = new Form2();
                    fr.hesap=mtbHesap.Text;
                    fr.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " +ex.Message,"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

        }
    }
}
