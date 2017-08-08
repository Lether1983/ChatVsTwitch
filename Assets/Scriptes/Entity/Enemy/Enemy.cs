using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entitys
{
    GameManager gManager;
    [SerializeField]
    private int health = 100;
    private Weapon myWeapon;
    private Armor myArmor;
    private Uniform mySkin;
    

    public int Health { get { return health; } set { health = value; } }

    void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewEnemy();
    }

    public void AddDecorator(IEntityGenerator decorator)
    {
        //Can be Used for more Decorators if i know something new
    }

    internal void ModifyDecorator(string decorator, string name)
    {
        if (decorator.GetType() == typeof(WeaponGenerator))
        {
            myWeapon = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Weapon;
        }
        else if (decorator.GetType() == typeof(ArmorGenerator))
        {
            myArmor = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Armor;
        }
        else
        {
            mySkin = Resources.Load("Prefabs/Equipment/Weapon/" + name) as Uniform;
        }
    }

    public void getDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateNewEnemy()
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
    }
}
