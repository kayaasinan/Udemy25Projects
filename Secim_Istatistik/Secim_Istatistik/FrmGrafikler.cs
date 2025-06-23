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
namespace Secim_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=DbSecimProje;Integrated Security=True;TrustServerCertificate=True");
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            #region İlçe Adlarını Combobox'a Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select IlceAd From TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Items.Add(dr[0]);
            }
            baglanti.Close();
            #endregion

            #region Grafiğe Toplam Sonuçları 
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select Sum(AParti),Sum(BParti),Sum(CParti),Sum(DParti),Sum(EParti) From TBLILCE", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("AParti", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("BParti", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("CParti", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("DParti", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("EParti", dr2[4]);
            }
            baglanti.Close();
            #endregion
        }

        private void cmbIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Comboboxtan Seçilen İlçenin Oy Oranını Progress Bar'a Yazdırma
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLILCE Where IlceAd=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbIlce.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                lblA.Text = dr[2].ToString();
                lblB.Text = dr[3].ToString();
                lblC.Text = dr[4].ToString();
                lblD.Text = dr[5].ToString();
                lblE.Text = dr[6].ToString();
            }
            baglanti.Close();
            #endregion
        }
    }
}
