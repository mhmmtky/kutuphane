using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> kitapAdi = new List<string>();
            List<string> yazarAdi = new List<string>();
            List<int> yayinYili = new List<int>();
            List<int> stokAdet = new List<int>();
            List<string> isim = new List<string>();
            List<int> gun = new List<int>();
            List<DateTime> tarih = new List<DateTime>();
            List<string> kiraKitap = new List<string>();
            
            
            string kararTekrar;

            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi seçin: "); Console.WriteLine("1-) Kitap Ekle"); Console.WriteLine("2-) Kitap Kirala"); Console.WriteLine("3-) Kitap İade"); Console.WriteLine("4-) Kitap Arama"); Console.WriteLine("5-) Raporlama"); Console.WriteLine("6-) ÇIKIŞ");
                int karar = Convert.ToInt32(Console.ReadLine());

                if (karar == 1)
                {
                    // KİTAP EKLE
                    Console.WriteLine("Kitap adını giriniz: ");
                    string kitapAd = Console.ReadLine();

                    if (kitapAdi.Contains(kitapAd))
                    {
                        Console.WriteLine(kitapAd + " bu listede var");
                        Console.WriteLine("Stok adedini giriniz: ");
                        int ifAdet = Convert.ToInt32(Console.ReadLine());
                        int adt = kitapAdi.IndexOf(kitapAd);
                        stokAdet[adt] = stokAdet[adt] + ifAdet;
                        Console.WriteLine("Stok güncellenmiştir! Stok: " + stokAdet[adt]);

                        Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H")
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Geçerli bir değer giremediniz.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Yazar adını giriniz: ");
                        string yazarAd = Console.ReadLine();
                        Console.WriteLine("Yayın yılını giriniz: ");
                        int yayinYil = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Stok adedini giriniz: ");
                        int adet = Convert.ToInt32(Console.ReadLine());
                        kitapAdi.Add(kitapAd);
                        yazarAdi.Add(yazarAd);
                        yayinYili.Add(yayinYil);
                        stokAdet.Add(adet);
                        Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H")
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Geçerli bir değer giremediniz.");
                        }

                    }
                }
                else if (karar == 2)
                {
                    // KİTAP KİRALA
                    int sira = 0;
                    Console.WriteLine("Kiralamak istediğiniz kitabı seçiniz: ");
                    foreach (string kitap in kitapAdi)
                    {
                        sira++;
                        Console.WriteLine(sira + ". " + kitap);
                    }
                    int secim = Convert.ToInt32(Console.ReadLine());
                    if (secim > sira) { Console.WriteLine("Geçersiz değer girdiniz! Tekrar deneyiniz."); }
                    else if (secim <= 0) { Console.WriteLine("Geçersiz değer girdiniz! Tekrar deneyiniz."); }
                    else
                    {
                        Console.WriteLine("Seçtiğiniz Kitap : " + kitapAdi[secim - 1]);
                        Console.WriteLine("Kiralamak istediğiniz gün sayısını seçiniz.");
                        int kirGun = Convert.ToInt32(Console.ReadLine());
                        gun.Add(kirGun);
                        DateTime kiralamaTarihi = DateTime.Now;
                        DateTime iadeTarihi = kiralamaTarihi.AddDays(gun[secim - 1]);

                        tarih.Add(iadeTarihi);

                        int bedel = gun[secim - 1] * 5;
                        Console.WriteLine("Kiralama bedeli: " + bedel + "TL. Bütçeniz ne kadar?");
                        int butce = Convert.ToInt32(Console.ReadLine());
                        if (butce < bedel)
                        {
                            Console.WriteLine("Maalesef bütçeniz kiralama bedeli için yeterli değil!");
                        }
                        else
                        {
                            if (stokAdet[secim - 1] <= 0)
                            { Console.WriteLine(kitapAdi[secim - 1] + " adlı kitap stoklarımızda kalmamşıtır."); System.Threading.Thread.Sleep(2500); Console.Clear(); }
                            else if (stokAdet[secim - 1] > 0)
                            {

                                Console.WriteLine("İsminiz nedir?");
                                string ad = Console.ReadLine();
                                isim.Add(ad);
                                stokAdet[secim - 1]--;
                                Console.WriteLine("Kiralama işlemi başarılı!");
                                string iade = iadeTarihi.ToString("dd/MM/yyyy");
                                kiraKitap.Add(kitapAdi[secim - 1]);
                                Console.WriteLine("Başka bir işlem yapmak istiyor musunuz? E - H");
                                kararTekrar = Console.ReadLine();
                                if (kararTekrar == "E") { Console.Clear(); }
                                else if (kararTekrar == "H") { break; }
                                else { Console.Clear(); Console.WriteLine("Geçerli bir değer giremediniz.");  }
                            }
                        }
                    }
                }
                else if (karar == 3)
                {
                    // KİTAP İADE
                    Console.WriteLine("Hangi Kitabı iade edeceksiniz?");
                    string kitap = Console.ReadLine();
                    if (kitapAdi.Contains(kitap))
                    {
                        int index = kitapAdi.BinarySearch(kitap);
                        stokAdet[index]++;
                        isim.RemoveAt(index);
                        kiraKitap.RemoveAt(index);
                        tarih.RemoveAt(index);
                        Console.Clear();
                        Console.WriteLine("İade başarılı!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Kitap ismini yanlış girdiniz. Tekrar deneyiniz...");
                        Console.WriteLine();
                    }
                }
                else if (karar == 4)
                {
                    // KİTAP ARAMA
                    Console.WriteLine("Kitap adıyla mı yoksa yazar adıyla mı arama yapmak istiyorsunuz? K - Y");
                    kararTekrar = Console.ReadLine();
                    if (kararTekrar == "K")
                    {
                        Console.WriteLine("Kitap adını giriniz: ");
                        string kAd = Console.ReadLine();
                        int index = kitapAdi.IndexOf(kAd);
                        if (kitapAdi.Contains(kAd))
                        {
                            foreach (string i in kitapAdi)
                            {
                                if (i == kAd)
                                {
                                    Console.Write("Kitap Adı: " + i);
                                    foreach (string j in yazarAdi)
                                    {
                                        int jIndex = yazarAdi.IndexOf(j);
                                        if (jIndex == index)
                                        {
                                            Console.Write("   Yazar Adı: " + j);
                                            foreach (int y in yayinYili)
                                            {
                                                int yIndex = yayinYili.IndexOf(y);
                                                if (yIndex == index)
                                                {
                                                    Console.Write("   Yayın Yılı: " + y);
                                                    foreach (int a in stokAdet)
                                                    {
                                                        int aIndex = stokAdet.IndexOf(a);
                                                        if (aIndex == index)
                                                        {
                                                            Console.WriteLine("   Stok Adedi: " + a);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H") { break;
                        }
                        else {
                            Console.Clear();
                            Console.WriteLine("Geçerli bir değer giremediniz."); 
                        }

                    }
                    else if(kararTekrar == "Y")
                    {
                        Console.WriteLine("Yazar adını giriniz: ");
                        string yAd = Console.ReadLine();
                        int index = yazarAdi.IndexOf(yAd);
                        if (yazarAdi.Contains(yAd))
                        {
                            foreach (string i in yazarAdi)
                            {
                                if (i == yAd)
                                {
                                    Console.Write("Yazar Adı: " + i);
                                    foreach (string j in kitapAdi)
                                    {
                                        int jIndex = kitapAdi.IndexOf(j);
                                        if (jIndex == index)
                                        {
                                            Console.Write("   Kitap Adı: " + j);
                                            foreach (int y in yayinYili)
                                            {
                                                int yIndex = yayinYili.IndexOf(y);
                                                if (yIndex == index)
                                                {
                                                    Console.Write("   Yayın Yılı: " + y);
                                                    foreach (int a in stokAdet)
                                                    {
                                                        int aIndex = stokAdet.IndexOf(a);
                                                        if (aIndex == index)
                                                        {
                                                            Console.WriteLine("   Stok Adedi: " + a);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else { Console.WriteLine("Yazar bulunamadı!"); }
                        Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H")
                        {
                            break; 
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Geçerli bir değer giremediniz.");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Geçerli bir değer giremediniz."); System.Threading.Thread.Sleep(1500); Console.Clear();
                    }
                }
                else if (karar == 5)
                {
                    // RAPORLAMA
                    Console.WriteLine("Hangi işlemi yapmak istersiniz?");
                    Console.WriteLine("a) Tüm kitapları listele"); Console.WriteLine("b) Belirli bir yazara ait tüm kitapları listele"); Console.WriteLine("c) Belirli bir yayın yılına ait tüm kitapları listele"); Console.WriteLine("d) Kiradaki kitapları görüntüle");
                    kararTekrar = Console.ReadLine();
                    Console.Clear();
                    if (kararTekrar == "a")
                    {
                        // Kitap Listeleme
                        foreach(string i in kitapAdi)
                        {
                            Console.Write("Kitap adı: "+i); Console.Write(" || ");
                        }
                        Console.WriteLine();
                        foreach(string i in yazarAdi)
                        {
                            Console.Write("Yazar adı: "+i); Console.Write(" || ");
                        }
                        Console.WriteLine();
                        foreach(int i in yayinYili)
                        {
                            Console.Write("Yayın yılı: "+i); Console.Write(" || ");
                        }
                        Console.WriteLine();
                        foreach(int i in stokAdet)
                        {
                            Console.Write("Stok adeti: "+i); Console.Write(" || ");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Başka bir işlem yapmak istiyor musun? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H") { break; }
                        else Console.Clear(); Console.WriteLine("Geçerli bir değer giremediniz.");
                    }
                    else if (kararTekrar == "b")
                    {
                        // Yazara GGöre Bulma
                        Console.WriteLine("Yazar adını giriniz: ");
                        string yazar = Console.ReadLine();

                        if (yazarAdi.Contains(yazar)){
                            int index = yazarAdi.IndexOf(yazar);
                            int sayac = 0;
                            for (int i = 0; i < kitapAdi.Count; i++)
                            {
                                
                                if (yazarAdi[i].Equals(yazar))
                                {
                                    sayac++;
                                    Console.WriteLine(sayac+". " + kitapAdi[i]);
                                }
                                
                            }
                            Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                            kararTekrar = Console.ReadLine();
                            if (kararTekrar == "E") { Console.Clear(); }
                            else if (kararTekrar == "H")
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Geçerli bir değer giremediniz.");
                            }
                        }
                        else { Console.WriteLine("Yazar bulunamadı!");
                            Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                            kararTekrar = Console.ReadLine();
                            if (kararTekrar == "E") { Console.Clear(); }
                            else if (kararTekrar == "H")
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Geçerli bir değer giremediniz.");
                            }
                        }
                    }
                    else if (kararTekrar == "c")
                    {
                        // Yayın Yılına Göre Bulma
                        Console.WriteLine("Yayın yılını giriniz: ");
                        int yil = Convert.ToInt32(Console.ReadLine());

                        if (yayinYili.Contains(yil))
                        {
                            int index = yayinYili.IndexOf(yil);
                            int sayac = 0;
                            for (int i = 0; i < kitapAdi.Count; i++)
                            {

                                if (yayinYili[i].Equals(yil))
                                {
                                    sayac++;
                                    Console.WriteLine(sayac + ". " + kitapAdi[i]);
                                }

                            }
                            Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                            kararTekrar = Console.ReadLine();
                            if (kararTekrar == "E") { Console.Clear(); }
                            else if (kararTekrar == "H")
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Geçerli bir değer giremediniz.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Yazar bulunamadı!");
                            Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                            kararTekrar = Console.ReadLine();
                            if (kararTekrar == "E") { Console.Clear(); }
                            else if (kararTekrar == "H")
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Geçerli bir değer giremediniz.");
                            }
                        }
                    }
                    else if (kararTekrar == "d")
                    {
                        // Kiradaki Kitaplar
                        foreach (string i in kiraKitap)
                        { 
                            Console.Write("Kitap Adı : "+i+"  ||  ");
                        }
                        Console.WriteLine();
                        foreach(string i in isim)
                        {
                            Console.Write("İsim:       "+i+"  ||  ");
                        }
                        Console.WriteLine();
                        foreach(DateTime i in tarih)
                        {
                            Console.Write("İade tarihi: "+i+"  ||  ");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Başka işlem Yapmak istiyor musunuz? E - H");
                        kararTekrar = Console.ReadLine();
                        if (kararTekrar == "E") { Console.Clear(); }
                        else if (kararTekrar == "H")
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Geçerli bir değer giremediniz.");
                        }
                    }
                }
                else if (karar == 6)
                    {
                        Console.WriteLine("Çıkış yapılıyor..."); System.Threading.Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                }

            }

        }

    }




