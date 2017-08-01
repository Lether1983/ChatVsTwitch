using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : IEntityGenerator
{
    private string weaponName;
    string Name = "WeaponGenerator";
    public Equipment Generate()
    {
        Weapon weapon = Resources.Load("Prefabs/Equipment/Weapon/"+ weaponName) as Weapon;
        return weapon;
    }

    public string GetName()
    {
        return Name;
    }

    public void Modify(string name)
    {
        if(name == "heavy")
        {
            
        }
        else
        {

        }
    }

    public void Setup(string name)
    {
        weaponName = name;
    }
}
