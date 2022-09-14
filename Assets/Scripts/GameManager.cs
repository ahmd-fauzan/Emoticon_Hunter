using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager
{
    static readonly string SAVE_FILE = "player.json";

    public static DataGame data;

    public static void OfflineUser(string name)
    {
        foreach(DataUser datas in data.dataUsers)
        {
            if(datas.nameUser != name)
            {
                datas.online = -1;
            }
        }
    }

    public static int GetOnlineUser()
    {
        int i = 0;

        foreach(DataUser datas in data.dataUsers)
        {
            if(datas.online == 1)
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    public static int SearchName(string name)
    {
        if(data.dataUsers.Count == 0)
        {
            return 1;
        }
        else
        {
            foreach(DataUser datas in data.dataUsers)
            {
                if (datas.nameUser == name)
                    return -1;
            }
            return 1;
        }
    }

    public static void InsertDataCharacter(DataCharacter dataChar)
    {
        if(data != null)
        {
            data.dataCharacters.Add(dataChar);
        }
            
    }

    public static void InsertDataUser(DataUser dataUser)
    {
        data.dataUsers.Add(dataUser);
    }

    public static void InsertDataWeapon(DataWeapon dataWeapon)
    {
        data.dataWeapons.Add(dataWeapon);
    }

    public static void SaveData(DataGame data)
    {
        string json = JsonUtility.ToJson(data);

        string filename = Path.Combine(Application.persistentDataPath + SAVE_FILE);

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        File.WriteAllText(filename, json);
    }

    public static DataGame LoadData()
    {
        string filename = Path.Combine(Application.persistentDataPath + SAVE_FILE);

        string jsonFromFile;

        DataGame data = new DataGame();

        if (File.Exists(filename))
        {
            jsonFromFile = File.ReadAllText(filename);

            data = JsonUtility.FromJson<DataGame>(jsonFromFile);
        }
            
        if(data == null)
        {
            data = new DataGame() { dataCharacters = new List<DataCharacter>(), dataUsers = new List<DataUser>(), dataWeapons = new List<DataWeapon>() };
        }
        return data;
    }

    public static void CreateNewUser(string name, int indexChar, int indexWeapon, int score, int online, int lockItemWeapon, int lockItemChar)
    {
        DataUser data = new DataUser();

        data.nameUser = name;
        data.indexChar = indexChar;
        data.score = score;
        data.online = online;
        data.lockItemWeapon = lockItemWeapon;
        data.lockItemChar = lockItemChar;

        InsertDataUser(data);
    }

    public static void CreateDataChar(string name, int damage, int hp)
    {
        DataCharacter data = new DataCharacter();

        data.nameChar = name;
        data.damage = damage;
        data.hp = hp;

        InsertDataCharacter(data);
    }

    public static void CreateDataWeapon(string name, int damage, int range, float fireRate, float r, float g, float b)
    {
        DataWeapon data = new DataWeapon();

        data.nameWeapon = name;
        data.damage = damage;
        data.range = range;
        data.rateOfFire = fireRate;
        data.r = r;
        data.g = g;
        data.b = b;

        InsertDataWeapon(data);
    }
}
