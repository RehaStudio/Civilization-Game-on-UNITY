using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Siralama : MonoBehaviour {

	// Use this for initialization
    bool bitti;
    public GameObject horse;
    public GameObject asker_atama;
    public Text text_altin_Sayisi;
    System.Random rastgele = new System.Random();
    public GameObject panel_oyun_sonu;
    public Text text_oyun_sonu;
	void Start () {
        Oyunn.mode_değisti += new Oyunn.EventHandlerr(Oyunn_mode_değisti);  
	}
    void Seferler_Bitti()
    {

        //Altın ekleme kısmı Seferler bitince gerçekleşecek
        foreach (ulus item in Tercihler.butun_uluslar)
        {
            item.Altin_Ekle();
        }
        text_altin_Sayisi.text = Tercihler.Kendi_ulusum.ulus_altin_miktari.ToString();

        //Yanıp Sönme animasyonu ve yenildin kısmı
        if (Tercihler.Tiklanan_bolge.bolgenin_ulusu != Tercihler.Kendi_ulusum)
        {
            if (Tercihler.Kendi_ulusum.Ulus_Bolgeleri.Count > 0)
            {
                Tercihler.Tiklanan_bolge = Tercihler.Kendi_ulusum.Ulus_Bolgeleri[0];
            }
            else 
            {
                Debug.Log("Sen Yenildin");
                panel_oyun_sonu.SetActive(true);
                text_oyun_sonu.text = "Kaybettin";
                return;
                //Sen yenildin
            }
        }

        //Oyun kazanma
        #region kazanma
        bool kazanama = true;
        foreach (var item in Tercihler.butun_uluslar)
        {
            if (item.Ulus_Bolgeleri.Count <= 0||item==Tercihler.Kendi_ulusum)
            {
                continue;
            }
            else 
            {
                if (Tercihler.Kendi_ulusum.Iliski_Kontrol(item) == Diplomacy_Durum.Baris)
                {

                }
                else 
                {
                    kazanama = false;
                    break;
                }
            }
        }
   
        //
        if (kazanama == true)
        {
            panel_oyun_sonu.SetActive(true);
            text_oyun_sonu.text = "Kazandın";
            Debug.Log("Sen kazandın");
        }
        #endregion
    }
    public void Oyunn_mode_değisti(object sender, Oyunn.EventArgss args)
    {

        if (Oyunn.Oyun_Modu == Oyun_Modları.Sira)
        {
            Tercihler.Tiklanan_bolge.Stop_All_Anim();
            if (this != null)
            {
                for (int i = 0; i < Tercihler.butun_uluslar.Count; i++)
                {
                    if (Tercihler.butun_uluslar[i].Controller != null)
                    {
                        Tercihler.butun_uluslar[i].Controller.Diplomacy_Yapilacaklari_Belirle();
                        Tercihler.butun_uluslar[i].dost_yaptim_mi_bu_turda = false;
                        Tercihler.butun_uluslar[i].Controller.Saldir();

                    }
                }
            }
            StartCoroutine(Sirayla_oynat());
        }
       
    }
    IEnumerator Sirayla_oynat()
    {
        asker_atama.SetActive(true);
        foreach (var uluss in Tercihler.butun_uluslar)
        {
            if (uluss != Tercihler.Kendi_ulusum)
            {
                Bilgisayar_kontrolu control = uluss.GetComponent<Bilgisayar_kontrolu>();
                control.Asker_Ata();
                foreach (var item in control.asker_atanacak_bolgeler)
                {
                    yield return StartCoroutine(item.Asker_Animasyonu());
                }
                //yield return StartCoroutine(
            }
        }
        //asker atama 
        asker_atama.SetActive(false);
        yield return new WaitForSeconds(0);
        bitti = false;
        horse.SetActive(true);

        for (int i = 0; i < Tercihler.butun_uluslar.Count; i++)
        {

            for (int z = 0; z < Tercihler.butun_uluslar[i].Ulus_Bolgeleri.Count; z++)
            {
                if (Tercihler.butun_uluslar[i].Ulus_Bolgeleri[z].bolgenin_seferleri.Count > 0)
                {


                        yield return StartCoroutine(Tercihler.butun_uluslar[i].Ulus_Bolgeleri[z].Sefere_Cik());
                        yield return new WaitForSeconds(0.2f);
                   
               
                    //yield return new 

                }
                else 
                {
                    continue;
                }
            }
        }
        bitti = true;//Seferler bitti
        Seferler_Bitti();
        Dostluk_kur();
        Tercihler.Kendi_ulusum.dostluk_yapilabilecekler.Clear();
        Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
        horse.SetActive(false);

        Yeni_Sira();
    }
    public void Yeni_Sira()
    {
        ulus ulus_hafiza = Tercihler.butun_uluslar[0];

        for (int i = 1; i < Tercihler.butun_uluslar.Count; i++)
        {
            Tercihler.butun_uluslar[i - 1] = Tercihler.butun_uluslar[i];
        }
        Tercihler.butun_uluslar[Tercihler.butun_uluslar.Count - 1] = ulus_hafiza;
    }
    public void İlk_Sira()
    {
        // 
        //c a d b
        List<ulus> siralama_ilk_liste = new List<ulus>();
        foreach (var item in Tercihler.butun_uluslar)
        {
            siralama_ilk_liste.Add(item);
        }

        for (int i = 0; i < Tercihler.butun_uluslar.Count; i++)
        {
           
            int x = rastgele.Next(0, siralama_ilk_liste.Count);
            Tercihler.butun_uluslar[i] = siralama_ilk_liste[x];
   
            siralama_ilk_liste.RemoveAt(x);
       
        }
    }
    public void Dostluk_kur()
    {
        foreach (var ulus in Tercihler.butun_uluslar)
        {
            foreach (var dostluk_ilsiki in ulus.dostluk_yapilabilecekler)
            {
                if (ulus.Iliski_Kontrol(dostluk_ilsiki) != Diplomacy_Durum.Savas)
                {

                    if (dostluk_ilsiki.dostluk_yapilabilecekler.Contains(ulus))
                    {
                    
                        if (ulus.dost_yaptim_mi_bu_turda == false && dostluk_ilsiki.dost_yaptim_mi_bu_turda == false && ulus.dostlar.Count<1 && dostluk_ilsiki.dostlar.Count<1)
                        {
                      
                            ulus.Iliski_Duzelt(dostluk_ilsiki, Diplomacy_Durum.Baris);
                            dostluk_ilsiki.Iliski_Duzelt(ulus, Diplomacy_Durum.Baris);
                            ulus.dostlar.Add(dostluk_ilsiki);
                            dostluk_ilsiki.dostlar.Add(ulus);
                            ulus.dost_yaptim_mi_bu_turda = true;
                            dostluk_ilsiki.dost_yaptim_mi_bu_turda = true;
                            break;
                        }
                    }
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
