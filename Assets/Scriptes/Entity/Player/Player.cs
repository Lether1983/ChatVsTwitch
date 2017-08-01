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
        Armorpoints = getArmorPoints();
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


            if (EntityDecorator[i].GetType() == typeof(WeaponGenerator))
            {
                IEntityGenerator.Setup("AssaultRifle");
                myWeapon = EntityDecorator[i].Generate() as Weapon;
            }
            else if (EntityDecorator[i].GetType() == typeof(ArmorGenerator))
            {
                IEntityGenerator.Setup("NormalArmor");
                myArmor = EntityDecorator[i].Generate() as Armor;
            }
            else
            {
                IEntityGenerator.Setup("NormalUniform");
                mySkin = EntityDecorator[i].Generate() as Uniform;
            }
        }
        health = 100;
    }

    internal void ModifyDecorater(string decorator, string name)
    {
        if(decorator.GetType() == typeof(WeaponGenerator))
        {
            myWeapon = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Weapon;
        }
        else if(decorator.GetType() == typeof(ArmorGenerator))
        {
            myArmor = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Armor;
        }
        else
        {
            mySkin = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Uniform;
        }
    }
}
