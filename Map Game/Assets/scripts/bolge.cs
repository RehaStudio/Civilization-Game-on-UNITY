using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Threading;
public class bolge : MonoBehaviour {

	// Use this for initialization
    public float bolge_nufusu = 500;
    SpriteRenderer bolge_sprite_renderer;
    public ulus bolgenin_ulusu;
    public Tercihler tercihler;
    //AnimasYon Yanıp Sönme Değişkenleri
    public bool yanip_son = false;
    bool yan_son_anim_azal = true;
    public GameObject bolge_bilgileri_paneli;
    public Button saldir_butonu;
    public Button Asker_atama_buttonu;
    public Text bolge_textii;
    public Text ulus_textii;
    float yan_Son_anim_A;
    public int bolgenin_asker_Sayisi;
    public int bolgenin_gosterilecek_asker_Sayisi;
    public List<bolge> bolgenin_komsulari;
    public List<Sefer> bolgenin_seferleri;
    public Slider slider_asker_sayisi_belirleme;
    public List<GameObject> noktalar_gameobject;
    public Button dostluk_button;
    public Button dusmanlik_button;
    //
    TextMesh mesh_Text;
	void Start () {
        bolgenin_seferleri = new List<Sefer>();
        bolge_sprite_renderer = GetComponent<SpriteRenderer>();
        Oyunn.mode_değisti += new Oyunn.EventHandlerr(Oyunn_mode_değisti);
        slider_asker_sayisi_belirleme.gameObject.active = false;
    
	}

    void Oyunn_mode_değisti(object sender, Oyunn.EventArgss args)
    {
        if (Oyunn.Oyun_Modu == Oyun_Modları.Baslangic)
        {
            if (bolgenin_ulusu != null)
            {
                bolgenin_asker_Sayisi = 2500;
            }
            else 
            {
                bolgenin_asker_Sayisi = 250;
            }
            bolgenin_gosterilecek_asker_Sayisi = bolgenin_asker_Sayisi;
            if (bolgenin_ulusu == Tercihler.Kendi_ulusum)
            {
                Tercihler.Tiklanan_bolge = this;
                yanip_son = true;
            }
            else
            {
                yanip_son = false;
            }
            Asker_Sayisi_Texti_Olustur();
            Bolge_Ulusa_Gore_Renklendir();
        }
        if (Oyunn.Oyun_Modu == Oyun_Modları.Nüfus)
        {
            Stop_Anim();
            Nufusa_Gore_Renklendir();
            Nufus_Sayisi_Goster();
        }
        if (Oyunn.Oyun_Modu == Oyun_Modları.Sınır)
        {
            Stop_All_Anim();
            Tercihler.Tiklanan_bolge.yanip_son = true;
          Bolge_Ulusa_Gore_Renklendir();
          Asker_Sayisi_Goster();
        }
        if (Oyunn.Oyun_Modu == Oyun_Modları.Saldırı)
        {
            if (this == Tercihler.Tiklanan_bolge)
            {
                slider_asker_sayisi_belirleme.gameObject.active = true;
                GameObject.Find("Text_max_value").GetComponent<Text>().text = bolgenin_asker_Sayisi.ToString();
                slider_asker_sayisi_belirleme.maxValue = bolgenin_asker_Sayisi;
                slider_asker_sayisi_belirleme.value = bolgenin_asker_Sayisi / 2;
            }
            
        }
        if (Oyunn.Oyun_Modu == Oyun_Modları.Asker_Atama)
        {
            if (this == Tercihler.Tiklanan_bolge)
            {
                slider_asker_sayisi_belirleme.gameObject.active = true;
                GameObject.Find("Text_max_value").GetComponent<Text>().text = bolgenin_ulusu.ulus_altin_miktari.ToString();
                slider_asker_sayisi_belirleme.maxValue = bolgenin_ulusu.ulus_altin_miktari;
                slider_asker_sayisi_belirleme.value = bolgenin_ulusu.ulus_altin_miktari / 2;
            }
        }
        if (Oyunn.Oyun_Modu == Oyun_Modları.Diplomacy)
        {
            if (this.bolgenin_ulusu == Tercihler.Kendi_ulusum)
            {
                Ulus_Diplomacy_Renklendir(Tercihler.Kendi_ulusum);
            }
        }
    }
    public void Asker_Sayisi_Texti_Olustur()
    {
        GameObject go_text = new GameObject("Text_Asker_Sayisi");
        go_text.transform.parent = transform;
        go_text.transform.position = transform.position;
        go_text.AddComponent<TextMesh>();
         mesh_Text = go_text.GetComponent<TextMesh>();
         mesh_Text.color = Color.black;
        mesh_Text.fontSize = 50;
        mesh_Text.characterSize = 0.1f;
        mesh_Text.fontStyle = FontStyle.Bold;
        mesh_Text.anchor = TextAnchor.MiddleCenter;
        Asker_Sayisi_Goster();
    }
    public void Asker_Sayisi_Goster()
    {
        mesh_Text.text = bolgenin_gosterilecek_asker_Sayisi.ToString();
    }
    public void Nufus_Sayisi_Goster()
    {
        mesh_Text.text = bolge_nufusu.ToString();
    }
    public void Bolge_Ulusa_Gore_Renklendir()
    {
        if (bolgenin_ulusu != null)
        {
            bolge_sprite_renderer.color = bolgenin_ulusu.Renk;
        }
        else
        {
            //kimsenin toprağı değilse henüz
            bolge_sprite_renderer.color = tercihler.renk_bos_bolge;
        }
    }
    public void Ulus_Diplomacy_Renklendir(ulus kime_göre_renklenecek)
    {
        Color renk = bolge_sprite_renderer.color;
        foreach (var item in Tercihler.butun_uluslar)
        {
            if (kime_göre_renklenecek == null)
            {
                break;
            }
            if (kime_göre_renklenecek == item)
            {
                renk = Color.green;
            }
            else if (item == null)
            {

            }
            else 
            {
             
                Diplomacy_Durum durum_diplomacy = kime_göre_renklenecek.Iliski_Kontrol(item);
                if (durum_diplomacy == Diplomacy_Durum.Yok)
                {
                    renk = Color.white;
                }
                else if (durum_diplomacy == Diplomacy_Durum.Savas)
                {
                    renk = Color.red;
                }
                else 
                {
                    renk = Color.green;
                }
            }
            foreach (var bolge in item.Ulus_Bolgeleri)
            {
                bolge.bolge_sprite_renderer.color = renk;
            }
        }
    }
    public void Ulus_Degistir(ulus kimin_topragi_olacak)
    {
        if (bolgenin_ulusu != null)
        {
            bolgenin_ulusu.Bolge_Cikar(this);
        }
        kimin_topragi_olacak.Bolge_Ekle(this);
        bolgenin_ulusu = kimin_topragi_olacak;
        bolgenin_seferleri.Clear();
    }
    public void Nufusa_Gore_Renklendir()
    {
        bolge_sprite_renderer.color = new Color(1, (255 - (bolge_nufusu / 35)) / 255, (255 - (bolge_nufusu / 35)) / 255);
    }
	// Update is called once per frame
	void Update () {
        AnimationControl();
	}
    void AnimationControl()
    {
        if (yanip_son == true)
        {
            Play_Yanip_Sonme_Animation();
        }
    }
    public void Play_Yanip_Sonme_Animation()
    {
        yanip_son = true;
        yan_Son_anim_A = bolge_sprite_renderer.color.a;
        if (yan_son_anim_azal)
        {
            if (yan_Son_anim_A > 0.1f)
            {
                yan_Son_anim_A -= 0.03f;
            }
            else
            {
                yan_son_anim_azal = false;
            }
        }
        else 
        {
            if (yan_Son_anim_A < 0.9f)
            {
                yan_Son_anim_A += 0.03f;

            }
            else
            {
                yan_son_anim_azal = true;
            }
        }
        bolge_sprite_renderer.color = new Color(bolge_sprite_renderer.color.r, bolge_sprite_renderer.color.g, bolge_sprite_renderer.color.b, yan_Son_anim_A);
    }
    public void Stop_Anim()
    {
        yanip_son = false;
        if (bolgenin_ulusu != null)
        {
            bolge_sprite_renderer.color = bolgenin_ulusu.Renk;
        }
        else
        {
            //kimsenin toprağı değilse henüz
            bolge_sprite_renderer.color = tercihler.renk_bos_bolge;
        }
    }
    public void Stop_All_Anim()
    {
        for (int i = 0; i < transform.parent.GetChildCount(); i++)
        {
            transform.parent.GetChild(i).GetComponent<bolge>().Stop_Anim();
        }
    }
    void OnMouseDown()
    {
        if (Tercihler.oyun_basladi_mi == true)
        {
            Tercihler.Tiklanan_bolge_tum_uluslar = this;
            if (Oyunn.Oyun_Modu == Oyun_Modları.Diplomacy)
            {
                Ulus_Diplomacy_Renklendir(bolgenin_ulusu);
            }
            else if (Oyunn.Oyun_Modu == Oyun_Modları.Saldırı)
            {
                Saldirma_Islemi_Kontrol();
            }
            else if (Oyunn.Oyun_Modu == Oyun_Modları.Asker_Atama)
            {
                Asker_Atama_Yap();
            }
            else
            {
                Bolge_bilgi_yazma();
               

            }
        }
    }
    void Asker_Atama_Yap()
    {
        if (Bu_Bolge_Benim_mi(this))
        {
            Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
            int slider_value = Convert.ToInt32(slider_asker_sayisi_belirleme.value);
            bolgenin_asker_Sayisi += slider_value;
            bolgenin_gosterilecek_asker_Sayisi += slider_value;
            Tercihler.Kendi_ulusum.ulus_altin_miktari -= slider_value;
            slider_asker_sayisi_belirleme.value = 0;
            Asker_atama_buttonu.transform.GetChild(0).GetComponent<Text>().text = "Asker_atama";
            Tiklamaa.askerata_mi_iptal_mi = !Tiklamaa.askerata_mi_iptal_mi;
            slider_asker_sayisi_belirleme.gameObject.active = false;
            Asker_Sayisi_Goster();
            GameObject.Find("Text_altin_sayisi").GetComponent<Text>().text = Tercihler.Kendi_ulusum.ulus_altin_miktari.ToString();
        }
    }
    void Saldirma_Islemi_Kontrol()
    {
        if (Tercihler.Tiklanan_bolge.Bu_Bolge_komsum_mu(this))
        {
 
            Oyunn.Oyun_Modu = Oyun_Modları.Sınır;
            saldir_butonu.transform.GetChild(0).GetComponent<Text>().text = "Saldırı";
            Tiklamaa.saldiri_mi_iptal_mi = !Tiklamaa.saldiri_mi_iptal_mi;
            Sefer yeni_sefer = new Sefer(Tercihler.Tiklanan_bolge, this,Convert.ToInt32( slider_asker_sayisi_belirleme.value), Tercihler.Tiklanan_bolge.bolgenin_ulusu);
            yeni_sefer.Kim.bolgenin_seferleri.Add(yeni_sefer);//hatayı düzelt
            yeni_sefer.Kim.bolgenin_asker_Sayisi -= yeni_sefer.Asker_Sayisi;
            slider_asker_sayisi_belirleme.gameObject.SetActive(false);
           // StartCoroutine(At_Animasyonu(yeni_sefer.Kim, yeni_sefer.Nereye));
        }
        else
        {

        }
    }
    void Dostlubutton_kontrol()
    {
        if (bolgenin_ulusu == Tercihler.Kendi_ulusum || bolgenin_ulusu == null)
        {
            dostluk_button.gameObject.SetActive(false);
            dusmanlik_button.gameObject.SetActive(false);
        }
        else
        {
            dostluk_button.gameObject.SetActive(true);
            dostluk_button.interactable = true;
            dusmanlik_button.gameObject.SetActive(true);
            dusmanlik_button.interactable = true;
            if (Tercihler.Kendi_ulusum.dostlar.Contains(bolgenin_ulusu) || Tercihler.Kendi_ulusum.dostluk_yapilabilecekler.Contains(bolgenin_ulusu))
            {
                dostluk_button.interactable = false;
            }
            if (Tercihler.Kendi_ulusum.Iliski_Kontrol(bolgenin_ulusu) == Diplomacy_Durum.Savas)
            {
                dusmanlik_button.interactable = false;
            }
        }
    }
    void Bolge_bilgi_yazma()
    {
      
       
        bolge_bilgileri_paneli.SetActive(true);
        Dostlubutton_kontrol();
        bolge_textii.text = "Bölge İsmi: " + this.name;
        if (bolgenin_ulusu != null)
        {


            ulus_textii.text = "Ulus  İsmi: " + bolgenin_ulusu.Isim;
        }
        else
        {
            ulus_textii.text = "Ulus  İsmi: Ulus Yok";
        }
        Stop_All_Anim();
        yanip_son = true;
        if (bolgenin_ulusu == Tercihler.Kendi_ulusum)
        {
            Tercihler.Tiklanan_bolge = this;
            saldir_butonu.gameObject.SetActive(true);
            bolge_bilgileri_paneli.SetActive(false);
        }
        else
        {
            saldir_butonu.gameObject.SetActive(false);
            bolge_bilgileri_paneli.SetActive(true);
        }
    }
    public void Saldiri_Hedeflerini_Goster()
    {
        Stop_All_Anim();
        for (int i = 0; i < bolgenin_komsulari.Count; i++)
        {
            if (bolgenin_komsulari[i].bolgenin_ulusu != Tercihler.Kendi_ulusum)
            {
          
                bolgenin_komsulari[i].yanip_son = true;
            }
        }
    }
    public bool Bu_Bolge_komsum_mu(bolge bolge_kontrol)
    {
        
        foreach (bolge item in bolgenin_komsulari)
        {
            if (item == bolge_kontrol)
            {
                return true;
            }
        }
        return false;
    }
    public bool Bu_Bolge_Benim_mi(bolge bolge_kontrol)
    {
        if (bolge_kontrol.bolgenin_ulusu == Tercihler.Kendi_ulusum)
        {
            return true;
        }
        return false;
    }
    public IEnumerator At_Animasyonu(bolge kim, bolge nereye,int asker_Sayisi)
    {
        noktalar_gameobject = new List<GameObject>();

        GameObject nokta = GameObject.Find("nokta");
        float nokta_Arasi_mesafe = 0.2f;
        GameObject horse = GameObject.Find("horse");
        horse.transform.position = kim.transform.position;
        horse.transform.Find("Text").GetComponent<TextMesh>().text = asker_Sayisi.ToString();
        horse.transform.Find("Image").GetComponent<SpriteRenderer>().sprite = kim.bolgenin_ulusu.ulus_bayragi;
        yield return new WaitForSeconds(1f);

        Vector2 toplam_uzaklik = nereye.transform.position - kim.transform.position;

        float x_hizi = kim.transform.position.x - nereye.transform.position.x;
        float y_hizi = kim.transform.position.y - nereye.transform.position.y;

        x_hizi = x_hizi / 20;
        y_hizi = y_hizi / 20;
        Vector2 hiz = new Vector2(x_hizi, y_hizi);
        float gidilen_mesafe = 0;
        for (int i = 0; i < 20; i++)
        {
            gidilen_mesafe += hiz.magnitude;
            while (gidilen_mesafe >= nokta_Arasi_mesafe)
            {
                gidilen_mesafe -= nokta_Arasi_mesafe;
                Vector3 eklenecek_konum = (noktalar_gameobject.Count+1)* (hiz.normalized*nokta_Arasi_mesafe);
                Vector3 hedef_konum = kim.transform.position-eklenecek_konum;
                GameObject new_nokta = GameObject.Instantiate(nokta,hedef_konum,nokta.transform.rotation) as GameObject;
                noktalar_gameobject.Add(new_nokta);
            }

            horse.transform.Translate(x_hizi, -y_hizi, 0);
            yield return new WaitForSeconds(0.04f);
        }
    }
    public IEnumerator Asker_Animasyonu()
    {
        GameObject asker_atama_object = GameObject.Find("asker ata");
        SpriteRenderer sp = asker_atama_object.GetComponent<SpriteRenderer>();
        Color renk = sp.color;
        renk = new Color(renk.r, renk.g, renk.b, 1);
        sp.color = renk;
        asker_atama_object.transform.position = transform.position;
        for (int i = 0; i < 20; i++)
        {
      
            yield return new WaitForSeconds(0.04f);
            asker_atama_object.transform.Translate(new Vector3(0, 0.02f, 0));
            renk = new Color(renk.r, renk.g, renk.b, renk.a / 25 * 24);
            sp.color = renk;
        }
        Asker_Sayisi_Goster();
    }
    public IEnumerator Sefere_Cik()
    {
        if (this != null)
        {
            for (int i = 0; i < bolgenin_seferleri.Count; i++)
            {
                //audio

                Sefer item = bolgenin_seferleri[i];
                if (item.Hangi_Ulus_Saldirdi.Iliski_Kontrol(item.Nereye.bolgenin_ulusu) == Diplomacy_Durum.Baris||item.Asker_Sayisi<=0)
                {
                    continue;
                }
                if (item.Asker_Sayisi > item.Kim.bolgenin_gosterilecek_asker_Sayisi)
                {
                    item.Asker_Sayisi = item.Kim.bolgenin_gosterilecek_asker_Sayisi;
                    item.Kim.bolgenin_asker_Sayisi = item.Kim.bolgenin_gosterilecek_asker_Sayisi;
                }
                item.Kim.bolgenin_gosterilecek_asker_Sayisi -= item.Asker_Sayisi;
                yield return StartCoroutine(At_Animasyonu(item.Kim, item.Nereye,item.Asker_Sayisi));
                yield return new WaitForSeconds(1);
                foreach (var destroy_nokta in noktalar_gameobject)
                {
                    Destroy(destroy_nokta);
                }
                if (item.Nereye.bolgenin_ulusu == item.Kim.bolgenin_ulusu)
                {
                    Asker_degisimi(item.Nereye, item.Asker_Sayisi);
                }
                else
                {
                        Saldirma(item.Kim, item.Nereye, item.Asker_Sayisi, item.Hangi_Ulus_Saldirdi);
                }
                item.Kim.Asker_Sayisi_Goster();
                item.Nereye.Asker_Sayisi_Goster();
                item.Nereye.Bolge_Ulusa_Gore_Renklendir();
                if (i >= bolgenin_seferleri.Count - 1)
                {
                    bolgenin_seferleri.Clear();
                }
            }
        }
        yield return new WaitForSeconds(0);
    }
    public void Saldirma(bolge kim,bolge nereye,int saldiran_Asker_sayisi,ulus hangiulus_saldiran)
    {
        if (nereye.bolgenin_ulusu != null)
        {
            hangiulus_saldiran.Iliski_Duzelt(nereye.bolgenin_ulusu, Diplomacy_Durum.Savas);
            nereye.bolgenin_ulusu.Iliski_Duzelt(hangiulus_saldiran, Diplomacy_Durum.Savas);

        }
        if (saldiran_Asker_sayisi > nereye.bolgenin_gosterilecek_asker_Sayisi)
        {
            nereye.bolgenin_gosterilecek_asker_Sayisi = saldiran_Asker_sayisi - nereye.bolgenin_gosterilecek_asker_Sayisi;
            nereye.bolgenin_asker_Sayisi = nereye.bolgenin_gosterilecek_asker_Sayisi;
     
           
            nereye.Ulus_Degistir(hangiulus_saldiran);
        }
        else 
        {
            Sefer_Basarisiz(saldiran_Asker_sayisi, nereye);
           
        }

    }
    public void Asker_degisimi( bolge nereye, int aktarilan_asker_sayisi)
    {
        nereye.bolgenin_gosterilecek_asker_Sayisi += aktarilan_asker_sayisi;
        nereye.bolgenin_asker_Sayisi += aktarilan_asker_sayisi;
    }
    public void Sefer_Basarisiz(int asker_saldiran, bolge nereye)
    {
        nereye.bolgenin_gosterilecek_asker_Sayisi -= asker_saldiran;
        foreach (Sefer item in nereye.bolgenin_seferleri)
        {
            if (asker_saldiran >= item.Asker_Sayisi)
            {
                asker_saldiran -= item.Asker_Sayisi;
                item.Asker_Sayisi = 0;

            }
            else 
            {
                item.Asker_Sayisi -= asker_saldiran;
                asker_saldiran = 0;
                break;
            }
        }
        if (nereye.bolgenin_asker_Sayisi > nereye.bolgenin_gosterilecek_asker_Sayisi)
        {
            nereye.bolgenin_asker_Sayisi = nereye.bolgenin_gosterilecek_asker_Sayisi;
        }
    }
   
}
