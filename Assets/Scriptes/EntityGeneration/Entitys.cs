using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entitys : MonoBehaviour
{
    protected List<IEntityGenerator> EntityDecorator;
    protected IEntityGenerator IEntityGenerator;
}
