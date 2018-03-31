using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Bilgisayar_kontrolu : MonoBehaviour {

	// Use this for initialization
    private ulus Ulus;
    public int hamle_sayisi;
    public bool saldiri_yapildi;
    private List<Avantaj> avantajlar;
    int max_hamle = 2;
    public List<bolge> asker_atanacak_bolgeler;
    Diplomacy_Style stilim;
	void Start () {
        stilim = Diplomacy_Style.Destekci;
        Ulus = GetComponent<ulus>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Saldir()
    {
        avantajlar = new List<Avantaj>();
        hamle_sayisi = 0;
        saldiri_yapildi = false;
        if (Ulus == null)
        {
            Ulus = GetComponent<ulus>();
        }
        if (Ulus.Ulus_Bolgeleri.Count <= 0)
        {
            return;
        }
        else 
        {
            Hamle_Avantaj_Hesapla();
        }
        avantajlar.Sort();
        Saldirilari_Belirle();
        saldiri_yapildi = true;
    }
    private void Hamle_Avantaj_Hesapla()
    {
        foreach (var bolge in Ulus.Ulus_Bolgeleri)
        {
            foreach (var komsu in bolge.bolgenin_komsulari)
            {
                if (bolge.bolgenin_ulusu != komsu.bolgenin_ulusu)
                {

                    if (bolge.bolgenin_ulusu.Iliski_Kontrol(komsu.bolgenin_ulusu) != Diplomacy_Durum.Baris)
                    {
                        int avantaj = (bolge.bolgenin_asker_Sayisi - komsu.bolgenin_asker_Sayisi) * Convert.ToInt32(komsu.bolge_nufusu) / 100;
                        avantajlar.Add(new Avantaj(bolge, komsu, avantaj));
                        //hedefinki asker_Sayısı daha fazlaysa o zaman avantaj - değer alır . 
                        //benim asker sayım fazlaysa nüfus ada bakacak 2 si arasından en mantıklısını seçcek
                        //0 
                    }
                }
            }
        }
    }
    private void Saldirilari_Belirle()
    {
        for (int i = 0; i < max_hamle; i++)
        {
            if (avantajlar.Count <= 0)
            {
                break;
            }
            if (avantajlar[0].Avantaj_miktari <= 0)
            {
                break;
            }
            int asker_sayisi_saldiran = avantajlar[0].Nereye.bolgenin_asker_Sayisi + (avantajlar[0].Kim.bolgenin_asker_Sayisi - avantajlar[0].Nereye.bolgenin_asker_Sayisi) / 3;
            Sefer yeni_sefer = new Sefer(avantajlar[0].Kim, avantajlar[0].Nereye, asker_sayisi_saldiran, avantajlar[0].Kim.bolgenin_ulusu);
            avantajlar[0].Kim.bolgenin_asker_Sayisi -= yeni_sefer.Asker_Sayisi;//asker sayısı düzenleniyor
            avantajlar[0].Kim.bolgenin_seferleri.Add(yeni_sefer);
            Avantaj_Duzenle(avantajlar[0].Kim);
            avantajlar.RemoveAt(0);
            avantajlar.Sort();
        }
    }
    private void Avantaj_Duzenle(bolge kim)
    {
        foreach (Avantaj item in avantajlar)
        {
            if (item.Kim == kim)
            {
                item.Avantaj_miktari = (item.Kim.bolgenin_asker_Sayisi - item.Nereye.bolgenin_asker_Sayisi) * Convert.ToInt32(item.Nereye.bolge_nufusu) / 100;
            }
        }
    }
    public void Asker_Ata()
    {
        if (Ulus.Ulus_Bolgeleri.Count <= 0)
        {
            return;
        }
        asker_atanacak_bolgeler = new List<bolge>();
        foreach (var bolgee in Ulus.Ulus_Bolgeleri)
        {
            foreach (var komsuu in bolgee.bolgenin_komsulari)
            {
                if (bolgee.bolgenin_ulusu != komsuu.bolgenin_ulusu)
                {
                    asker_atanacak_bolgeler.Add(bolgee);
                    break;
                }
            }
        }
        int ortalama_Altin_miktari = Convert.ToInt32( Ulus.ulus_altin_miktari) / asker_atanacak_bolgeler.Count;
        if (ortalama_Altin_miktari > 0)
        {
            foreach (var item in asker_atanacak_bolgeler)
            {
                item.bolgenin_asker_Sayisi += ortalama_Altin_miktari;
                item.bolgenin_gosterilecek_asker_Sayisi += ortalama_Altin_miktari;
            }
        }
        Ulus.ulus_altin_miktari = 0;
    }
    public void Diplomacy_Yapilacaklari_Belirle()
    {
        if (Ulus.Ulus_Bolgeleri.Count <= 0)
        {
            return;
        }
        foreach (var item in Tercihler.butun_uluslar)
        {
            if (item == Ulus)
            {
                continue;
            }

            if (Ulus.Iliski_Kontrol(item) == Diplomacy_Durum.Yok)
            {
             
                if (Diplomasi_Yapayim_mi(item))
                {

                    Ulus.dostluk_yapilabilecekler.Add(item);
                }
            }
        }
    }
    public bool Diplomasi_Yapayim_mi(ulus kimle)
    {
        if (stilim == Diplomacy_Style.Aggresive)
        {
            if (Ulus.Guc - Ulus.Guc / 8 < kimle.Guc)
            {
                return false;
            }
        }
        else if (stilim == Diplomacy_Style.Kurnaz)
        {
            if (Ulus.Guc + Ulus.Guc / 8 < kimle.Guc)
            {
                return false;
            }
        }
        return true;
    
    }

}
