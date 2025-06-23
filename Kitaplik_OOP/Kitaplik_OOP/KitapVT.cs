using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Kitaplik_OOP
{
    internal class KitapVT
    {
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\skaya\OneDrive\Belgeler\KitaplarProje.accdb");
        public List<Kitap> Liste()
        {
            List<Kitap> ktp = new List<Kitap>();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar", baglanti);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Kitap k = new Kitap();
                k.ID = Convert.ToInt16(dr[0].ToString());
                k.Adi = dr[1].ToString();
                k.Yazari = dr[2].ToString();
                ktp.Add(k);
            }
            baglanti.Close();
            return ktp;
        }
        public void KitapEkle(Kitap kt)
        {
            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("insert into Kitaplar (AD,YAZAR) values (@p1,@p2)", baglanti);
            komut2.Parameters.AddWithValue("@p1", kt.Adi);
            komut2.Parameters.AddWithValue("@p2", kt.Yazari);
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
