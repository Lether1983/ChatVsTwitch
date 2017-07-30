using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHolderScript : MonoBehaviour
{
    public string TwitchName;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
