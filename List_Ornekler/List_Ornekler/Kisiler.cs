using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_Ornekler
{
    internal class Kisiler
    {
        string ad;
        string soyad;
        string meslek;

        public string ADI
        {
            get { return ad; }
            set { ad = value; }
        }
        public string SOYADI
        {
            get { return soyad; }
            set { soyad = value; }
        }
        public string MEKLEKI
        {
            get { return meslek; }
            set { meslek = value; }
        }
    }
    internal class Arabalar
    {
        string marka;
        string model;
        int fiyat;

        public string Markası
        {
            get { return marka; }
            set { marka = value; }
        }
        public string Modeli
        {
            get { return model; }
            set { model = value; }
        }
        public int Fiyati
        {
            get { return fiyat; }
            set { fiyat = value; }
        }
    }
}
