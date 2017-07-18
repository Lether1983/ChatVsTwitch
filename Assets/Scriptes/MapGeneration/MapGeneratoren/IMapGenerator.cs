using UnityEngine;
using System.Collections;

public interface IMapGenerator
{
    string GetName();
    void Setup(int width, int height,int[,] Randommap);
    int[,] Generate();
    void Modify(int newValue);
}