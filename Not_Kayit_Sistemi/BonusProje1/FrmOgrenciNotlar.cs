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
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=SINAN;Initial Catalog=BonusOkul;Integrated Security=True;TrustServerCertificate=True");
        public string numara;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand kmtNot = new SqlCommand("select DersAd,S1,S2,S3,Proje,Ortalama,Durum from Tbl_Notlar\r\ninner join Tbl_Dersler on Tbl_Notlar.DersId=Tbl_Dersler.DersId where OgrId=@p1", conn);
            kmtNot.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(kmtNot);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtOgrenciNotlar.DataSource = dt;

            SqlCommand kmtAd = new SqlCommand("Select OgrAd,OgrSoyad from Tbl_Ogrenciler where OgrId=@p2",conn);
            kmtAd.Parameters.AddWithValue("@p2", numara);
            conn.Open();
            SqlDataReader dr=kmtAd.ExecuteReader();
            while(dr.Read())
            {
                this.Text = dr[0].ToString() +" " + dr[1].ToString();
            }
            conn.Close();
        }
    }
}
