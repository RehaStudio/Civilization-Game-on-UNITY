using UnityEngine;
using System.Collections;
using System;

public class Avantaj :IComparable  {

    bolge _kim;
    bolge _nereye;
    int _avantaj_miktari;
    public Avantaj(bolge kim, bolge nereye, int avantaj_miktari)
    {
        _kim = kim;
        _nereye = nereye;
        _avantaj_miktari = avantaj_miktari;
    }

    public bolge Kim
    {
        get 
        {
            return _kim;
        }
    }
    public bolge Nereye
    {
        get 
        {
            return _nereye;
        }
    }
    public int Avantaj_miktari
    {
        get 
        {
            return _avantaj_miktari;
        }
        set 
        {
            _avantaj_miktari = value;
        }
    }
    public int CompareTo(object obj)
    {
        Avantaj a = (Avantaj)obj;
        if (this.Avantaj_miktari > a.Avantaj_miktari)
        {
            return -1;
        }
        else if (this.Avantaj_miktari < a.Avantaj_miktari)
        {
            return 1;
        }
        else 
        {
            System.Random rd = new System.Random();
            int x = rd.Next(0, 2);
            if (x == 0)
            {
                return 1;
            }
            else 
            {
                return -1;
            }
        }
        return 0;
    
    }
}
