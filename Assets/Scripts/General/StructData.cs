using UnityEngine;
using System.Collections;

public struct Stat_Common
{
    public int ATTACK;
    public int DEFENCE;
    public int HP;
    public float ACCURACY;
    public float AVOID;
    public float CRITICAL_PROBABILITY;
    public float CRITICAL_RATIO;
}

public struct Key_Value
{
    private string _key;
    private string _value;

    public Key_Value(string key, string val)
    {
        _key = key;
        _value = val;
    }

    public string GetKey()
    {
        return _key;
    }

    public string GetValue()
    {
        return _value;
    }

    public override bool Equals(object obj)
    {
        Key_Value temp = (Key_Value)obj;

        return _key.Equals(temp._key) && _value.Equals(temp._value);
    }

    public override int GetHashCode()
    {
        return _key.GetHashCode() ^ _value.GetHashCode();
    }
}

public struct AutoHeroDeadInfo
{
    public AUTO_HERO auto_hero;
    public int hero_count;
}
