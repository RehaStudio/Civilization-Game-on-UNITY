using UnityEngine;
using System.Collections;

public static class Oyunn  {

    static Oyun_Modları oyun_modu;
    public static event EventHandlerr mode_değisti;
    public delegate void EventHandlerr(object sender,EventArgss args);
    //255,255,171,255
    static Oyunn()
    {
        oyun_modu = Oyun_Modları.Oyna;
    }
    public static Oyun_Modları Oyun_Modu
    {
        get 
        {
            return oyun_modu;
        }
        set 
        {
            oyun_modu = value;
            mode_değisti(Oyunn.oyun_modu, new EventArgss(oyun_modu));
        }
    }
    public class EventArgss 
    {
        Oyun_Modları oyun_modumuz;
        public EventArgss(Oyun_Modları mod)
        {
            oyun_modumuz = mod;
        }
    }
}
public enum Oyun_Modları
{ 
Oyna,Nüfus,Saldırı,Sınır,Baslangic,Sira,Asker_Atama,Diplomacy
}
