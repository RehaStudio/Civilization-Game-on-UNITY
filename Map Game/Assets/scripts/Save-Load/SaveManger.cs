using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManger : MonoBehaviour {

	// Use this for initialization
	public static SaveManger instance { get; set;}
	void Start () {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	
	// Update is called once per frame
	public void Save()
	{
		//PlayerPrefs.SetString ("save", sahne);
		SaveSahne sahne = new SaveSahne ();
		foreach (var item in Tercihler.butun_uluslar) {
			sahne.uluslar_data.Add(Import_Ulus(item));
		}
		PlayerPrefs.SetString ("save", DeSerialize.Serialize<SaveSahne> (sahne).ToString ());
		Debug.Log (DeSerialize.Serialize<SaveSahne> (sahne).ToString ());
	}
	public void Load()
	{
		SaveSahne sahne = DeSerialize.Deserialize<SaveSahne>( PlayerPrefs.GetString ("save"));
		foreach (var item in sahne.uluslar_data) {
			Export_Ulus(item);
		}
	}
	public ulusdata Import_Ulus(ulus Ulus_i)
	{
		ulusdata data = new ulusdata ();
		data.ulus_altin_miktari = Ulus_i.ulus_altin_miktari;
		data.isim = Ulus_i.gameObject.name;
		//data.renk = Ulus_i.Renk;
		data.dost_yaptim_mi_bu_turda = Ulus_i.dost_yaptim_mi_bu_turda;
		//data.dostlar = 
		return data;
	}
	public void Export_Ulus(ulusdata data)
	{
		GameObject go_ulus = GameObject.Find (data.isim);
		ulus ulus_load = go_ulus.GetComponent<ulus> ();
		ulus_load.ulus_altin_miktari = data.ulus_altin_miktari;
	}
}
