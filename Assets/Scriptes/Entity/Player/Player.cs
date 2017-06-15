﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entitys
{
    GameManager gmanager;
    Weapon myWeapon;
    Armor myArmor;
    Uniform mySkin;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int lifes = 3;



    void Start()
    {
        gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewPlayer();
    }

    public void getDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            lifes--;
        }
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

            if (EntityDecorator[i].GetType() == typeof(WeaponGenerator))
            {
                myWeapon = EntityDecorator[i].Generate() as Weapon;
            }
            else if (EntityDecorator[i].GetType() == typeof(ArmorGenerator))
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