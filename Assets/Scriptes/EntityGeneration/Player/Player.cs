using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entitys
{
    Weapon myWeapon;
    Armor myArmor;
    Uniform mySkin; 

    void Start()
    {
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewPlayer();
    }
    public void AddDecorator()
    {
        
    }
    public void ModifyDecorator()
    {

    }

    public void CreateNewPlayer()
    {
        for (int i = 0; i < EntityDecorator.Count; i++)
        {
            IEntityGenerator = EntityDecorator[i];
            IEntityGenerator.Setup();
           
            if(EntityDecorator[i].GetType() == typeof(WeaponGenerator))
            {
                myWeapon = EntityDecorator[i].Generate() as Weapon;
            }
            else if(EntityDecorator[i].GetType() == typeof(ArmorGenerator))
            {
                myArmor = EntityDecorator[i].Generate() as Armor;
            }
            else
            {
                mySkin = EntityDecorator[i].Generate() as Uniform;
            }

        }
    }
}
