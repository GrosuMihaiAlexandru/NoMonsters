using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost
{
    public int Fish { get; set; }
    public int GFish { get; set; }
    public int SpecialCurrency { get; set; }

    public Cost (Type type, int value)
    {
        switch (type)
        {
            case Type.fish:
                Fish = value;
                GFish = 0;
                SpecialCurrency = 0;
                break;
            case Type.gFish:
                Fish = 0;
                GFish = value;
                SpecialCurrency = 0;
                break;
            case Type.specialCurrency:
                Fish = 0;
                GFish = 0;
                SpecialCurrency = value;
                break;
        }
    }

    public int GetCost()
    {
        if (Fish != 0)
            return Fish;
        if (GFish != 0)
            return GFish;
        if (SpecialCurrency != 0)
            return SpecialCurrency;

        return -1;
    }

    public Type GetCurrency()
    {
        if (Fish != 0)
            return Type.fish;
        if (GFish != 0)
            return Type.gFish;

        return Type.specialCurrency;
    }

    public enum Type { fish, gFish, specialCurrency }
}
