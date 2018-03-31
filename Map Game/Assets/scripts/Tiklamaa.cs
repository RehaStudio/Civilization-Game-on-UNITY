using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tiklamaa : MonoBehaviour
{

    // Use this for initialization
    public GameObject panel_harita;
    public GameObject panel_ulus;
    public Slider sliderimiz_bolge_sayisi;
    public Text textimiz_bolge_sayisi;
    public Text saldiri_button_texti;
    public static bool saldiri_mi_iptal_mi = true;

    public Text asker_Atama_Texti;
    public static bool askerata_mi_iptal_mi = true;
    public Slider slider_asker_Sayisi_belirleme;

	public Camera main_kamera;
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Nufus_moduna_gec()
    {
        Oyunn.Oyun_Modu = Oyun_Modları.Nüfus;
    }
    public void Sinir_moduna_gec()
    {
        Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
    }
    public void Diplomacy_modu_gec()
    {
        Oyunn.Oyun_Modu = Oyun_Modları.Diplomacy;
    }


    public void Normal_Harita()
    {
        Tercihler.harita_tipimiz = Harita_Tipi.Normal;
        panel_harita.SetActive(false);
        panel_ulus.SetActive(true);
        Tercihler.baslangic_bolge_sayisi = System.Convert.ToInt32(sliderimiz_bolge_sayisi.value);
    }
    public void Random_Harita()
    {
        Tercihler.harita_tipimiz = Harita_Tipi.Random;
        panel_harita.SetActive(false);
        panel_ulus.SetActive(true);
        Tercihler.baslangic_bolge_sayisi = System.Convert.ToInt32(sliderimiz_bolge_sayisi.value);
    }
    public void Slider_Value_change_bolge_sayisi()
    {
        textimiz_bolge_sayisi.text =  sliderimiz_bolge_sayisi.value.ToString();
    }
    public void Saldiri_Hedefini_Belirle()
    {
        if (saldiri_mi_iptal_mi)
        {
            Oyunn.Oyun_Modu = Oyun_Modları.Saldırı;
            Tercihler.Tiklanan_bolge.Saldiri_Hedeflerini_Goster();
       
            saldiri_button_texti.text = "İptal";
        }
        else 
        {
            Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
         //   slider_asker_Sayisi_belirleme.value = slider_asker_Sayisi_belirleme.maxValue/2;
            slider_asker_Sayisi_belirleme.gameObject.SetActive(false);

            saldiri_button_texti.text = "Saldırı";
        }
        saldiri_mi_iptal_mi = !saldiri_mi_iptal_mi;
    }
    public void Asker_Atama_belirle()
    {
        if (askerata_mi_iptal_mi)
        {
            Oyunn.Oyun_Modu = Oyun_Modları.Asker_Atama;
            Tercihler.Tiklanan_bolge.Stop_All_Anim();
            foreach (var bolgeee in Tercihler.Kendi_ulusum.Ulus_Bolgeleri)
            {
                bolgeee.yanip_son = true;
            }
            asker_Atama_Texti.text = "İptal";
        }
        else
        {
            Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
            //   slider_asker_Sayisi_belirleme.value = slider_asker_Sayisi_belirleme.maxValue/2;
            slider_asker_Sayisi_belirleme.gameObject.SetActive(false);

            asker_Atama_Texti.text = "Asker Atama";
        }
        askerata_mi_iptal_mi = !askerata_mi_iptal_mi;
    }
    public void slider_asker_sayisi_Change()
    {
        GameObject.Find("Text_value").GetComponent<Text>().text = slider_asker_Sayisi_belirleme.value.ToString();
    }
    public void Sonraki_Sira()
    {
        Oyunn.Oyun_Modu = Oyun_Modları.Sira;
    }
    public void Dostluk_istegi_yolla()
    {
        Tercihler.Kendi_ulusum.dostluk_yapilabilecekler.Add(Tercihler.Tiklanan_bolge_tum_uluslar.bolgenin_ulusu);
        GameObject.Find("dost_ol_button").GetComponent<Button>().interactable = false;
    }
    public void Savas_ilan_Et()
    {
        Tercihler.Kendi_ulusum.Iliski_Duzelt(Tercihler.Tiklanan_bolge_tum_uluslar.bolgenin_ulusu, Diplomacy_Durum.Savas);
        Tercihler.Tiklanan_bolge_tum_uluslar.bolgenin_ulusu.Iliski_Duzelt(Tercihler.Kendi_ulusum, Diplomacy_Durum.Savas);
        GameObject.Find("savas_ol_button").GetComponent<Button>().interactable = false;
    }
    public void Tekrar_Oyna()
    {
       Application.LoadLevel(Application.loadedLevel);
       
    }
	public void Uzaklastir()
	{

		main_kamera.orthographicSize = 10f;
	}



   
}
