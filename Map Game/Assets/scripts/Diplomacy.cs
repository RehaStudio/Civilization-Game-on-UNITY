using UnityEngine;
using System.Collections;

public class Diplomacy {

	// Use this for initialization
    ulus Kim;
    ulus Kimle;
    Diplomacy_Durum durumumuz;
    public Diplomacy(ulus kim, ulus kimle,Diplomacy_Durum durum=Diplomacy_Durum.Yok)
    {
        Kim = kim;
        Kimle = kimle;
        durumumuz = durum;
    }
    public Diplomacy_Durum Diplomatik_Iliski
    {
        get 
        {
            return durumumuz;
        }
        set 
        {
            durumumuz = value;
        }
    }
    public ulus Hangi_ulusla
    {
        get 
        {
            return Kimle;
        }
    }
}
public enum Diplomacy_Durum
{ 
Savas,Baris,Yok
}
public enum Diplomacy_Style
{ 
Destekci,Kurnaz,Aggresive
}
