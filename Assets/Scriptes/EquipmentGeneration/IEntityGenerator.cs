using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityGenerator
{
    void Setup(string name);
    Equipment Generate();
    string GetName();
    void Modify(string name);
}
