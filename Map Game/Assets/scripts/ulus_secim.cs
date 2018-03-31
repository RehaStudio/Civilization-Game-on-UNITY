using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ulus_secim : MonoBehaviour {

	// Use this for initialization
    System.Random random = new System.Random();
    void Start()
    { 
    }
    public void Click()
    {
        transform.parent.gameObject.SetActive(false);//panel görünürlüğü kapandı
        Tercihler.butun_uluslar = new System.Collections.Generic.List<ulus>();//bütün ulusların olduğu bi liste oluştruduk
        for (int i = 0; i < transform.parent.GetChildCount(); i++)//bütün ulusları tek tek sırayla aldık
        {

            GameObject ulus_game_objecti = new GameObject("ulus" + transform.parent.GetChild(i).name);//bir tane boş gameobject oluşturduk ulusA ulusB
            ulus ulusu = new ulus(); // ulus scripti oluşturduk
            ulusu.Isim = transform.parent.GetChild(i).name;//ona isim verdik
            ulusu.Renk = Tercihler.Ulus_renkleri[i];
            ulus_game_objecti.AddComponent<ulus>();//oyun nesnesi ulus scriptini ekledik
            ulus_game_objecti.GetComponent<ulus>().Renk = ulusu.Renk;
            ulus_game_objecti.GetComponent<ulus>().Isim = ulusu.Isim;//ismimiz verdik
            ulus_game_objecti.GetComponent<ulus>().ulus_bayragi = transform.parent.GetChild(i).transform.Find("Image").GetComponent<Image>().sprite;
            Tercihler.butun_uluslar.Add(ulus_game_objecti.GetComponent<ulus>());//butun uluslar listesine aktardık

            if (ulusu.Isim != name)
            {
                ulus_game_objecti.AddComponent<Bilgisayar_kontrolu>();
            }
            for (int z = 0; z < Tercihler.baslangic_bolge_sayisi; z++)
            {


                if (Tercihler.harita_tipimiz == Harita_Tipi.Normal)
                {
                    GameObject.Find("area" + Tercihler.ulus_yerleri[i + z * transform.parent.GetChildCount()]).GetComponent<bolge>().Ulus_Degistir(ulus_game_objecti.GetComponent<ulus>());
                }
                else
                {

                    int x = random.Next(0, Tercihler.ulus_yerleri_rastgele.Count);
                    GameObject.Find("area" + Tercihler.ulus_yerleri_rastgele[x]).GetComponent<bolge>().Ulus_Degistir(ulus_game_objecti.GetComponent<ulus>());
                    Tercihler.ulus_yerleri_rastgele.RemoveAt(x);
                }
            }

        }
        Tercihler.Ulusu_Degistir(GameObject.Find("ulus" + name).GetComponent<ulus>());//Kendi ulusumuzu seçtik
        Tercihler.oyun_basladi_mi = true;
        GameObject.Find("Text_altin_sayisi").GetComponent<Text>().text = Tercihler.Kendi_ulusum.ulus_altin_miktari.ToString();
        Oyunn.Oyun_Modu = Oyun_Modları.Baslangic;
        foreach (var ulus_diplamcy in Tercihler.butun_uluslar)
        {
            ulus_diplamcy.Diplomacy_Iliskileri_Olustur();
        }
        GameObject.Find("Tercihler_nesnesi").GetComponent<Siralama>().İlk_Sira();
    }
}
