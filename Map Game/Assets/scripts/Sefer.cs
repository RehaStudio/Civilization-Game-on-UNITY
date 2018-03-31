using UnityEngine;
using System.Collections;

public class Sefer  {

    bolge kim_;
    bolge nereye_;
    int asker_Sayisi;
    ulus hangi_ulus;
    public Sefer(bolge kim, bolge nereye, int kac_Asker_saldiriyor,ulus hangi_ulus_saldiriyor)
    {
        kim_ = kim;
        nereye_ = nereye;
        asker_Sayisi = kac_Asker_saldiriyor;
        hangi_ulus = hangi_ulus_saldiriyor;
    }
    public bolge Kim
    {
        get 
        {
            return kim_;
        }
    }
    public bolge Nereye
    {
        get
        {
            return nereye_;
        }
    }
    public int Asker_Sayisi
    {
        get
        {
            return asker_Sayisi;
        }
        set 
        {
            asker_Sayisi = value;
        }
    }
    public ulus Hangi_Ulus_Saldirdi
    {
        get
        {
            return hangi_ulus;
        }
    }
}
