using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ulus : MonoBehaviour {

	// Use this for initialization
    Color renk;
    string isim;
    List<bolge> ulusun_bolgeleri;
    public Sprite ulus_bayragi;
    public float ulus_altin_miktari;
    List<Diplomacy> diplomacy_iliskiler;
    public List<ulus> dostlar;
    public List<ulus> dostluk_yapilabilecekler;
    public bool dost_yaptim_mi_bu_turda;
  //  Bilgisayar_kontrolu controller;
    public ulus()
    {

        if (this != null)
        {
            dostlar = new List<ulus>();
            dostluk_yapilabilecekler = new List<ulus>();
            ulusun_bolgeleri = new List<bolge>();
            ulus_altin_miktari = 500;

              

        }
    }
	void Start () {
    
	}

    public void Diplomacy_Iliskileri_Olustur()
    {
        diplomacy_iliskiler = new List<Diplomacy>();
        foreach (var item in Tercihler.butun_uluslar)
        {
            if(item != this)
            {
                Diplomacy diplomatik_iliski = new Diplomacy(this, item);
                diplomacy_iliskiler.Add(diplomatik_iliski);
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void Bolge_Ekle(bolge ek_bolge) 
    {
        ulusun_bolgeleri.Add(ek_bolge);
    }
    public void Bolge_Cikar(bolge cik_bolge)
    {
        ulusun_bolgeleri.Remove(cik_bolge);
    }
    public Color Renk 
    {
        get 
        {
            return renk;
        }
        set 
        {
            renk = value;
        }
    }
    public string Isim
    {
        get
        {
            return isim;
        }
        set
        {
            isim = value;
        }
    }
    public List<bolge> Ulus_Bolgeleri
    {
        get 
        {
            return ulusun_bolgeleri;
        }
    }
    public Bilgisayar_kontrolu Controller
    {
        get 
        {
           return   gameObject.GetComponent<Bilgisayar_kontrolu>();
        }
    }
    public void Altin_Ekle()
    {
        foreach (bolge item in ulusun_bolgeleri)
        {
            ulus_altin_miktari += item.bolge_nufusu / 10;
        }
    }
    public Diplomacy_Durum Iliski_Kontrol(ulus kimle)
    {
        foreach (var item in diplomacy_iliskiler)
        {
            if (kimle == item.Hangi_ulusla)
            {
                return item.Diplomatik_Iliski;
            }
        }
        return Diplomacy_Durum.Yok;
    }
    public void Iliski_Duzelt(ulus kimle, Diplomacy_Durum yeni_drum)
    {
        foreach (var item in diplomacy_iliskiler)
        {
            if (kimle == item.Hangi_ulusla)
            {
                item.Diplomatik_Iliski = yeni_drum;
            }
        }
    }
    public float Guc
    {
        get 
        {
            float hesap_guc = 0;
            foreach (var item in ulusun_bolgeleri)
            {
                hesap_guc += item.bolge_nufusu / 10 * item.bolgenin_asker_Sayisi;
            }
            return hesap_guc;
        }
    }
}
