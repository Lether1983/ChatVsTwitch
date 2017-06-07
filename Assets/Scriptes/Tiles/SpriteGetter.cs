using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGetter : MonoBehaviour
{
    [SerializeField]
    public TileManager tManager;
    [SerializeField]
    private string spriteName;
    [SerializeField]
    private bool sortActive;

    // Use this for initialization
    void Start()
    {
        var spriterenderer = GetComponent<SpriteRenderer>();
        spriterenderer.sprite = tManager.GetSprite(spriteName);
        if (sortActive)
        {
            spriterenderer.sortingOrder = (int)(short.MaxValue - transform.position.y);
        }
    }
}
