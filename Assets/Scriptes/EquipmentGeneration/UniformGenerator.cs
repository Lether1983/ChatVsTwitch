using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformGenerator : IEntityGenerator
{
    private string uniformName;
    string Name = "UniformGenerator";

    public Equipment Generate()
    {
        Uniform uniform = Resources.Load("Prefabs/Equipment/Uniform/" + uniformName) as Uniform;
        return uniform;
    }

    public string GetName()
    {
        return Name;
    }

    public void Modify(string name)
    {

    }

    public void Setup(string name)
    {
        uniformName = name;
    }
}
