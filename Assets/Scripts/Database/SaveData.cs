using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class DataCharacter
{
    public string nameChar;
    public int damage;
    public int hp;
}

[Serializable]
public class DataUser
{
    public string nameUser;
    public int indexChar;
    public int indexWeapon;
    public int score;
    public int online;
    public int lockItemWeapon;
    public int lockItemChar;
}

[Serializable]
public class DataWeapon
{
    public string nameWeapon;
    public int damage;
    public int range;
    public float rateOfFire;
    public float r, g, b;
}

public class DataGame
{
    public List<DataCharacter> dataCharacters = new List<DataCharacter>();
    public List<DataUser> dataUsers = new List<DataUser>();
    public List<DataWeapon> dataWeapons = new List<DataWeapon>();
}

