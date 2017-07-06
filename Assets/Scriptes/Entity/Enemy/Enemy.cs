using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entitys
{
    [SerializeField]
    private int health = 100;
    Weapon myWeapon;
    Armor myArmor;
    Uniform mySkin;

    void Start()
    {
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewEnemy();
    }
    public int Health { get { return health; } set { health = value; } }
    public void AddDecorator()
    {

    }
    public void ModifyDecorator()
    {

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
