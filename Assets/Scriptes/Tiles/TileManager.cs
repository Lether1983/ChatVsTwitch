using System;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameManager gManager;
    [SerializeField]
    private Texture2D texture;
    private Dictionary<string, Vector2[]> UVLookUp;
    private Dictionary<string, Sprite> SpriteLookUp;

    private void Awake()
    {
        UVLookUp = new Dictionary<string, Vector2[]>();
        SpriteLookUp = new Dictionary<string, Sprite>();
    }

    public Vector2[] getUV(string Key)
    {
        Vector2[] uv;
        if (UVLookUp.TryGetValue(Key.ToLowerInvariant(), out uv))
        {
            return uv;
        }

        var container = gManager.container;
        var meta = container.Meta;
        var frame = GetFrame(container, Key);
        double UnitX = 1.0 / meta.Size.Width;
        double UnitY = 1.0 / meta.Size.Height;

        int left = frame.Frame.X;
        int right = frame.Frame.X + frame.Frame.Width;
        int Bottom = meta.Size.Height - (frame.Frame.Y + frame.Frame.Height);
        int Top = meta.Size.Height - frame.Frame.Y;

        uv = new Vector2[]
        {
                    new Vector2((float)(left*UnitX+UnitX),(float)(Bottom*UnitY+UnitY)),
                    new Vector2((float)(left*UnitX+UnitX),(float)(Top*UnitY-UnitY)),
                    new Vector2((float)(right*UnitX-UnitX),(float)(Top*UnitY-UnitY)),
                    new Vector2((float)(right*UnitX-UnitX),(float)(Bottom*UnitY+UnitY))
        };
        return UVLookUp[Key.ToLowerInvariant()] = uv;
    }

    private static SpriteFrame GetFrame(Container container, string key)
    {
        for (int i = 0; i < container.Frames.Length; i++)
        {
            var Frame = container.Frames[i];
            if (string.Equals(Frame.Filename, key, StringComparison.InvariantCultureIgnoreCase))
            {
                return Frame;
            }
        }
        throw new KeyNotFoundException(key);
    }

    public Sprite GetSprite(string key)
    {
        Sprite sprite;
        if (SpriteLookUp.TryGetValue(key.ToLowerInvariant(), out sprite))
        {
            return sprite;
        }

        var container = gManager.container;
        var meta = container.Meta;
        var frame = GetFrame(container, key);
        var rect = new Rect(frame.Frame.X, meta.Size.Height - (frame.Frame.Y + frame.Frame.Height), frame.Frame.Width, frame.Frame.Height);
        sprite = Sprite.Create(texture, rect, new Vector2(0, 0), frame.Frame.Width);
        return SpriteLookUp[key.ToLowerInvariant()] = sprite;
    }
}
