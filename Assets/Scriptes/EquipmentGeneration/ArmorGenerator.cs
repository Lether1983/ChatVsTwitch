using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorGenerator : IEntityGenerator
{
    private string armorName;
    string Name = "ArmorGenerator";

    public Equipment Generate()
    {
        Armor armor = Resources.Load("Prefabs/Equipment/Armor/" + armorName) as Armor;
        return armor;
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
        armorName = name;
    }
}
