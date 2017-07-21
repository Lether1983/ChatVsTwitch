using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entitys
{
    public GameManager gmanager;
    Weapon myWeapon;
    Armor myArmor;
    Uniform mySkin;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int lifes = 3;
    private int Armorpoints;

    public int Health { get { return health; } }
    public int Lifes { get { return lifes; } }

    void Start()
    {
        gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewPlayer();
        LoadPlayerValue();
        //Armorpoints = getArmorPoints();
    }

    private int getArmorPoints()
    {
        return myArmor.ArmorPoints;
    }

    public int getFirePower()
    {
        return myWeapon.Damage;
    }

    public void getDamage(int Damage)
    {
        if (Armorpoints <= 0)
        {
            health -= Damage;
        }
        else
        {
            Armorpoints -= Damage;
        }
        if (health <= 0)
        {
            lifes--;
            if (lifes == 0)
            {
                SceneManager.LoadScene("LostScene");
            }   
            else
            {
                SavePlayerValueAndDestroy();
                RestartPlayer();
                DestroyObject();
            }
        }
        Debug.Log(Health);
    }

    private void RestartPlayer()
    {
        CreateNewPlayer();
        LoadPlayerValue();
        gmanager.SManager.RestartPlayerPosition();
    }

    //TODO: ADD And MODIFY Decorator create
    public void AddDecorator()
    {

    }

    public void ModifyDecorator()
    {

    }

    public void SavePlayerValueAndDestroy()
    {
        gmanager.GetComponent<PlayerValueHolder>().Lifes = lifes;
    }

    public void DestroyObject()
    {
        for (int i = 0; i < gmanager.BulletPool.Count; i++)
        {
            Destroy(gmanager.BulletPool[i]);
        }
        Destroy(gameObject);
    }

    private void LoadPlayerValue()
    {
        lifes = gmanager.GetComponent<PlayerValueHolder>().Lifes;
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
        health = 100;
    }
}
