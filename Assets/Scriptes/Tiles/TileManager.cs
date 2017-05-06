using System;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameManager gManager;
    private Dictionary<string, Vector2[]> tilelookup;

    private void Awake()
    {
        tilelookup = new Dictionary<string, Vector2[]>();
    }

    public Vector2[] getUV(string Key)
    {
        Vector2[] uv;
        if(tilelookup.TryGetValue(Key.ToLowerInvariant(),out uv))
        {
            return uv;
        }

        var container = gManager.container;
        var meta = container.Meta;
        for (int i = 0; i < container.Frames.Length; i++)
        {
            var Frame = container.Frames[i];
            if(string.Equals(Frame.Filename,Key,StringComparison.InvariantCultureIgnoreCase))
            {
                double UnitX = 1.0 / meta.Size.Width;
                double UnitY = 1.0 / meta.Size.Height;

                int left = Frame.Frame.X;
                int right = Frame.Frame.X + Frame.Frame.Width;
                int Bottom = meta.Size.Height - (Frame.Frame.Y + Frame.Frame.Height);
                int Top = meta.Size.Height - Frame.Frame.Y;

                uv = new Vector2[]
                {
                    new Vector2((float)(left*UnitX+UnitX),(float)(Bottom*UnitY+UnitY)),
                    new Vector2((float)(left*UnitX+UnitX),(float)(Top*UnitY-UnitY)),
                    new Vector2((float)(right*UnitX-UnitX),(float)(Top*UnitY-UnitY)),
                    new Vector2((float)(right*UnitX-UnitX),(float)(Bottom*UnitY+UnitY))
                };
                return tilelookup[Key.ToLowerInvariant()] = uv;
            }
        }

        throw new KeyNotFoundException(Key);
    }
}
