using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityGenerator
{
    void Setup();
    Equipment Generate();
}
