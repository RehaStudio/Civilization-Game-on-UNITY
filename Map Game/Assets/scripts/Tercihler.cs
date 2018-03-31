using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Tercihler : MonoBehaviour {

	// Use this for initialization
    public  Color renk_bos_bolge;
    public static List<ulus> butun_uluslar;
    public static ulus Kendi_ulusum;
    public static Color[] Ulus_renkleri = {Color.red,Color.green,Color.blue,Color.yellow };
    public static int[] ulus_yerleri = { 1, 6, 8, 13 ,2,5,9,11,4,3,7,12};
    public static Harita_Tipi harita_tipimiz;
    public static List<int> ulus_yerleri_rastgele = new List<int>();
    public static bool oyun_basladi_mi;
    public static int baslangic_bolge_sayisi = 1;
    public static bolge Tiklanan_bolge;
    public static bolge Tiklanan_bolge_tum_uluslar;
	void Start () {
        oyun_basladi_mi = false;
        for (int i = 1; i <= 13; i++)
        {
            ulus_yerleri_rastgele.Add(i);   
        }

    }

	
	// Update is called once per frame
	void Update () {
	
	}
    public static void Ulusu_Degistir(ulus istedigim_ulus)
    {
        Kendi_ulusum = istedigim_ulus;
    }
    
}
public enum Harita_Tipi
{ 
Normal,Random
}
