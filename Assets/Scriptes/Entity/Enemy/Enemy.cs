using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entitys
{
   
    [SerializeField]
    private int health = 100;
    private Weapon myWeapon;
    private Armor myArmor;
    private Uniform mySkin;
    private bool inFireDistance;

    public bool IsInFireDistance
    {
        get { return inFireDistance; }
        set { inFireDistance = value; }
    }



    public int Health { get { return health; } set { health = value; } }

    void Start()
    {
        EntityDecorator = new List<IEntityGenerator>();
        EntityDecorator.Add(new WeaponGenerator());
        EntityDecorator.Add(new ArmorGenerator());
        EntityDecorator.Add(new UniformGenerator());
        CreateNewEnemy();
    }

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
